using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Game
    {
        private static Player p1 = new Player();
        private static Player p2 = new Player();

        private static char[,] array = new char[6,7];
        
        public static void PlayGame()
        {
            Console.WriteLine("Let's Play Connect 4!\n");
            Console.Write("What is the name of Player One: ");
            string input1 = Console.ReadLine();
            p1.playerName = (input1 != string.Empty) ? input1 : "Player 1";
            p1.playerSymbol = 'X';
            Console.WriteLine($"{p1.playerName}, your symbol is 'X'.\n");

            Console.Write("What is the name of Player Two: ");
            string input2 = Console.ReadLine();
            p2.playerName = (input2 != string.Empty) ? input2 : "Player 2";
            p2.playerSymbol = 'O';
            Console.WriteLine($"{p2.playerName}, your symbol is 'O'.\n");

            Console.Write("Who want to start first (1/2): \n");
            string input = Console.ReadLine();

            if(String.IsNullOrEmpty(input) || Convert.ToByte(input) == 1) 
            {
                PlayNext(p1, p2, array);  
            } else
            {
                PlayNext(p2, p1, array);
            }
        }

        static void PlayNext(Player p1, Player p2, char[,] arr)
        {
            Console.WriteLine($"{p1.playerName}, where do you want to place your token '{p1.playerSymbol}'?");

            byte column = 0;
            do
            {
                try
                {
                    Console.Write("Select a column (1-7): ");
                    column = Convert.ToByte(Console.ReadLine());
                    column--;  // the first element in array is referenced as 0 so we substract 1 from the user input

                    int columnElementsCount = CheckColumn(array, column);
                    if (columnElementsCount >= 6)
                    {
                        Console.WriteLine($"The {column+1} column is full! Try another one!");
                    } else
                    {
                        int rowCoordOfLastInserted = 0;

                        array[5 - columnElementsCount, column] = p1.playerSymbol; // placing the new symbol above the first one
                        rowCoordOfLastInserted = 5 - columnElementsCount;
                        
                        DisplayBoard(array);

                        if( (p1.CheckVertically(array, rowCoordOfLastInserted, column)) || (p1.CheckHorizontally(array, rowCoordOfLastInserted, column)) || (p1.CheckDiagonallyAscending(array, rowCoordOfLastInserted, column)) || (p1.CheckDiagonallyDescending(array, rowCoordOfLastInserted, column)))
                        {
                            Console.WriteLine($"{p1.playerName} win! Congratulations!");

                            Console.WriteLine("Do you want to play again? (y/n)");
                            string answer = Console.ReadLine().ToLower();

                            while(!answer.Contains('n'))
                            {
                                // clear the entire array and start game from the beginning
                                Array.Clear(array, 0, array.Length); 
                                PlayGame();

                                if (answer.Contains('n'))
                                {
                                    return;
                                }
                            }
                            return;
                        }

                        Console.WriteLine($"{p2.playerName}'s turn, here we go... :)\n");

                        PlayNext(p2, p1, array);
                        return;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (!(TestRange(column, 1, 7)) || CheckColumn(array, column) >= 6);
        }

        static void DisplayBoard(char[,] array)
        {
            int rowLength = array.GetLength(0);
            int colLength = array.GetLength(1);

            for (int row = 0; row < rowLength; row++)
            {
                for (int col = 0; col < colLength; col++)
                {
                    if (array[row, col] == 'X' || array[row, col] == 'O') {
                        Console.Write(" " + array[row, col]);
                    } else
                    {
                        Console.Write(array[row, col] + ".");
                    } 
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }

        /// <summary>
        ///     A simple method that takes a two-dimensional array and the number of a column and returns how many 'X' or 'O' symbols has that specific column.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="col"></param>
        /// <returns>The total of elements in the column</returns>
        static int CheckColumn(char[,] arr, byte col)
        {
            int rowLength = array.GetLength(0);

            int count = 0;
            for (int row = 0; row < rowLength; row++)
            {
                if(array[row, col] == 'X' || array[row, col] == 'O')
                    count++;
            }

            return count;
        }

        static bool TestRange(int numberToCheck, int bottom, int top)
        {
            return (numberToCheck >= bottom && numberToCheck <= top);
        }
    }
}
