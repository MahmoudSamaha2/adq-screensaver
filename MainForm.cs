using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreensaverPlayer
{
    public class MainForm : Form
    {
    private string gifResourceName = "ScreensaverPlayer.sc.gif";
    private PictureBox pictureBox;
    private System.IO.MemoryStream gifStream;

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
            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Black
            };
            this.Controls.Add(pictureBox);

            var stream = typeof(MainForm).Assembly.GetManifestResourceStream(gifResourceName);
            if (stream != null)
            {
                // Copy to a MemoryStream and keep it alive
                byte[] gifBytes;
                using (var ms = new System.IO.MemoryStream())
                {
                    stream.CopyTo(ms);
                    gifBytes = ms.ToArray();
                }
                gifStream = new System.IO.MemoryStream(gifBytes);
                pictureBox.Image = Image.FromStream(gifStream);
            }
            else
            {
                MessageBox.Show("Could not find embedded GIF resource.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                gifStream?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
