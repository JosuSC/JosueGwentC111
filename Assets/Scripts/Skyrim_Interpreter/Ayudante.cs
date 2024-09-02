using System.Runtime.InteropServices.ComTypes;
using System.IO.Enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.AccessControl;

namespace Skyrim_Interpreter
{
    public static class Ayudante
    {
        public static bool IsNumber(params object[] myobjects)
        {
            foreach (var obejects in myobjects)
            {
                if (obejects is not double) return false;
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
                    if (IsNumber(son)) return (double)son + 1;
                    break;
                case "--":
                    if (IsNumber(son)) return (double)son - 1;
                    break;
                default: return null;
            }
            return null;
        }

        public static object PerformOperation(object left,object right,Func<double,double,double> operation)
        {
           if(left is IdentifierASTNode identLeft)
           {

           }
            throw new InvalidOperationException();
        }

        public static bool CheckRange(List<string> range)
        {
            foreach (var node in range) 
            {
                if (node != "Melee" && node != "Ranged" && node != "Siege") { return false; }
            }
            return true;
        }
        public static bool CheckSource(string source) 
        {
            List<string> list =new List<string> {"board", "hand", "otherHand", "deck", "othreDeck","field","otherField","parent" };
            foreach (var item in list) 
            {
                if(source == item) return true; 
            }
            return false;
        }
        public static object ReturnList(IdentifierASTNode i1,IdentifierASTNode i2,Context context) 
        {
            if (i1.value == "context") 
            {
                switch (i2.value) 
                {
                    case "Hand":
                        return context.Hand;
                    case "Deck":
                        return context.Deck;
                    case "Field":
                        return context.Field;
                    case "Graveyarad":
                        return context.Graveyard;
                    case "Board":
                        return context.Board;
                    default:
                       throw new InvalidOperationException("Invalid access in context");   
                }
            }
            return null;
        }

        public static object ReturnChangeAux(List<Cards> cards, string method,ASTnode param,Context context,Targets target)
        {
            var evaluation = param.Evaluar(context,target);
            if (evaluation is CardASTNode card) 
            {
                if (method != "Push" && method != "SendBottom" && method != "Remove" && method != "Add") { throw new InvalidOperationException("Invaliod method for card"); }
                Cards thecard = FindCard(target,card);
                if (thecard == null) { throw new Exception("No se encontro la carta buscanda"); }
                return ApplyToCard(cards,method,thecard);
            }
            else if (evaluation is Predicate<Cards> predicate) 
            {
                return ApplyToPredicate( cards,predicate);
            }
            return null;
        }

        public static Cards FindCard(Targets targets,CardASTNode card)
        {
            foreach (var target in targets.targets) 
            {
                if (target.Name == card.Name && target.Faction == card.Faction && target.Power == card.Power) return target;
            }
            return null; 
        }
        private static List<Cards> ApplyToPredicate(List<Cards> cards,Predicate<Cards> predicate) 
        {
            return cards.Where(card => predicate(card)).ToList();
        }

         private static object ApplyToCard(List<Cards> cards,string method,Cards actualcard)
         {
            switch (method) 
            {
                case "Push":
                     cards.Insert(0,actualcard);
                    return true;
                case "SendBottom":
                    cards.Insert(cards.Count-1,actualcard);
                    return true;
                case "Remove":
                    RemoveCard(cards,actualcard);
                    return true;
                case "Add":
                    cards.Add(actualcard);
                    return true;
                    default: return false;  
            }
         }

        private static void RemoveCard(List<Cards> cards,Cards card) 
        {
            foreach (Cards actualcard in cards) 
            {
                if (actualcard.Equals(card)) 
                {
                    cards.Remove(actualcard);
                }
            }
        }
    }
} 
