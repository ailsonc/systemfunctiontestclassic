using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace Led
{
    public partial class MainForm : Form
    {
        #region Fields

        private static ResourceManager LocRM;
        private dynamic ws = Microsoft.VisualBasic.Interaction.CreateObject("WScript.Shell", "");
        private ArrayList lista = new ArrayList();
        private Random rand = new Random();
        private int count = 0;
        private int[] arrayInt;
        private List<int> arrayIntTest = new List<int>();
        public Boolean AutoPass;

        #endregion

        public MainForm()
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
            SetString();
            SetConfigKeys();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
            InitializeLed();
        }

        /// <summary>
        /// Initialize Led Test
        /// </summary>
        private void InitializeLed()
        {
            this.arrayInt = this.arrayIntTest.Select(x => new { x, r = rand.Next() })
                                             .OrderBy(x => x.r)
                                             .Select(x => x.x)
                                             .ToArray();

            this.senKeys(this.arrayInt[this.arrayIntTest.Count() - 1]);
        }

        /// <summary>
        /// senKeys
        /// </summary>
        /// <param name="number">int.</param>
        private void senKeys(int number)
        {
            turnOFF();
            switch (number)
            {
                case 1:
                    this.ws.SendKeys("{CAPSLOCK}");
                    break;
                case 2:
                    this.ws.SendKeys("{NUMLOCK}");
                    break;
            }
        }

        /// <summary>
        /// turnOFF
        /// </summary>
        private void turnOFF()
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                this.ws.SendKeys("{CAPSLOCK}");
            }
            if (Control.IsKeyLocked(Keys.NumLock))
            {
                this.ws.SendKeys("{NUMLOCK}");
            }
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
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = $"{LocRM.GetString("Led")} {LocRM.GetString("Test")}";
            MessageLB.Text = LocRM.GetString("LedMessage");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
        }

        /// <summary>
        /// Enable configurable keys from program arguments
        /// </summary>
        private void SetConfigKeys()
        {
            if (Program.ProgramArgs == null || Program.ProgramArgs.Length < 1)
            {
                return;
            }

            for (int i = 0; i < Program.ProgramArgs.Length; i++)
            {
                SetArrayTest(Program.ProgramArgs[i]);
                Control[] _control = this.Controls.Find(Program.ProgramArgs[i], true);
                if (_control.Length > 0)
                {
                    Control c = _control[0];
                    c.Visible = true;
                    //HiddenKeys--;
                }
            }
        }

        private void SetArrayTest(string ProgramArgs)
        {
            switch (ProgramArgs)
            {
                case "capslock":
                    this.arrayIntTest.Add(1);
                    break;
                case "numlock":
                    this.arrayIntTest.Add(2);
                    break;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            string _key = e.KeyCode.ToString();
            Debug.WriteLine(_key);

            if (e.KeyCode == Keys.ShiftKey)
            {
                switch (_key)
                {
                    case "D1":
                        this.arrayIntTest.Add(1);
                        break;
                    case "D2":
                        this.arrayIntTest.Add(2);
                        break;
                }
            }
        }

        private void capslock_Click(object sender, EventArgs e)
        {

        }
    }
}
