//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using win81FactoryTest.Functions;
using win81FactoryTest.Setting;

using System.IO;


namespace win81FactoryTest
{
    public partial class TestForm : Form
    {
        bool is_upload_exist = false;
        #region Fields

        private static ResourceManager LocRM;
        private static bool isFinish;
        private static bool ifBT;
        private static bool ifStopAtFail; 
        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MainForm form class.
        /// </summary>
        public TestForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            Label.CheckForIllegalCrossThreadCalls = false;

            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(TestForm).Assembly);
            isFinish = false;
            InitlializeTests();
            SetString();
            ifStopAtFail = ConfigSettings.GetAutotest();
            CheckLogFile();
        }

        #endregion // Constructor

        /// <summary>
        /// Initialize test phases and load default phase
        /// </summary>
        private void InitlializeTests()
        {
            ClearMenuPanel(); //initial clear of panels
            //load Phase in dropdown 
            string[] phaseList = ConfigSettings.GetAllPhase();
            for (int i = 0; i < phaseList.Length; i++)
            {
                PhaseDropDown.Items.Add(phaseList[i]);
            }
            //load the menu list with first phase in xml (default)
            LoadMenuList(ConfigSettings.GetTestSettingPhase(phaseList[0]));
            LoadMenuList(ConfigSettings.GetTestSettingPhase(phaseList[1]));
            PhaseDropDown.SelectedIndex = 0;

            //Turn on BT radio
            ifBT = ExecuteTest.BTRadio(true);
        }

        /// <summary>
        /// Reads the Tests from config for the selected phase.
        /// Set the UI accordingly.
        /// </summary>
        private void LoadMenuList(string[] MenuList)
        {
            int length = MenuList.Length;
            if (MenuList.Length > this.MenuListTable.Controls.Count)
            {
                length = this.MenuListTable.Controls.Count;
            }

            for (int i = 0; i < length; i++)
            {
                Control c = this.MenuListTable.Controls[i];
                c.Name = MenuList[i];
                c.BackColor = Color.DimGray;
                c.Enabled = true;
                c.Text = LocRM.GetString(MenuList[i]);
                if (c.Name == "Upload")
                    is_upload_exist = true;
            }
            InitializeTestResults();
        }

        /// <summary>
        /// Load test results from system Registry.
        /// Set the UI accordingly.
        /// </summary>
        private void InitializeTestResults()
        {
            if (Program.SFTRegKey != null)
            {
                foreach (string keyID in Program.SFTRegKey.GetValueNames())
                {
                    string testResult = (string)Program.SFTRegKey.GetValue(keyID);
                    Control[] _control = MenuListTable.Controls.Find(keyID, true);
                    if (_control.Length > 0)
                    {
                        Control c = _control[0];
                        AnimateResult(c, Convert.ToBoolean(testResult));
                    }
                }
            }
        }

        /// <summary>
        /// Color each test menu depends on the result
        /// </summary>
        private static void AnimateResult(Control c, bool result)
        {
            if (result)
            {
                c.BackColor = Color.YellowGreen;
            }
            else
            {
                c.BackColor = Color.LightCoral;
            }
        }

        /// <summary>
        /// Clear all test results on the UI
        /// </summary>
        private void ClearMenuPanel()
        {
            for (int i = 0; i < this.MenuListTable.Controls.Count; i++)
            {
                Control panelControl = this.MenuListTable.Controls[i];
                panelControl.Name = String.Empty;
                panelControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                panelControl.Enabled = false;
                panelControl.Text = String.Empty;
            }
            isFinish = false;
        }

        /// <summary>
        /// Runs all available tests
        /// </summary>
        private void RunAllTests()
        {
            for (int i = 0; i < this.MenuListTable.Controls.Count; i++)
            {
                Control test = this.MenuListTable.Controls[i];
                if (!String.IsNullOrEmpty(test.Name))
                {
                    //If 'Run All' is not complete, skip the ones with results
                    if (!isFinish && ifStopAtFail &&
                       test.BackColor != Color.DimGray)
                    {
                        continue;
                    }
                    DateTime startTest = System.DateTime.Now;
                    bool testResult = ExecuteTest.Run(test.Name);
                    AddTestRegistry(test.Name, testResult);
                    AnimateResult(test, testResult);
                    //if test item fail: stop autorun (if ifStopAtFail == true)
                    if (!testResult && ifStopAtFail)
                    {
                        RunButton.Text = LocRM.GetString("Resume");
                        RunButton.BackColor = Color.YellowGreen;
                        return;
                    }
                }
            }
            //When Run All completes
            RunButton.Text = LocRM.GetString("RunAll");
            RunButton.BackColor = Color.YellowGreen;
            isFinish = true;
        }

        /// <summary>
        /// Beging Runs all available tests
        /// </summary>
        private void BeginRunAllTests()
        {
            for (int i = 0; i < this.MenuListTable.Controls.Count; i++)
            {
                Control test = this.MenuListTable.Controls[i];
                if (!String.IsNullOrEmpty(test.Name))
                {
                    //If 'Run All' is not complete, skip the ones with results
                    if (!isFinish && ifStopAtFail &&
                       test.BackColor == Color.YellowGreen)
                    {
                        continue;
                    }
                    DateTime startTest = System.DateTime.Now;
                    bool testResult = ExecuteTest.Run(test.Name);
                    AddTestRegistry(test.Name, testResult);
                    AnimateResult(test, testResult);
                    //if test item fail: stop autorun (if ifStopAtFail == true)
                    if (!testResult && ifStopAtFail)
                    {
                        RunButton.Text = LocRM.GetString("Resume");
                        RunButton.BackColor = Color.YellowGreen;
                        return;
                    }
                }
            }
            //When Run All completes
            RunButton.Text = LocRM.GetString("RunAll");
            RunButton.BackColor = Color.YellowGreen;
            isFinish = true;
        }

        /// <summary>
        /// Control.SelectedIndexChange Event handler. Where control is the Phase drop down menu.
        /// When phase is changed, redraw the UI.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void PhaseDropDown_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ClearMenuPanel();
            Control selected = (Control)sender;
            LoadMenuList(ConfigSettings.GetTestSettingPhase(selected.Text));

        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Run button.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void RunButton_Click_1(object sender, EventArgs e)
        {
            RunButton.BackColor = Color.LightCoral;
            RunAllTests();
        }

        /// <summary>
        /// Control.Click Event handler. Where control is a single test menu item.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void TestMenu_Click(object sender, EventArgs e)
        {
            Control test = (Control)sender;
            if (!String.IsNullOrEmpty(test.Name))
            {
                DateTime startTest = System.DateTime.Now;
                bool testResult = ExecuteTest.Run(test.Name);
                AddTestRegistry(test.Name, testResult);
                AnimateResult(test, testResult);
            }
        }

        /// <summary>
        /// Control.Click Event handler. Where control is a settings.
        /// Will Run app "GenerateSystemSettings"
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            ExecuteTest.Run("GenerateSystemSettings");
        }

        /// <summary>
        /// Control.Click Event handler. Where control is a single test menu item.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void ExitBtn_Click_1(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// Form.FormClosing Event handler.
        /// When this form closes, log the results.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void TestForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ifBT) ExecuteTest.BTRadio(false); //if BT was turned on, turn it off again
           
            if (Program.SFTRegKey!= null) Program.SFTRegKey.Close();
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the clear button.
        /// Clears results from Registry and redraws UI
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void ClearResultBtn_Click(object sender, EventArgs e)
        {
            ClearMenuPanel();
            DeleteRegistryResult();//Clear result from registry
            LoadMenuList(ConfigSettings.GetTestSettingPhase(PhaseDropDown.SelectedItem.ToString()));
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            RunButton.Text = LocRM.GetString("RunAll");
            ClearResultBtn.Text = LocRM.GetString("Clear");
            ExitBtn.Text = LocRM.GetString("Exit");
            SettingsBtn.Text = "\uE115";
        }

        /// <summary>
        /// Add test result to system's Registry
        /// </summary>
        private static void AddTestRegistry(string testID, bool result)
        {
            if (Program.SFTRegKey != null )
            {
                Program.SFTRegKey.SetValue(testID, result);
            }
            else
            {
                Program.SFTRegKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\SFTClassic");
                Program.SFTRegKey.SetValue(testID, result);
            }
        }

        /// <summary>
        /// Delete all results in system's Registry
        /// </summary>
        private static void DeleteRegistryResult()
        {
            if (Program.SFTRegKey != null)
            {
                Registry.CurrentUser.DeleteSubKeyTree(@"SOFTWARE\SFTClassic");
                Program.SFTRegKey = null;
            }
        }
        private void CheckLogFile()
        {  
            string filepath = Application.StartupPath + "\\";
            if ((is_upload_exist==true)&&(File.Exists(filepath + "\\" + "SFTClassicLog.txt")))
            {
                DialogResult result1 = MessageBox.Show("Need to delete an existing SFTClassicLog.txt ?", "Notice", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result1 == DialogResult.OK)
                    File.Delete(filepath + "\\" + "SFTClassicLog.txt");
            }
        }

    }
}
