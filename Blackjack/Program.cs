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
            string[,] names_cards = new string[names.Length, 3];
            string[] possibleCards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "King", "Queen" };
            for (int i = 0; i < names_cards.GetLength(0); i++) //loops through every name in the array
            {
                names_cards[i, 0] = names[i];
                //this loop starts at 1 to avoid out of index errors or whatever its called
                for (int j = 1; j < names_cards.GetLength(1); j++) //loops through each location for every name, each person gets two cards
                {                    
                    names_cards[i, j] = possibleCards[r.Next(0, possibleCards.Length)];
                }
                Console.WriteLine($"{names[i]}: {names_cards[i, 1]} and {names_cards[i, 2]}"); //outputs what cards each player has
            }
            return names_cards;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Blackjack! Here are the rules:" + 
                "\n\t- Each person starts out with two cards." + 
                "\n\t- On your turn, you can choose to either take another card, or hold onto what you have." +
                "\n\t- The aim of the game is to get the total value of your cards to be 21, or as close to that as possible." + 
                "\n\t- If the total value of your cards exceeds 21, you are out." + 
                "\n\t- An ace can be either be worth a 1 or 11, you choose what it's worth");

            Console.WriteLine("\nNow we begin. Please wait...\n");
            Thread.Sleep(2000); //allows Fred to sleep for 2 seconds

            Console.WriteLine("How many people are playing today?");
            string[] names = InitPlayers(Convert.ToInt32(Console.ReadLine()!));
            string[,] names_cards = InitCards(names);
        }
    }
}
