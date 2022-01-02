/*Idea comes from: https://www.youtube.com/watch?v=kwfc7QkCDZI */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace CountDownAutoShutdown
{
    public partial class Form1 : Form
    {
        int secondsSelected;
        int minutesSelected;
        int totalSecRemaining;

        TimeSpan timeDisplay;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i=0;i<60;i++)
            {
                this.comboBox1.Items.Add(i);
                this.comboBox2.Items.Add(i);
            }
            this.comboBox1.SelectedItem = 59;
            this.comboBox2.SelectedItem = 59;
            this.lbTime.Text = "Time";
            this.timer1.Interval = 1000;
        }

        private void btnShutDown_Click(object sender, EventArgs e)
        {
            this.secondsSelected = int.Parse(comboBox2.SelectedItem.ToString());
            this.minutesSelected = int.Parse(comboBox1.SelectedItem.ToString());

            this.totalSecRemaining = this.minutesSelected * 60 + this.secondsSelected;
            btnShutDown.Enabled = false;
            btnStopShut.Enabled = true;
            timer1.Start();
        }

        private void btnStopShut_Click(object sender, EventArgs e)
        {
            btnStopShut.Enabled = false;
            btnShutDown.Enabled = true;
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (totalSecRemaining > 0)
            {
                this.totalSecRemaining--;
                timeDisplay = new TimeSpan(0, totalSecRemaining / 60, totalSecRemaining % 60);
                lbTime.Text = this.timeDisplay.ToString();
            }
            else
            {
                timer1.Stop();
                MessageBox.Show("Time up!!!\n\aWe 're shutting down right now!!!");
                System.Media.SoundPlayer audier = new System.Media.SoundPlayer();
                audier.Play();
                Process.Start("shutdown", "/s /t 0");
            }

        }
    }
}
