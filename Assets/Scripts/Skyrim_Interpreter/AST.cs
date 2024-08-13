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
        

        public ASTNode(Token_Type type,string value)
        {
            Type = type;    
            Value = value;  
          
           children= new List<ASTNode>();   
        }

        public void AddChild(ASTNode node) 
        {
            children.Add(node); 
        }


        // rules = []
        List<Func<List<string>>> Rules = new List<Func<List<string>>>() ;

    }


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


    public class Node 
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



    public class AndASTNode : ASTNode 
    {
        public ASTNode left { get; set;}
        public ASTNode right { get; set;}

        public AndASTNode() : base(Token_Type.AND, "&&") { }

        public void AddLeft(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            left = n;
        }

        public void AddRigth(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            right = n;
        }

    }


    public class OrASTNode : ASTNode
    {
        public ASTNode left { get; set; }
        public ASTNode right { get; set; }

        public OrASTNode() : base(Token_Type.OR, "||") { }

        public void AddLeft(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            left = n;
        }

        public void AddRigth(ASTNode n)
        {
            if (n == null)
            {
                throw new ArgumentNullException(nameof(n));
            }

            right = n;
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


    public class NotEqualASTNode : ASTNode
    {
        public ASTNode Left { get; private set; }
        public ASTNode Right { get; private set; }

        public NotEqualASTNode(ASTNode left, ASTNode right)
            : base(Token_Type.NOT_EQUAL, "!=")
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

    public class EqualASTNode : ASTNode
    {
        public ASTNode Left { get; private set; }
        public ASTNode Right { get; private set; }

        public EqualASTNode(ASTNode left, ASTNode right)
            : base(Token_Type.EQUAL, "==")
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


    public class Params 
    {
        public List<ASTNode> parametros { get; set; }


        public Params()
        {
            parametros = new List<ASTNode>();   
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











}




