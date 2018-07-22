using System;
using System.Collections.Generic;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            new Battle();
            Console.ReadLine();
        }
    }

    class Battle{

        public Player User;
        public Player Opponent;

        public int Turn = 1;

        public Battle()
        {
            this.User = new Player("Evil Derek", "Evil man");
            this.Opponent = new Player("Derek", "Dudesmith");

            this.User.PrintStats();
            this.Opponent.PrintStats();
        }

    }

    public class Player
    {
        public List<MonsterCard> cardsOnField = new List<MonsterCard>();
        public List<Card> cardsInDeck = new List<Card>();
        public List<Card> cardsInHand = new List<Card>();

        public String name = "Dog";
        public String heroType = "Mage";

        public int health = 30;
        public int currentMana = 1;
        public int maxMana = 10;
        public int maxHandSize = 10;

        public Player(String name, String heroType)
        {
            this.name = name;
            this.heroType = heroType;
            this.InitDeck();
        }

        public void InitDeck()
        {
            Card c = new Card();
            c.name = "John";
            c.description = "A fun dude";
            cardsInDeck.Add(c);

            for (int i = 0; i < 10; i++)
            {
                cardsInDeck.Add(new MonsterCard());
            }
            cardsInDeck.Add(new Card());
            this.DrawCards(4);
        }


        public void DrawCards(int numOfCards)
        {
            for (int i = 0; i < numOfCards; i++)
            {
                this.DrawCard();
            }
        }

        public void DrawCard()
        {
            if(this.cardsInDeck.Count <= 0)
            {
                Console.WriteLine("No cards, man!");
            }
            else if (this.cardsInHand.Count < this.maxHandSize) {
            this.cardsInHand.Add(this.cardsInDeck[0]);
            }
            else
            {
                Console.WriteLine("Burned Card: " + this.cardsInDeck[0]);
            }
            this.cardsInDeck.RemoveAt(0);
        }

        public void PrintStats()
        {
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Hero Type: " + heroType);
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Current Mana: " + currentMana);
            Console.WriteLine("Max Mana: " + maxMana);
            Console.WriteLine("Max Hand Size: " + maxHandSize);

            Console.WriteLine("Cards In Hand:");
            foreach(var Card in this.cardsInHand){
                Card.PrintStats();
            }
        }
    }

    public class Card
    {

        public String name = "Dude";
        public String description = "He do stuff";
        public int manaCost = 1;

        public void Action()
        {
            Console.WriteLine("Hey");
        }

        public virtual void PrintStats()
        {
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Description: " + description);
            Console.WriteLine("ManaCost: " + manaCost);
        }

    }

    public class MonsterCard : Card
    {
        public int health = 1;
        public int attack = 1;

        public override void PrintStats()
        {
            base.PrintStats();
            Console.WriteLine("Health: " + health);
            Console.WriteLine("Attack: " + attack);
        }
}
}
