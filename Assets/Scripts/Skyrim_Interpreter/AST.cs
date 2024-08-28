using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Skyrim_Interpreter
{
    public abstract class ASTnode { public abstract object Evaluar(Context context,Targets targets);  }
    public class ASTnodeTree : ASTnode
    {
      public List<ASTnode> children;
        public ASTnodeTree()
        {
            children= new List<ASTnode>();
        }
    }

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

        public override object Evaluar(Context context, Targets targets) 
        {
            var left = LeftChild.Evaluar( context, targets);
            var right = RightChild.Evaluar( context,targets);
            return Ayudante.EvaluateBinary(left,this.type,right);
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

        public override object Evaluar(Context context, Targets targets) { return value; }
        

    }

    public class MinusASTNode : ASTnode
    {
        Token_Type type = Token_Type.MINUS;

        public ASTnode LeftChild { get; set; }
        public ASTnode RightChild { get; set; }

        public MinusASTNode(ASTnode left, ASTnode right)
        {
            LeftChild = left;
            RightChild = right;
        }

        public override object Evaluar(Context context, Targets targets) 
        {
            var left = LeftChild.Evaluar( context,  targets);
            var right = RightChild.Evaluar( context,  targets);
            return Ayudante.EvaluateBinary(left,this.type,right);
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

        public override object Evaluar(Context context, Targets targets) 
        {
            return Value;
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

        public override object Evaluar(Context context, Targets targets)
        {
            var left = Number.Evaluar( context, targets);
            var right = Pow.Evaluar(context, targets);
            return Ayudante.EvaluateBinary(left, this.type, right);
        }

    }
    public class AndASTNode : ASTnode
    {
        public ASTnode left { get; set; }
        Token_Type type = Token_Type.AND;

        public ASTnode right { get; set; }
        public AndASTNode(ASTnode left, ASTnode rigth)
        {
            this.left = left;
           

            this.right = rigth;
        }
        public override object Evaluar(Context context, Targets targets) 
        {
            var left = this.left.Evaluar( context,  targets);
            var right = this.right.Evaluar( context, targets);
            return Ayudante.EvaluateBinary(left,this.type,right);
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

        public override object Evaluar(Context context, Targets targets)
        {
            var left = this.left.Evaluar( context, targets);
            var right = this.right.Evaluar( context,  targets);
            return Ayudante.EvaluateBinary(left, this.type, right);
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

        public override object Evaluar(Context context, Targets targets) 
        {
            var left = this.Left.Evaluar( context,  targets);
            var right = this.Right.Evaluar( context,  targets);
            return Ayudante.EvaluateBinary(left,this.type,right);
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

        public override object Evaluar(Context context, Targets targets) 
        {
            var left = this.Left.Evaluar( context,  targets);
            var right = this.Right.Evaluar(context,  targets);
            return Ayudante.EvaluateBinary(left,this.type,right);
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

        public override object Evaluar(Context context, Targets targets)
        {
            var left = this.Left.Evaluar( context,  targets);
            var right = this.Right.Evaluar( context,  targets);
            if (left is IdentifierASTNode) 
            {
                return left = right;
            }
            throw new InvalidOperationException("Invalid types for assignement");
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
        public override object Evaluar(Context context, Targets targets)
        {
            var son = this.Son.Evaluar( context, targets);
            return Ayudante.EvaluateUnary(this.value,son);
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

        public override object Evaluar(Context context, Targets targets)
        {
           var left = this.left.Evaluar( context,  targets);
            var right = this.right.Evaluar( context,  targets);
            if (left is IdentifierASTNode identifier) 
            {
               
            }
            throw new InvalidOperationException("Invalid types for colon");
        }
    }

    public class Params : ASTnode
    {
        public List<ASTnode> param { get; set; }
        public Params()
        {
            param = new List<ASTnode>();
        }

        public override object Evaluar(Context context, Targets targets)
        {
            var results = new List<object>();
            for (int i = 0; i < param.Count; i++)
            {
                var item = param[i].Evaluar(context,  targets);
                results.Add(item);  
            }
            return results;
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

    public class CommaASTNode : ASTnode 
    {
       public Token_Type type = Token_Type.COMMA;
        public string value = ",";
        public override object Evaluar(Context context, Targets targets) { return value; }
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
        public override object Evaluar(Context context, Targets targets)
        {
            var left = this.left.Evaluar( context,  targets);
            var right = this.right.Evaluar( context,  targets);

            if (left is IdentifierASTNode identifier1 && right is IdentifierASTNode idetifier2) 
            {
               
            }
            throw new NotImplementedException();    
        }

    }
    public class ActionASTNode : ASTnode
    {
        public List<ASTnode> parametros { get; set; }
        public List<ASTnode> actions { get; set; }
        
        public LambdaForAction Lambda {get; set;}
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
        public override object Evaluar(Context context, Targets targets)
        {
            var left = this.left.Evaluar(context,  targets); 
            var right = this.right.Evaluar( context,  targets);
            return Ayudante.EvaluateBinary(left,this.type,right);
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
        public override object Evaluar(Context context, Targets targets)
        {
           var left = this.left.Evaluar(context,  targets);
           var right = this.right.Evaluar( context,  targets);
            return Ayudante.EvaluateBinary(left,this.type,right); 
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
        public override object Evaluar(Context context, Targets targets)
        {
            var left = leftchild.Evaluar( context,  targets); 
            var right = rightchild.Evaluar( context, targets);
            return Ayudante.EvaluateBinary(left,this.type,right);
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

        public override object Evaluar(Context context, Targets targets) 
        {
            if (Type == Token_Type.NUMBER)
            {
                return double.Parse(value);
            }
            else if (Type == Token_Type.STRING)
            {
                return value;
            }
            else if (Type == Token_Type.BOOLEAN)
            {
                return bool.Parse(value);
            }
             return null;
        }

    }

    public class GroupingASTNode : ASTnode
    {
        public ASTnode groupnode { get; }
        public GroupingASTNode(ASTnode groupnode)
        {
            this.groupnode = groupnode; 
        }

        public override object Evaluar(Context context, Targets targets)
        {
            return groupnode.Evaluar( context,  targets);
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

    public class SelectorCardNode : ASTnode
    {
        public string Source { get; set;}
        public bool Single { get; set;}

        public ASTnode Predicate { get; set; }
        public SelectorCardNode()
        {
            Single = false;
        }
    }

    public class LambdaForAction  :ASTnode
    {
        public List<ASTnode> left { get; set; }
        public List<ASTnode> right { get; set; }

        public LambdaForAction(List<ASTnode> left, List<ASTnode> right)
        {
            this.left = left;
            this.right = right;
        }
    }
    public class LambdaASTNode : ASTnode 
    {
        Token_Type type = Token_Type.LAMBDA;
        public ASTnode Left { get; set; }
        public ASTnode Right { get; set; }
        public LambdaASTNode(ASTnode left,ASTnode right)
        {
                Left = left;    
                Right = right;
        }

        public override object Evaluar(Context context, Targets targets)
        {
           var left = this.Left.Evaluar( context, targets);  
           var right = this.Right.Evaluar( context,  targets);

            if (left is bool && right is Func<bool>)
            {
                return new Func<bool>(() => (bool)left && ((Func<bool>)right)());
            }
            else throw new InvalidOperationException("Invalid types for lambda evaluation");
        }

    }

    public static class Ayudante
    {
        public static bool IsNumber(params object[] myobjects)
        {
            foreach (var obejects in myobjects) 
            {
                if(obejects is not double) return false;    
            }
            return true;
        }

        public static bool IsBoolean(params object[] myobjects)
        {
            foreach (var objects in myobjects)
            {
                if (objects is not Boolean)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsString(params object[] myobejects)
        {
            foreach (var objects in myobejects)
            {
                if (objects is not string) return false;
            }
            return true;
        }

        public static bool IsEqual(object a, object b)
        {
            if (a == null && b == null) return true;

            return a == null ? false : a.Equals(b);
        }

        public static object EvaluateBinary(object left, Token_Type type, object right)
        {
            switch (type)
            {
                case Token_Type.PLUS:
                    if (IsNumber(left, right)) return (double)left + (double)right;
                    throw new InvalidOperationException("Invalid types for plus evaluation");
                case Token_Type.MINUS:
                    if (IsNumber(left, right)) return (double)left - (double)right;
                    throw new InvalidOperationException("Invalid types for minus evaluation");
                case Token_Type.MULTIPLY:
                    if (IsNumber(left, right)) return (double)left * (double)right;
                    throw new InvalidOperationException("Invalid types for multlipy evaluation");
                case Token_Type.DIVIDE:
                    if (IsNumber(left, right)) return (double)left / (double)right;
                    throw new InvalidOperationException("Invalid types for divide evaluation");
                case Token_Type.MODULUS:
                    if (IsNumber(left, right)) return (double)left % (double)right;
                    throw new InvalidOperationException("Invalid types for modulus evaluation");
                case Token_Type.POWER:
                    if (IsNumber(left, right))
                    {
                        double salida = 1;
                        for (int i = 0; i < (double)right; i++)
                        {
                            salida *= (double)left;
                        }
                        return salida;
                    }
                    throw new InvalidOperationException("Invalid types for power evaluation");

                case Token_Type.GREATER:
                    if (IsNumber(left, right)) return (double)left > (double)right;
                    throw new InvalidOperationException("Invalid types for greater evaluation");
                case Token_Type.LESS:
                    if (IsNumber(left, right)) return (double)left < (double)right;
                    throw new InvalidOperationException("Invalid types for less evaluation");
                case Token_Type.LESS_EQUAL:
                    if (IsNumber(left, right)) return (double)left <= (double)right;
                    throw new InvalidOperationException("Invalid types for less_equal evaluation");
                case Token_Type.GREATER_EQUAL:
                    if (IsNumber(left, right)) return (double)left >= (double)right;
                    throw new InvalidOperationException("Invalid types for greater_equal evaluation");

                case Token_Type.AND:
                    if (IsBoolean(left, right)) return (bool)left && (bool)right;
                    throw new InvalidOperationException("Invalid types for And evaluation");
                case Token_Type.OR:
                    if (IsBoolean(left, right)) return (bool)left || (bool)right;
                    throw new InvalidOperationException("Invalid types for Or evaluation");

                case Token_Type.EQUAL:
                    return IsEqual(left, right);
                case Token_Type.NOT_EQUAL:
                    return !IsEqual(left, right);

                case Token_Type.CONCAT:
                    return left.ToString() + right.ToString();

                default: return null;
            }
        }

     
        public static object EvaluateUnary(string value, object son) 
        {
            switch (value) 
            {
                case "!":
                    if (IsBoolean(son)) return !(bool)son;
                    break;
                case "++":
                    if(IsNumber(son)) return (double)son + 1;
                    break;
                case "--":
                    if (IsNumber(son)) return (double)son - 1;
                    break;
                default: return null;
            }
            return null;
        }

    }

}
