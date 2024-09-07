using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Skyrim_Interpreter
{
    public interface ICard
    {
        public List<Cards> Find(Predicate<Cards> predicate);
        public List<Cards> cards { get; set; }
        public void Push(Cards card);
        public Cards Pop();
        public void SendBottom(Cards card);
        public void Remove(Cards card);
        public void Shuffle();
    }

    public class Cards 
    {
        public string Name { get; set; }
        public string Faction { get; set;}
        public string Type { get; set; }    
        public int Power { get; set; } 
        public string[] Range { get; set; }
        public List<ASTnode> OnActivation { get; set; }
        public Player player { get; set; }
        public Cards() 
        {
            OnActivation = new List<ASTnode>();  
            Range = new string[0];
        }

        public  Guid Owner() 
        {
            return player.ID;
        }
    }

    public class EffectDef
    {
        public string Name { get; set;}
       List<string> Parameters { get; set; }
        public EffectDef()
        {
            Parameters= new List<string>(); 
        }
    }

    public class Player
    {
        public Guid ID { get; set; }
        public Deck deck { get; set; }
        public Hand hand { get; set; }
        public Graveyard graveyard { get; set; }
        public Field field { get; set; }

        public Player(Guid id)
        {
                this.ID = id;   
            deck = new Deck();
            hand= new Hand();
            graveyard= new Graveyard();
            field = new Field();
        }
    }

    public class Hand : ICard
    {
        public List<Cards> cards { get; set; }
        public Hand()
        {
            cards = new List<Cards>(); 
        }

        public List<Cards> Find(Predicate<Cards> predicate) 
        {
            List<Cards> list = new List<Cards>();
            foreach (var item in cards)
            {
                if (predicate(item)) { list.Add(item); }
            }
          return list;  
        }

        public void Push(Cards card) 
        {
            cards.Insert(0,card);    
        }

        public Cards Pop() 
        {
            Cards card = cards.First();
            cards.RemoveAt(0);
            return card;
        }

        public void SendBottom(Cards card) 
        {
            cards.Add(card);    
        }

        public void Remove(Cards card) 
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Equals(card)) 
                {
                    cards.RemoveAt(i);
                }
            }
        }

        public void Shuffle() 
        {
            Cards[] newcards = new Cards[cards.Count];
            Random random= new Random();
            List<int> indexT= new List<int>();
            List<int> indexP = new List<int>(); 
            int count = 0;
            Cards card = null;
            while (count < cards.Count) 
            {
                int aleatory = random.Next(0, cards.Count);
                while (indexT.Contains(aleatory)) 
                {
                    aleatory = random.Next(0, cards.Count);
                }
                card = cards[aleatory]; 
                indexT.Add(aleatory);   
                aleatory = random.Next(0, cards.Count);
                while (indexP.Contains(aleatory)) 
                {
                    aleatory = random.Next(0, cards.Count);
                }
                newcards[aleatory] = card;
                indexP.Add(aleatory);   
                count++;
            }
            cards = newcards.ToList<Cards>();
        }

    }

    public class Graveyard : ICard
    {
        public List<Cards> cards { get; set; }
        public Graveyard()
        {
            cards = new List<Cards>();
        }
        public List<Cards> Find(Predicate<Cards> predicate)
        {
            List<Cards> list = new List<Cards>();
            foreach (var item in cards)
            {
                if (predicate(item)) { list.Add(item); }
            }
            return list;
        }

        public void Push(Cards card)
        {
            cards.Insert(0, card);
        }

        public Cards Pop()
        {
            Cards card = cards.First();
            cards.RemoveAt(0);
            return card;
        }

        public void SendBottom(Cards card)
        {
            cards.Add(card);
        }

        public void Remove(Cards card)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Equals(card))
                {
                    cards.RemoveAt(i);
                }
            }
        }

        public void Shuffle()
        {
            Cards[] newcards = new Cards[cards.Count];
            Random random = new Random();
            List<int> indexT = new List<int>();
            List<int> indexP = new List<int>();
            int count = 0;
            Cards card = null;
            while (count < cards.Count)
            {
                int aleatory = random.Next(0, cards.Count);
                while (indexT.Contains(aleatory))
                {
                    aleatory = random.Next(0, cards.Count);
                }
                card = cards[aleatory];
                indexT.Add(aleatory);
                aleatory = random.Next(0, cards.Count);
                while (indexP.Contains(aleatory))
                {
                    aleatory = random.Next(0, cards.Count);
                }
                newcards[aleatory] = card;
                indexP.Add(aleatory);
                count++;
            }
            cards = newcards.ToList<Cards>();
        }


    }

    public class Field : ICard
    {
        public List<Cards> cards { get; set; }
        public Field()
        {
            cards = new List<Cards>();
        }

        public List<Cards> Find(Predicate<Cards> predicate)
        {
            List<Cards> list = new List<Cards>();
            foreach (var item in cards)
            {
                if (predicate(item)) { list.Add(item); }
            }
            return list;
        }

        public void Push(Cards card)
        {
            cards.Insert(0, card);
        }

        public Cards Pop()
        {
            Cards card = cards.First();
            cards.RemoveAt(0);
            return card;
        }

        public void SendBottom(Cards card)
        {
            cards.Add(card);
        }

        public void Remove(Cards card)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Equals(card))
                {
                    cards.RemoveAt(i);
                }
            }
        }

        public void Shuffle()
        {
            Cards[] newcards = new Cards[cards.Count];
            Random random = new Random();
            List<int> indexT = new List<int>();
            List<int> indexP = new List<int>();
            int count = 0;
            Cards card = null;
            while (count < cards.Count)
            {
                int aleatory = random.Next(0, cards.Count);
                while (indexT.Contains(aleatory))
                {
                    aleatory = random.Next(0, cards.Count);
                }
                card = cards[aleatory];
                indexT.Add(aleatory);
                aleatory = random.Next(0, cards.Count);
                while (indexP.Contains(aleatory))
                {
                    aleatory = random.Next(0, cards.Count);
                }
                newcards[aleatory] = card;
                indexP.Add(aleatory);
                count++;
            }
            cards = newcards.ToList<Cards>();
        }
    }

    public class Deck : ICard
    {
        public List<Cards> cards { get; set; }
        public Deck()
        {
            cards = new List<Cards>();
        }

        public List<Cards> Find(Predicate<Cards> predicate)
        {
            List<Cards> list = new List<Cards>();
            foreach (var item in cards)
            {
                if (predicate(item)) { list.Add(item); }
            }
            return list;
        }

        public void Push(Cards card)
        {
            cards.Insert(0, card);
        }

        public Cards Pop()
        {
            Cards card = cards.First();
            cards.RemoveAt(0);
            return card;
        }

        public void SendBottom(Cards card)
        {
            cards.Add(card);
        }

        public void Remove(Cards card)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Equals(card))
                {
                    cards.RemoveAt(i);
                }
            }
        }

        public void Shuffle()
        {
            Cards[] newcards = new Cards[cards.Count];
            Random random = new Random();
            List<int> indexT = new List<int>();
            List<int> indexP = new List<int>();
            int count = 0;
            Cards card = null;
            while (count < cards.Count)
            {
                int aleatory = random.Next(0, cards.Count);
                while (indexT.Contains(aleatory))
                {
                    aleatory = random.Next(0, cards.Count);
                }
                card = cards[aleatory];
                indexT.Add(aleatory);
                aleatory = random.Next(0, cards.Count);
                while (indexP.Contains(aleatory))
                {
                    aleatory = random.Next(0, cards.Count);
                }
                newcards[aleatory] = card;
                indexP.Add(aleatory);
                count++;
            }
            cards = newcards.ToList<Cards>();
        }
    }
}
