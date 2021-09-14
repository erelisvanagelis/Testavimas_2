using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testavimas_2
{
    public partial class MainForm : Form
    {
        private Game game;
        public MainForm()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            char[,] array = {
                { 'X', 'O', 'X'},
                { 'D', 'X', 'F' },
                { 'X', 's', 'O' } };

            Game game = new Game(3);
            game.Grid = array;
            Console.WriteLine(game.GameWon());
        }

        private void SetUp()
        {
            Game game = new Game(gridSizeTextBox.Text);
        }
    }
}
