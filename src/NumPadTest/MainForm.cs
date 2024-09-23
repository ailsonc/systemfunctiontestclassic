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

namespace NumPadTest
{
    public partial class MainForm : Form
    {
        #region Fields
        private Boolean isShiftKey = false;
        private static int CountKeys = 0;
        #endregion //Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
        }

        #endregion //Constructor

        /// <summary>
        /// Event that occurs when the main form of an application is loaded.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetCountKeys();
        }


        /// <summary>
        /// Counter related to keyboard keys.
        /// </summary>
        private void SetCountKeys()
        {
            foreach (Control control in this.tableLayoutPanel1.Controls)
            {
                if (control.Visible == true && control.BackColor != Color.DodgerBlue && control.BackColor != Color.Gray)
                {
                    CountKeys++;
                }
            }

            Log.LogStart($"CountKeys: {CountKeys}");
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

            if ((e.KeyCode == Keys.ShiftKey))
            {
                isShiftKey = true;
            }
            else
            if ((e.KeyCode == Keys.D5))
            {
                if (isShiftKey)
                {
                    isShiftKey = false;
                    this.ShiftKey.BackColor = (this.ShiftKey.BackColor == Color.Green ? Color.Red : Color.Green);
                }
            } 
            else
            {
                Control[] _control = base.Controls.Find(_key, true);
                if (_control.Length != 0)
                {
                    Control c = _control[0];
                    c.BackColor = (c.BackColor == Color.Green ? Color.Red : Color.Green);
                }
            }

            CheckPass();
        }

        /// <summary>
        /// Check result
        /// </summary>
        private void CheckPass()
        {
            int countGreenControls = 0;
            foreach (Control control in this.tableLayoutPanel1.Controls)
            {
                if (control.BackColor == Color.Green)
                {
                    countGreenControls++;
                }
            }

            if (CountKeys == countGreenControls)
            {
                Log.LogStart($"CountGreenControls: {countGreenControls}");
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
    }
}

