//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Numberpad
{
    public partial class MainForm : Form
    {
        #region Fields
        private Boolean isShiftKey = false;
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
            } else 
            if ((e.KeyCode == Keys.D5))
            {
                if (!isShiftKey)
                    D5.Dispose();
                if (isShiftKey)
                {
                    isShiftKey = false;
                    ShiftKey.Dispose();
                }
            }
            else
            {
                Control[] _control = this.Controls.Find(_key, true);
                if (_control.Length > 0)
                {
                    Control c = _control[0];
                    if (c.Visible)
                        c.Dispose();
                }
            }

            if (tableLayoutPanel1.Controls.Count == 1)
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
    }
}

