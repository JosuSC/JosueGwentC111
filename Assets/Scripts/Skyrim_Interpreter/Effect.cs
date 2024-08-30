using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyrim_Interpreter
{
    public class Effect
    {
        public string Name { get; set; }
        public List<object> Params { get; set; }
        public Selector selector { get; set; }
        public Effect()
        {
            Params= new List<object>(); 
        }
    }

    public class Selector 
    {
        public string Source { get; set;}
        public bool Single { get; set; }
        public  Predicate<Cards> predicate { get; set;}

        public bool SelectCard(Cards card) 
        {
            return predicate(card);
        }
    }
}
