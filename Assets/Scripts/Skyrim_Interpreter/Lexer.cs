using System.ComponentModel;
using Skyrim_Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Skyrim_Interpreter
{
    class Lexer
    {
        //list for tokens 
        public List<Token> tokens { get; private set; }
        //string a analizar
         string   source;
         int position;

        public Lexer(string source)
        {
            tokens = new List<Token>();
            this.source = source;
            position= 0;
        }

        // let's create our regex
         public List<Token> Tokenizer() 
        {

            // ignore white spaces 
            source = source.Replace(" ", string.Empty);

            //regex 
            string keywords = @"\b(Effect|card|for|while|if|else|return|Params|Action|effect)\b";
            string indetifiers = @"^[a-zA-Z_][a-zA-Z0-9_]*";
            string lambdaoperator = @"=>";
            string arithmeticsoperators = @"(?:[+\-*/%])";
            string relational = @"(?:==|<=|>=|>|<|!=)";
            string logicoperations = @"(?: &&|\|\||!)";
            string assignmentoperations = @"(?:=|\+=|-=|\*=|/=|%=|:)";
            string unaryoperations = @"(?:\+\+|--)";
            string number = @"\d+";
            string floats = @"\d+(\.\d+)?";
            string dilimiter = @"[\(\)\{\}\[\]]";
            string colom = @",";
            string semocolom = @";";
            string acccess = @"\.";
            string strings = @"""((^""\\]|\.)*?)""";
            string singlecomment = @"//.*";
            string multiplecomment = @"/\*.*";

            // dictionary for to know what tokens we have  
            Dictionary<string, Token_Type> WeHave = new Dictionary<string, Token_Type>()
            {
                { keywords, Token_Type.KEYWORD },
                { indetifiers , Token_Type.IDENTIFIER },
                { lambdaoperator , Token_Type.LAMBDA },
                { arithmeticsoperators , Token_Type.ARITHMETIC },
                { relational,Token_Type.RELATION },
                { logicoperations,Token_Type.LOGIC },
                { assignmentoperations,Token_Type.ASSIGN },
                { unaryoperations,Token_Type.UNARY  },
                { number,Token_Type.NUMBER },
                { floats,Token_Type.FLOAT},
                { dilimiter,Token_Type.DELIMITIER },
                { colom, Token_Type.COMMA },
                { semocolom,Token_Type.SEMICOLON},
                { acccess,Token_Type.ACCESS },
                { strings , Token_Type.STRING },
               
            };

            //we need to scaning the source
            while (position < source.Length ) 
            {
                string bestmatch = null;
                int bestlength = 0;
                Token_Type besttokentype  = Token_Type.EOF;

                foreach ( var element in WeHave ) 
                {
                    Match match = Regex.Match(source, element.Key);
                    if (match.Success && match.Index == 0 && match.Length > bestlength )
                    {
                        bestmatch = match.Value;
                        besttokentype = element.Value; 
                        bestlength = match.Length;
                    }
                }
                if ( bestmatch == null ) 
                {
                    source = source.Substring(1).Replace(" ",string.Empty);
                    continue;
                }
                tokens.Add(new Token(besttokentype, bestmatch));
                source = source.Substring(bestlength).Replace(" ",string.Empty);
                if (besttokentype == Token_Type.UNARY && !string.IsNullOrEmpty(source))
                {
                    var next = Regex.Match(source, unaryoperations);
                    if (next.Success && next.Index == 0)
                    {
                        tokens[tokens.Count - 1].Value += next.Value;
                        source = source.Replace(" ", string.Empty);
                    }
                }
            }
            return tokens;
        }
    

        //obtener la linea
        private int GetLineNumber(string source, int position)
        {
            int lineNumber = 1;
            int columnNumber = 1;

            for (int i = 0; i < position; i++)
            {
                if (source[i] == '\n')
                {
                    lineNumber++;
                    columnNumber = 1;
                }
                else
                {
                    columnNumber++;
                }
            }

            return lineNumber;
        }


        //obtener la columna
        private int GetColumnNumber(string source, int position)
        {
            int columnNumber = 1;

            for (int i = 0; i < position; i++)
            {
                if (source[i] == '\n')
                {
                    columnNumber = 1;
                }
                else
                {
                    columnNumber++;
                }
            }

            return columnNumber;
        }

    }

    
}
public class Program
{
    public static void Main()
    {
        //agamos una prueba a ver si pincha

        // string hello = "effect {\r\n            Name: \"\"Damage\"\",\r\n            Params: {\r\n                Amount: Number\r\n            },\r\n            Action: (targets, context) => {\r\n                for target in targets {\r\n                    i = 0;\r\n                    while (i++ < Amount)\r\n                        target.Power -= 1;\r\n                };\r\n            }\r\n        }\r\n\r\n        effect {\r\n            Name: \"\"Draw\"\",\r\n            Action: (targets, context) => {\r\n                topCard = context.Deck.Pop();\r\n                context.Hand.Add(topCard);\r\n                context.Hand.Shuffle();\r\n            }\r\n        }\r\n\r\n        effect {\r\n            Name: \"\"ReturnToDeck\"\",\r\n            Action: (targets, context) => {\r\n                for target in targets {\r\n                    owner = target.Owner;\r\n                    deck = context.DeckOfPlayer(owner);\r\n                    deck.Push(target);\r\n                    deck.Shuffle();\r\n                    context.Board.Remove(target);\r\n                };\r\n            }\r\n        }\r\n\r\n        card {\r\n            Type: \"\"Oro\"\",\r\n            Name: \"\"Beluga\"\",\r\n            Faction: \"\"Northern Realms\"\",\r\n            Power: 10,\r\n            Range: [\"\"Melee\"\", \"\"Ranged\"\"],\r\n            OnActivation: [\r\n                {\r\n                    Effect: {\r\n                        Name: \"\"Damage\"\",\r\n                        Amount: 5,\r\n                    },\r\n                    Selector: {\r\n                        Source: \"\"board\"\",\r\n                        Single: false,\r\n                        Predicate: (unit) => unit.Faction == \"\"Northern\"\" @@ \"\"Realms\"\"\r\n                    },\r\n                    PostAction: {\r\n                        Type: \"\"ReturnToDeck\"\",\r\n                        Selector: {\r\n                            Source: \"\"parent\"\",\r\n                            Single: false,\r\n                            Predicate: (unit) => unit.Power < 1\r\n                        },\r\n                    }\r\n                },\r\n                {\r\n                    Effect: \"\"Draw\"\"\r\n                }\r\n            ]\r\n        }";

        string tutu = "10*10/10+10+10*10";

        Lexer mylexer = new Lexer(tutu);

        List<Token> mytokens = new List<Token>();
        mytokens = mylexer.Tokenizer();

        //foreach (Token token in mytokens)
        //{
        //    Console.WriteLine(token);
        //}

        Parser par = new Parser(mytokens);

        par.ART();
         
         

         Parser.MandarPython(par.Word());

    }
}
