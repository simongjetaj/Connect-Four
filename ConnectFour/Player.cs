using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    struct Player
    {
        public string playerName;
        public char playerSymbol;

        /* 
         * Loop through vertically, horizontally, diagonally (ascending & descenging) and check if there are at least 4 'X' or 'O' 
         */

        public bool CheckVertically(char[,] arr, int row, int col)
        {
            char symbol = arr[row, col]; // the symbol where was placed the last token
            int count = 0;

            for(int i = 0; i < 6; i++)
            {
                if (arr[i, col] == symbol)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }

                if (count >= 4) return true;
            }
            return false;
        }

        public bool CheckHorizontally(char[,] arr, int row, int col)
        {
            char symbol = arr[row, col]; // the symbol where was placed the last token
            int count = 0;

            for (int i = 0; i < 7; i++)
            {
                if (arr[row, i] == symbol)
                {
                    count++;
                }
                else
                {
                    count = 0;
                }

                if (count >= 4)
                    return true;
                
            }
            return false;
        }

        public bool CheckDiagonallyDescending(char[,] arr, int row, int col)
        {
            char symbol = arr[row, col]; // the symbol where was placed the last token

            for (int i = 3; i < 6; i++)
            {
                for (int j = 0; j < 7 - 3; j++)
                {
                    if (arr[i,j] == symbol && arr[i - 1, j + 1] == symbol && arr[i - 2, j + 2] == symbol && arr[i - 3, j + 3] == symbol)
                        return true;
                }
            }
            return false;
        }

        public bool CheckDiagonallyAscending(char[,] arr, int row, int col)
        {
            char symbol = arr[row, col]; // the symbol where was placed the last token

            for (int i = 3; i < 6; i++)
            {
                for (int j = 3; j < 7; j++)
                {
                    if (arr[i, j] == symbol && arr[i - 1, j - 1] == symbol && arr[i - 2, j - 2] == symbol && arr[i - 3, j - 3] == symbol)
                        return true;
                }
            }
            return false;
        }
    };
}