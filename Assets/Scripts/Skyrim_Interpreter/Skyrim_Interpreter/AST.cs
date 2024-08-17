using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Skyrim_Interpreter
{
    
    public abstract class ASTNode 
    {
        public string Name { get; set; }
        public Token_Type Type { get; set; }//tipo de nodo
        public object Value { get; internal set; }

        public List<ASTNode> children;


        public ASTNode(Token_Type type, string value)
        {
            Type = type;
            Value = value;

            children = new List<ASTNode>();
        }

        public void AddChild(ASTNode node)
        {
            children.Add(node);
        }


        // rules = []
        List<Func<List<string>>> Rules = new List<Func<List<string>>>();

    }


    public abstract class ASTnode { }

    public class PlusAST : ASTNode 
    {

        public Token_Type type { get; set; }
        public string value { get; set; }

        public ASTNode LeftChild { get; set; }
        public ASTNode RightChild { get; set; }

        public PlusAST() : base(Token_Type.PLUS, "+") { }

        public void AddLeft(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            LeftChild = n;
        }

        public void AddRigth(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            RightChild = n;
        }

    }

    // Variables
    public class IdentifierASTNode : ASTNode
    {
        public string value { get; set; }

        public IdentifierASTNode(string value) : base(Token_Type.IDENTIFIER, value) { this.value = value; }
       
    }

    public class MinusASTNode : ASTNode
    {
        public ASTNode LeftChildren { get; set; }
        public ASTNode RightChildren { get; set; }  

        public MinusASTNode() : base(Token_Type.MINUS, "-") { }

        public void AddLeft(ASTNode n) 
        {
            if (n == null)
            {
                throw  new ArgumentNullException(nameof(n));
            }

            LeftChildren = n;
        }

        public void AddRigth(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            RightChildren = n;
        }

    }


    public class Node  : ASTnode
    {
        public Token_Type type { get; set; }
        public string Value { get; set; }
        public  List<Node> Children { get; set;}

        public Node(Token_Type type,string value)
        {
            this.type = type;   
            this.Value = value; 
            Children= new List<Node>(); 
        }

    }

    public class DivideASTNode : ASTNode
    {
        public ASTNode LeftChildren { get; set;}
        public ASTNode RightChildren { get; set;}

        public DivideASTNode() : base(Token_Type.DIVIDE , "/") { }


        public void AddLeft(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            LeftChildren = n;
        }

        public void AddRigth(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            RightChildren = n;
        }
    }

    public class MultiplyASTNode : ASTNode 
    {
        public ASTNode LeftChildren { get; set;}    public ASTNode RightChildren { get; set;}

        public MultiplyASTNode() : base(Token_Type.MULTIPLY , "*") {  }

        public void AddLeft(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            LeftChildren = n;
        }

        public void AddRigth(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            RightChildren = n;
        }

    }

    public class PowerASTNode : ASTNode 
    {
        public ASTNode Number { get; set;}
        public ASTNode Pow { get; set;}

        public PowerASTNode() : base(Token_Type.POWER, "^") { }


        public void AddNumber(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            Number = n;
        }

        public void AddPow(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            Pow = n;
        }
    }



    public class AndASTNode : ASTnode 
    {
        public ASTnode left { get; set;}
        Token_Type token;
       
        public ASTnode right { get; set;}

        public AndASTNode(ASTnode left, ASTnode rigth) 
        {
            this.left = left;
         token = Token_Type.AND;    

            this.right = rigth; 
        } 

     
    }


    public class OrASTNode : ASTnode
    {
        public ASTnode left { get; set; }
        Token_Type type = Token_Type.OR;

        public ASTnode right { get; set; }

        public OrASTNode(ASTnode left,ASTnode right) 
        {

            this.left = left;   
            this.right = right;
        } 

       

    }

    public class NotASTNode : ASTNode 
    {
        public ASTNode son { get; set; }

        public NotASTNode() : base(Token_Type.NOT,"!") { }

        public void AddSon(ASTNode t) 
        {
            if (t == null)
            {
                throw new ArgumentNullException(nameof(t));
            }
            son = t;    
        }
    }


    public class NotEqualASTNode : ASTnode
    {
        public Token_Type type = Token_Type.NOT_EQUAL;
        public ASTnode Left { get; private set; }
        public ASTnode Right { get; private set; }

        public NotEqualASTNode(ASTnode left, ASTnode right)
            
        {
            Left = left;
            Right = right;
        }

        public void AddLeft(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Left node cannot be null");
            }
            Left = node;
        }

        public void AddRight(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Right node cannot be null");
            }
            Right = node;
        }
    }

    public class EqualASTNode : ASTnode
    {
        Token_Type type = Token_Type.EQUAL;
        public ASTnode Left { get; private set; }
        public ASTnode Right { get; private set; }

        public EqualASTNode(ASTnode left, ASTnode right)
            
        {
            Left = left;
            Right = right;
        }   
    }

    public class AssignASTNode : ASTNode
    {
        public ASTNode Left { get; private set; }
        public ASTNode Right { get; private set; }

        public AssignASTNode(ASTNode left, ASTNode right)
            : base(Token_Type.ASSIGN, "=")
        {
            Left = left;
            Right = right;
        }

        public void AddLeft(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Left node cannot be null");
            }
            Left = node;
        }

        public void AddRight(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Right node cannot be null");
            }
            Right = node;
        }
    }

    public class GreaterASTNode : ASTNode
    {
        public ASTNode Left { get; private set; }
        public ASTNode Right { get; private set; }

        public GreaterASTNode(ASTNode left, ASTNode right)
            : base(Token_Type.GREATER, ">")
        {
            Left = left;
            Right = right;
        }

        public void AddLeft(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Left node cannot be null");
            }
            Left = node;
        }

        public void AddRight(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Right node cannot be null");
            }
            Right = node;
        }
    }

    public class GreaterEqualASTNode : ASTNode
    {
        public ASTNode Left { get; private set; }
        public ASTNode Right { get; private set; }

        public GreaterEqualASTNode(ASTNode left, ASTNode right)
            : base(Token_Type.GREATER_EQUAL, ">=")
        {
            Left = left;
            Right = right;
        }

        public void AddLeft(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Left node cannot be null");
            }
            Left = node;
        }

        public void AddRight(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Right node cannot be null");
            }
            Right = node;
        }
    }

    public class LessASTNode : ASTNode
    {
        public ASTNode Left { get; private set; }
        public ASTNode Right { get; private set; }

        public LessASTNode(ASTNode left, ASTNode right)
            : base(Token_Type.LESS, "<")
        {
            Left = left;
            Right = right;
        }

        public void AddLeft(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Left node cannot be null");
            }
            Left = node;
        }

        public void AddRight(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Right node cannot be null");
            }
            Right = node;
        }
    }

    public class LessEqualASTNode : ASTNode
    {
        public ASTNode Left { get; private set; }
        public ASTNode Right { get; private set; }

        public LessEqualASTNode(ASTNode left, ASTNode right)
            : base(Token_Type.LESS_EQUAL, "<=")
        {
            Left = left;
            Right = right;
        }

        public void AddLeft(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Left node cannot be null");
            }
            Left = node;
        }

        public void AddRight(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Right node cannot be null");
            }
            Right = node;
        }
    }

    public class LambdaASTNode : ASTNode
    {
        public ASTNode Parameter { get; private set; }
        public ASTNode Body { get; private set; }

        public LambdaASTNode(ASTNode parameter, ASTNode body)
            : base(Token_Type.LAMBDA, "=>")
        {
            Parameter = parameter;
            Body = body;
        }

        public void AddParameter(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Parameter node cannot be null");
            }
            Parameter = node;
        }

        public void AddBody(ASTNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node), "Body node cannot be null");
            }
            Body = node;
        }
    }

    public class UnaryASTNode : ASTNode
    {
        public ASTNode Operand { get; private set; }
        public string value { get; set; }

        public UnaryASTNode(string value): base(Token_Type.UNARY,value) { this.value = value;  }
     
    }


    public class Params : ASTnode
    {
        public List<ASTnode> param { get; set; }


        public Params()
        {
            param = new List<ASTnode>();   
        }
  
    }



    public class ConditionalASTNode
    {
        public Params condicion { get; set; }

    }


    public class BlockASTNode 
    {
        public Params Block { get; set; } 

    }


    public class IfASTNode
    {
        public ConditionalASTNode conditional { get; set; }
        public BlockASTNode block{ get; set; }
    }


    public class ActionASTNode : ASTnode
    {

        public List<ASTnode> parametros { get; set;}
        public List<ASTnode> actions { get; set; }
    }

    public class EffectASTNode 
    {
        public string Name { get; set; }
        public Params Params { get; set; }
        public ActionASTNode Action { get; set; }  
        public List<ASTnode> children { get; set; }
        public EffectASTNode()
        {
            children= new List<ASTnode>();  
        }
    }

    public class ComparationASTNode : ASTnode 
    {
        Token_Type type { get; set; }
        ASTnode left { get; set; }
        ASTnode right { get; set; }
        public ComparationASTNode(ASTnode left,Token_Type type,ASTnode right)
        {
            this.left = left;
            this.right = right; 
            this.type= type;    
        }

    }

}




