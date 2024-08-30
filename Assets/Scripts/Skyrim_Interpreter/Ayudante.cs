using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    Console.WriteLine("Hola otra vez");
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

        public static void CheckAccess(IdentifierASTNode i1,IdentifierASTNode i2) 
        {
            if (i1.value == "context")
            {
                if (i2.value == "Hand") {AccessASTNode.Property = "Hand";}
                else if (i2.value == "Deck") { AccessASTNode.Property = "Deck"; }
                else if (i2.value == "Board") { AccessASTNode.Property = "Board"; }
                else if (i2.value == "Graveyard") { AccessASTNode.Property = "Graveyard"; }
                else if (i2.value == "Field") { AccessASTNode.Property = "Field"; }
            }
        }

        public static void CheckAccess2(IdentifierASTNode i3,string property) 
        {
            Context context = new Context();
            switch (property)
            {
                case "Hand":
                    if (i3.value == "Shuffle") context.Hand.Shuffle();
                    else if (i3.value == "Pop") context.Hand.Pop();
                    else if (i3.value == "Remove") context.Hand.Remove();
                    else if (i3.value == "Push") context.Hand.Push();
                    break;

            }
        }

    }
} 
