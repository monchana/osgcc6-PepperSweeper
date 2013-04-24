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
    public partial class second : Form
    {
        public second()
        {
            InitializeComponent();
        }
        DateTime starttime;
        private void second_Load(object sender, EventArgs e)
        {
            String loc = Path.GetDirectoryName(Application.ExecutablePath) + "\\Pictures\\";
            System.Media.SoundPlayer sp = new System.Media.SoundPlayer(@loc + "tastyburger.wav");

            sp.Play();
            timer1.Start();
            starttime = DateTime.Now;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            int startmin = starttime.Minute;
            int startsec = starttime.Second;
            int nowmin = now.Minute;
            int nowsec = now.Second;
            nowmin = nowmin - startmin;
            nowsec = nowsec - startsec;
            nowsec += (nowmin * 60);
            if (nowsec >= 4)
            {
                mainmenu m = new mainmenu();
                m.Show();
                this.Close();
            }
        }
    }
}
