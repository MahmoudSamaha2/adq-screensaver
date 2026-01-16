using System;
using System.Windows.Forms;
using AxWMPLib;

namespace ScreensaverPlayer
{
    public class MainForm : Form
    {
        private AxWindowsMediaPlayer player;
        private string videoPath = "adq screensaver.mp4";

        public MainForm(string[] args)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.Load += MainForm_Load;
            this.KeyDown += (s, e) => this.Close();
            this.MouseMove += (s, e) => this.Close();
            this.MouseClick += (s, e) => this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            player = new AxWindowsMediaPlayer();
            player.Dock = DockStyle.Fill;
            player.uiMode = "none";
            player.URL = videoPath;
            player.settings.setMode("loop", true);
            this.Controls.Add(player);
        }
    }
}
