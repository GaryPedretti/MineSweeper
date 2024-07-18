using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MineSweeper.Business;
using System.IO;

namespace MineSweeper.UI
{
    public partial class MineSweeperForm : Form
    {
        private MineSweeperGame game;
        private TableLayoutPanel mineFieldGrid = new TableLayoutPanel();
        private List<ButtonSquare> buttonSquares;

        private Bitmap pic_Win = new Bitmap("../../images/happy.gif");
        private Bitmap pic_Flag = new Bitmap("../../images/flag.gif");
        private Bitmap pic_Lose = new Bitmap("../../images/r_bloody.jpg");
        private Bitmap pic_Start = new Bitmap("../../images/r_ehh.jpg");
        private Bitmap pic_Mine = new Bitmap("../../images/mine.gif");

        public MineSweeperForm()
        {
            InitializeComponent();
            InitMineField();
        }


        private void InitMineField()
        {

            int cols = 10;
            int rows = 10;
            int mines = 10;

            game = MineSweeperGame.Instance;
            game.Initialize(cols, rows, mines);
            Console.WriteLine(game);
            
            mineFieldGrid.Controls.Clear();
            mineFieldGrid.ColumnCount = game.Cols;
            mineFieldGrid.Location = new System.Drawing.Point(0, 55); 
            mineFieldGrid.AutoSize = true;
            this.AutoSize = true;
            this.Controls.Add(mineFieldGrid);

            buttonSquares = new List<ButtonSquare>();
            int i = 0;
            foreach (Square square in game.Squares.Values)
	        { 
                ButtonSquare button = new ButtonSquare(i, square.ToString());
                button.Size = new System.Drawing.Size(30, 30);
                button.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                button.MouseDown += new MouseEventHandler(OnClick);
                
                mineFieldGrid.Controls.Add(button);
                buttonSquares.Add(button);
                i++;
	        }

            newGame.Image = pic_Start;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnClick(object sender, MouseEventArgs e)
        {
            ButtonSquare square = ((ButtonSquare)sender);
            switch (e.Button)
            {
                case MouseButtons.Left:
                    game.Uncover(square.Position);
                    paintButtons();
                    break;
                default:
                    break;
            }
 
        }

        private void paintButtons()
        {
            int i = 0;
            foreach (Square square in game.Squares.Values)
	        {
                if (!square.IsCovered)
                {

                    if (square.IsMine)
                    {

                        buttonSquares[i].BackColor = Color.Pink;
                        buttonSquares[i].Image = pic_Mine;
                        newGame.Image = pic_Lose;
                        disableSquares();
                    }
                    else
                    {
                        buttonSquares[i].Text = square.ToString();
                        buttonSquares[i].Enabled = false;

                        if (game.IsWon)
                        {
                            newGame.Image = pic_Win;
                            disableSquares();
                        }
                    }
                }
                else if (square.IsFlagged)
                {
                    buttonSquares[i].Image = pic_Flag;
                }
                else
                {
                    buttonSquares[i].Image = null;
                }

                i++;
            }
        }

        private void disableSquares()
        {
            foreach (ButtonSquare button in buttonSquares)
            {
                button.Enabled = false;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            InitMineField();
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            InitMineField();
        }
    }


    public class ButtonSquare : Button
    {
        private int position;
        private string faceValue;

        public ButtonSquare(int position, string faceValue)
        {
            this.position = position;
            this.faceValue = faceValue;
        }

        public int Position {
            get { return position; }
        }

        public string Value
        {
            get { return faceValue; }
        }
    }
}
