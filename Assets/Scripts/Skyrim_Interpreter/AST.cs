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

    public class ASTnodeTree : ASTnode
    {
      public List<ASTnode> children;
        public ASTnodeTree()
        {
            children= new List<ASTnode>();
        }
    }
    public abstract class ASTnode { }

    public class PlusAST : ASTnode
    {

        public Token_Type type = Token_Type.PLUS;


        public ASTnode LeftChild { get; set; }
        public ASTnode RightChild { get; set; }

        public PlusAST(ASTnode left, ASTnode right)
        {
            LeftChild = left;
            RightChild = right;
        }
    }

    // Variables
    public class IdentifierASTNode : ASTnode
    {
        public Token_Type type { get; set; }
        public string value { get; set; }

        public IdentifierASTNode(Token_Type type, string value) 
        {
            this.type = type;   
            this.value = value;
        } 

    }

    public class MinusASTNode : ASTnode
    {
        Token_Type type = Token_Type.MINUS;

        public ASTnode LeftChildren { get; set; }
        public ASTnode RightChildren { get; set; }

        public MinusASTNode(ASTnode left, ASTnode right)
        {
            LeftChildren = left;
            RightChildren = right;
        }
    }

    public class Node : ASTnode
    {
        public Token_Type type { get; set; }
        public string Value { get; set; }
        public List<Node> Children { get; set; }

        public Node(Token_Type type, string value)
        {
            this.type = type;
            this.Value = value;
            Children = new List<Node>();
        }

    }
    public class PowerASTNode : ASTnode
    {
        Token_Type type = Token_Type.POWER;
        public ASTnode Number { get; set; }
        public ASTnode Pow { get; set; }

        public PowerASTNode(ASTnode number, ASTnode pow)
        {
            this.Number = number;
            this.Pow = pow;
        }

    }
    public class AndASTNode : ASTnode
    {
        public ASTnode left { get; set; }
        Token_Type token;

        public ASTnode right { get; set; }

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

        public OrASTNode(ASTnode left, ASTnode right)
        {

            this.left = left;
            this.right = right;
        }



    }

    public class NotASTNode : ASTNode
    {
        public ASTNode son { get; set; }

        public NotASTNode() : base(Token_Type.NOT, "!") { }

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

    public class AssignASTNode : ASTnode
    {

        Token_Type type = Token_Type.ASSIGN;
        public ASTnode Left { get;  set; }
        public ASTnode Right { get; set; }

        public AssignASTNode(ASTnode left, ASTnode right)
        {
            Left = left;
            Right = right;
        }
    }

    public class AssingnementWithValue  : ASTnode
    {
        public Token_Type type = Token_Type.ASSIGN;
        public string value { get; set; }
        public ASTnode left { get; set; }
        public ASTnode right { get; set; }
        public AssingnementWithValue(ASTnode left, string value,ASTnode right)
        {
            this.left = left;
            this.right = right;
            this.value= value;  
        }


    }
    public class UnaryASTNode : ASTnode
    {
        public Token_Type Operand { get; set; }
        public string value { get; set; }
        public ASTnode Son { get; set; }

        public UnaryASTNode(Token_Type type,string value, ASTnode son)
        {
            Operand = type;
            this.value = value; 
            Son = son;
        }

    }

    public class ColonASTNode : ASTnode 
    {
        public Token_Type type = Token_Type.COLON;
        public ASTnode left { get; set; }
        public ASTnode right { get; set; }
        public ColonASTNode( ASTnode left, ASTnode right)
        {
            this.left = left;
            this.right = right;
        }
    }

    public class Params : ASTnode
    {
        public List<ASTnode> param { get; set; }
        public Params()
        {
            param = new List<ASTnode>();
        }
    }

    public class ConditionalASTNode : ASTnode
    {
        public Params condicion { get; set; }
        public ConditionalASTNode()
        {
            condicion= new Params();    
        }

    }


    public class BlockASTNode : ASTnode
    {
        public Params Block { get; set;}
        public BlockASTNode()
        {
            Block= new Params();    
        }
    }

    public class IfASTNode : ASTnode
    {
        public ConditionalASTNode conditional { get; set; }
        public BlockASTNode block { get; set; }
        public IfASTNode(ConditionalASTNode conditional, BlockASTNode block)
        {
            this.conditional = conditional;
            this.block = block;
        }
    }

    public class CommaASTNode : ASTnode 
    {
       public Token_Type type = Token_Type.COMMA;
        public string value = ",";
    }

    public class AccessASTNode :ASTnode
    {
        Token_Type Type= Token_Type.ACCESS;
        public ASTnode left { get; set; }
        public ASTnode right { get; set; }
        public AccessASTNode(ASTnode left, ASTnode right)
        {
            this.left = left;
            this.right = right;
        }
    }
    public class ActionASTNode : ASTnode
    {

        public List<ASTnode> parametros { get; set; }
        public List<ASTnode> actions { get; set; }
        public ActionASTNode()
        {
            parametros= new List<ASTnode>();    
            actions= new List<ASTnode>();   
        }
    }

    public class EffectASTNode : ASTnode    
    {
        public string Name { get; set; }
        public Params Params { get; set; }
        public ActionASTNode Action { get; set; }
        public List<ASTnode> children { get; set; }
        public EffectASTNode()
        {
            children = new List<ASTnode>();
        }
    }

    public class ComparationASTNode : ASTnode
    {
        Token_Type type { get; set; }
        ASTnode left { get; set; }
        ASTnode right { get; set; }
        public ComparationASTNode(ASTnode left, Token_Type type, ASTnode right)
        {
            this.left = left;
            this.right = right;
            this.type = type;
        }

    }

    public class ConcatenationASTNode : ASTnode
    {
        public Token_Type type = Token_Type.CONCAT;
        public ASTnode left { get; set; }
        public ASTnode right { get; set; }
        public ConcatenationASTNode(ASTnode left, ASTnode right)
        {
            this.left = left;
            this.right = right;
        }
    }

    public class FactorASTNode : ASTnode
    {
        Token_Type type { get; set; }
        ASTnode leftchild { get; set; }
        public ASTnode rightchild { get; set; }
        public FactorASTNode(ASTnode leftchild, Token_Type type, ASTnode rightchild)
        {
            this.type = type;
            this.leftchild = leftchild;
            this.rightchild = rightchild;
        }
    }

    public class LiteralASTNode : ASTnode   
    {
        public Token_Type Type { get; set; }
        public string value { get; set; }
        public LiteralASTNode(Token_Type type,string value)
        {
            this.Type = type;
            this.value = value; 
        }

    }

    public class GroupingASTNode : ASTnode
    {
        public ASTnode groupnode { get; }
        public GroupingASTNode(ASTnode groupnode)
        {
            this.groupnode = groupnode; 
        }
    }

    public class WhileASTNode  : ASTnode
    {
        public ASTnode condition { get; set; }
        public ASTnode block { get; set;}
        public WhileASTNode(ASTnode condition,ASTnode block)
        {
            this.condition = condition; 
            this.block = block; 
        }

    }

    public class ForASTNode : ASTnode
    {
        public BlockASTNode block { get; set; }
        public ForASTNode(BlockASTNode block)
        {
            this.block = block; 
        }
    }

    public class CardASTNode  : ASTnode
    {
        public string Name { get; set; }    
        public string Type { get; set; }
        public string Faction { get; set;}
        public int Power { get; set;}
        public List<ASTnode> Range { get; set;}
        public List<ASTnode> OnActivation { get; set; }
        EffectCardNode EffectCardNode { get; set; }
        SelectorCardNode SelectorCardNode { get; set; } 
        public CardASTNode()
        {
                Range = new List<ASTnode>();    
            OnActivation = new List<ASTnode>(); 
        }
    }

    public class EffectCardNode  : ASTnode
    {
        public string Name { get; set; }    
        public List<ASTnode> Amaunts { get; set; }
        public EffectCardNode()
        {
            Amaunts= new List<ASTnode>();   
        }
    }

    //falta hacerle el predicate
    public class SelectorCardNode : ASTnode
    {
        public string Source { get; set;}
        public bool Single { get; set;}

        public SelectorCardNode()
        {
            Single = false;
        }

    }

}
