using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testavimas_2
{
    public class Game
    {
        private char[,] grid;
        private int winLineSize;
        private bool xTurn;

        public char[,] Grid { get => grid; set => grid = value; }
        public bool XTurn { get => xTurn; set => xTurn = value; }

        public Game(string gridSize, int winLineSize = 3, bool xTurn = true)
        {
            if (String.IsNullOrWhiteSpace(gridSize))
                throw new Exception("Wrong input");

            int size = Convert.ToInt32(gridSize);
            grid = new char[size, size];
            this.winLineSize = winLineSize;
            this.xTurn = xTurn;
        }

        public char GetTurn()
        {
            char value = 'O';
            if(xTurn)
            {
                value = 'X';
            }

            return value;
        }

        public char GetTurn(bool xTurn)
        {
            char value = 'O';
            if (xTurn)
            {
                value = 'X';
            }

            return value;
        }

        public void SetGridValue(int x, int y, char value)
        {
            grid[x, y] = value;
            ChangeTurn();
        }

        public void SetGridValue(int x, int y)
        {
            SetGridValue(x, y, GetTurn());
        }

        public void ChangeTurn()
        {
            if(xTurn)
            {
                xTurn = false;
            }
            else
            {
                xTurn = true;
            }
        }



        public bool ConsecutiveValueCounter(char[] array, char value, int countToReach)
        {
            int matchCount = 0;
            foreach(char c in array)
            {
                if (c == '\0' || c != value)
                {
                    matchCount = 0;
                }
                else
                {
                    matchCount++;
                }

                if(countToReach == matchCount)
                {
                    return true;
                }
            }

            return false;
        }



        public bool GameWon()
        {
            int gridDimLength = (int)Math.Sqrt(grid.Length);

            List<List<char>> lines = GetAllArrayLines(grid);

            foreach(List<char> list in lines)
            {
                if (ConsecutiveValueCounter(list.ToArray(), GetTurn(!XTurn), winLineSize))
                {
                    return true;
                }
            }
            return false;
        }

        public List<List<char>> GetAllArrayLines(char[,] array)
        {
            List<List<char>> lines = new List<List<char>>();
            int gridDimLength = (int)Math.Sqrt(grid.Length);

            for (int i = 0; i < gridDimLength; i++)
            {
                lines.Add(ExtractRow(grid, i, gridDimLength).ToList());
                lines.Add(ExtractColumn(grid, i, gridDimLength).ToList());
            }

            lines.AddRange(ExtractDiagnals(array));
            lines.AddRange(ExtractDiagnals(RotateMatrix(array)));

            return lines;
        }

        public char[] ExtractRow(char[,] array, int x, int length)
        {
            char[] row = new char[length];
            for (int i = 0; i < length; i++)
            {
                row[i] = array[x, i];
            }

            return row;
        }

        public char[] ExtractColumn(char[,] array, int y, int length)
        {
            char[] column = new char[length];
            for (int i = 0; i < length; i++)
            {
                column[i] = array[i, y];
            }

            return column;
        }

        public List<List<char>> ExtractDiagnals(char[,] array)
        {
            List<List<char>> lines = new List<List<char>>();
            int dim = (int)Math.Sqrt(array.Length);
            for (int k = 0; k < dim * 2; k++)
            {
                List<char> temp = new List<char>();
                for (int j = 0; j <= k; j++)
                {
                    int i = k - j;
                    if (i < dim && j < dim)
                    {
                        temp.Add(array[i, j]);
                        Console.Write(array[i,j] + " ");
                    }
                }
                lines.Add(temp);
                Console.WriteLine();
            }

            return lines;
        }

        public char[,] RotateMatrix(char[,] matrix)
        {
            int dim = (int)Math.Sqrt(matrix.Length);
            char[,] ret = new char[dim, dim];

            for (int i = 0; i < dim; ++i)
            {
                for (int j = 0; j < dim; ++j)
                {
                    ret[i, j] = matrix[dim - j - 1, i];
                }
            }

            return ret;
        }
    }
}
