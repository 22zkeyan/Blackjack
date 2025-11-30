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
                    if (cards[i, j] == "Ace")
                    {
                        Console.WriteLine($"{name} has an ace in their cards, what value do they want it to be? (1 or 11)");
                        temp = Convert.ToInt32(Console.ReadLine()!);
                    }
                    else if (cards[i, j] == "Jack" || cards[i, j] == "King" || cards[i, j] == "Queen")
                    {
                        temp = 10;
                    }                        
                }
                total += temp;
            }                
            return total;
        }
        static string AddCard(string[,] cards, string[] names, string name) 
        {
            Random r = new Random();
            Resize2DArray(cards, cards.GetLength(0), cards.GetLength(1) + 1);
            string[] possibleCards = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "King", "Queen" };
            cards[Array.IndexOf(names, name), cards.GetLength(1) - 1] = possibleCards[r.Next(0, possibleCards.Length)];
            return cards[Array.IndexOf(names, name), cards.GetLength(1) - 1]; //returns the last card (the new card)
        }
        static string[,] Resize2DArray(string[,] original, int newRows, int newCols) //courtesy of GitHub Copilot AI
        {
            int originalRows = original.GetLength(0);
            int originalCols = original.GetLength(1);

            string[,] result = new string[newRows, newCols];

            int minRows = Math.Min(originalRows, newRows);
            int minCols = Math.Min(originalCols, newCols);

            for (int row = 0; row < minRows; row++)
                for (int col = 0; col < minCols; col++)
                    result[row, col] = original[row, col];

            return result;
        }
        static void StandHit(string[,] cards, string[] names)
        {
            int[] totals = new int[cards.GetLength(0)];
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"{names[i]}, do you wish to stand or hit?");
                string input = (Console.ReadLine()!).ToLower();
                while (input != "stand")
                {
                    Console.WriteLine("You will now be given another card...");
                    string new_card = AddCard(cards, names, names[i]);
                    Thread.Sleep(2000);
                    Console.WriteLine($"Your card is {new_card}");
                    totals[i] = CalcTotal(cards, names, names[i]);
                    if (totals[i] > 21)
                    {
                        Console.WriteLine("Unfortunately, you got over 21, so you are now out.");
                        input = "hit";
                    }
                    else
                    {
                        Console.WriteLine($"{names[i]}, do you wish to stand or hit?");
                        input = (Console.ReadLine()!).ToLower();
                    }
                }
                Console.WriteLine("Very well.");
                Thread.Sleep(2000);
            }
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
