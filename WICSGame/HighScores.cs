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
    public partial class HighScores : Form
    {
        int diff;
        String loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\Pictures\\";
        public HighScores(int difficulty)
        {
            InitializeComponent();
            diff = difficulty;
            if (diff == 0)
            {
                label1.Text = "Beginner High Scores";
            }
            else if (diff == 1)
            {
                label1.Text = "Intermediate High Scores";

            }
            else
            {
                label1.Text = "Advanced High Scores";
            }
        }

        private void HighScores_Load(object sender, EventArgs e)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(@loc + "" + diff + "HS.txt");
            String line;
            ArrayList scores = new ArrayList();
            while ((line = file.ReadLine()) != null)
            {
                scores.Add(line);
                
            }
            file.Close();
            for (int i = 0; i < scores.Count; i++)
            {
                String cur = "" + scores[i];
                int pos = cur.IndexOf(" ");
                String name = cur.Substring(0, pos).Trim();
                String score = cur.Substring(pos).Trim();
                String[] row = new String[]{name, score};
                int n = dataGridView1.Rows.Add(row);
            }
        }


    }
}
