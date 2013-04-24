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
    public partial class winner : Form
    {
        int difficutly;
        String timetaken;
        public winner(String time, int d)
        {
            InitializeComponent();
            label2.Text = "You took " + time + " seconds";
            difficutly = 0;
            timetaken = time;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (difficutly == 1)
            {

                Form1 f = new Form1();
                f.Show();
                this.Close();
            }
            else if (difficutly == 0)
            {
                beginner b = new beginner();
                b.Show();
                this.Close();
            }
            else
            {
                advanced a = new advanced();
                a.Show();
                this.Close();
            }
        }
        private void winner_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            advanced a = new advanced();
            a.Show();
            this.Hide();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (difficutly == 1)
            {

                Form1 f = new Form1();
                f.Show();
                this.Close();
            }
            else if (difficutly == 0)
            {
                beginner b = new beginner();
                b.Show();
                this.Close();
            }
            else
            {
                advanced a = new advanced();
                a.Show();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\Pictures\\";
            System.IO.StreamReader file = new System.IO.StreamReader(@loc + "" + difficutly  + "HS.txt");
            String line;
            ArrayList scores = new ArrayList();
            while ((line = file.ReadLine()) != null)
            {
                scores.Add(line);

            }
            file.Close();
            ArrayList highscores = new ArrayList();
            for (int i = 0; i < scores.Count; i++)
            {
                String cur = "" + scores[i];
                int pos = cur.IndexOf(" ");
                String name = cur.Substring(0, pos).Trim();
                String score = cur.Substring(pos).Trim();
                highscores.Add(name);
                highscores.Add(score);
            }
            String username = txtName.Text;
            username.Replace(" ", "_");
            for (int x = 0; x < highscores.Count; x+=2)
            {
                String name = highscores[x].ToString();
                String score = highscores[x + 1].ToString();
                int theirscore = int.Parse(score);
                int yourtime = int.Parse(timetaken);
                if (yourtime < theirscore)
                {
                    highscores.Insert(x, username);
                    highscores.Insert(x + 1, timetaken);
                    x += highscores.Count;
                }
            }
            using(StreamWriter write = new StreamWriter(loc + "" + difficutly + "HS.txt"))
            {
                for (int i = 0; i < highscores.Count; i+= 2)
                {
                    write.WriteLine(highscores[i] + " " + highscores[i + 1]);
                }
            }
            lbldone.Text = "Ok, you have been added.";
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            info i = new info();
            i.Show();
        }
    }
}
