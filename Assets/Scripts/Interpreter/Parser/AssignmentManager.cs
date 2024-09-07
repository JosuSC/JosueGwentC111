using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Skyrim_Interpreter
{
    public  class GameContext
    {
        public static Dictionary<string, object> Assignment { get; set; } = new Dictionary<string, object>();
        public static Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public static Dictionary<string, Effect> EffectAssignmet { get; set; } = new Dictionary<string, Effect>();
        public static Dictionary<string, Cards> Cards { get; set; } = new Dictionary<string, Cards>();
        public static bool Search(Dictionary<string,object> dictionary,string key) 
        {
         if(dictionary.ContainsKey(key))return true;
            return false;   
        }
        public static void InputKeyParameter(ASTnode left ,ASTnode right) 
        {
            //si el left es un idebtificador
            if (left is IdentifierASTNode ident)
            {
              //si no se encuentra ese identificador en el dictionary
                if (!Search(Parameters, ident.value))
                {
                    //si el derecho es un identifier 
                    if (right is IdentifierASTNode ide)
                    {
                        if (ide.value == "Number") Parameters.Add(ident.value , 0);
                        else if (ide.value == "String") Parameters.Add(ident.value,"");
                        else if (ide.value == "Boolean") Parameters.Add(ident.value, false);
                    }
                    else
                    {
                        object value = right.Evaluar();
                       Parameters.Add(ident.value, value);
                    }
                }
                else 
                {
                    //commprobamos si el actual y el que ya estaba son del mismo tipo
                    object v1 = Parameters[ident.value];
                    object v2 = right.Evaluar();
                    if (v1.GetType() == v2.GetType()) { Parameters[ident.value] = v2;}
                    else { throw new InvalidOperationException("Deben ser los mismo tipos"); }
                }
            }
            else 
            {
                throw new InvalidOperationException("No se puede asignar un valor a algo que no sea una variable");
            }
        }
        public static void InputKeyAssign(ASTnode left,ASTnode right) 
        {
            if (left is IdentifierASTNode ident)
            {
                var value = right.Evaluar();
                Type obj = value.GetType();
                Console.WriteLine(obj);
                if (!Search(Parameters, ident.value))
                {
                    Assignment.Add(ident.value,value);
                }
                else
                {
                    Assignment[ident.value] = value;   
                } 
            }
            else
            {
                throw new InvalidOperationException("No se puede asignar un valor a algo que no sea una variable");
            }
        }

        public static void InputAssignmentwithValue(ASTnode left,ASTnode right,string oparator) 
        {
            if (left is IdentifierASTNode ident)
            {
                object v1 = right.Evaluar();

                if (!Search(Assignment, ident.value))
                {

                }
                else 
                {
                    object v2 = Assignment[ident.value];
                    if (v1.GetType() == v2.GetType()) 
                    {
                       
                    }
                }
            }
            else 
            {
                throw new InvalidOperationException("Solo se pude asignar a una variable");  
            }
        }
        public static void InputEffcet(string name,Effect effect) 
        {
            //comprobamos si existe el efecto
            if (EffectAssignmet.ContainsKey(name))
            {
               
            }
            else 
            {
                EffectAssignmet.Add(name, effect);  
            }
        }

        public static bool IsContainsEffcet(string name) 
        {
            if (EffectAssignmet.ContainsKey(name)) return true;
            return false;   
        }

        public static bool IsContainsAssignment(string name) 
        {
            if (Assignment.ContainsKey(name)) return true;
            return false;
        }
    }
}
