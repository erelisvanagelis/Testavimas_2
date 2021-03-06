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

        private void UpdateState(string state)
        {
            turnButton.Text = game.GetTurn().ToString();
            if(state == "Continue")
            {
                return;
            }

            flowLayoutPanel.Enabled = false;
            turnButton.Enabled = true;
            MessageBox.Show(state);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                bool xTurn = true;
                if(turnButton.Text == "O")
                {
                    xTurn = false;
                }
                
                game = new Game(gridSizeTextBox.Text, UpdateState, winLineSizeTextBox.Text, xTurn);
                PrepareTheBoard();
                turnButton.Enabled = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void PrepareTheBoard()
        {
            turnButton.Text = game.GetTurn().ToString();

            flowLayoutPanel.Controls.Clear();
            flowLayoutPanel.Enabled = true;

            int dimension = Convert.ToInt32(gridSizeTextBox.Text);
            int buttonSize = flowLayoutPanel.Width / dimension - 10;
            int fontSize = (int)(buttonSize * 0.40);
            Console.WriteLine(fontSize);
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    Button button = new Button();
                    button.Tag = new Container(i, j);
                    button.Height = buttonSize;
                    button.Width = buttonSize;
                    button.Font = new Font(button.Font.FontFamily, fontSize);
                    button.MouseClick += Button_MouseClick;
                    flowLayoutPanel.Controls.Add(button);
                }
            }
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            Container c = (Container)b.Tag;

            game.SetGridValue(c.X, c.Y);
            b.Text += game.GetGridValue(c.X, c.Y);
            b.Enabled = false;
        }

        private void turnButton_Click(object sender, EventArgs e)
        {
            if(turnButton.Text == "X")
            {
                turnButton.Text = "O";
            }
            else
            {
                turnButton.Text = "X";
            }
        }
    }

    class Container
    {
        private int x;
        private int y;

        public Container(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
