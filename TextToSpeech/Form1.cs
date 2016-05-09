using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.IO; 

namespace TextToSpeech
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer reader;
        public Form1()
        {
            InitializeComponent();
            pauseButton.Enabled = false;
            reader = new SpeechSynthesizer();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            play();
        }

        //event handler 
        void reader_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            startButton.Text = "Start";
            pauseButton.Enabled = false;
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (reader != null)
            {
                if (reader.State == SynthesizerState.Paused)
                {
                    reader.Resume();
                    pauseButton.Enabled = true;
                    pauseButton.Text = "Pause";
                }
                else if (reader.State == SynthesizerState.Speaking)
                {
                    reader.Pause();
                    pauseButton.Enabled = true;
                    pauseButton.Text = "Resume";

                }
            }
        }

        private void play()
        {
            reader.Dispose();
            if (mainTextBox.Text != "")
            {
                startButton.Text = "Replay";
                pauseButton.Text = "Pause";
                reader = new SpeechSynthesizer();
                reader.SpeakAsync(mainTextBox.Text.ToString().Trim());
                pauseButton.Enabled = true;
                reader.SpeakCompleted += new EventHandler<SpeakCompletedEventArgs>(reader_SpeakCompleted);
            }
            else
            {
                MessageBox.Show("Please enter some text in the textbox", "Message", MessageBoxButtons.OK);
            }
        }
    }
}
