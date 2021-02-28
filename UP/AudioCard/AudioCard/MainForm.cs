using System;
using System.IO;
using System.Windows.Forms;

namespace AudioCard
{
    public partial class MainForm : Form
    {
        bool playingWMP = false;
        bool pauseWMP = true;
        string fileAxWMP = null;

        public MainForm()
        {
            InitializeComponent();
        }

        //WAV|*.wav|MP3|*.mp3
        string GetFile(string filter)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Wybierz plik",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Filter = filter,
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
                return dialog.FileName;
            return null;
        }

        private void buttonPlaySound_Click(object sender, EventArgs e)
        {
            string file = GetFile("WAV|*.wav");
            if (file != null)
                AudioController.PlayNormal(file);
        }

        private void buttonPlayWMP_Click(object sender, EventArgs e)
        {
            string file = GetFile("WAV|*.wav|MP3|*.mp3");
            if (file != null)
            {
                AudioController.PlayWMP(file);
                playingWMP = true;
                pauseWMP = false;
            }
        }

        private void buttonPauseResumeWMP_Click(object sender, EventArgs e)
        {
            if(playingWMP)
            {
                if (pauseWMP)
                    AudioController.ResumeWMP();
                else
                    AudioController.PauseWMP();
                pauseWMP = !pauseWMP;
            }
        }

        private void buttonStopWMP_Click(object sender, EventArgs e)
        {
            AudioController.StopWMP();
            playingWMP = false;
        }

        private void buttonAxWMPGetFile_Click(object sender, EventArgs e)
        {
            fileAxWMP = GetFile("WAV|*.wav|MP3|*.mp3");
        }

        private void axWindowsMediaPlayer_Enter(object sender, EventArgs e)
        {
            if(fileAxWMP!=null)
            {
                axWindowsMediaPlayer.URL = fileAxWMP;
                axWindowsMediaPlayer.Ctlcontrols.play();
            }
        }

        private void buttonPlayDirect_Click(object sender, EventArgs e)
        {
            string file = GetFile("WAV|*.wav|MP3|*.mp3");
            if (file != null)
                AudioController.PlayDirect(file);
        }

        private void buttonShowWAV_Click(object sender, EventArgs e)
        {
        string file = GetFile("WAV|*.wav");
            if (file != null)
            {
                var hdr = AudioController.GetWavInfo(file);
                MessageBox.Show($"File size: {hdr.FileSize}\nFile format: {hdr.FileFormat}\nChunk ID: {hdr.ChunkID}\nChunk format: {hdr.ChunkFormat}\nSubchunk 1 ID: {hdr.Subchunk1ID}\nSubchunk 1 size: {hdr.Subchunk1Size}", "WAV Header Info");
            }
        }
    }
}
