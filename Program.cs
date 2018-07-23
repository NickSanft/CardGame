using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

namespace CardGame
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new Battle();
            Console.ReadLine();
        }
    }

    class Battle{

        private Player playerOne;
        private Player playerTwo;
        private Player currentPlayer;

        public int turnNumber = 1;
        public Boolean turnEnded = false;

        public Battle()
        {
            this.playerOne = new Player("Evil Derek", HeroType.Mage);
            this.playerTwo = new Player("Derek", HeroType.Mage);
            this.currentPlayer = this.playerOne;
            this.currentPlayer.DrawCard();

            this.playerOne.PrintStats();
            this.playerTwo.PrintStats();

            while (!this.IsMatchOver())
            {
                if (this.currentPlayer.CheckForAction())
                {
                    Thread.Sleep(1000);
                    if (this.currentPlayer.Equals(playerOne))
                    {
                        this.currentPlayer = this.playerTwo;
                    }
                    else
                    {
                        this.currentPlayer = this.playerOne;
                    }
                    this.currentPlayer.DrawCard();
                }
            }
        }

        public Boolean IsMatchOver()
        {
            if(playerOne.health <= 0)
            {
                Console.WriteLine(playerTwo.name + " WINS!");
                return true;
            }
            else if (playerTwo.health <= 0)
            {
                Console.WriteLine(playerOne.name + " WINS!");
                return true;
            }
            return false;
        }
    }
    public enum HeroType { Mage };

    public class Player
    {
        public List<MonsterCard> cardsOnField = new List<MonsterCard>();
        public List<Card> cardsInDeck = new List<Card>();
        public List<Card> cardsInHand = new List<Card>();

        public String name = "Dog";
        public HeroType heroType;

        public int health = 30;
        public int currentMana = 1;
        public int maxMana = 10;
        public int maxHandSize = 10;

        public Player(String name, HeroType heroType)
        {
            this.name = name;
            this.heroType = heroType;
            this.InitDeck();
        }

        public void InitDeck()
        {
            Card c = new Card
            {
                name = "John",
                description = "A fun dude"
            };
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

        public Boolean CheckForAction()
        {
            if (Keyboard.IsKeyDown(Key.A))
            {
                this.PlayCard(0);
                Thread.Sleep(1000);
            }
            else if (Keyboard.IsKeyDown(Key.S))
            {
                Console.WriteLine("Ending Turn!");
                return true;
            }
            return false;
        }

        public void PlayCard(int index)
        {
            foreach (var Card in this.cardsInHand)
            {
                Card.PrintStats();
            }
            Card card = this.cardsInHand[index];
            if (card.manaCost <= this.currentMana)
            {
                if (card is MonsterCard)
                {
                    this.cardsOnField.Add((MonsterCard) card);
                    Console.WriteLine("Played Card: " + card.name);
                }
                card.Action();
                this.cardsInHand.Remove(card);
            }
            else
            {
                Console.WriteLine("Not enough mana!");
            }
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
