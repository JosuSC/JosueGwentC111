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
        private Token Consume(Token_Type type, string message)
        {
            if (Check(type)) return Advance();
            // Error(Peek(), message, synchroTypes);
            Console.WriteLine($"Se esperaba un token del tipo {type}");
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



        public void Parse()
        {
            foreach (var item in tokens)
            {
                CreateNode();
            }


          
        }

        private ASTnode CreateNode()
        {
            //if (item.Type == Token_Type.KEYWORD)
            //{
            //    //llamar al metodo para keyword
            //    return KeywordNode(item);
            //}

           
            return LogicalNode();

        }


        private ASTNode KeywordNode(Token item)
        {
            string val = item.Value;
            string keywords = @"\b(Effect|card|for|while|if|else|return|Params|Action|effect)\b";

            EffectNode();

            throw new NotImplementedException();    

        }

        private EffectASTNode EffectNode()
        {
            EffectASTNode effect = new EffectASTNode();
           PeekNext();
            Check(Token_Type.DELIMITIER);
            Node delimiter = new Node(Peek().Type,Peek().Value);
            effect.children.Add(delimiter); 
            PeekNext();
            //nombre de efecto
            if (Check(Token_Type.IDENTIFIER))
            {
                PeekNext();
                while (Peek().Type != Token_Type.IDENTIFIER)
                {
                    Node newnode = new Node(Peek().Type,Peek().Value);
                    effect.children.Add(newnode);
                    PeekNext();
                }
                effect.Name = Peek().Value;
                PeekNext();
            }

            if (Peek().Value == "Parmas")
            {
               Params parametros = new Params();
                ParamsNode(ref parametros);
                effect.children.Add(parametros);
                effect.Params= parametros;  
            }

            if (Peek().Value == "Action")
            {
                ActionASTNode action = new ActionASTNode(); 
                PeekNext(); 
            }

            return effect;  
        }

        private void ParamsNode(ref Params parametros)
        {
            Consume(Token_Type.DELIMITIER,"Se Esperaba un delimitador");
            Node delimiter = new Node(Peek().Type,Peek().Value);
            parametros.param.Add(delimiter);
            PeekNext();

            while (Peek().Value != "}") 
            {
                Node newnod = new Node(Peek().Type, Peek().Value);
                parametros.param.Add(newnod);   
                PeekNext();
            }
            PeekNext();
        }

        private void ActionNode(ActionASTNode action)
        {
            if (Check(Token_Type.DELIMITIER))
            {
                while (Peek().Value != ")")
                {
                    Node newnod = new Node(Peek().Type,Peek().Value);
                    action.parametros.Add(newnod);
                    PeekNext(); 
                }
            }
            PeekNext();
            Consume(Token_Type.LAMBDA,"Se esperaba un lambda");
            
            if (Check(Token_Type.DELIMITIER))
            {
                while (Peek().Value != "}")
                {
                    //empezamos a preguntar



                    Node newnod = new Node(Peek().Type, Peek().Value);
                    action.parametros.Add(newnod);
                    PeekNext();
                }
            }

        }
        /*
        private ASTNode WhileNode(Token item)
        {

        }

        private ASTNode IfNode(Token item)
        {

        }

        private ASTNode ElseNode(Token item) { }

        private ASTNode ElseIfNode(Token item) { }

        private ASTNode ForNode(Token item) { }

        private ASTNode ReturnNode(Token item) { }
        */
        private ASTnode LogicalNode()
        {
            ASTnode node = EqualASTNode();
            while (Match(Token_Type.AND, Token_Type.OR))
            {
                Token boolean = Previous();
                ASTnode right = EqualASTNode();
                if (boolean.Type == Token_Type.AND) node = new AndASTNode(node, right);
                else { node = new OrASTNode(node, right); }
            }

           return node;
          
        }

        private ASTnode EqualASTNode()
        {
            ASTnode node = ComparasionASTNode();
            while (Match(Token_Type.NOT_EQUAL,Token_Type.EQUAL))
            {
               Token equality = Previous(); 
                ASTnode right = ComparasionASTNode();
                if (equality.Type == Token_Type.EQUAL) { node = new EqualASTNode(node, right); }
                else { node = new NotEqualASTNode(node, right); }
            }

            return node;
        }

        private ASTnode ComparasionASTNode() 
        {
            ASTnode node = ConcatenationASTNode();
            while (Match(Token_Type.GREATER, Token_Type.GREATER_EQUAL, Token_Type.LESS, Token_Type.LESS_EQUAL))
            {
                Token comparasion = Previous();
                 ASTnode right = ConcatenationASTNode();
                if (comparasion.Type == Token_Type.GREATER) { node = new ComparationASTNode(node, comparasion.Type, right); }
                else if (comparasion.Type == Token_Type.GREATER_EQUAL) { node = new ComparationASTNode(node, comparasion.Type, right); }
                else if (comparasion.Type == Token_Type.LESS) { node = new ComparationASTNode(node, comparasion.Type, right); }
                else if (comparasion.Type == Token_Type.LESS_EQUAL) { node = new ComparationASTNode(node, comparasion.Type, right); }
            }
            return node;
           
        }

        private ASTnode ConcatenationASTNode() 
        {
            ASTnode node = TermASTNode();
            while (Match(Token_Type.CONCAT))
            {
                Token concat = Previous();
                ASTnode right = TermASTNode();
                node = new ConcatenationASTNode(node, right);
            }
            return node;
        }

        private ASTnode TermASTNode() 
        {
            ASTnode node = FactorNode();
            while (Match(Token_Type.PLUS,Token_Type.MINUS)) 
            {
                Token term = Previous();
                ASTnode right = FactorNode();
                if (term.Type == Token_Type.PLUS) { node = new PlusAST(node, right); }
                else
                {
                    node = new MinusASTNode(node, right);   
                }

            }
            return node;
        }

        private ASTnode FactorNode() 
        {
            ASTnode node = PowerNode();
            while (Match(Token_Type.DIVIDE,Token_Type.MULTIPLY,Token_Type.MODULUS)) 
            {
                Token fact = Previous();    
                ASTnode right = PowerNode();
                if (fact.Type == Token_Type.DIVIDE) { node = new FactorASTNode(node,fact.Type,right); }
               else if (fact.Type == Token_Type.MULTIPLY) { node = new FactorASTNode(node,fact.Type,right); }
              else  if (fact.Type == Token_Type.MODULUS) { node = new FactorASTNode(node,fact.Type,right); }
            }
            return node;    
        }

        private ASTnode PowerNode() 
        {
            ASTnode node = UnaryNode();
            while (Match(Token_Type.POWER)) 
            {
                Token power = Previous();
                ASTnode right = UnaryNode();
                node = new PowerASTNode(node,right);
            }
            return node;
        }

        private ASTnode UnaryNode() 
        {
            while (Match(Token_Type.NOT,Token_Type.MINUS,Token_Type.PLUS)) 
            {
                Token unary= Previous();
                ASTnode node = UnaryNode();
                return new UnaryASTNode(unary.Type,node);

            }
            return LiteralNode();
        }


        private ASTnode LiteralNode() 
        {
            while (Match(Token_Type.NUMBER,Token_Type.BOOLEAN,Token_Type.STRING))
            {
                return new LiteralASTNode(Previous().Type, Previous().Value);
            }
            if (Match(Token_Type.LEFT_PAREN))
            {
                ASTnode insidetheparent = CreateNode();
                Consume(Token_Type.RIGHT_PAREN,"Se esperaba un parentisis derecho");

            }
          
        }


        //----------------------------------------------------------------------------------------------------------------------------------
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




      
        
    }

    
}