using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WICSGame
{
    public partial class intro : Form
    {
        public intro()
        {
            InitializeComponent();
        }
        DateTime starttime;
        bool a = true;
        private void intro_Load(object sender, EventArgs e)
        {
            starttime = DateTime.Now;
            timer1.Start();
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
            if (nowsec == 5 && a)
            {
                second m = new second();
                m.Show();
                this.Hide();
                a = false;
            }
        }
    }
}
