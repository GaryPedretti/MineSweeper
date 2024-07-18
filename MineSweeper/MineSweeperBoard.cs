using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeper.Business
{
    public class MineSweeperBoard
    {
        int _cols;
        int _rows;
        int _numberOfMines;
        int _uncoverCount;

        private Dictionary<int, Square> _squares;

        public int NumberOfSquares
        {
            get { return _cols * _rows; }
        }

        public Dictionary<int, Square> Squares
        {
            get { return _squares; }
        }

        public int Cols
        {
            get { return _cols; }
        }
        public int Rows
        {
            get { return _rows; }
        }
        public MineSweeperBoard() : this(10, 10, 10) { }
        public MineSweeperBoard(int cols, int rows, int numberOfMines)
        {
            _cols = cols;
            _rows = rows;
            _numberOfMines = numberOfMines;
            _uncoverCount = 0;

            _squares = new Dictionary<int, Square>();
            for (int i = 0; i < NumberOfSquares; i++)
            {
                Square square = new Square(false);
                Squares.Add(i, square);
            }

            HashSet<int> minePositions = GenerateRandomMinePositions(_numberOfMines);

            foreach (int minePosition in minePositions)
            {
                Squares[minePosition] = new Square(true);
                List<Square> neighbors = FindNeighbors(minePosition);

                foreach (Square square in neighbors)
                {
                    if (!square.IsMine)
                    {
                        square.IncrementValue();
                    }
                }
            }

            for (int i = 0; i < NumberOfSquares; i++)
            {
                Squares[i].Neighbors = FindNeighbors(i);
            }
        }
        public List<Square> FindNeighbors(int squareIndex)
        {
            List<Square> neighbors = new List<Square>();

            if (squareIndex > _cols)
            {
                neighbors.Add(Squares[squareIndex - _cols]);

                if (squareIndex % _cols != 0)
                {
                    neighbors.Add(Squares[squareIndex - _cols - 1]);
                }

                if ((squareIndex + 1) % _cols != 0)
                {
                    neighbors.Add(Squares[squareIndex - _cols + 1]);
                }
            }

            if (squareIndex < (NumberOfSquares - _cols))
            {
                neighbors.Add(Squares[squareIndex + _cols]);

                if (squareIndex % _cols != 0)
                {
                    neighbors.Add(Squares[squareIndex + _cols - 1]);
                }

                if ((squareIndex + 1) % _cols != 0)
                {
                    neighbors.Add(Squares[squareIndex + _cols + 1]);
                }
            }

            if (squareIndex % _cols != 0)
            {
                neighbors.Add(Squares[squareIndex - 1]);
            }

            if ((squareIndex + 1) % _cols != 0)
            {
                neighbors.Add(Squares[squareIndex + 1]);
            }
            return neighbors;
        }


        public virtual HashSet<int> GenerateRandomMinePositions(int numberOfMines)
        {
            HashSet<int> minePositions = new HashSet<int>();
            Random random = new Random();
            do
            {
                int randomPosition = random.Next(NumberOfSquares);
                minePositions.Add(randomPosition);

            } while (minePositions.Count < numberOfMines);
            return minePositions;
        }

        public void EndGame()
        {
            foreach (Square square in _squares.Values)
            {
                if (square.IsMine)
                {
                    square.Uncover();
                }
            }
        }

        public override string ToString()
        {
            String s = null;
            for (int i = 0; i < NumberOfSquares; i++)
            {
                if (i % _cols == 0)
                {
                    s += "\n|";
                }
                s += Squares[i] + "|";
            }

            return s;
        }

        public bool CheckForWin()
        {
            _uncoverCount++;
            return _uncoverCount == (_cols * _rows - _numberOfMines);
        }
    
    }
}
