using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MineSweeper.UI;
using MineSweeper.Business;

namespace MineSweeper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Starting Minesweeper..."); 
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MineSweeperForm());
        }
    }
}
