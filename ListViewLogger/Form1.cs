using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListViewLogger
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private int numberOfWorkers = 0;

        private void btnStart_Click(object sender, EventArgs e)
        {
            numberOfWorkers++;
            DisplayNumberOfWorkers();
            //btnStart.Enabled = false;
            tspbProgress.Value = 0;
            BackgroundWorker bw = new BackgroundWorker();
            bw.ProgressChanged += BackgroundWorker1_ProgressChanged;
            bw.DoWork += BackgroundWorker1_DoWork;
            bw.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;
            bw.WorkerReportsProgress = true;
            bw.RunWorkerAsync();

        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            numberOfWorkers--;
            tspbProgress.Value = 0;
            // btnStart.Enabled = true;
            DisplayNumberOfWorkers();
        }

        private void DisplayNumberOfWorkers()
        {
            tsslNumberOfWorkers.Text = numberOfWorkers == 0 ? "" : $"Workers {numberOfWorkers}";
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> text = new List<string>();
            text.Add("Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, ");
            text.Add("there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the");
            text.Add(" Semantics, a large language ocean. A small river named Duden flows by their place and supplies");
            text.Add(" it with the necessary regelialia. It is a paradisematic country, in which roasted parts of ");
            text.Add("sentences fly into your mouth. Even the all-powerful Pointing has no control about the blind ");
            text.Add("texts it is an almost unorthographic life One day however a small line of blind text by the name");
            text.Add(" of Lorem Ipsum decided to leave for the far World of Grammar. The Big Oxmox advised her not to ");
            text.Add("do so, because there were thousands of bad Commas, wild Question Marks and devious Semikoli, but ");
            text.Add("the Little Blind Text didn’t listen. She packed her seven versalia, put her initial into the belt");
            text.Add(" and made herself on the way. When she reached the first hills of the Italic Mountains, she had ");
            text.Add("a last view back on the skyline of her hometown Bookmarksgrove, the headline of Alphabet Village");
            text.Add(" and the subline of her own road, the Line Lane. Pityful a rethoric question ran over her cheek, then");

            var worker = sender as BackgroundWorker;
            var rnd = new Random(DateTime.Now.Millisecond);
            string startTime = DateTime.Now.Millisecond.ToString();
            int i = 0;
            int count = text.Count();
            foreach(string txt in text)
            {
                i++;
                int ticks = rnd.Next(500, 2000);
                Debug.WriteLine(ticks);
                Thread.Sleep(ticks);
                double progress = Math.Round((double)i / (double)count,2) * (double)100;
                worker.ReportProgress(Int32.Parse(progress.ToString()), $"[{startTime}] {i.ToString()} {txt}");
                
            }

            worker.ReportProgress(100, $"[{startTime}] Finished");
            
        }

        private void BackgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            DisplayNumberOfWorkers();
            tspbProgress.Value = e.ProgressPercentage;
            string message = (string)e.UserState;
            lbLogData.Items.Add($"[{DateTime.Now.ToString("o")}][{e.ProgressPercentage}] {message}");
            lbLogData.TopIndex = lbLogData.Items.Count - 1;
        }
    }
}
