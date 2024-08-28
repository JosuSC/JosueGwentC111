using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using System.Xml.Linq;

namespace Skyrim_Interpreter
{
    internal class Parser
    {
        private List<Token> tokens;
        private int currentPosition;
        private List<string> Errors;
        private ASTnodeTree MyTree;
        static Token comprobar;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            this.currentPosition = 0;
            this.Errors = new List<string>();
            comprobar = null;
            MyTree = new ASTnodeTree(); 
        }

        #region Metodos auxilar
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

        private bool CheckValue(string value) 
        {
            if (IsAtEnd()) return false;
            return Peek().Value == value;   
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
           
            Console.WriteLine($"Se esperaba un token del tipo {type}");
            return null;
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

        #endregion

        public void Parse()
        {
            KeywordNode();
        }

        private ASTnode KeywordNode()
        {
           // string keywords = @"\b(Effect|card|for|while|if|else|return|Params|Action|effect)\b";
           //comprobamos si el nodo es de tipo effect o card
            if ( (Peek().Value == "effect" || Peek().Value == "card"))
            {
                Token keyword = Peek();
                if (keyword.Value == "effect") {MyTree.children.Add (EffectNode()); }
                else if (keyword.Value == "card") { MyTree.children.Add(CardNode()); }
            }
            Console.WriteLine(Peek()) ;
                while (Peek().Type != Token_Type.EOF) 
                {
                  Console.WriteLine(Peek());
                    if (Peek().Value != "}" && Peek().Value != ";")
                    {
                        if (Peek().Value == "card" || Peek().Value == "effect")
                        {
                            return KeywordNode();
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    Advance();
                }
            return MyTree;
        }

        #region Effect

        //creamos el nodo effect
        private EffectASTNode EffectNode()
        {
            EffectASTNode effect = new EffectASTNode();
            Advance();
            if (!Check(Token_Type.DELIMITIER)) return null;
            Advance();
            //nombre de efecto
            if (Check(Token_Type.IDENTIFIER))
            {
                Advance();
                while (Peek().Type != Token_Type.STRING)
                {
                    Advance();  
                }
                effect.Name = Peek().Value;                           
               Advance();
             comprobar =  Consume(Token_Type.COMMA,"se esperaba una comma despues del name");
                if (comprobar == null) { Console.WriteLine("null  en 144"); return null;}
            }
            else { return null; }   
            if (Peek().Value == "Params") 
            {
                Advance();
               Params parametros = new Params();
               parametros = ParamsNode(parametros);
                if (parametros == null) { Console.WriteLine("null en 152"); return null; } 
                effect.children.Add(parametros);
                effect.Params= parametros;  
            }
            Console.WriteLine(Peek());                 

            if (Peek().Value == "Action")
            {
                Advance();
                ActionASTNode action = new ActionASTNode(); 
                action  =   ActionNode( action);
                effect.children.Add(action);
                effect.Action= action;  
                Advance(); 
            }                                                  //hasta aqui bien
            Console.WriteLine(Peek());
            comprobar = Consume(Token_Type.SEMICOLON,"Se esperaba un ;");
            if (comprobar == null) { Console.WriteLine("null en 168"); return null; }
            comprobar = Consume(Token_Type.DELIMITIER,"se esperaba un delitador");
            if (comprobar == null) { Console.WriteLine("null en 173"); return null; }
            return effect;  
        }

        //los parametros de effect,en el caso que tenga
        private Params ParamsNode(Params parametros)
        {
            Console.WriteLine(Peek());
           comprobar = Consume(Token_Type.COLON,"Se esperaban dos puntos");
            if (comprobar == null) { Console.WriteLine("null en 176"); return null; }
            comprobar = Consume(Token_Type.DELIMITIER,"Se Esperaba un {");
            if (comprobar == null) { Console.WriteLine("null en 178"); return null; }
            while (Peek().Value != "}") 
            {
                Console.WriteLine(Peek());
                parametros.param.Add(CreateNode());
                Console.WriteLine(Peek());
            }
            Advance();
            return parametros;
        }

        //el actio de effect
        private ActionASTNode ActionNode(ActionASTNode action)
        {
            comprobar = Consume(Token_Type.COLON,"se esperaba un : ");
            if (comprobar == null) { Console.WriteLine("null en 192"); return null; }
            if (Check(Token_Type.LEFT_PAREN))
            {
                Advance();
                while (Peek().Type != Token_Type.RIGHT_PAREN)
                {
                    Console.WriteLine(Peek());
                    action.parametros.Add(CreateNode());
                    Console.WriteLine(Peek());
                }
               
            }
            else { Console.WriteLine("null en 203"); return null; }
            Advance();
           comprobar = Consume(Token_Type.LAMBDA,"Se esperaba un =>");
            action.Lambda = new LambdaForAction(action.parametros,action.actions);
            if (comprobar == null) { Console.WriteLine("null en 206"); return null; }
            if (Check(Token_Type.DELIMITIER))
            {
                Advance();  
                while (Peek().Value != "}")         //hasta aqui estas bien
                {
                    Console.WriteLine(Peek());  
                    action.actions.Add(CreateNode());
                    Console.WriteLine(Peek());
                }
            }
            else { return null; }
            return action;
        }

        #endregion

        #region Card
        private ASTnode CardNode() 
        {
            CardASTNode card = new CardASTNode();
            Advance();
            comprobar = Consume(Token_Type.DELIMITIER,"se esperaba un { despues de card");
            if (comprobar == null) { Console.WriteLine("null en 253"); return null;}
            card = Meter("Name", card);
            if (card == null) { Console.WriteLine( "null en 255"); return null; };
            card = Meter("Type", card);
            if (card == null) { Console.WriteLine("null en 257"); return null; };
            card = Meter("Faction", card);
            if (card == null) { Console.WriteLine("null en 259"); return null; };
                                                                                        //hasta aqui esta perfecto
            if (Check(Token_Type.IDENTIFIER) && Peek().Value == "Power")
            {
                Advance();
                while (Peek().Type != Token_Type.NUMBER) { Advance(); }
                card.Power = Convert.ToInt32(Peek().Value);
                Advance();
                comprobar = Consume(Token_Type.COMMA, "se esperaba una ,");
                if (comprobar == null) { return null; }
            }
            else { Console.WriteLine("null en 270"); return null; }
                                                                                          //bien
            if (Check(Token_Type.IDENTIFIER) && Peek().Value == "Range")
            {
                Advance();
                card = MeterenRange(card);
                if (card == null) return null;
                comprobar = Consume(Token_Type.COMMA, "se esperaba una comma");
                if (comprobar == null) return null;
            }
            else return null;

            Console.WriteLine(Peek());
            if (Check(Token_Type.IDENTIFIER) && Peek().Value == "OnActivation")
            {
                Advance();
                Console.WriteLine(Peek());
                comprobar = Consume(Token_Type.COLON,"se esperaba : despues de OnActivation");
                comprobar = Consume(Token_Type.DELIMITIER,"Se esperaba un [");
                if (comprobar == null) return null;
                card = OnActivationList(card);
            }

            return card;
        }

        private CardASTNode OnActivationList(CardASTNode card) 
        {
            while (Peek().Value != "]") 
            {
                Console.WriteLine(Peek());
                if (Peek().Value == "{")
                {
                    Advance();
                    while (Peek().Value != "}") 
                    {
                        if (Peek().Value == "Effect")
                        {
                            Advance();  
                            card.OnActivation.Add(EffectForCard());
                        }  
                    }
                    Advance();
                }                                                     // esta bien

                if (CheckValue("Selector"))
                {
                    Advance();
                    comprobar = Consume(Token_Type.COLON,"se esperaba :");
                    if (comprobar == null) return null;
                    comprobar = Consume(Token_Type.DELIMITIER,"se esperaba {");
                    if (comprobar == null) return null;
                    while (!CheckValue("}")) 
                    {
                       card.OnActivation.Add(SelectorForCard());  
                    }
                  
                }
                Advance();
                Console.WriteLine(Peek());                                 //hasta aqui esta bien
            }
            Advance();  
            return card;
        }

        private EffectCardNode EffectForCard() 
        {
            EffectCardNode effcard = new EffectCardNode();
            comprobar = Consume(Token_Type.COLON,"se esperaba un :");
            if (comprobar == null) return null;
            Console.WriteLine(Peek());
            if (Peek().Type == Token_Type.STRING)
            {
                effcard.Name = Peek().Value;
                Advance();
                return effcard; 
            }
            comprobar = Consume(Token_Type.DELIMITIER,"se esperaba un {");
            if (comprobar == null) return null;
            while (Peek().Value != "}") 
            {
                Console.WriteLine(Peek());
                if (Peek().Value == "Name") 
                { 
                    Advance(); 
                    if (!Check(Token_Type.COLON)) 
                    {
                        Console.WriteLine("Se esperaba dos puntos");
                        return null; };
                    while (Peek().Type != Token_Type.STRING) { Advance(); }
                    effcard.Name = Peek().Value;
                    Advance();
                    comprobar = Consume(Token_Type.COMMA, "se esperaba ,");
                }
                else if (Peek().Value == "Amount") 
                { 
                    Advance();
                    comprobar = Consume(Token_Type.COLON, "se esparaba :");
                    while(Peek().Type != Token_Type.COMMA) { effcard.Amaunts.Add(CreateNode()); };
                    Advance();  
                }
            }
            return effcard;
        }
        private SelectorCardNode SelectorForCard() 
        {
            SelectorCardNode selector = new SelectorCardNode();
            if (CheckValue("Source"))
            {
               Advance();   
                comprobar = Consume(Token_Type.COLON, "se esperaba :");
                if (comprobar == null) return null;
                if (Check(Token_Type.STRING))
                {
                    selector.Source = Peek().Value;
                    Advance();  
                }
            }
            if (CheckValue("Single"))
            {
                Advance();
                comprobar = Consume(Token_Type.COLON, "se esperaba un :");
                if (comprobar == null) return null;
                if (Check(Token_Type.BOOLEAN)) selector.Single = Convert.ToBoolean(Peek().Value);
                Advance();
            }
            comprobar = Consume(Token_Type.COMMA, "se esperaba ,");
            if (CheckValue("Predicate"))
            {
                Advance();
                comprobar = Consume(Token_Type.COLON,"se esperaba : despuesde Predicate");
                selector.Predicate = CreateNode();
                Console.WriteLine(Peek());
            }
            return selector;
        }

        private CardASTNode MeterenRange(CardASTNode card) 
        {
            comprobar = Consume(Token_Type.COLON,"se esperaba :despues de Range");
            comprobar = Consume(Token_Type.DELIMITIER,"se esperaba un delimitador");
            if (comprobar == null) { return null; }
            while (Peek().Type != Token_Type.DELIMITIER) 
            {
                if (Peek().Type == Token_Type.STRING) { card.Range.Add(new Node(Peek().Type,Peek().Value));}
                Advance();
                if (Peek().Type != Token_Type.DELIMITIER) { comprobar = Consume(Token_Type.COMMA, "se esperaba una comma");}
            }
            Advance();
            return card;
        }

        private CardASTNode Meter(string value,CardASTNode card) 
        {
            bool t = false, n = false , f = false;
            if (value == "Name") { n = true; }
            else if (value == "Type") { t = true; }
            else if (value == "Faction") { f = true; }
            comprobar = Consume(Token_Type.IDENTIFIER, "se esperaba el type");
            if (comprobar == null) { return null; }
            while (Peek().Type != Token_Type.STRING) { Advance(); }
            if (t) { card.Type = Peek().Value; }
            else if (n) { card.Name = Peek().Value; }
            else if (f) { card.Faction = Peek().Value; }
            Advance();
            comprobar = Consume(Token_Type.COMMA,"se esperaba una ,");
            if (comprobar == null) { return null; }
            return card;
        }

        #endregion

        #region Make nodos
        //creamos nodos
        private ASTnode CreateNode() {return LogicalNode();}

        //&& y ||
        private ASTnode LogicalNode()
        {
            ASTnode node = EqualASTNode();
            while (Match(Token_Type.AND, Token_Type.LOGIC))
            {
                Token boolean = Previous();
                ASTnode right = EqualASTNode();
                if (boolean.Type == Token_Type.AND) node = new AndASTNode(node, right);
                else { node = new OrASTNode(node, right); }
            }
           return node;
        }

        // == y !=
        private ASTnode EqualASTNode()
        {
            ASTnode node = ComparasionASTNode();
            while (Match(Token_Type.EQUAL,Token_Type.NOT_EQUAL) )
            {
                Token equality = Previous();
                ASTnode right = ComparasionASTNode();
                if (equality.Value == " == ") { node = new EqualASTNode(node, right); }
                else { node = new NotEqualASTNode(node, right); }
            }
            return node;
        }

        // < ,> , <= , >=
        private ASTnode ComparasionASTNode() 
        {
            ASTnode node = ConcatenationASTNode();
            while (Match(Token_Type.GREATER,Token_Type.GREATER_EQUAL,Token_Type.LESS,Token_Type.LESS_EQUAL))
            {
                Token comparasion = Previous();
                ASTnode right = ConcatenationASTNode();
                if (comparasion.Value == ">") { node = new ComparationASTNode(node, Token_Type.GREATER, right); }
                else if (comparasion.Value == ">=") { node = new ComparationASTNode(node, Token_Type.GREATER_EQUAL, right); }
                else if (comparasion.Value == "<") { node = new ComparationASTNode(node, Token_Type.LESS, right); }
                else if (comparasion.Value == "<=") { node = new ComparationASTNode(node, Token_Type.LESS_EQUAL, right); }
            }
            return node;        
        }
        //@@
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
        // + , -
        private ASTnode TermASTNode() 
        {
            ASTnode node = FactorNode();
            while (Match(Token_Type.PLUS , Token_Type.MINUS)) 
            {
                Token term = Previous();
                ASTnode right = FactorNode();
                if (term.Value == "+") { node = new PlusAST(node, right); }
                else
                {
                    node = new MinusASTNode(node, right);   
                }
            }
            return node;
        }
        // *,/,%
        private ASTnode FactorNode() 
        {
            ASTnode node = PowerNode();
            while (Match(Token_Type.DIVIDE,Token_Type.MULTIPLY,Token_Type.MODULUS)) 
            {
                Token fact = Previous();    
                ASTnode right = PowerNode();
                if (fact.Value == "*") { node = new FactorASTNode(node,Token_Type.MULTIPLY,right); }
               else if (fact.Value == "/") { node = new FactorASTNode(node,Token_Type.DIVIDE,right); }
               else  if (fact.Value == "%") { node = new FactorASTNode(node,Token_Type.MODULUS,right); }
            }
            return node;    
        }
        //^
        private ASTnode PowerNode() 
        {
            ASTnode node = AssignationNode();
            while (Match(Token_Type.POWER)) 
            {
                Token power = Previous();
                ASTnode right = AssignationNode();
                node = new PowerASTNode(node,right);
            }
            return node;
        }

        //=
        private ASTnode AssignationNode() 
        {
          ASTnode node = AssignemetWithValue();
            while (Match(Token_Type.ASSIGN) && Previous().Value == "=")
            {
                Token assign= Previous();
                ASTnode right = AssignemetWithValue();
                node = new AssignASTNode(node,right);
            }
            return node;
        }
        // -=, +=, *=, /=, %=
        private ASTnode AssignemetWithValue() 
        {
            ASTnode node = ColonNode();
            while (Match(Token_Type.ASSIGN) && (Previous().Value == "+=" || Previous().Value == "-=" || Previous().Value == "/=" || Previous().Value == "%=" || Previous().Value == "*="))
            {
                Token awv = Previous();
                ASTnode right = ColonNode();
                    if (awv.Value == "+=") { node = new AssingnementWithValue(node,awv.Value,right); }
               else if (awv.Value == "-=") { node = new AssingnementWithValue(node,awv.Value,right); }
               else if (awv.Value == "*=") { node = new AssingnementWithValue(node,awv.Value,right); }
               else if (awv.Value == "/=") { node = new AssingnementWithValue(node,awv.Value,right); }
               else if (awv.Value == "%=") { node = new AssingnementWithValue(node,awv.Value,right); } 
            }
            return node;
        }
        // :
        private ASTnode ColonNode() 
        {
            ASTnode node = AccessNode();
            while (Match(Token_Type.COLON)) 
            {
                Console.WriteLine(Peek());
                Token col = Previous();
                ASTnode right = AccessNode();
                node = new ColonASTNode(node,right);
            }
            return node;
        }
        //.
        private ASTnode AccessNode() 
        {
            ASTnode node = LambdaASTnode();
            while (Match(Token_Type.ACCESS))
            {
                Token access = Previous();
                ASTnode right = LambdaASTnode();
                node = new AccessASTNode(node,right);
            }
            return node;
        }

        private ASTnode LambdaASTnode() 
        {
            ASTnode node = UnaryNode();
            while (Match(Token_Type.LAMBDA)) 
            {
                Token lamb = Previous();
                ASTnode right = UnaryNode();
                node = new LambdaASTNode(node,right);
            }
            return node;    
        }

        //! , ++, --
        private ASTnode UnaryNode() 
        {
            while (Match(Token_Type.NOT,Token_Type.UNARY)) 
            {
                Token unary= Previous();
                ASTnode node = UnaryNode();
                return new UnaryASTNode(unary.Type,unary.Value,node);
            }
            return LiteralNode();
        }
        //string ,number,boolean
        private ASTnode LiteralNode() 
        {
            while (Match(Token_Type.NUMBER,Token_Type.BOOLEAN,Token_Type.STRING))
            {
                return new LiteralASTNode(Previous().Type, Previous().Value);
            }
            while (Match(Token_Type.IDENTIFIER)) { return new IdentifierASTNode(Previous().Type, Previous().Value);}
            if (Match(Token_Type.LEFT_PAREN))
            {
                ASTnode insidetheparent = CreateNode();
                Consume(Token_Type.RIGHT_PAREN,"Se esperaba un parentisis derecho");
                return new GroupingASTNode(insidetheparent);    
            }
            return PrimaryNode();
        }

        //keyweords que no son ,ni effect , ni card
        public ASTnode PrimaryNode() 
        {
            if (Match(Token_Type.KEYWORD))
            {
                Token node = Previous();
                if (node.Value == "if") { return IfASTNode(); }
                else if (node.Value == "while") { return WhileNode(); }
                else if (node.Value == "for") { return ForNode(); }
            }
            if (Match(Token_Type.COMMA))
            {
                return new CommaASTNode();
            }
            if (!Check(Token_Type.EOF))
            {
                return CreateNode();
            }
           
          return KeywordNode();
        }
        public ASTnode IfASTNode() 
        {
            //creamos el nodo para la condicicon y el nodo para el bloque
            ConditionalASTNode condition = new ConditionalASTNode();
            BlockASTNode block = new BlockASTNode();
            
          comprobar =  Consume(Token_Type.LEFT_PAREN,"Se espera un ( despues del if");
            if (comprobar == null) { return null;}
            while (Peek().Type != Token_Type.RIGHT_PAREN) 
            {
                if (Peek().Type == Token_Type.EOF) { break; throw new Exception("Esta mal el codigo en la condicion if");  }
                Console.WriteLine(Peek());
                condition.condicion.param.Add(CreateNode());
                Console.WriteLine(Peek());
            }
            Advance();
           comprobar = Consume(Token_Type.DELIMITIER,"Se esperaba un { despues de la condicion");
            if (comprobar == null) { return null; }
            while (Peek().Type != Token_Type.DELIMITIER)
            {
                if (Peek().Type == Token_Type.EOF) { break; throw new Exception("Esta mal el codigo en la condicion if"); }
                Console.WriteLine(Peek());
                block.Block.param.Add(CreateNode());
                Console.WriteLine(Peek());
            }
            Advance();   
            return new IfASTNode(condition, block); 
        }

        public ASTnode WhileNode() 
        {
            Console.WriteLine(Peek());
            ConditionalASTNode condition = new ConditionalASTNode();  
            BlockASTNode block = new BlockASTNode();
           comprobar =   Consume(Token_Type.LEFT_PAREN,"se esperaba un (");
            if (comprobar == null) { return null; }
            while (Peek().Type != Token_Type.RIGHT_PAREN) { Console.WriteLine(Peek()); condition.condicion.param.Add(CreateNode()); Console.WriteLine(Peek()); }
            Advance();  
            Console.WriteLine(Peek());
           comprobar = Consume(Token_Type.DELIMITIER, "se esperaba un {");
            if (comprobar == null) { return null; }
            while (Peek().Type != Token_Type.DELIMITIER) { Console.WriteLine(Peek()); block.Block.param.Add(CreateNode()); Console.WriteLine(Peek());if (Check(Token_Type.SEMICOLON)) { Advance();} }
           comprobar = Consume(Token_Type.DELIMITIER, "se esperaba un }");
            if (comprobar == null) { return null; }
            return new WhileASTNode(condition, block);  
        }

        public ASTnode ForNode() 
        {
            Console.WriteLine(Peek());  
            BlockASTNode block = new BlockASTNode();
           
            if (Peek().Type == Token_Type.IDENTIFIER)
            {
               Advance();
                if (Peek().Type == Token_Type.IDENTIFIER && Peek().Value == "in")
                {
                    Advance();
                    if (Peek().Type == Token_Type.IDENTIFIER)
                    {
                        Advance();
                        Consume(Token_Type.DELIMITIER, "se esperaba una {");
                        while (Peek().Type != Token_Type.DELIMITIER) { Console.WriteLine(Peek()); block.Block.param.Add(CreateNode()); Console.WriteLine(Peek()); }
                    }
                    else { throw new Exception(); }
                }
                else{ throw new Exception();}
            }
            else{throw new Exception();}

            return new ForASTNode(block);
        }
        #endregion
        //--------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------------------------------------------------------------------------------------------
        //public static void MandarPython(string args)
        //    {
        //        string pythonScriptPath = @"C:\Python_Programs\PythonApplication1\PythonApplication1\PythonApplication1.py";
        //string command = $"\"{pythonScriptPath}\" " + args; // Enclose the path in quotes to handle spaces

        //ProcessStartInfo startInfo = new ProcessStartInfo
        //{
        //    WindowStyle = ProcessWindowStyle.Hidden,
        //    RedirectStandardOutput = true,
        //    UseShellExecute = false, // Keep this false for shell commands
        //    CreateNoWindow = true,
        //    FileName = "python.exe", // Specify the Python interpreter
        //    Arguments = command
        //};

        //Process process = Process.Start(startInfo);
        //string result = process.StandardOutput.ReadToEnd();
        //process.WaitForExit();
        //Console.WriteLine(result);
        //    }

        //   public string Word()
        //   {
        //      Node n = ART();
        //      string t =  MakeList(n);
        //      return t;
        //   }

         
        //  public string MakeList(Node root)
        //  {
        //    List<(Node,int)> exist = new List<(Node,int)>(); 
        //    exist.Add((root,root.Children.Count));
        //    int pivote =0;

        //     while(pivote < exist.Count ){
            
        //         Meter(exist[pivote].Item1,  exist );
                
        //      pivote ++;
        //     }
            
        //    List<string> s = new List<string>();
        //    int count=0;
        //   foreach(var item in exist)
        //   {
        //      s.Add(item.Item1.Value +  count  + $"{item.Item1.Children.Count}");
        //      count++;
        //   }
            
        //    string w = "";
        //     for(int h = 0 ; h < s.Count;h++)
        //     {
        //        w += s[h] + " ";
        //     }

        //     return w;
        //  }

        //  private void Meter(Node father, List<(Node,int)> t)
        //  {
        //     for(int i = 0 ; i < father.Children.Count;i++)
        //     {
        //           t.Add((father.Children[i],father.Children[i].Children.Count));
        //     }
        //  }


        //Dictionary<string, int> Arit = new Dictionary<string, int>()
        //{
        //    { "+", 1 },
        //    { "-",1 },
        //    { "*",0 },
        //    { "/",0 },
        //    { "%",-1},
        //    { "^",-1 }  
        //};

        //public Node ART() 
        //{
        //   Node aa = Aritmetics(this.tokens,0,null,null,null); 
        //   return  aa;
        //}


        //private Node Aritmetics(List<Token> tokens,int count,Node lastNumber,Node lastOperator,Node maxOperator) 
        //{
        //    if (count == tokens.Count)
        //    {
        //        SonOf(lastNumber,lastOperator);
        //        return maxOperator;
        //    }

        //    if (Arit.ContainsKey(tokens[count].Value))
        //    {
        //        bool mayor = false;
        //        Node newOpeator = new Node(tokens[count].Type, tokens[count].Value);
        //        CompararaConMayor(ref newOpeator,ref maxOperator,ref mayor,ref lastNumber,ref lastOperator);
        //        if (!mayor)
        //        {
        //            CompararConAnterior(ref newOpeator,ref lastOperator,ref lastNumber,ref maxOperator);
        //        }
        //     return  Aritmetics(tokens,count += 1,lastNumber,lastOperator,maxOperator);
        //    }
        //    else
        //    {
        //        lastNumber = new Node(tokens[count].Type, tokens[count].Value);
        //     return   Aritmetics(tokens,count += 1,lastNumber,lastOperator,maxOperator);
        //    }
        //}

        //private void CompararConAnterior(ref Node newnode,ref Node lastNode,ref Node lastnumber,ref Node max) 
        //{
        //    if (lastNode == null)
        //    {
        //        SonOf(lastnumber, newnode);
        //        lastNode = newnode;
        //        return;
        //    }
        //    else if (Arit[newnode.Value] >= Arit[lastNode.Value])
        //    {
        //        SonOf(lastnumber,lastNode);
        //        SonOf(lastNode,newnode);

        //        if (max.Children.Contains(lastNode))
        //        {
        //            Quitar(lastNode, max);
        //            SonOf(newnode, max);
        //        }
        //    }
        //    else if (Arit[newnode.Value] < Arit[lastNode.Value])
        //    {
        //        SonOf(lastnumber,newnode);
        //        SonOf(newnode,lastNode);
        //    }
        //    lastNode = newnode;
        //}

        //private void CompararaConMayor(ref Node newnode,ref Node max,ref bool m,ref Node lastnumber,ref Node lastnode) 
        //{
        //    if (max == null)
        //    {
        //        SonOf(lastnumber, newnode);
        //        max = newnode;
        //        m = true;  
        //        lastnode= newnode;   
        //        return;
        //    }
        //    else if (Arit[max.Value] <= Arit[newnode.Value])
        //    {
        //        SonOf(max,newnode);
        //        SonOf(lastnumber,lastnode);
        //        max= newnode;
        //        m = true;
        //        lastnode = newnode;
        //    }
        //}

        //private void SonOf(Node son,Node father) 
        //{
        //    father.Children.Add(son);
        //}

        //private void Quitar(Node son,Node father) 
        //{
        //    for (int i = 0; i < father.Children.Count; i++)
        //    {
        //        if (father.Children[i].Equals(son))
        //        {
        //            father.Children.RemoveAt(i);
        //        }

        //    }
        //}

    }

}