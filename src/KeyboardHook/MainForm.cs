using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace KeyboardHook
{
    public partial class MainForm : Form
    {
        #region Fields

        private static ResourceManager LocRM;
        private Color corAtual = Color.Green;

        #endregion

        public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
            SetString();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = LocRM.GetString("Buttons");
            VolumeUP.Text = LocRM.GetString("VolumeUp");
            VolumeDown.Text = LocRM.GetString("VolumeDown");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PassBtn_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(0);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void FailBtn_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(255);
        }

        /// <summary>
        /// Exit the test with result = Pass
        /// </summary>
        private static void TestPass()
        {
            System.Threading.Thread.Sleep(1000);
            Program.ExitApplication(0);
        }

        private void MudarCorImagem(PictureBox pictureBox, Color color)
        {
            Bitmap imagem = new Bitmap(pictureBox.Image);
            for (int x = 0; x < imagem.Width; x++)
            {
                for (int y = 0; y < imagem.Height; y++)
                {
                    Color pixelColor = imagem.GetPixel(x, y);

                    if (pixelColor.A > 0)
                    {
                        imagem.SetPixel(x, y, color);
                    }
                }
            }
            pictureBox.Image = imagem;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.VolumeDown)
            {
                corAtual = this.VolumeDown.ForeColor == Color.Green ? Color.Red : Color.Green;
                this.VolumeDown.ForeColor = corAtual;
                MudarCorImagem(pictureBoxDown, corAtual);
            }
            else if (e.KeyCode == Keys.VolumeUp)
            {
                corAtual = this.VolumeUP.ForeColor == Color.Green ? Color.Red : Color.Green;
                VolumeUP.ForeColor = corAtual;
                MudarCorImagem(pictureBoxUp, corAtual);
            }

            if (this.VolumeDown.ForeColor.Equals(Color.Green) && this.VolumeUP.ForeColor.Equals(Color.Green))
            {
                TestPass();
            }
        }
    }
}
