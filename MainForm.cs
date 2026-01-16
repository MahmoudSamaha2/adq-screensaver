using System;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace ScreensaverPlayer
{
    public class MainForm : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private VideoView _videoView;
    private string videoResourceName = "ScreensaverPlayer.adq screensaver.mp4";
    private string tempVideoPath;


        public MainForm(string[] args)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.Load += MainForm_Load;
            this.FormClosed += MainForm_FormClosed;
            this.KeyDown += (s, e) => this.Close();
            this.MouseMove += (s, e) => this.Close();
            this.MouseClick += (s, e) => this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Extract embedded video to temp file
            tempVideoPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ".mp4");
            using (var stream = typeof(MainForm).Assembly.GetManifestResourceStream(videoResourceName))
            using (var file = System.IO.File.OpenWrite(tempVideoPath))
            {
                stream.CopyTo(file);
            }

            Core.Initialize();
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);
            _videoView = new VideoView
            {
                MediaPlayer = _mediaPlayer,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(_videoView);

            var media = new Media(_libVLC, tempVideoPath, FromType.FromPath);
            _mediaPlayer.Play(media);
            _mediaPlayer.EndReached += (s, ev) =>
            {
                // Loop video
                _mediaPlayer.Stop();
                _mediaPlayer.Play(media);
            };
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Clean up temp file
            try
            {
                if (!string.IsNullOrEmpty(tempVideoPath) && System.IO.File.Exists(tempVideoPath))
                    System.IO.File.Delete(tempVideoPath);
            }
            catch { }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mediaPlayer?.Dispose();
                _libVLC?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
