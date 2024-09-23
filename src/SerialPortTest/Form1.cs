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
using System.Globalization;
using System.Resources;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.IO.Ports;
using DllLog;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Serial
{
    public partial class Form1 : Form
    {
        private static ResourceManager LocRM;
        System.Timers.Timer _timer = new System.Timers.Timer();

        SerialPort _serialport;
        public Boolean IsAutoPass = false;
        CancellationTokenSource cts;
        public string SendStr = "";
        public string ReceiveStr = "";
        string[] _COMList;

        int BaudRate = 9600;
        int MinWait = 0;

        bool g_isCompleted = false;
        bool g_testResult = false;

        DateTime time_start;
        DateTime time_end;

        int g_ComLength = 0;
        int g_TestIndex = 0;


        public struct TestResult
        {
            public string Name;
            public bool isPass;
        };
        TestResult[] tResult;

        /// <summary>
        /// Initializes a new instance of the Form1 form class.
        /// </summary>
        public Form1(string[] SerialPara)
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly);
            InitializeComponent();
            SetString();

            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;

            //SerialPara sample: "3" "9600" "COM1" "COM2" "COM3"
            if (SerialPara != null && SerialPara.Length >= 3)
            {

                if (SerialPara[0].ToString().ToUpper() == "NULL")//Program.ProgramArgs[0].ToString().ToUpper() == "NULL"
                    MinWait = 3;
                else
                    MinWait = Int32.Parse(SerialPara[0].ToString());

                if (SerialPara[1].Length > 0)
                    BaudRate = Int32.Parse(SerialPara[1].ToString());


                //Items index 3 and later are all COMList
                if (SerialPara[2].Length > 0 && (!SerialPara[2].Contains("NULL")))
                {
                    _COMList = new string[SerialPara.Length - 2];
                    Array.Copy(SerialPara, 2, _COMList, 0, SerialPara.Length - 2);
                }
            }

            //Init tResult array
            if (_COMList != null)
            {
                g_ComLength = _COMList.Length;
                tResult = new TestResult[_COMList.Length];
                for(int i =0; i < tResult.Length; i++)
                {
                    tResult[i].Name = _COMList[i].ToString().ToUpper();
                    tResult[i].isPass = false;
                    if (SerialPort.GetPortNames().Any(x => string.Compare(x, tResult[i].Name, true) == 0))
                        tResult[i].isPass = true;
                }                           
            }

        }


        /// <summary>
        /// Initialize SerialPort settings
        /// </summary>
        private async void InitializeSerialPort()
        {
            try
            {
                //Set Timer to update Serial port 
                _timer.Interval = 500;
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateSerialPort);

                if (Program.ProgramArgs == null || Program.ProgramArgs.Length == 0)
                {
                    PassBtn.Visible = false;
                    Log.LogError("SerialTest Fail: Please provide correct value in SFTConfig");
                    return;
                }
                else if (Program.ProgramArgs[0].ToString().ToUpper() == "NULL")
                {
                    //Autopass not enabled, display buttons
                    PassBtn.Visible = true;
                    FailBtn.Visible = true;
                    RetryBtn.Visible = true;

                    IsAutoPass = false;
                }
                else
                {
                    PassBtn.Visible = false;
                    FailBtn.Visible = false;
                    RetryBtn.Visible = false;

                    IsAutoPass = true;
                }

                if (tResult != null)
                    await Task.Run(() => { TestSerialPort(); });


            }
            catch (Exception ex)
            {
                //If exception exists, set all array to false
                SetAllComList(false);
                Log.LogError(ex.ToString());
            }
            finally
            {
                //check all test result
                if (IsAutoPass)
                {
                    if(Is_All_Pass())
                        Program.ExitApplication(0);
                    else
                    {
                        ShowSerialResult();
                        Program.ExitApplication(255);
                    }
                }
                else
                    UpdateTestResult();
            }
        }

        /// <summary>
        /// Update Serial port status and check timing
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void UpdateSerialPort(object sender, EventArgs e)
        {
            time_end = DateTime.Now;

            if (((TimeSpan)(time_end - time_start)).TotalSeconds > Convert.ToDouble(MinWait))
            {
                _timer.Stop();
                cts.Cancel();
            }
        }


        private void ShowSerialResult()
        {
            foreach (TestResult testitem in tResult)
            {
                if (testitem.isPass == false)
                    Log.LogError("SerialPort Auto Test fail: " + "Port - " + testitem.Name);
            }
        }

        private bool PortTest(String name, CancellationToken token)
        {
            bool result = false;
            try
            {
                _serialport = new SerialPort(name, BaudRate, Parity.None, 8, StopBits.One);
                //_serialport.ReadTimeout = MinWait;
                _serialport.DataReceived += Comport_DataReceived;

                if (!_serialport.IsOpen)
                    _serialport.Open();

                SendStr = "";//Clean SendStr
                SendStr = name;

                time_start = DateTime.Now;
                _timer.Start();

                Task.Run(() =>
                {
                    _serialport.DiscardOutBuffer();
                    _serialport.DiscardInBuffer();
                    _serialport.WriteLine(SendStr);

                });


                while (true)
                {
                    if(token.IsCancellationRequested)
                    {
                        if (g_isCompleted)
                        {
                            if(g_testResult)
                                result = true;
                        }
                            
                        if (_serialport.IsOpen)
                            _serialport.Close();

                        _timer.Close();
                        break;
                    }
                }
            }
            catch
            {
                Log.LogError("PortTest fail: " + "Port - " + name);
            }

            return result;
        }

        private void TestSerialPort()
        {
            try
            {
                int jump_id = -1;
                bool result = true;
                for (g_TestIndex = 0; g_TestIndex < g_ComLength;)
                {
                    if (tResult[g_TestIndex].isPass == true)
                    {
                       
                        if (jump_id != g_TestIndex)
                        {
                            jump_id = g_TestIndex;
                            if (cts != null)
                                cts = null;
                            cts = new CancellationTokenSource();
                            g_isCompleted = false;

                            result = PortTest(tResult[g_TestIndex].Name, cts.Token);
                        }

                        if (result == false)
                            tResult[g_TestIndex].isPass = false;

                        g_TestIndex++;
                    }
                    else
                    {
                        _timer.Close();
                        g_TestIndex++;
                    }                        
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex.ToString());
            }          
        }

        private void Comport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if ((sender as SerialPort).BytesToRead > 0)
            {
                try
                {
                    ReceiveStr = (sender as SerialPort).ReadLine();
                    g_testResult = false;

                    /*Compare string*/
                    if (SendStr == ReceiveStr)
                        g_testResult = true;

                }
                catch (TimeoutException timeoutEx)
                {
                    Log.LogError(timeoutEx.Message);
                }
                catch (Exception ex)
                {
                     Log.LogError(ex.Message);
                }
            }

            g_isCompleted = true;
            cts.Cancel();

        }

        // Set all array to false
        private void SetAllComList(bool tresult)
        {
            if (tResult == null)
                return;

            for (int i = 0; i < tResult.Length; i++)
                tResult[i].isPass = tresult;
        }

        private bool Is_All_Pass()
        {
            bool result = true;
            for (int i = 0; i < tResult.Length; i++)
            {
                if(tResult[i].isPass == false)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
       
        private void UpdateTestResult()
        {
            string passstr = "";
            string failstr = "";
            passlabel.Text = "";
            faillabe.Text = "";

            foreach (TestResult testitem in tResult)
            {
                if (testitem.isPass)
                    passstr += testitem.Name + "; ";
                else
                    failstr += testitem.Name + "; ";
            }

            passlabel.Text = passstr;
            faillabe.Text = failstr;
        }
                         
        /// <summary>
        /// Control.Click Event handler. Where control is the Retry button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void RetryBtn_Click(object sender, EventArgs e)
        {
            passlabel.Text = "";
            faillabe.Text = "";

            //Init tResult array
            if (_COMList != null)
            {
                tResult = new TestResult[_COMList.Length];
                for (int i = 0; i < tResult.Length; i++)
                {
                    tResult[i].Name = _COMList[i].ToString().ToUpper();
                    tResult[i].isPass = false;
                    if (SerialPort.GetPortNames().Any(x => string.Compare(x, tResult[i].Name, true) == 0))
                        tResult[i].isPass = true;
                }
            }

            InitializeSerialPort();
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Pass button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(0);
        }

        /// <summary>
        /// Control.Click Event handler. Where control is the Fail button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            Program.ExitApplication(255);
        }


        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        private void SetString()
        {
            Title.Text = LocRM.GetString("SerialPort") + LocRM.GetString("Test");
            this.Text = LocRM.GetString("SerialPort");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            RetryBtn.Text = LocRM.GetString("Retry");
            PassResultLab.Text = LocRM.GetString("SerialPort_Pass");
            FailResultLab.Text = LocRM.GetString("SerialPort_Fail");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeSerialPort();
        }

    }

}

