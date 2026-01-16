using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreensaverPlayer
{
    public class MainForm : Form
    {
        private string gifResourceName = "ScreensaverPlayer.sc.gif";
        private PictureBox pictureBox;

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

            using (var stream = typeof(MainForm).Assembly.GetManifestResourceStream(gifResourceName))
            {
                if (stream != null)
                {
                    pictureBox.Image = Image.FromStream(stream);
                }
                else
                {
                    MessageBox.Show("Could not find embedded GIF resource.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
        }
    }
}
