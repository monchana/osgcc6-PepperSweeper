using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace WICSGame
{
    public partial class advanced : Form
    {
        public advanced()
        {
            InitializeComponent();
        }
        private void advanced_Load(object sender, EventArgs e)
        {
            initial = Image.FromFile(loc + "square.jpg");
            flag = Image.FromFile(loc + "flag.jpg");
            mine = Image.FromFile(loc + "peppermine.jpg");
            numbackground = Image.FromFile(loc + "numbackground.jpg");
            makeBoard();
            makeButtons();
        }
        int[,] board;//board, 1 for mine, 0 for not mine
        String loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\Pictures\\";
        int difficulty = 2;
        int buttonsize = 30;
        int xsize = 30;
        int ysize = 16;
        DateTime starttime;
        int squaresleft = 0;
        bool timerstarted = false;//has the timer been already set
        bool over = false;
        bool rightclick = false;
        int mineshidden;
        Image initial;
        Image flag;
        Image numbackground;
        Image mine;
        public void makeButtons()
        {

            int xspaceneeded = xsize * buttonsize;
            int yspaceneeded = ysize * buttonsize;
            this.Height = yspaceneeded + 150;
            this.Width = xspaceneeded + 100;
            for (int r = 0; r < xsize; r++)//display buttons
            {
                for (int c = 0; c < ysize; c++)
                {
                    Button newButton = new Button();
                    newButton.Height = 30;
                    newButton.Width = 30;
                    newButton.Left = (r * 30) + 50;
                    newButton.Top = (c * 30) + 100;
                    newButton.Click += new EventHandler(ButtonClicked);
                    newButton.Image = initial;
                    newButton.BackColor = Color.Transparent;
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.FlatAppearance.BorderSize = 0;
                    newButton.Name = "" + r + "_" + c;
                    this.Controls.Add(newButton);
                }
            }//end of create buttons for loop'
        }
        public void makeBoard()//display all the buttons for the board
        {
            gametimer.Stop();
            foreach (Button btn in this.Controls.OfType<Button>())
            {

                btn.Enabled = true;
                btn.Text = "";
                btn.Image = initial;
            }
            timerstarted = false;
            over = false;
            btexit.Text = "Exit";
            btReset.Text = "New Game";
            btexit.Image = null;
            btReset.Image = null;
            lbltimer.Text = "0";
            int nummines = 0;
            xsize = 0;
            ysize = 0;
            if (difficulty == 1)
            {
                nummines = 40;
                xsize = 16;
                ysize = 16;
            }
            else if (difficulty == 0)
            {
                nummines = 10;
                xsize = 9;
                ysize = 9;
            }
            else
            {
                nummines = 99;
                xsize = 30;
                ysize = 16;
            }
            mineshidden = nummines;
            lblmines.Text = "" + mineshidden;
            squaresleft = (xsize * ysize) - nummines;
            board = new int[xsize, ysize];
            this.Top = 20;
            this.Left = 20;

            int minesset = 0;
            while (minesset < nummines)//set mines
            {
                Random random = new Random();
                int xspot = random.Next(xsize);
                int yspot = random.Next(ysize);
                int minespot = board[xspot, yspot];
                if (minespot == 0)
                {
                    board[xspot, yspot] = 1;
                    minesset++;
                }
            }//end set mines loop
            //squaresleft = 3;
        }//end of set board method
        public void ButtonClicked(object sender, EventArgs e)
        {
            if (!timerstarted)
            {
                gametimer.Start();
                starttime = DateTime.Now;
                timerstarted = true;
            }
            if (!over)
            {

                Button button = sender as Button;
                if (ckcFlag.Checked)
                {
                    if (button.Image == flag)
                    {
                        button.Image = initial;
                        mineshidden++;
                    }
                    else
                    {
                        button.Image = flag;
                        mineshidden--;
                    }
                    lblmines.Text = "" + mineshidden;
                }
                else
                {

                    if (button.Image != flag)
                    {
                        int x = (button.Location.X - 50) / 30;
                        int y = (button.Location.Y - 100) / 30;

                        int todo = Game(x, y);
                        if (todo == 9)
                        {
                            gametimer.Stop();
                            over = true;
                            button.Image = null;
                            button.BackgroundImage = mine;
                            button.BackgroundImageLayout = ImageLayout.Stretch;
                            //button.BackColor = Color.SandyBrown;
                            //button.Text = "M";
                            button.Enabled = false;
                        }
                        else
                        {
                            squaresleft--;
                            if (squaresleft == 0)
                            {//They've won
                                gametimer.Stop();
                                winner form = new winner(lbltimer.Text, difficulty);

                                form.Show();
                                this.Hide();
                            }
                            button.Text = "" + todo;
                            button.Font = new Font(button.Font.FontFamily.Name, 10);
                            button.Font = new Font(button.Font, FontStyle.Bold);
                            button.Image = null;
                            button.BackgroundImage = numbackground;

                            button.Enabled = false;
                            //update based on return value
                        }
                    }
                }
            }
        }//end of button clicked method

        public int Game(int x, int y)
        {
            //timer set
            int space = board[x, y];
            if (space == 0)//NOT MINE
            {
                int minesadjacent = 0;
                minesadjacent += checkSquare(x - 1, y - 1);
                minesadjacent += checkSquare(x, y - 1);
                minesadjacent += checkSquare(x + 1, y - 1);
                minesadjacent += checkSquare(x + 1, y);
                minesadjacent += checkSquare(x + 1, y + 1);
                minesadjacent += checkSquare(x, y + 1);
                minesadjacent += checkSquare(x - 1, y + 1);
                minesadjacent += checkSquare(x - 1, y);
                return minesadjacent;
            }
            else//MINE
            {
                return 9;
            }
            //mines left set
            //if button clicked is mine
            //imgage pepper
            //game over display
            //show stats
            //timer stop

            //if button clicked is number
            //calculate number
            //add up adjacent numbers
            //display number

            //if flagged
            //toggle flag
            //display flag
            return 0;
        }
        public int checkSquare(int x, int y)
        {
            int z = 0;
            if (x < 0 || x >= xsize || y < 0 || y >= ysize)//if it it ouside the board
            {
                return 0;
            }
            else
            {
                z = board[x, y];
            }
            return z;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            beginner f = new beginner();
            f.Show();
            this.Hide();
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeBoard();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            makeBoard();
        }

        private void btexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gametimer_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int startmin = starttime.Minute;
            int startsec = starttime.Second;
            int nowmin = now.Minute;
            int nowsec = now.Second;
            nowmin = nowmin - startmin;
            nowsec = nowsec - startsec;
            nowsec += (nowmin * 60);
            lbltimer.Text = "" + nowsec;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeBoard();
        }

        private void highScoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HighScores hs = new HighScores(difficulty);
            hs.Show();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            info i = new info();
            i.Show();
        }

    }
}
