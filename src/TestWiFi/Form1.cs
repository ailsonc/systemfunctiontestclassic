using DllComponent;
using DllLog;
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
using NativeWifi;
using System.Text;

namespace Wifi
{
    public partial class TestWifi : Form
    {
        private static ResourceManager LocRM;
        private static string[] PhyTypes = new string []
        { "Unknown/Any" , //0
            "2.4GHz; ", //1 , 802.11_FHSS 2.4GHz
            "2.4GHz; ", //2 , 802.11_DHSS 2.4GHz
            "Infrared (IR) baseband; ", //3 
            "5GHz; ", //4 , 802.11a_OFDM 5GHz
            "2.4GHz; ", //5, 802.11b_HRDSSS 2.4GHz
            "2.4GHz; ", //6, 802.11g_ERP 2.4GHz
            "2.4 or 5GHz; ", //7, 802.11n_HT 2.4 or 5GHz
            "5GHz; " //8 , 802.11ac_VHT 5GHz
        };

        string sConnectionName = "";
        bool bIfConnect = false;
        int iSignalSpec = 0;
        string[] _APList;

        int[] WifiListSignal = new int[64];
        string[] APScan = new string[64];
        string[] APType = new string[64];

        /// <summary>
        /// Initializes a new instance of the TestWifi form class.
        /// </summary>
        /// <param name="WiFiPara">WiFi AP lists and signal strength SPEC will need to be searched.</param>
        public TestWifi(string[] WiFiPara)
        {
            LocRM = new ResourceManager("win81FactoryTest.AppResources.Res", typeof(win81FactoryTest.TestForm).Assembly); // Set language
            InitializeComponent();
            SetString();

            //WifiPara sample: "MSFTCORP" "50" "MSFTCORP" "NOKIA"
            //Exemplo Wifi parâmetro: pt-BR NULL 70 PD MULTILASER
            if (WiFiPara != null && WiFiPara.Length >= 1)
            {
                if (WiFiPara[0].Length > 0 && (!WiFiPara[0].Contains("NULL")))
                {
                    sConnectionName = WiFiPara[0].Trim();
                    bIfConnect = true;
                }
            }
            if (WiFiPara != null && WiFiPara.Length >= 3)
            {
                if (WiFiPara[1].Length > 0 && (!WiFiPara[1].Contains("NULL")))
                iSignalSpec = Int32.Parse(WiFiPara[1].ToString(), CultureInfo.InvariantCulture);

                //Items index 2 and later are all APList
                if(WiFiPara[2].Length > 0 && (!WiFiPara[2].Contains("NULL")))
                {
                    _APList = new string[WiFiPara.Length - 2];
                    Array.Copy(WiFiPara, 2, _APList, 0, WiFiPara.Length - 2);
                }

            }
        }

        private void TestWiFi_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;//Full screen and no title
            this.WindowState = FormWindowState.Maximized;
            WifiInformation();
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
            Title.Text = LocRM.GetString("WiFi") + LocRM.GetString("Test");
            this.Text = LocRM.GetString("WiFi");
            PassBtn.Text = LocRM.GetString("Pass");
            FailBtn.Text = LocRM.GetString("Fail");
            RetryBtn.Text = LocRM.GetString("Retry");
        }
        /// <summary>
        /// Control.Click Event handler. Where control is the Retry button
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">The <see cref="object"/> instance containing the event data.</param>
        private void RetryBtn_Click(object sender, EventArgs e)
        {
            WifiInfoGrid.Rows.Clear();
            WifiInformation();
        }

