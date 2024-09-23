//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using DllLog;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace KeyboardNewTest
{
    public partial class MainForm : Form
    {
        #region Fields

        //Total number of hidden keys is 9
        private static int HiddenKeys = 12;
        private LowLevelKeyboardListener _listener;
        private String KeyboardListener;

        #endregion //Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            SetConfigKeys();

            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
        }

        #endregion //Constructor


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
                Control[] _control = this.Controls.Find(Program.ProgramArgs[i], true);
                if (_control.Length > 0)
                {
                    Control c = _control[0];
                    c.Visible = true;
                    HiddenKeys--;
                }
            }
        }

        /// <summary>
        /// Control.KeyDown Event handler. When a key is pressed down, remove key from the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            string _key = e.KeyCode.ToString();
            Debug.WriteLine(_key);

            if (e.KeyCode == Keys.ShiftKey)
            {
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.LShiftKey)))
                {
                    //Shift_L.Dispose();
                    Shift_L.BackColor = Shift_L.BackColor == Color.Green ? Color.Red : Color.Green;
                }
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.RShiftKey)))
                {
                    //if (Shift_R.Visible)
                    //    Shift_R.Dispose();
                    Shift_R.BackColor = Shift_R.BackColor == Color.Green ? Color.Red : Color.Green;
                }
            }
            else if (e.KeyCode == Keys.ControlKey)
            {
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.LControlKey)))
                {
                    //Ctrl_L.Dispose();
                    Ctrl_L.BackColor = Ctrl_L.BackColor == Color.Green ? Color.Red : Color.Green;
                }
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.RControlKey)))
                {
                    //if (Ctrl_R.Visible)
                    //    Ctrl_R.Dispose();
                    Ctrl_R.BackColor = Ctrl_R.BackColor == Color.Green ? Color.Red : Color.Green;
                }
            }
            else if (e.KeyCode == Keys.Menu)
            {
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.LMenu)))
                {
                    //Alt_L.Dispose();
                    Alt_L.BackColor = Alt_L.BackColor == Color.Green ? Color.Red : Color.Green;
                }
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.RMenu)))
                {
                    //if (Alt_R.Visible)
                    //    Alt_R.Dispose();
                    Alt_R.BackColor = Alt_R.BackColor == Color.Green ? Color.Red : Color.Green;
                }
            }
            else if (e.KeyCode == Keys.LWin || e.KeyCode == Keys.RWin)
            {
                //Win.Dispose();
                Win.BackColor = Win.BackColor == Color.Green ? Color.Red : Color.Green;
            }
            else
            {
                Control[] _control = this.Controls.Find(_key, true);

                if (_control.Length > 0)
                {
                    Control c = _control[0];
                    
                    c.BackColor = c.BackColor == Color.Green ? Color.Red : Color.Green;

                    /*
                    if (c.Visible)
                        c.Dispose();
                    */
                }
            }

            int countGreenControls = 0;

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control.BackColor == Color.Red || control.BackColor == Color.Black || control.BackColor == Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64))))))
                {
                    countGreenControls++;
                }
            }

            //MessageBox.Show("Quantidade" + countRedControls);
            /*
            var totalKeys = tableLayoutPanel1.Controls.Count - HiddenKeys;
            if (totalKeys == 2)
            {
                TestPass();
            }
            */
            var totalKeys = countGreenControls - HiddenKeys;
            if (totalKeys == 1)
            {
                TestPass();
            }

        }

        /// <summary>
        /// Exit the test with result = Pass
        /// </summary>
        private static void TestPass()
        {
            System.Threading.Thread.Sleep(1000);
            Program.ExitApplication(0);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Exit button. Test exits with result = Fail
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void ExitLabel_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(255);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Keys vKey);

        private void MainForm_Load(object sender, EventArgs e)
        {
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;
            _listener.HookKeyboard();
        }

        void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            this.KeyboardListener += e.KeyPressed.ToString();

            if (this.KeyboardListener.Contains("NETFLIX") && (this.KeyboardListener.Contains("OemPeriod") || this.KeyboardListener.Contains("OemQuestion")))
            {
                Log.LogStart($"OnKeyPressed: {this.KeyboardListener}");
                Netflix.Dispose();
                this.KeyboardListener = "";
            }
            if (this.KeyboardListener.Contains("PRIMEVIDEO"))
            {
                Log.LogStart($"OnKeyPressed: {this.KeyboardListener}");
                Prime.Dispose();
                this.KeyboardListener = "";
            }
            if (this.KeyboardListener.Contains("Return"))
            {
                this.KeyboardListener = "";
            }
        }
    }
}
