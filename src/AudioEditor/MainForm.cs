using System;
using System.IO;
using System.Diagnostics;
using System.Resources;
using System.Windows.Forms;
using DllLog;

namespace AudioEditor
{
    public partial class MainForm : Form
    {
        #region Fields

        private static ResourceManager LocRM;

        #endregion //Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
            SetString();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
        }

        #endregion //Constructor

        /// <summary>
        /// MainForm_Load
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeAudioEditor();
        }

        /// <summary>
        /// Initialize AudioEditor 
        /// </summary>
        private void InitializeAudioEditor()
        {
            Process p = null;
            try
            {
                p = new Process();
                p.StartInfo.FileName = @"Wavosaur.exe";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                p.WaitForExit();
                if (p.ExitCode != 0)
                {
                    Log.LogError(p.StandardOutput.ReadLine());
                }
                Log.LogPass("Wavosaur Pass");
                Program.ExitApplication(0);
            }
            catch (Exception ex)
            {
                PitchLbl.Text = LocRM.GetString("Error");
                Log.LogError(ex.ToString());
            }
            finally
            {
                if (p != null)
                    p.Close();
            }
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PassBtn_Click(object sender, EventArgs e)
        {
            Log.LogPass("Wavosaur Pass");
            Program.ExitApplication(0);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void FailBtn_Click(object sender, EventArgs e)
        {
            Log.LogPass("Wavosaur Fail");
            Program.ExitApplication(255);
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = $"{LocRM.GetString("AudioEditor")} {LocRM.GetString("Test")}";
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
        }
    }
}
