using System.Threading;
using System.Xml.Linq;
namespace Blackjack
{
    internal class Program
    {
        static string[] InitPlayers(int people) //this function adds the people's names to an array
        {
            string[] names = new string[people]; //creates an array of people the length of the amount of people playing
            for (int i = 0; i < names.Length; i++) //loops through every position in the array
            {
                Console.Write($"Enter name {i+1}: "); //interpolated string
                names[i] = Console.ReadLine()!; //assigns the current value of names to the user input
            }

            return names;          
        }
        static string[,] InitCards(string[] names) //this function assigns the cards to each player
        {
            Console.WriteLine("\nExcellent. Assigning cards now...\n");
            Thread.Sleep(2000); //2 seconds of sleep

            Random r = new Random();
            string[,] cards = new string[names.Length, 2];
            string[] possibleCards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "King", "Queen" };
            for (int i = 0; i < cards.GetLength(0); i++) //loops through every name in the array
            {
                for (int j = 0; j < cards.GetLength(1); j++) //loops through each location for every name, each person gets two cards
                {                    
                    cards[i, j] = possibleCards[r.Next(0, possibleCards.Length)];
                }
                Console.WriteLine($"{names[i]}: {cards[i, 0]} and {cards[i, 1]}"); //outputs what cards each player has
            }
            return cards;
        }
        static int CalcTotal(string[,] cards, string[] names, string name)
        {
            int total = 0;
            int temp = 0; //temporary variable that will store the value of the current card
            int i = Array.IndexOf(names, name);
            for (int j = 0; j < cards.GetLength(1); j++)
            {
                bool success = int.TryParse(cards[i, j], out temp);
                if (!success)
                {
                    temp = DetermineCardVal(cards[i, j], name);
                }
                total += temp;
            }                
            return total;
        }
        static int DetermineCardVal(string card, string name)
        {
            int temp = 0;
            if (card == "Ace")
            {
                Console.WriteLine($"{name} has an ace in their cards, what value do they want it to be? (1 or 11)");
                temp = Convert.ToInt32(Console.ReadLine()!);
            }
            else if (card == "Jack" || card == "King" || card == "Queen")
            {
                temp = 10;
            }
            return temp;
        }
        static string AddCard(string[,] cards, string[] names, string name) 
        {
            Random r = new Random();
            string[] possibleCards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "King", "Queen" };
            string new_card = possibleCards[r.Next(0, possibleCards.Length)];
            return new_card; //returns the last card (the new card)
        }
        static void StandHit(string[,] cards, string[] names)
        {
            int[] totals = new int[cards.GetLength(0)];
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"\n{names[i]}, do you wish to stand or hit?");
                string input = (Console.ReadLine()!).ToLower();
                while (input != "stand")
                {
                    Console.WriteLine("You will now be given another card...");
                    string new_card = AddCard(cards, names, names[i]);
                    int int_card = DetermineCardVal(new_card, names[i]);
                    Thread.Sleep(2000);
                    totals[i] += CalcTotal(cards, names, names[i]) + int_card;
                    if (totals[i] > 21)
                    {
                        Console.WriteLine("Unfortunately, you got over 21, so you are now out.");
                        input = "stand";
                    }
                    else if (totals[i] == 21)
                    {
                        Console.WriteLine($"{names[i]} has exactly 21, so they are a winner");                        
                    }
                    else
                    {
                        Console.WriteLine($"{names[i]}, your card is {new_card}, your new total is {totals[i]}, do you wish to stand or hit?");
                        input = (Console.ReadLine()!).ToLower();
                    }
                }
                if (input == "stand")
                {
                    Console.WriteLine("Very well.");
                    Thread.Sleep(2000);
                }
            }
        }
        static void DetermineWinner(string[] names, int[] totals)
        {
            string[,] candidates = new string[names.Length, 2];

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Blackjack. Here are the rules:" + 
                "\n\t- Each person starts out with two cards." + 
                "\n\t- On your turn, you can choose to either take another card, or 'hit', or 'stand', where you don't take anymore cards." +
                "\n\t- The aim of the game is to get the total value of your cards to be 21, or as close to that as possible." +
                "\n\t- You can take as many cards as you wish until you feel you are close enough to 21, in which case you 'hold', or until you exceed 21" + 
                "\n\t- If the total value of your cards exceeds 21, you are out." + 
                "\n\t- An ace can be either be worth a 1 or 11, you choose what it's worth");

            Console.WriteLine("\nNow we begin. Please wait...\n");
            Thread.Sleep(2000); //allows Fred to sleep for 2 seconds

            Console.WriteLine("How many people are playing today?");
            Console.Write("No. of people: ");
            string[] names = InitPlayers(Convert.ToInt32(Console.ReadLine()!));
            string[,] cards = InitCards(names);

            //int[] totals = CalcTotal(cards, names);

            StandHit(cards, names);
        }
    }
}
