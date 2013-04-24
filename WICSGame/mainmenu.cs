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
{//Written by Phillip Faust and Kathryn Lavelle
    public partial class mainmenu : Form
    {
        public mainmenu()
        {
            InitializeComponent();
        }
        String loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\Pictures\\";
        private void mainmenu_Load(object sender, EventArgs e)
        {

            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@loc + "Kalimba.wav");

            sp.PlayLooping();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            beginner b = new beginner();
            b.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            advanced a = new advanced();
            a.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