        /// <summary>
        /// Initializes strings from SystemFunctionTestClass resources
        /// </summary>
        /// <param name="APList">WiFi AP list will need to be researched and signal strength SPEC. 
        /// If users configure the WiFi AP list and signal strength SPEC on SFTConfig.xml, this test will be auto-judge pass/fail.</param>
        private void WifiInformation()
        {
            bool result = true;
            string WiFiInfo;
            int APCheckCount = 0;
            int iWiFiCount = 0;
            CoreComponent component = null;

            //Use Component to gather Wifi info and Test Connection
            try
            {
                component = new CoreComponent();
                if (bIfConnect)
                {
                    WiFiInfo = component.TestWiFi(sConnectionName, Convert.ToInt32(bIfConnect));
                    Log.LogComment(Log.LogLevel.Info, "The SSID for wifi connection:" + sConnectionName);

                }
                else
                {
                    WiFiInfo = component.TestWiFi(null, Convert.ToInt32(bIfConnect));
                }

                if (WiFiInfo.IndexOf("WiFi_Connect_Fail", StringComparison.Ordinal) >= 0)
                {
                    result = false;
                    Log.LogComment(DllLog.Log.LogLevel.Error, "The SSID for wifi connection status : Fail");
                }                 
            }
            finally
            {
                if (component != null)
                {
                    component.Dispose();
                    component = null;
                }
            }

            //Parse Wifi info and add as row in WifiInfoGrid
            string[] apList = WiFiInfo.Split('\n'); //Split into each AP
            string[] apItemList;
            for (iWiFiCount = 0; iWiFiCount < apList.Length; iWiFiCount++) 
            {
                apItemList = apList[iWiFiCount].Split(','); //Split into each item of an AP
                for (int j = 0; j < apItemList.Length; j++)
                { 
                    switch (j) {
                        case 0:
                            APScan[iWiFiCount] = apItemList[j];
                            break;
                        case 1:
                            WifiListSignal[iWiFiCount] = Int32.Parse(apItemList[j]);
                            break;
                        case 2:
                            APType[iWiFiCount] = apItemList[j];
                            break;
                    }
                    
                }
            }
            ConvertPhyTypeToString();//Converts PhyType into readable info

            // Add wifi signal amd SSID into dataview object
            for (int i = 0; i < iWiFiCount; i++)   
            {
                DataGridViewRowCollection rows = WifiInfoGrid.Rows;
                rows.Add(new Object[] { WifiListSignal[i], APScan[i], APType[i] });
                Log.LogComment(Log.LogLevel.Info,"Signal = " + WifiListSignal[i] + " , SSID = " + APScan[i] + ", PhyType = " + APType[i]);
            }
            
            //Check if APList exists and passes strength test
            if (_APList != null && _APList.Length > 0)
            {
                for (int k = 0; k < _APList.Length; k++)
                {
                    for (int i = 0; i < iWiFiCount; i++)
                    {
                        if (APScan[i].IndexOf(_APList[k], StringComparison.Ordinal) >= 0 && WifiListSignal[i] > iSignalSpec)
                        {
                            //conectar wifi
                            string SSID = APScan[i];
                            ConnectToSSID(SSID);

                            //string profileXml = string.Format("<?xml version=\"1.0\"?><WLANProfile xmlns=\"http://www.microsoft.com/networking/WLAN/profile/v1\"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><MSM><security><authEncryption><authentication>open</authentication><encryption>WEP</encryption><useOneX>false</useOneX></authEncryption><sharedKey><keyType>networkKey</keyType><protected>false</protected><keyMaterial>{2}</keyMaterial></sharedKey><keyIndex>0</keyIndex></security></MSM></WLANProfile>", profileName, mac, key);

                            APCheckCount++;
                            break;
                        }
                    }
                }
                if (APCheckCount == _APList.Length)
                {
                    result = result && true;
                } else
                {
                    result = false;
                }
            }

            //If Auto-Pass tests were executed
            if (bIfConnect || (_APList != null && _APList.Length > 0))
            {
                if (result)
                    Program.ExitApplication(0);
                else
                    Program.ExitApplication(255);
            }

        }

        public void ConnectToSSID(string ssid)
        {
            // Connects to a known network with WEP security
            string profileName = ssid; // this is also the SSID
            string mac = StringToHex(profileName);
            string myProfileXML = string.Format("<? xml version =\"1.0\"?><WLANProfile xmlns =\"http://www.microsoft.com/networking/WLAN/profile/v1 Jump \"><name>{0}</name><SSIDConfig><SSID><hex>{1}</hex><name>{0}</name></SSID></SSIDConfig><connectionType>ESS</connectionType><connectionMode>manual</connectionMode><MSM><security><authEncryption><authentication>open</authentication><encryption>none</encryption><useOneX>false</useOneX></authEncryption></security></MSM></WLANProfile>", profileName, mac);

            WlanClient.WlanInterface wlanInterface = null;
            wlanInterface.SetProfile(Wlan.WlanProfileFlags.AllUser, myProfileXML, true);
            wlanInterface.Connect(Wlan.WlanConnectionMode.Profile, Wlan.Dot11BssType.Any, profileName);
        }

        public static string StringToHex(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.Default.GetBytes(str);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString().ToUpper());
        }

        private void ConvertPhyTypeToString()
        {
            string[] result = new string[] { };
            for (int i = 0; i < APType.Length; i++)
            {
                if (APType[i] == null) return;

                string temp = "";
                for(int j = 1; j < 9; j++)
                {
                    //Check is PhyType 1~8 exists
                    if (APType[i].Contains(j.ToString())) {
                        temp += PhyTypes[j];
                    }
                }
                APType[i] = temp;
            } 
        }

    }
}


