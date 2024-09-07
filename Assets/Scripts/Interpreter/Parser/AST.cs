using Skyrim_Interpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Skyrim_Interpreter
{
    public abstract class ASTnode 
    {
        public abstract object Evaluar(Context context, Targets targets);
        public abstract object Evaluar();
    }
    public class ASTnodeTree : ASTnode
    {
      public List<ASTnode> children;
        public ASTnodeTree()
        {
            children= new List<ASTnode>();
        }
        public override object Evaluar(Context context, Targets targets)
        {
            var results = new List<object>();
            foreach (var child in children)
            {
                try
                {
                    var result = child.Evaluar(context, targets);
                    results.Add(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error evaluating child node {child}: {ex.Message}");
                    throw;
                }
            }
            return results;
        }
        public override object Evaluar()
        {
            throw new NotImplementedException();
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

        public override object Evaluar() 
        {
            var left = LeftChild.Evaluar();
            var right = RightChild.Evaluar();
            return Ayudante.EvaluateBinary(left,this.type,right);
        }

        public override object Evaluar(Context context, Targets targets)
        {
            throw new NotImplementedException();
        }
    }
    // Variables
    public class IdentifierASTNode : ASTnode
    {
        public Token_Type type { get; set; }
        public string value { get; set; }

        public ASTnode Parameters { get; set;}
        public IdentifierASTNode(Token_Type type, string value) 
        {
            this.type = type;   
            this.value = value;
        }
        public override object Evaluar() { return value; }
        public override object Evaluar(Context context, Targets targets)
        {
            return null;
        }
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

        public override object Evaluar()
        {
            var left = LeftChild.Evaluar();
            var right = RightChild.Evaluar();
            return Ayudante.EvaluateBinary(left, this.type, right);
        }
        public override object Evaluar(Context context, Targets targets) { return null; }
        
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
        public override object Evaluar()
        {
            return null;
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

        public override object Evaluar()
        {
            var left = Number.Evaluar();
            var right = Pow.Evaluar();
            return Ayudante.EvaluateBinary(left, this.type, right);
        }
        public override object Evaluar(Context context, Targets targets) { throw new NotImplementedException(); }
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
        public override object Evaluar()
        {
            var left = this.left.Evaluar();
            var right = this.right.Evaluar();
            return Ayudante.EvaluateBinary(left, this.type, right);
        }

        public override object Evaluar(Context context, Targets targets) { return null; }
        
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

        public override object Evaluar()
        {
            var left = this.left.Evaluar();
            var right = this.right.Evaluar();
            return Ayudante.EvaluateBinary(left, this.type, right);
        }
        public override object Evaluar(Context context, Targets targets) { throw new NotImplementedException(); }
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

        public override object Evaluar() 
        {
            var left = this.Left.Evaluar( );
            var right = this.Right.Evaluar( );
            return Ayudante.EvaluateBinary(left,this.type,right);
        }
        public override object Evaluar(Context context, Targets targets) { throw new NotImplementedException(); }
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

        public override object Evaluar() 
        {
            var left = this.Left.Evaluar();
            var right = this.Right.Evaluar();
            return Ayudante.EvaluateBinary(left,this.type,right);
        }
        public override object Evaluar(Context context, Targets targets) { throw new NotImplementedException(); }
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
            if (left is IdentifierASTNode ident) 
            {
               ident.value = right.ToString(); 
               return right;
            }
            throw new InvalidOperationException("Invalid types for assignement");
        }
        public override object Evaluar() { throw new NotImplementedException(); }
    }

    public class AssgnWithValueASTNode : ASTnode
    {
        string value { get; set; }  
        ASTnode left { get; set; }
        public ASTnode right { get; set; }
        public AssgnWithValueASTNode( ASTnode left, string value, ASTnode right)
        {
            this.value = value;
            this.left = left;
            this.right = right;
        }
        public override object Evaluar(Context context, Targets targets)
        {
            throw new NotImplementedException();
        }
        public override object Evaluar() { return null; }
    }

    public class UnaryASTNode : ASTnode
    {
        public string value { get; set; }
        public ASTnode Son { get; set; }

        public UnaryASTNode(string value, ASTnode son)
        {
            this.value = value; 
            Son = son;
        }
        public override object Evaluar(Context context, Targets targets)
        {
            var son = this.Son.Evaluar( context, targets);
            return Ayudante.EvaluateUnary(this.value,son);
        }
        public override object Evaluar() { return null; }
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

        public override object Evaluar()
        {
           var left = this.left.Evaluar( );
            var right = this.right.Evaluar();
            if (left is IdentifierASTNode identifier) 
            {
               
            }
            throw new InvalidOperationException("Invalid types for colon");
        }
        public override object Evaluar(Context context, Targets targets) { throw new NotImplementedException(); }
    }

    public class Params : ASTnode
    {
        public List<ASTnode> param { get; set; }
        public Params() => param = new List<ASTnode>();
        public override object Evaluar()
        {
            var results = new List<object>();
            for (int i = 0; i < param.Count; i++)
            {
                var item = param[i].Evaluar();
                results.Add(item);  
            }
            return results;
        }
        public override object Evaluar(Context context, Targets targets) { throw new NotImplementedException(); }
    }

    public class ConditionalASTNode : ASTnode
    {
        public ASTnode condicion { get; set; }
        public override object Evaluar()
        {
            var condtion = this.condicion.Evaluar();
            if (condtion is bool) { return (bool)condtion; }
            else { throw new InvalidOperationException("Invalid type for condition evalue"); }
        }
        public override object Evaluar(Context context, Targets targets) { throw new NotImplementedException(); }
    }

    public class BlockASTNode : ASTnode
    {
        public Params Block { get; set;}
        public BlockASTNode()
        {
            Block= new Params();    
        }
        public override object Evaluar(Context context, Targets targets)
        {
            var results = new List<object>();
            foreach (var item in Block.param)
            {
                try
                {
                    var result = item.Evaluar(context, targets);
                    results.Add(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error evaluating block parameter {item}: {ex.Message}");
                    throw;
                }
            }
            return results;
        }
        public override object Evaluar()
        {
            return null;
        }
    }

    public class CommaASTNode : ASTnode 
    {
        public Token_Type type = Token_Type.COMMA;
        public string value = ",";
        public override object Evaluar() { return value; }
        public override object Evaluar(Context context, Targets targets) { throw new NotImplementedException(); }
    }

    public class AccessASTNode : ASTnode
    {
        public ASTnode left { get; set; }
        public ASTnode right { get; set; }

        public string HI { get; set; }
        public string HD { get; set; }

        public AccessASTNode(ASTnode left, ASTnode right)
        {
            this.left = left;
            this.right = right;
        }
        //ejemplos a evaluar context.hand.power.
        public override object Evaluar(Context context, Targets targets)
        {
            var left = this.left.Evaluar(context, targets);
            var right = this.right.Evaluar(context, targets);

            if (left is IdentifierASTNode identifier1 && right is IdentifierASTNode identifier2 && identifier1.value == "context")
            {
                Ayudante.ReturnList(identifier1, identifier2, context);
            }
            if (left is List<Cards> a && right is IdentifierASTNode ide)
            {
                List<Cards> newlist = a;
                return Ayudante.ReturnChangeAux(newlist, ide.value, ide.Parameters, context, targets);
            }
            throw new NotImplementedException();
        }
         public override object Evaluar() { return null; }
    }
    public class ActionASTNode : ASTnode
    {
        public string Target { get; set; }
        public string context { get; set; }
        public List<ASTnode> actions { get; set; }
        
        public LambdaForAction Lambda {get; set;}
        public ActionASTNode()
        {
             
            actions= new List<ASTnode>();   
        }

        public override object Evaluar(Context context, Targets targets)
        {
            throw new NotImplementedException();
        }
        public override object Evaluar() { throw new NotImplementedException(); }
    }
    //*********************************************************************
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
        public override object Evaluar()
        {
           Effect neweffect = new Effect();
            if (this.Name != null) { neweffect.Name = Name; }
            else { throw new InvalidOperationException("The name for effect is null"); }
            if (Params != null) 
            { 
                neweffect.Parameters = this.Params.param;
                Ayudante.MakeParameters(this.Params.param);
            }
            neweffect.Action = Action;
            GameContext.EffectAssignmet.Add(neweffect.Name,neweffect);
            return true; 
        }
        public override object Evaluar(Context context, Targets targets) { return Evaluar(); }
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
        public override object Evaluar()
        {
            var left = this.left.Evaluar(); 
            var right = this.right.Evaluar();
            return Ayudante.EvaluateBinary(left,this.type,right);
        }
        public override object Evaluar(Context context, Targets targets) { return Evaluar(); }
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
        public override object Evaluar()
        {
           var left = this.left.Evaluar();
           var right = this.right.Evaluar();
            return Ayudante.EvaluateBinary(left,this.type,right); 
        }
        public override object Evaluar(Context context, Targets targets) { return Evaluar(); }
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
        public override object Evaluar()
        {
            var left = leftchild.Evaluar(); 
            var right = rightchild.Evaluar();
            return Ayudante.EvaluateBinary(left,this.type,right);
        }
        public override object Evaluar(Context context,Targets targets) { return Evaluar(); }
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

        public override object Evaluar() 
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
        public override object Evaluar(Context context, Targets targets) { return Evaluar(); }
    }

    public class GroupingASTNode : ASTnode
    {
        public ASTnode groupnode { get; }
        public GroupingASTNode(ASTnode groupnode)
        {
            this.groupnode = groupnode; 
        }

        public override object Evaluar()
        {
            return groupnode.Evaluar();
        }
        public override object Evaluar(Context context, Targets targets) { return Evaluar();}   
    }
    public class WhileASTNode  : ASTnode
    {
        public ASTnode condition { get; set; }
        public BlockASTNode block { get; set;}
        public WhileASTNode(ASTnode condition,BlockASTNode block)
        {
            this.condition = condition; 
            this.block = block; 
        }
        public override object Evaluar(Context context,Targets targets) 
        {
            bool continueLoop = true;
            while (continueLoop)
            {
                var conditionResult = condition.Evaluar(context, targets);
                if (conditionResult is bool)
                {
                    continueLoop = (bool)conditionResult;
                }
                else
                {
                    throw new ArgumentException("Condition must evaluate to a boolean value");
                }
                if (continueLoop)
                {
                    foreach (var item in block.Block.param)
                    {
                        item.Evaluar(context, targets);
                    }
                }
            }
            return null;
        }
        public override object Evaluar(){return null;}
    }

    public class ForASTNode : ASTnode
    {
        public IdentifierASTNode colection { get; set; }
        public BlockASTNode block { get; set; }
        public ForASTNode(BlockASTNode block,IdentifierASTNode colection)
        {
            this.block = block; 
            this.colection = colection; 
        }
        public override object Evaluar(Context context, Targets targets)
        {
          var results = new List<object>();
          var loopVariable = new Dictionary<string, object>();

        // Inicializar la variable de bucle
        foreach (var param in block.Block.param)
        {
            var result = param.Evaluar(context, targets);
            if (result is IdentifierASTNode identifier)
            {
                loopVariable[identifier.value] = result;
            }
            else
            {
                throw new InvalidOperationException("Inicialización de bucle inválida");
            }
        }

        // Ejecutar el cuerpo del bucle
        while (true)
        {
            // Ejecutar el cuerpo del bucle
            foreach (var item in block.Block.param.Skip(1))
            {
                var result = item.Evaluar(context, targets);
                if (result is IdentifierASTNode identifier)
                {
                    loopVariable[identifier.value] = result;
                }
                else
                {
                    throw new InvalidOperationException("Cuerpo del bucle inválido");
                }
            }
            // Verificar si debemos continuar el bucle
            var condition = block.Block.param.FirstOrDefault(p => p is ConditionalASTNode);
            if (condition != null)
            {
                var conditionResult = ((ConditionalASTNode)condition).Evaluar(context, targets);
                if (!(conditionResult is bool))
                {
                    throw new InvalidOperationException("Condición de bucle inválida");
                }
                if (!(bool)conditionResult)
                {
                    break; // Salir del bucle
                }
            }
            else
            {
                // Si no hay condición, asumimos que el bucle debe continuar
                continue;
            }

            // Incrementar la variable de bucle
            foreach (var param in block.Block.param)
            {
                if (param is IdentifierASTNode identifier)
                {
                    var currentValue = loopVariable[identifier.value];
                    if (currentValue is int)
                    {
                        loopVariable[identifier.value] = (int)currentValue + 1;
                    }
                    else if (currentValue is float)
                    {
                        loopVariable[identifier.value] = (float)currentValue + 1;
                    }
                    else
                    {
                        throw new InvalidOperationException("Tipo de variable de bucle no soportado");
                    }
                }
            }
        }
          return results;
        }
        public override object Evaluar(){return null;}
    }
   //*******************************************************************************
    public class CardASTNode  : ASTnode
    {
        public string Name { get; set; }    
        public string Type { get; set; }
        public string Faction { get; set;}
        public int Power { get; set;}
        public List<string> Range { get; set;}
        public List<ASTnode> OnActivation { get; set; }
        public CardASTNode()
        {
                Range = new List<string>();    
            OnActivation = new List<ASTnode>(); 
        }
        public override object Evaluar()
        {
            Cards newcard = new Cards();
            if (Name != null) 
            {
                newcard.Name = Name; 
            }
            if (Faction != null) { newcard.Faction = Faction; }
            if (Type != null) { newcard.Type = Type; }
            if (Range.Count != 0)
            {
                if (Ayudante.CheckRange(this.Range)) { newcard.Range = this.Range.ToArray(); }
                else { throw new InvalidOperationException("Invalid types for range of card evaluation"); }
            }
            else { throw new InvalidOperationException("The range for the card is empty"); }
            if (Power != null)
            {
                if ((newcard.Type == "Clima" || newcard.Type == "Aumento") && this.Power != 0) { throw new InvalidOperationException("Ivalid power for this card "); }
                newcard.Power = Power;
            }
            else { throw new InvalidOperationException("The power card is empty"); }
            if (this.OnActivation.Count != 0)
            {
                newcard.OnActivation = OnActivation;
            }
            else 
            {
                throw new InvalidOperationException("This card don't have any Effcet");
            }
            GameContext.Cards.Add(this.Name,newcard);
            return true; 
        }
        public override object Evaluar(Context context ,Targets targets)
        {
            return Evaluar();
        }
    }
    public class EffectCardNode : ASTnode
    {
        public string Name { get; set; }    
        public List<ASTnode> Parameters { get; set; }

        public SelectorCardNode Selector { get; set; }
        public EffectCardNode()
        {
            Parameters = new List<ASTnode>();   
        }
        public override object Evaluar(Context context, Targets targets)
        {
           EffectDef neweffect = new EffectDef();
            if (Name != null) { neweffect.Name = this.Name;}
            if (this.Parameters.Count != 0) 
            {

            }
            return neweffect;
        }

        public override object Evaluar()
        {
            throw new NotImplementedException();
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

        public override object Evaluar(Context context, Targets targets)
        {
           Selector newselector = new Selector();
            if (Source != null && Ayudante.CheckSource(Source)) 
            {
                newselector.Source = Source;
            }
            else 
            {
                throw new InvalidOperationException("The source is empty or is invalid");
            }
            newselector.Single = Single;
            var predicate = this.Predicate.Evaluar(context,targets);
            
            return newselector;
        }
        public override object Evaluar()
        {
            return null;
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
        public override object Evaluar(Context context, Targets targets)
        {
            return null;
        }

        public override object Evaluar() 
        {
            return null;
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
            var left = Left.Evaluar(context, targets);
            var right = Right.Evaluar(context, targets);

            // Asumimos que el lado izquierdo es una condición booleana
            // y el lado derecho es una función que devuelve un Predicate<Cards>
            if (left is bool condition && right is Func<Predicate<Cards>> func)
            {
                return new Func<Cards, bool>((card) =>
                {
                    return condition && func()(card);
                });
            }
            else
            {
                throw new InvalidOperationException("Invalid types for lambda evaluation");
            }
        }
        public override object Evaluar()
        {
            return null;
        }

    }

}

