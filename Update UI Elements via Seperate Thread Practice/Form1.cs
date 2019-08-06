using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Update_UI_Elements_via_Seperate_Thread_Practice
{    

    public partial class Form1 : Form
    {
        public delegate void delUpdateUITextBox(string text);

        ThreadStart threadstart;
        Thread myUpdateThread;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //instantiate thread start obj and pass in what method it will call
            threadstart = new ThreadStart(GetTheThreadStarted);

            myUpdateThread = new Thread(threadstart);

            myUpdateThread.Name = "Second Thread";

            myUpdateThread.Start();
        }

        //will get the thread started
        private void GetTheThreadStarted()
        {
            delUpdateUITextBox DelupdateUITextBox = new delUpdateUITextBox(UpdateUITextBox);

            this.textBox1.BeginInvoke(DelupdateUITextBox, "I was updated by from the thread: " + myUpdateThread.Name);
            

            for (int i = 0; i < 10; i++)
            {
                this.label1.BeginInvoke(DelupdateUITextBox, i.ToString());

                Thread.Sleep(1000);
            }
        }

        private void UpdateUITextBox(string text)
        {
            this.textBox1.Text = text;
            this.label1.Text = text;
        }
    }
}
