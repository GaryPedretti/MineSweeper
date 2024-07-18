using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper.Business
{
    public class Square
    {
        private Boolean isCovered = true;
        public Boolean IsMine { get; private set; }
        public int Value { get; private set; }

        public List<Square> Neighbors { get; set; }

        public Square(Boolean isMine)
        {
            IsMine = isMine;
        }

        public bool IsCovered
        {
            get { return isCovered; }
        }

        public void IncrementValue()
        {
            Value++;
        }

        public void Uncover()
        {
            if (isCovered)
            {
                isCovered = false;

                if (IsMine)
                {
                    MineSweeperGame.Instance.EndGame();
                }
                else
                {
                    if (Value == 0)
                    {
                        foreach (Square neighbor in Neighbors)
                        {
                            neighbor.Uncover();
                        }
                    }

                    MineSweeperGame.Instance.CheckForWin();
                }

            }
        }

        public void ToggleFlag()
        {
            // Not Implemented !!!!!!
        }

        public Boolean IsFlagged
        {
            get
            {
                // Not Implemented !!!!!!
                return false; 
            }
        }

        public override string ToString()
        {
            if (IsMine)
            {
                return "*";
            }
            else { 
                return Value.ToString();
            }
        }
        
    }
}
