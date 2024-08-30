using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skyrim_Interpreter
{
    public class GenerateCode 
    {
        private List<ASTnode> mynodes;
        public List<Cards> cards { get; set;}
        public List<Effect> effects { get; set;}

        public GenerateCode(ASTnodeTree tree)
        {
            mynodes = tree.children;
            cards = new List<Cards>();
            effects = new List<Effect>();
        }

        public void ProcesingNodes(List<ASTnode> nodes) 
        {

        }

    }
}
