using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyrim_Interpreter
{
    public class Context
    {
        public Guid TriggerPlayer { get; set; }
        public List<CardASTNode> Board { get; set; }
        public Context(Guid id, List<CardASTNode> board)
        {
            this.TriggerPlayer = id;
            this.Board = board;
        }
        public Hand HandOfPlayer(Guid id) 
        {
            Player player = PlayerID(id);
            return player.hand;
        }

        public Deck DeckOfPlayer(Guid id) 
        {
            Player player = PlayerID(id);
            return player.deck;
        }

        public Graveyard GraveyardOfPlayer(Guid id) 
        {
            Player player = PlayerID(id);
            return player.graveyard;
        }

         public Field FieldOfPlayer(Guid id) 
         {
            Player player = PlayerID(id);
            return player.field;
         }

        public Deck Deck
        {
            get { return DeckOfPlayer(TriggerPlayer); }
        }
        public Hand Hand
        {
            get { return HandOfPlayer(TriggerPlayer); }
        }
        public Field Field
        {
            get { return FieldOfPlayer(TriggerPlayer); }
        }
        public Graveyard Graveyard
        {
            get { return GraveyardOfPlayer(TriggerPlayer); }
        }
        public Player PlayerID(Guid id)
        {
          return Game.PlayerID(id);
        }
    }
    public class Targets 
    {
        public List<object> targets { get; set; }
        public Targets()
        {
            targets= new List<object>();    
        }
    }
}
