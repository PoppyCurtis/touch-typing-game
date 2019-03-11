using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace typinggame
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Stats stats = new Stats();

        public Form1()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// This method runs when the timer ticks. This is originally every 800 milliseconds. After everytick, the eventhandler on timer1_Tick
        /// is fired. With every tick we add a random letter to the listbox1 items. If the listbox item count is above 7, all listbox items are 
        /// cleared, "game over" text appears and timer1 stops.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            listBox1.Items.Add((Keys)random.Next(65, 90));

            if (listBox1.Items.Count > 7)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Game over");
                timer1.Stop();
            }
        }

        /// <summary>
        /// This methods runs when a key is pressed/down, because the event handler fires each time this happens. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.Items.Contains(e.KeyCode))
            {
                listBox1.Items.Remove(e.KeyCode);
                listBox1.Refresh();
                if (timer1.Interval > 400)
                {
                    timer1.Interval -= 10;
                }
                if (timer1.Interval > 250)
                {
                    timer1.Interval -= 7;
                }
                if (timer1.Interval > 100)
                {
                    timer1.Interval -= 2;
                }
                difficultyProgressBar.Value = 800 - timer1.Interval;
                stats.Update(true);
            }
            else
            {
                stats.Update(false);

            }
            UpdateLabels();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TryAgain();
 
        }
        /// <summary>
        /// 
        /// </summary>
        private void TryAgain()
        {
            stats.Reset();
            timer1.Interval = 800;
            listBox1.Items.Clear();
            timer1.Start();
            UpdateLabels();
            difficultyProgressBar.Value = 0;
        }

        private void UpdateLabels()
        {
            correctLabel.Text = "Correct: " + stats.Correct;
            missedLabel.Text = "Missed: " + stats.Missed;
            totalLabel.Text = "Total " + stats.Total;
            accuracyLabel.Text = "Accuracy: " + stats.Accuracy + "%";
        }

    }
}
