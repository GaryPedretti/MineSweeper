using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MineSweeper.Business;

namespace MineSweeper.Tests
{
    internal class DeterministicMinesweeperBoard : MineSweeperBoard
    {
        /// <summary>
        /// Returns deterministic positions of mines - position 0 through
        /// the numberOfMines parameter value - e.g., numOfMines = 4
        /// returns a HashSet of ints 0, 1, 2, 3
        /// </summary>
        /// <param name="numberOfMines"></param>
        /// <returns></returns>
        public override HashSet<int> GenerateRandomMinePositions(int numberOfMines)
        {
            HashSet<int> minePositions = new HashSet<int>();
            for(int i = 0;i< numberOfMines; i++)
            {
                minePositions.Add(i);
            }
            return minePositions;
        }
    }
}
