using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Music_Turing_Machine
{
    public partial class ProgramForm : Form
    {
        RuntimeEnvironment env;
        private bool isAborted;
        public ProgramForm()
        {
            InitializeComponent();
            env = RuntimeEnvironment.GetInstance();
            isAborted = false;
        }
        private void buttonRun_Click(object sender, EventArgs e)
        {
            try
            {
                env.Clear();
                if (textBoxDuration.Text.Length != 0)
                {
                    int dur = Convert.ToInt32(textBoxDuration.Text);
                    env.SetDuration(dur);
                }                          
                env.PutSource(textBoxEditor.Text.ToCharArray());
                isAborted = false;
                if (!backgroundWorker1.IsBusy)
                    backgroundWorker1.RunWorkerAsync();
            }
            catch (FormatException)
            {
                textBoxEditor.Text += " Lol put number in Duration box";
            }
            catch (NullReferenceException)
            {
                textBoxEditor.Text += " Lol trouble with null pointer";
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            env.Start(ref isAborted);
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            isAborted = true;
        }

        private void textBoxEditor_TextChanged(object sender, EventArgs e)
        {

        }
    }
}