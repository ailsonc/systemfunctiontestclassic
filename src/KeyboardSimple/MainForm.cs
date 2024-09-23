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
using KeyboardSimple.models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Management;
using System.Windows.Forms;

namespace KeyboardSimple
{
    public partial class MainForm : Form
    {
        #region Fields

        //Total number of keys
        private static int CountKeys = 0;

        List<Keys> lastKeyPressed = new List<Keys>();

        // OnKeyPressed
        private LowLevelKeyboardListener _listener;
        private bool ControlKey_KeyPressed = false;
        private bool isWin_KeyPressed = false;

        // TimerOnKeyPressed
        private Timer timerOnKeyPressed;

        private dynamic ws = Interaction.CreateObject("WScript.Shell", "");
        #endregion //Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            SetConfigKeys();
            SettingOnKeyPressed();

            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
        }

        #endregion //Constructor

        private void SettingOnKeyPressed()
        {
            timerOnKeyPressed = new Timer();
            timerOnKeyPressed.Interval = 1000;
            timerOnKeyPressed.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Timer expirou");
            this.Win.BackColor = (this.Win.BackColor == Color.Green ? Color.Red : Color.Green);
            isWin_KeyPressed = false;
            timerOnKeyPressed.Stop();
            CheckPass();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _listener = new LowLevelKeyboardListener();
            _listener.OnKeyPressed += _listener_OnKeyPressed;
            _listener.HookKeyboard();

            SetCountKeys();

            TurnON();
        } 

        private void TurnON()
        {
            if (!Control.IsKeyLocked(Keys.NumLock))
            {
                this.ws.SendKeys("{NUMLOCK}");
            }
        }

        private void SetCountKeys()
        {
            foreach (Control control in this.tableLayoutPanel1.Controls)
            {
                if (control.Visible == true && control.BackColor != Color.DodgerBlue && control.BackColor != Color.Gray)
                {
                    CountKeys++;
                }
            }

            InfoKeys();
        }

        private void InfoKeys(int approved = 0)
        {
            lbInfo.Text = $"Total de teclas: {CountKeys} \nTotal aprovados: {approved}";
            Log.LogStart($"CountKeys: {CountKeys}");
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
                Control[] _control = this.Controls.Find(Program.ProgramArgs[i], true);
                if (_control.Length > 0)
                {
                    Control c = _control[0];
                    c.Visible = true;
                }
            }
        }

        /// <summary>
        /// Control.KeyDown Event handler. When a key is pressed down, remove key from the form.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            string _key = e.KeyCode.ToString();

            Debug.WriteLine(_key);

            // Test keyCode
            //MessageBox.Show(_key);

            if (e.KeyCode == Keys.ShiftKey)
            {
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.LShiftKey)))
                {
                    this.Shift_L.BackColor = (this.Shift_L.BackColor == Color.Green ? Color.Red : Color.Green);
                }
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.RShiftKey)))
                {
                    this.Shift_R.BackColor = (this.Shift_R.BackColor == Color.Green ? Color.Red : Color.Green);
                }
            }
            else if (e.KeyCode == Keys.ControlKey)
            {
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.RControlKey)))
                {
                    this.Ctrl_R.BackColor = (this.Ctrl_R.BackColor == Color.Green ? Color.Red : Color.Green);
                }
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.LControlKey)) && ControlKey_KeyPressed.Equals(true))
                {
                    this.Ctrl_L.BackColor = (this.Ctrl_L.BackColor == Color.Green ? Color.Red : Color.Green);
                }
                ControlKey_KeyPressed = false;
            }
            else if (e.KeyCode == Keys.Menu)
            {
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.LMenu)))
                {
                    this.Alt_L.BackColor = (this.Alt_L.BackColor == Color.Green ? Color.Red : Color.Green);
                }
                if (Convert.ToBoolean(GetAsyncKeyState(Keys.RMenu)))
                {
                    this.Alt_R.BackColor = (this.Alt_R.BackColor == Color.Green ? Color.Red : Color.Green);
                }
            }
            else if ((e.KeyCode == Keys.LWin ? false : e.KeyCode != Keys.RWin))
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

            InfoKeys(countGreenControls);

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

        void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            String keycode = e.KeyPressed.ToString();

            switch (keycode)
            {
                case "HotKey1":
                    Log.LogStart($"OnKeyPressed: {keycode}");
                    this.GloboPlay.BackColor = (this.GloboPlay.BackColor == Color.Green ? Color.Red : Color.Green);
                    CheckPass();
                    break;
                case "HotKey2":
                    Log.LogStart($"OnKeyPressed: {keycode}");
                    this.Youtube.BackColor = (this.Youtube.BackColor == Color.Green ? Color.Red : Color.Green);
                    CheckPass();
                    break;
                case "HotKey3":
                    Log.LogStart($"OnKeyPressed: {keycode}");
                    this.ControlKey_KeyPressed = true;
                    CheckPass();
                    break;
                case "HotKey4":
                    Log.LogStart($"OnKeyPressed: {keycode}");
                    this.PrtScn.BackColor = (this.PrtScn.BackColor == Color.Green ? Color.Red : Color.Green);
                    CheckPass();
                    break;
                case "HotKey5":
                    Log.LogStart($"OnKeyPressed: {keycode}");
                    isWin_KeyPressed = true;
                    // Start or restart the timer
                    timerOnKeyPressed.Stop();
                    timerOnKeyPressed.Start();
                    CheckPass();
                    break;
                case "HotKey6":
                    Log.LogStart($"OnKeyPressed: {keycode}");
                    if (this.isWin_KeyPressed)
                    {
                        this.Menu.BackColor = (this.Menu.BackColor == Color.Green ? Color.Red : Color.Green);
                    }
                    this.isWin_KeyPressed = false;
                    timerOnKeyPressed.Stop();
                    CheckPass();
                    break;
                default:
                    Log.LogStart($"OnKeyPressed - Default: {keycode}");
                    CheckPass();
                    break;                
            }
        }
    }
}