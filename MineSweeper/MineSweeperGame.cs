using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MineSweeper;

namespace MineSweeper.Business
{
    public class MineSweeperGame
    {
        private Dictionary<int, Square> squares;

        private int rows;
        private int cols;
        private int mines;

        private int uncoverCount;
        private bool isWon;
        private bool gameOver;

        private static MineSweeperGame instance = new MineSweeperGame();

        private MineSweeperGame() {}

        public static MineSweeperGame Instance 
        {
            get { return instance; }
        }

        public void Initialize(int cols, int rows, int mines)
        {
            this.cols = cols;
            this.rows = rows;
            this.mines = mines;
            this.uncoverCount = 0;

            CreateInitialNumberSquares();

            CreateMineSquares(mines);

            GetToKnowNeighbors();
        }

        private void GetToKnowNeighbors()
        {
            for (int i = 0; i < NumberOfSquares; i++)
            {
                Squares[i].Neighbors = FindNeighbors(i);
            }
        }

        private void CreateMineSquares(int mines)
        {
            HashSet<int> minePositions = GenerateRandomMinePositions(mines);

            foreach (int minePosition in minePositions)
            {
                Squares[minePosition] = new Square(true);
                IncrementNeighboringSquares(minePosition);
            }
        }

        private void IncrementNeighboringSquares(int minePosition)
        {
            List<Square> neighbors = FindNeighbors(minePosition);

            foreach (Square square in neighbors)
            {
                if (!square.IsMine)
                {
                    square.IncrementValue();
                }
            }
        }

        private HashSet<int> GenerateRandomMinePositions(int mines)
        {
            HashSet<int> minePositions = new HashSet<int>();
            Random random = new Random();
            do
            {
                int randomPosition = random.Next(NumberOfSquares);
                minePositions.Add(randomPosition);

            } while (minePositions.Count < mines);
            return minePositions;
        }

        private void CreateInitialNumberSquares()
        {
            squares = new Dictionary<int, Square>();
            for (int i = 0; i < NumberOfSquares; i++)
            {
                Square square = new Square(false);
                Squares.Add(i, square);
            }
        }

        private List<Square> FindNeighbors(int i)
        {
            List<Square> neighbors = new List<Square>();

            if (i > cols)
            {
                neighbors.Add(Squares[i - cols]);

                if (i % cols != 0)
                {
                    neighbors.Add(Squares[i - cols - 1]);
                }

                if ((i + 1) % cols != 0)
                {
                    neighbors.Add(Squares[i - cols + 1]);
                }
            }

            if (i < (NumberOfSquares - cols))
            {
                neighbors.Add(Squares[i + cols]);

                if (i % cols != 0)
                {
                    neighbors.Add(Squares[i + cols - 1]);
                }

                if ((i + 1) % cols != 0)
                {
                    neighbors.Add(Squares[i + cols + 1]);
                }
            }

            if (i % cols != 0)
            {
                neighbors.Add(Squares[i - 1]);
            }

            if ((i + 1) % cols != 0)
            {
                neighbors.Add(Squares[i + 1]);
            }
            return neighbors;
        }

        public Dictionary<int, Square> Squares
        {
            get { return squares; }
        }

        public int Cols
        {
            get { return cols; }
        }

        public int NumberOfSquares
        {
            get { return cols * rows; }
        }

        public void Uncover(int location)
        {
            squares[location].Uncover();
        }
         
        public override string ToString()
        {
            String s = null;
            for (int i = 0; i < NumberOfSquares; i++)
            {
                if (i % cols == 0)
                {
                    s += "\n|";
                }
                s += Squares[i] + "|";
            }

            return s;
        }


        public void ToggleFlag(int location)
        {
            squares[location].ToggleFlag();
        }

        public void EndGame()
        {
            foreach (Square square in squares.Values)
            {
                if (square.IsMine)
                {
                    square.Uncover();
                }
            }
            gameOver = true;
        }

        public bool GameOver
        {
            get { return gameOver; }
        }
        public bool IsWon
        {
            get { return isWon; }
        }

        internal void CheckForWin()
        {
            uncoverCount++;
            isWon = uncoverCount == (cols * rows - mines);
        }

    }
}
