using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyrim_Interpreter
{
    public  class AssignmentManager
    {
        public static Dictionary<string, object> Assignment { get; set; }
        public static Dictionary<string, object> Parameters { get; set; }

        public AssignmentManager()
        {
            Assignment = new Dictionary<string, object>();
            Parameters = new Dictionary<string, object>();
        }

        public static bool Search(Dictionary<string,object> dictionary,string key) 
        {
            foreach (var item in dictionary) 
            {
                if (item.Key == key) return true;
            }
            return false;   
        }

        public static void Actualizar(Dictionary<string,object> dictionary,string key,object newvalue) 
        {
            dictionary[key] = newvalue; 
        }
        public static void Addy(Dictionary<string,object> dictionary,string newkey,object newvalue) 
        {
            dictionary.Add(newkey,newvalue);
        }
    }
}
