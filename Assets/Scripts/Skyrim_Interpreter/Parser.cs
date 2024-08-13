using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace Skyrim_Interpreter
{

    internal class Parser
    {
        private List<Token> tokens;
        private int currentPosition;
        private List<string> Errors;
        private ASTNode MyTree;

        public object DSL { get; private set; }

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            this.currentPosition = 0;
            this.Errors = new List<string>();
          
        }

        //metodos auxuliares 
        //----------------------------------------------------------------------------------------------------------------
        //Return the actual token
        private Token Peek() 
        {
            return tokens[currentPosition]; 
        }
        
        //Comprueba si el token actual coincide con el token dado
        private bool Check(Token_Type type) 
        {
            if (IsAtEnd()) return false;
            return Peek().Type == type;
        }

        //Verifica si ya se llego al final de la lista de tokens
        private bool IsAtEnd() 
        {
            return Peek().Type == Token_Type.EOF;
        }
        
        //Avanza al siguiente token
        private Token Advance()
        {
            if (!IsAtEnd()) currentPosition++;
            return Previous();
        }
        //Devuelve el token anterior
        private Token Previous()
        {
            return tokens[currentPosition - 1];
        }

        private Token PeekNext() 
        {
            if (IsAtEnd()) return tokens[currentPosition];

            return tokens[currentPosition++];
            
        }


        // consume si el token actual coincide con el tipo dado
        private Token Consume(Token_Type type, string message, List<Token_Type> synchroTypes)
        {
            if (Check(type)) return Advance();
            Error(Peek(), message, synchroTypes);
            return null;
        }
        // tratar con los errores
        private void Error(Token token, string message, List<Token_Type> synchroTypes)
        {
            Synchronize(synchroTypes);
             //DSL.Error(token, message); // Uncomment this line to report the error.
        }
        //Este método sincroniza el estado del analizador después de un error. Se basa en una lista dada de tokens que determinan dónde detener la sincronización
        private void Synchronize(List<Token_Type> synchroTypes)
        {
            if (synchroTypes == null) return;
            while (!IsAtEnd())
            {
                if (synchroTypes.Contains(Peek().Type)) return;
                Advance();
            }
        }

        private bool Match(params Token_Type[] types)
        {
            foreach (Token_Type type in types)
            {
                if (Peek().Type == type)
                {
                    PeekNext();
                    return true;
                }
            }
            return false;
        }

        //-----------------------------------------------------------------------------------------------------------------------------



        //public AST Parse() 
        //{
        //    foreach (var item in tokens)
        //    {
        //        CreateNode(item);
        //    }


        //    return MyTree;
        //}

        //private ASTNode CreateNode(Token item)
        //{
        //    if (item.Type == Token_Type.KEYWORD)
        //    {
        //        //llamar al metodo para keyword
        //        return KeywordNode(item);
        //    }

        //    return LogicalNode();

        //}


        //private ASTNode KeywordNode(Token item) 
        //{
        //    string val = item.Value;
        //    string keywords = @"\b(Effect|card|for|while|if|else|return|Params|Action|effect)\b";

            
          
        //}

        //private ASTNode EffectNode(Token item) 
        //{

        //}

        //private ASTNode ParamsNode(Token item) 
        //{

        //}

        //private ASTNode ActionNode(Token item) 
        //{

        //}

        //private ASTNode WhileNode(Token item) 
        //{

        //}

        //private ASTNode IfNode(Token item) 
        //{

        //}

        //private ASTNode ElseNode(Token item) { }

        //private ASTNode ElseIfNode(Token item) { }

        //private ASTNode ForNode(Token item) { }

        //private ASTNode ReturnNode(Token item) { }

        //private ASTNode LogicalNode() 
        //{
        //    ASTNode node = EqualASTNode();
        //    while (Match(Token_Type.AND,Token_Type.OR)) 
        //    {

        //    }


        //}

        //private ASTNode EqualASTNode() 
        //{

        //}

















        /*
        private ASTNode ParseExpression()
        {
            //obtener token actual
            Token token = Peek();

            //varificamos si el token es una kyword,un identificador,un numero,cadena o bool
            if (Match(Token_Type.KEYWORD, Token_Type.IDENTIFIER, Token_Type.NUMBER, Token_Type.FLOAT, Token_Type.STRING, Token_Type.BOOLEAN))
            {
                ASTNode node = new ASTNode(token.Type, token.Value);
                token = PeekNext();
                return node;
            }
            //verificar si es un opeador unario
            else if (Match(Token_Type.PLUS, Token_Type.MINUS))
            {
                // Crear un nodo en el AST para el operador unario
                ASTNode operatorNode = new ASTNode(token.Type, token.Value);
                token = PeekNext();

                // Analizar la expresión derecha
                ASTNode rightNode = ParseExpression();

                // Crear un nodo en el AST para la expresión completa
                ASTNode expressionNode = new ASTNode(Token_Type.EXPRESSION, "");
                expressionNode.children.Add(operatorNode);
                expressionNode.children.Add(rightNode);

                return expressionNode;
            }
            //verificamos is es aritmetico
            else if (Match(Token_Type.PLUS, Token_Type.MINUS, Token_Type.POWER, Token_Type.DIVIDE, Token_Type.MULTIPLY, Token_Type.MODULUS))
            {
                // Obtener el token actual
                Token operatorToken = Peek();

                // Analizar la expresión izquierda
                ASTNode leftNode = ParseExpression();

                
                token = PeekNext();

                // Verificar si el token actual es un operador aritmético
                if (Match(operatorToken.Type))
                {
                    // Crear un nodo en el AST para el operador aritmético
                    ASTNode operatorNode = new ASTNode(operatorToken.Type, operatorToken.Value);

                    // Analizar la expresión derecha
                    ASTNode rightNode = ParseExpression();

                    // Crear un nodo en el AST para la expresión completa
                    ASTNode expressionNode = new ASTNode(Token_Type.EXPRESSION, "");
                    expressionNode.children.Add(leftNode);
                    expressionNode.children.Add(operatorNode);
                    expressionNode.children.Add(rightNode);

                    return expressionNode;
                }
                else
                {
                    // Devolver la expresión izquierda
                    return leftNode;
                }
            }
            //verificas si es un operador relacional o logico
            else if (Match(Token_Type.NOT_EQUAL, Token_Type.EQUAL, Token_Type.NOT, Token_Type.GREATER, Token_Type.GREATER_EQUAL, Token_Type.LESS_EQUAL, Token_Type.LESS, Token_Type.AND, Token_Type.OR))
            {
                // Crear un nodo en el AST para el operador relacional o lógico
                ASTNode operatorNode = new ASTNode(token.Type, token.Value);
                token = PeekNext();

                // Analizar la expresión derecha
                ASTNode rightNode = ParseExpression();

                // Crear un nodo en el AST para la expresión completa
                ASTNode expressionNode = new ASTNode(Token_Type.EXPRESSION, "");
                expressionNode.children.Add(operatorNode);
                expressionNode.children.Add(rightNode);

                return expressionNode;
            }
            //verificar si es de asignacion
            else if (Match(Token_Type.ASSIGN))
            {
                //creamos el AST para las asignacion
                ASTNode assignNode = new ASTNode(token.Type, token.Value);
                token = PeekNext();

                //analizar a la dercha
                ASTNode rightNode = ParseExpression();

                // Crear un nodo en el AST para la expresión completa
                ASTNode expressionNode = new ASTNode(Token_Type.EXPRESSION, "");
                expressionNode.children.Add(assignNode);
                expressionNode.children.Add(rightNode);

                return expressionNode;
            }
            // Verificar si el token es un paréntesis izquierdo
            else if (token.Type == Token_Type.LEFT_PAREN)
            {
                
                token = PeekNext();

                // Analizar la expresión dentro del paréntesis
                ASTNode expressionNode = ParseExpression();

                // Verificar si el token actual es un paréntesis derecho
                Consume(Token_Type.RIGHT_PAREN, "Expect ')' after expression.", null);

                return expressionNode;
            }
            // Verificar si el token es un punto y coma
            else if (Match(Token_Type.SEMICOLON))
            {
               
                token = PeekNext();

                // Devolver un nodo vacío
                return new ASTNode(Token_Type.EMPTY, "");
            }
            //vreificas si es una coma
            else if (Match(Token_Type.COMMA)) 
            {
               
               token = PeekNext();

                // Devolver un nodo vacío
                return new ASTNode(Token_Type.EMPTY, "");
            }
            // Verificar si el token es un delimitador
            else if (Match(Token_Type.DELIMITIER))
            {
                
                token = PeekNext();

                // Devolver un nodo vacío
                return new ASTNode(Token_Type.EMPTY, "");
            }
            // Si no se reconoce el token, lanzar una excepción
            else
            {
                throw new Exception("Token no reconocido: " + token.Type);
            }
        }


        private ASTNode ParseStatement()
        {
            // Intentar analizar una declaración
            ASTNode declaration = ParseDeclaration();
            if (declaration != null) return declaration;

            // Intentar analizar una expresión
            return ParseExpression();
        }


        private bool Match(params Token_Type[] types)
        {
            foreach (Token_Type type in types)
            {
                if (Peek().Type == type)
                {
                    PeekNext();
                    return true;
                }
            }
            return false;
        }


        private ASTNode ParseDeclaration() 
        {
            // Obtener el token actual
            Token token = Peek();

            // Verificar si el token es un identificador
            if (Match(Token_Type.IDENTIFIER))
            {
                // Crear un nodo en el AST para el identificador
                ASTNode identifierNode = new ASTNode(Token_Type.IDENTIFIER, token.Value);
                PeekNext();

                // Verificar si el token es un signo de asignación (=)
                if (Match(Token_Type.ASSIGN))
                {
                    // Avanzar al siguiente token
                    PeekNext();

                    // Analizar la expresión derecha
                    ASTNode expressionNode = ParseExpression();

                    // Crear un nodo en el AST para la declaración
                    ASTNode declarationNode = new ASTNode(Token_Type.DECLARATION, "");
                    declarationNode.children.Add(identifierNode);
                    declarationNode.children.Add(expressionNode);

                    return declarationNode;
                }
                else
                {
                    // Crear un nodo en el AST para la declaración
                    ASTNode declarationNode = new ASTNode(Token_Type.DECLARATION, "");
                    declarationNode.children.Add(identifierNode);

                    return declarationNode;
                }
            }
            else
            {
                // Error: token no válido
                throw new Exception("Token no válido en ParseDeclaration");
            }

        }
        */

            public static void MandarPython(string args)
            {
                string pythonScriptPath = @"C:\Python_Programs\PythonApplication1\PythonApplication1\PythonApplication1.py";
        string command = $"\"{pythonScriptPath}\" " + args; // Enclose the path in quotes to handle spaces

        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardOutput = true,
            UseShellExecute = false, // Keep this false for shell commands
            CreateNoWindow = true,
            FileName = "python.exe", // Specify the Python interpreter
            Arguments = command
        };

        Process process = Process.Start(startInfo);
        string result = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        Console.WriteLine(result);
            }

           public string Word()
           {
              Node n = ART();
              string t =  MakeList(n);
              return t;
           }

         
          public string MakeList(Node root)
          {
            List<(Node,int)> exist = new List<(Node,int)>(); 
            exist.Add((root,root.Children.Count));
            int pivote =0;

             while(pivote < exist.Count ){
            
                 Meter(exist[pivote].Item1,  exist );
                
              pivote ++;
             }
            
            List<string> s = new List<string>();
            int count=0;
           foreach(var item in exist)
           {
              s.Add(item.Item1.Value +  count  + $"{item.Item1.Children.Count}");
              count++;
           }
            
            string w = "";
             for(int h = 0 ; h < s.Count;h++)
             {
                w += s[h] + " ";
             }

             return w;
          }

          private void Meter(Node father, List<(Node,int)> t)
          {
             for(int i = 0 ; i < father.Children.Count;i++)
             {
                   t.Add((father.Children[i],father.Children[i].Children.Count));
             }
          }


        Dictionary<string, int> Arit = new Dictionary<string, int>()
        {
            { "+", 1 },
            { "-",1 },
            { "*",0 },
            { "/",0 },
            { "%",-1},
            { "^",-1 }  
        };

        public Node ART() 
        {
           Node aa = Aritmetics(this.tokens,0,null,null,null); 
           return  aa;
        }


        private Node Aritmetics(List<Token> tokens,int count,Node lastNumber,Node lastOperator,Node maxOperator) 
        {
            if (count == tokens.Count)
            {
                SonOf(lastNumber,lastOperator);
                return maxOperator;
            }

            if (Arit.ContainsKey(tokens[count].Value))
            {
                bool mayor = false;
                Node newOpeator = new Node(tokens[count].Type, tokens[count].Value);
                CompararaConMayor(ref newOpeator,ref maxOperator,ref mayor,ref lastNumber,ref lastOperator);
                if (!mayor)
                {
                    CompararConAnterior(ref newOpeator,ref lastOperator,ref lastNumber,ref maxOperator);
                }
             return  Aritmetics(tokens,count += 1,lastNumber,lastOperator,maxOperator);
            }
            else
            {
                lastNumber = new Node(tokens[count].Type, tokens[count].Value);
             return   Aritmetics(tokens,count += 1,lastNumber,lastOperator,maxOperator);
            }

                    


        }

        private void CompararConAnterior(ref Node newnode,ref Node lastNode,ref Node lastnumber,ref Node max) 
        {
            if (lastNode == null)
            {
                SonOf(lastnumber, newnode);
                lastNode = newnode;
                return;
            }
            else if (Arit[newnode.Value] >= Arit[lastNode.Value])
            {
                SonOf(lastnumber,lastNode);
                SonOf(lastNode,newnode);

                if (max.Children.Contains(lastNode))
                {
                    Quitar(lastNode, max);
                    SonOf(newnode, max);
                }
            }
            else if (Arit[newnode.Value] < Arit[lastNode.Value])
            {
                SonOf(lastnumber,newnode);
                SonOf(newnode,lastNode);
            }
            lastNode = newnode;
        }

        private void CompararaConMayor(ref Node newnode,ref Node max,ref bool m,ref Node lastnumber,ref Node lastnode) 
        {
            if (max == null)
            {
                SonOf(lastnumber, newnode);
                max = newnode;
                m = true;  
                lastnode= newnode;   
                return;
            }
            else if (Arit[max.Value] <= Arit[newnode.Value])
            {
                SonOf(max,newnode);
                SonOf(lastnumber,lastnode);
                max= newnode;
                m = true;
                lastnode = newnode;
            }
        }


        private void SonOf(Node son,Node father) 
        {
            father.Children.Add(son);
        }

        private void Quitar(Node son,Node father) 
        {
            for (int i = 0; i < father.Children.Count; i++)
            {
                if (father.Children[i].Equals(son))
                {
                    father.Children.RemoveAt(i);
                }

            }
        }




        /*
        public AST StructedTree(List<Token> tokens ) 
        {
            return AritOp(tokens,new AST(),new List<string>(),0);
        }


        private AST AritOp(List<Token> tokens, AST Tree,List<string> takens,int poss) 
        {
            Token  token = Peek();
            ASTNode previus = new ASTNode(Token_Type.EOF,"EOF");
            ASTNode mayor = new ASTNode(Token_Type.EOF,"EOF");

          
                //obtenemos token actual 
                Token actual = Peek();
                //mientas no eset el final
                while (poss < tokens.Count)
                {

                    //si es aritmetico
                    if (AritmeticsOp.ContainsKey(token.Value) && token.Type == Token_Type.ARITHMETIC)
                    {
                        //si la lista de operaciones tienen algo
                        if (takens != null && takens.Count != 0)
                        {
                          // si el nuevo operador es menro que el anterior (+  *)
                            if (AritmeticsOp[takens[takens.Count-1]] > AritmeticsOp[Peek().Value])
                            {
                                //tomamos el nuevo nodo
                                ASTNode next = new ASTNode(Peek().Type,Peek().Value);
                                takens.Add(Peek().Value);

                               //metemos el ultimo como hijo del que tenemos 
                                previus.AddChild(next);

                                //buscamos el token anterior 
                                token = Previous();

                               
                                if (!EsNumero(token.Type)) { Console.WriteLine("Error  "); break; }

                                //metemos el hijo izquierdo con el ultimo
                                ASTNode left = new ASTNode(token.Type,token.Value);  
                                next.AddChild(left);

                                //el anterior es igual al nuevo
                                previus = next;
                            }
                            //(*  +)
                            else if (AritmeticsOp[takens[takens.Count - 1]] < AritmeticsOp[Peek().Value])
                            {
                                //nuevo
                                ASTNode next = new ASTNode(Peek().Type,Peek().Value);
                                takens.Add(Peek().Value);
                                // buscamos el mayor si hay
                                for (int i = takens.Count-1; i >= 0; i--)
                                {


                                    if (AritmeticsOp[takens[i]] == AritmeticsOp[Peek().Value])
                                    {
                                        next.AddChild(mayor);
                                        break;
                                    }
                                }


                            if (next.children == null)
                            {
                                next.AddChild(previus);
                            }

                                //nuevo mayor
                                mayor = next;

                                token = Previous();
                                if (!EsNumero(token.Type))
                                {
                                    Console.WriteLine("Error"); 
                                    break;
                                }

                                ASTNode right = new ASTNode(token.Type, token.Value);
                                previus.AddChild(right);
                                previus = next;

                            }
                            else
                            {
                                //nuevo nodo
                                ASTNode next = new ASTNode(Peek().Type, Peek().Value);
                                takens.Add(Peek().Value);

                                if (AritmeticsOp[Peek().Value] == 1)//(++)
                                {
                                    next.AddChild(previus);
                                    token = Previous();
                                if (!EsNumero(token.Type)) { Console.WriteLine("Error"); break; }


                                    ASTNode left = new ASTNode(token.Type, token.Value);
                                    previus.AddChild(left);
                                    mayor= next;
                                }
                                else //(**)
                                {
                                //buscamos el mayor ,hacemos al nuevo hijo del mayor,y el previo hijo del nuevo
                                if (mayor.Type != Token_Type.EOF)
                                {

                                    mayor.AddChild(next);
                                    next.AddChild(previus);
                                }
                                else 
                                    next.AddChild(previus);
                                

                                }

                                previus= next;
                            }
                            //annadimos las operacion
                            takens.Add(Peek().Value);

                            //avanzamos
                            token = PeekNext();
                            continue;
                        }

                        token = tokens[poss -1];
                        if (EsNumero(token.Type))
                        {
                            //tomamos en nodo anterior 
                            previus = new ASTNode(token.Type,token.Value);

                        if (AritmeticsOp.ContainsKey(token.Value))
                        {


                            if (AritmeticsOp[token.Value] == 1)
                            {
                                mayor = new ASTNode(Peek().Type, Peek().Value);
                            }

                        }
                            //lo annadimos a las lista de operaciones 
                            takens.Add(Peek().Value);

                            //creamos le hijo izquierdos
                            ASTNode lefChild = new ASTNode(token.Type, token.Value);


                            //lo hacemos hijo de la operacion 
                            previus.AddChild(lefChild);
                        }
                        else
                        {

                            Console.WriteLine("algo esta mal");
                            break;
                        }
                    }
                 previus = new ASTNode(token.Type, token.Value);

                poss++;
                 token = tokens[poss];
                 

                }

            Tree.AddNode(mayor);

            return Tree;
        }


        private bool EsNumero(Token_Type type ) 
        {
            if (type == Token_Type.NUMBER || type == Token_Type.FLOAT)
            {
                return true;
            }

            return false;   
        }
*/
        
    }

    
}