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
            return GetTurn(xTurn);
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

        /// <summary>
        /// Set specific value to the matrix
        /// </summary>
        /// <param name="x">Row index</param>
        /// <param name="y">Column index</param>
        /// <param name="value">Specific value to set</param>
        public void SetGridValue(int x, int y, char value)
        {
            grid[x, y] = value;
            ChangeTurn();
        }

        /// <summary>
        /// Sets value automatically based on turn, if xTurn == true, value set is 'X' 
        /// </summary>
        /// <param name="x">Row index</param>
        /// <param name="y">Collumn index</param>
        public void SetGridValue(int x, int y)
        {
            SetGridValue(x, y, GetTurn());
        }

        public char GetGridValue(int x, int y) => grid[x, y];

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

        public bool IsDraw()
        {
            foreach(char c in grid)
            {
                if(c == '\0')
                {
                    return false;
                }
            }
            return true;
        }

        public bool ConsecutiveValueReached(char[] array, char value, int countToReach)
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
            int dim = DimensionLength(grid);

            List<List<char>> lines = GetAllArrayLines(grid, dim);

            foreach(List<char> list in lines)
            {
                if (ConsecutiveValueReached(list.ToArray(), GetTurn(!XTurn), winLineSize))
                {
                    return true;
                }
            }
            return false;
        }

        public List<List<char>> GetAllArrayLines(char[,] array, int dim)
        {
            List<List<char>> lines = new List<List<char>>();
            for (int i = 0; i < dim; i++)
            {
                lines.Add(ExtractRow(array, i, dim).ToList());
                lines.Add(ExtractColumn(array, i, dim).ToList());
            }

            lines.AddRange(ExtractDiagnals(array, dim));
            lines.AddRange(ExtractDiagnals(RotateMatrix(array, dim), dim));

            return lines;
        }

        public char[] ExtractRow(char[,] array, int x, int dim)
        {
            char[] row = new char[dim];
            for (int i = 0; i < dim; i++)
            {
                row[i] = array[x, i];
            }

            return row;
        }

        public char[] ExtractColumn(char[,] array, int y, int dim)
        {
            char[] column = new char[dim];
            for (int i = 0; i < dim; i++)
            {
                column[i] = array[i, y];
            }

            return column;
        }

        public List<List<char>> ExtractDiagnals(char[,] array, int dim)
        {
            List<List<char>> lines = new List<List<char>>();
            for (int k = 0; k < dim * 2; k++)
            {
                List<char> temp = new List<char>();
                for (int j = 0; j <= k; j++)
                {
                    int i = k - j;
                    if (i < dim && j < dim)
                    {
                        temp.Add(array[i, j]);
                    }
                }
                if(temp.Count != 0)
                {
                    lines.Add(temp);
                }

            }

            return lines;
        }

        public char[,] RotateMatrix(char[,] array, int dim)
        {
            char[,] rotated = new char[dim, dim];

            for (int i = 0; i < dim; ++i)
            {
                for (int j = 0; j < dim; ++j)
                {
                    rotated[i, j] = array[dim - j - 1, i];
                }
            }

            return rotated;
        }

        public int DimensionLength<T>(T[,] array)
        {
            return (int)Math.Sqrt(array.Length);
        }
    }
}
