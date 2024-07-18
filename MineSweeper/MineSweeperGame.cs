using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MineSweeper;

namespace MineSweeper.Business
{
    public class MineSweeperGame
    {
        
        //private int _rows = 10;
        //private int _cols = 10;
        //private int _mines = 10;
        MineSweeperBoard _board;
        private int _uncoverCount;
        private bool _isWon;
        private bool _gameOver;

        private readonly static MineSweeperGame _instance = new MineSweeperGame();

        private MineSweeperGame() {}

        public static MineSweeperGame Instance 
        {
            get { return _instance; }
        }

        public void Initialize()
        {
            _uncoverCount = 0;
            _board = new MineSweeperBoard(); 
        }

        public Dictionary<int, Square> Squares
        {
            get { return _board.Squares; }
        }

        public int Cols
        {
            get { return _board.Cols; }
        }

        public int NumberOfSquares
        {
            get { return _board.Cols * _board.Rows; }
        }

        public void Uncover(int location)
        {
            _board.Squares[location].Uncover();
        }
         
        public override string ToString()
        {
            return _board.ToString();
        }


        public void ToggleFlag(int location)
        {
            _board.Squares[location].ToggleFlag();
        }

        public void EndGame()
        {
            _board.EndGame();
            _gameOver = true;
        }

        public bool GameOver
        {
            get { return _gameOver; }
        }
        public bool IsWon
        {
            get { return _isWon; }
        }

        internal void CheckForWin()
        {
            _isWon = _board.CheckForWin();
        }

    }
}
