using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BlackJack
{
    class Program
    {
        static List<Card> cards = new List<Card>();             //List containing all cards
        static List<Card> yourCards = new List<Card>();         //Player's cards
        static List<Card> dealerCards = new List<Card>();       //Dealer's cards
        static Random randomValue = new Random();               //Shuffle deck before dealing
        static Stack<Card> card = new Stack<Card>();
        static void Main(string[] args)
        {
            
            int cash = 500;
            string game = "y";
            AddCards();
            while (game == "y")
            {               
                Card card;                                          //Object card to hold player's card or dealer's card  when dealing a card           
                int numOfAces = 0;
                int options =0;
                int yourSum = 0;
                int dealerSum = 0;
                if(cards.Count == 0)
                {
                    AddCards();                                          //Adding cards to deck if deck has 0 cards
                }
                Console.Write($"Cards remaining in deck: {cards.Count}\n*************************\n");
                for (int i = 0; i < 2; i++)                              //Deal initial 2 cards to player
                {
                    card = DealCard();
                    yourCards.Add(card);
                    if(card.Value == 1 && (yourSum+11) <= 21)
                    {
                        numOfAces++;
                        yourSum += 11;                        
                    }
                    else
                    {
                        yourSum += card.Value;                       
                    }
                    
                }

                Console.WriteLine("Your Cards: ");
                foreach (Card c in yourCards)
                {
                    Console.WriteLine(c.ToString());
                }
                Console.WriteLine($"Point: {yourSum}\n");
                

                while (yourSum <= 21 && dealerSum < yourSum)                                     //Display options: Hit or Stand
                {
                    Console.WriteLine($"Cash: {cash:c}");
                    Console.WriteLine("Options:");
                    Console.WriteLine("1.Hit \n2.Stand");

                    try
                    {
                        options = Int32.Parse(Console.ReadLine());
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                                                                             
                    Console.Clear();

                    switch (options)
                    {
                        case 1:                                                                 //If Hit - deal one more card and calculate sum
                            card = DealCard();
                            yourCards.Add(card);
                            if (card.Value == 1 && (yourSum+11) <= 21)                          //Ace can be either 1 or 11 depending on current sum
                            {
                                numOfAces++;
                                yourSum += 11;
                            }
                            else
                            {
                                yourSum += card.Value;
                            }
                                                                            
                            Console.WriteLine("Your Cards: ");
                            foreach (Card c in yourCards)
                            {
                                Console.WriteLine(c.ToString());
                            }
                            if (yourSum <= 21)                                                  //Continue as long as sum is less than or equal to 21
                            {
                                Console.WriteLine($"Your Score: {yourSum}\n");
                            }
                            else
                            {
                                Console.WriteLine($"Your Score: Bust! {yourSum}\n");            //Automatic loss if sum is over 21
                            }
                            break;

                        case 2:                                                                 //If Stand - add cards to dealer until either dealer's score is higher than player's or bust
                            while (dealerSum <= yourSum)
                            {
                                card = DealCard();
                                dealerCards.Add(card);
                                dealerSum += card.Value;
                            }
                            Console.WriteLine("Your Cards: ");
                            foreach (Card c in yourCards)
                            {
                                Console.WriteLine(c.ToString());
                            }
                            Console.WriteLine($"Your Score: {yourSum}\n");
                            Console.WriteLine("Dealer Cards: ");
                            foreach (Card c in dealerCards)
                            {
                                Console.WriteLine(c.ToString());
                            }
                            if (dealerSum > 21)
                            {
                                Console.WriteLine($"Dealer Score: Bust! {dealerSum}!\n");
                            }
                            else
                            {
                                Console.WriteLine($"Dealer Score: {dealerSum}!\n");
                            }
                            break;

                        default:
                            Console.Write("Invalid option\n");
                            Console.Write($"Your Point: {yourSum}\n");
                            break;
                    }
                                  
                }
                if ((yourSum > dealerSum && yourSum <= 21) ||(dealerSum >21))   //Display result
                {
                    Console.WriteLine("You Win");
                    cash += 50;
                    Console.ReadLine();
                    Console.Clear();
                    
                    yourCards.Clear();
                    dealerCards.Clear();
                }
                else 
                {
                    Console.WriteLine("You Lose");
                    cash -= 50;
                    Console.ReadLine();
                    Console.Clear();
                    
                    yourCards.Clear();
                    dealerCards.Clear();
                }
                if (cash == 0){
                    Console.WriteLine("Out of Cash. Play Again? Y/N");
                    game = Console.ReadLine().ToLower();
                    cash = 500;
                    Console.Clear();                                            //Clear console, deck of cards, player and dealer cards
                    cards.Clear();
                    yourCards.Clear();
                    dealerCards.Clear();
                }
               
            }
            
            Console.ReadKey();
        }
         
        static void AddCards()                                              //This method loads the deck with 52 cards
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            for (int i = 1; i<=13; i++)
            {
                cards.Add(Card.Parse($"{i}\t{i}\t\u2660"));                 //load 13 spades
            }
            for (int i = 1; i <= 13; i++)
            {
                cards.Add(Card.Parse($"{i}\t{i}\t\u2663"));                  //load 13 clubs
            }
            for (int i = 1; i <= 13; i++)
            {
                cards.Add(Card.Parse($"{i}\t{i}\t\u2666"));                 //load 13 diamonds
            }
            for (int i = 1; i <= 13; i++)
            {
                cards.Add(Card.Parse($"{i}\t{i}\t\u2665"));                 //load 13 hearts
            }

        }

        static Card DealCard()                                              //This method deal cards to either player or dealer
        {         
            int dealtCard = randomValue.Next(0, cards.Count);         
            
            if (cards.Count <= 0)                                           //When deck reachest 0 cards, add cards to deck
            {
                AddCards();
            }

            Card card = cards[dealtCard];
            cards.RemoveAt(dealtCard);
            
            return card;
        }
    }
}
