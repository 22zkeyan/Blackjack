using System.Threading;
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
            Console.WriteLine("\nExcellent. Assigning cards now...");
            Thread.Sleep(2000); //2 seconds of sleep

            Random r = new Random();
            string[,] names_cards = new string[names.Length, 2];
            string[] possibleCards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "King", "Queen" };
            for (int i = 0; i < names_cards.GetLength(0); i++) //loops through every name in the array
            {
                for (int j = 0; j < names_cards.GetLength(1); j++) //loops through each location for every name, each person gets two cards
                {                    
                    names_cards[i, j] = possibleCards[r.Next(0, possibleCards.Length)];
                }
                Console.WriteLine($"{names[i]}: {names_cards[i, 0]} and {names_cards[i, 1]}"); //outputs what cards each player has
            }
            return names_cards;
        }

        static int[] CalcTotal(string[,] names_cards, string[] names)
        {
            int[] totals = new int[names_cards.GetLength(0)];
            int temp = 0; //temporary variable that will store the value of the current card
            for (int i = 0; i < names_cards.GetLength(0); i++) //loops through every name in the array
            {
                //this loop starts at 1 to avoid out of index errors or whatever its called
                for (int j = 0; j < names_cards.GetLength(1); j++)
                {
                    bool success = int.TryParse(names_cards[i, j], out temp);
                    if (!success)
                    {
                        if (names_cards[i, j] == "Ace")
                        {
                            Console.WriteLine($"{names[i]} has an ace in their cards, what value do they want it to be? (1 or 11)");
                            temp = Convert.ToInt32(Console.ReadLine()!);
                        }
                        else if (names_cards[i, j] == "Jack" || names_cards[i, j] == "King" || names_cards[i, j] == "Queen")
                        {
                            temp = 10;
                        }                        
                    }
                    totals[i] += temp;
                }                
            }
            return totals;
        }

        static void StandHit()
        {

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Blackjack! Here are the rules:" + 
                "\n\t- Each person starts out with two cards." + 
                "\n\t- On your turn, you can choose to either take another card, or 'hit', or 'stand', where you don't take anymore cards." +
                "\n\t- The aim of the game is to get the total value of your cards to be 21, or as close to that as possible." +
                "\n\t- You can take as many cards as you wish until you feel you are close enough to 21, in which case you 'hold', or until you exceed 21" + 
                "\n\t- If the total value of your cards exceeds 21, you are out." + 
                "\n\t- An ace can be either be worth a 1 or 11, you choose what it's worth");

            Console.WriteLine("\nNow we begin. Please wait...\n");
            Thread.Sleep(2000); //allows Fred to sleep for 2 seconds

            Console.WriteLine("How many people are playing today?");
            string[] names = InitPlayers(Convert.ToInt32(Console.ReadLine()!));
            string[,] names_cards = InitCards(names);
            int[] totals = CalcTotal(names_cards, names);
            Console.WriteLine("\nCalculating totals...\n");
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"{names[i]} total is {totals[i]}");
            }
        }
    }
}
