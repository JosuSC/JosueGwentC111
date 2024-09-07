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
        public Targets Targets { get; set; }    
        public Context Context { get; set; }    
        public string destination { get; set; }

        public List<ASTnode> Parameters { get; set;}
        public ActionASTNode Action { get; set; }

        public Selector selector { get; set; }
        public Effect()
        {
            Parameters = new List<ASTnode>();
        }
        public void DoIt(Targets targets,Context context) 
        {

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
