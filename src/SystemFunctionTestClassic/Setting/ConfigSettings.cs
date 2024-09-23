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
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace win81FactoryTest.Setting
{

    /// <summary>
    /// ConfigSettings: Reads data from SFTCnfig.xml file
    /// </summary>
    static class ConfigSettings
    {
        static string CurrentPhase = "";
        static string ConfigLang = "en-US"; //default English

        /// <summary>
        /// Gets language code from Config file
        /// </summary>
        public static bool GetAutotest ()
        {
            string xmlPath = Program.TestSettingsFile;
            bool result = false;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode autotestNode = xmlDoc.SelectSingleNode(@"/SystemFunctionalTest/AutoTest");
                if (autotestNode != null)
                {
                    string temp = autotestNode.Attributes["StopAtFail"].Value;
                    if (temp.Length > 0)
                        bool.TryParse(temp, out result);
                }
            }
            catch (IOException)
            {
                Log.LogError("GetLang: Cannot read XML file: " + xmlPath);
            }
            catch (Exception e)
            {
                Log.LogError("GetLang: " + e.ToString());
            }
            return result;
        }
        
        /// <summary>
        /// Gets language code from Config file
        /// </summary>
        public static string GetLang()
        {
            string xmlPath = Program.TestSettingsFile;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode langNode = xmlDoc.SelectSingleNode(@"/SystemFunctionalTest/Language");
                if (langNode != null)
                {
                    ConfigLang = langNode.Attributes["Name"].Value;
                }
            }
            catch (IOException)
            {
                Log.LogError("GetLang: Cannot read XML file: " + xmlPath);
            }
            catch (Exception e)
            {
                Log.LogError("GetLang: " + e.ToString());
            }
            return ConfigLang;
        }

        /// <summary>
        /// Gets all phases from Config file
        /// </summary>
        public static string[] GetAllPhase()
        {
            string xmlPath = Program.TestSettingsFile;
            string[] result = new string[0];

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNodeList phaseList = xmlDoc.SelectNodes(@"/SystemFunctionalTest/Phase");
                result = new string[phaseList.Count];
                for (int i = 0; i < phaseList.Count; i++)
                {
                    result[i] = phaseList[i].Attributes["Name"].Value;
                }
            }
            catch (IOException)
            {
                Log.LogError("GetAllPhase: Cannot read XML file: " + xmlPath);
            }
            catch (Exception e)
            {
                Log.LogError("GetAllPhase: " + e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Gets all test item from given phase [phaseName]
        /// </summary>
        public static string[] GetTestSettingPhase(string phaseName) {

            CurrentPhase = phaseName;

            string xmlPath = Program.TestSettingsFile;
            string[] result = new string[0];
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNodeList phaseList = xmlDoc.SelectNodes(@"/SystemFunctionalTest/Phase");
                for (int i = 0; i< phaseList.Count; i++) {
                    if (phaseList[i].Attributes["Name"].Value.Equals(phaseName))
                    {
                        XmlNodeList menuNode = phaseList[i].SelectNodes(@"TestMenu/MenuItem");
                        result = new string[menuNode.Count];
                        for (int j = 0; j < menuNode.Count; j++)
                        {
                            result[j] = menuNode[j].Attributes["Name"].Value;
                        }
                    }
                }
            }
            catch (IOException)
            {
                Log.LogError("GetTestSettingPhase: Cannot read XML file: " + xmlPath);
            }
            catch (Exception e)
            {
                Log.LogError("GetTestSettingPhase: " + e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Gets test argument from given test name [testName]
        /// </summary>
         public static string GetTestArguments(string testName)
        {
            string xmlPath = Program.TestSettingsFile;
            string result = string.Empty;

            string lang = ConfigLang;
            if (lang.Length < 1)
            {
                lang = "en-US";
            }

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNodeList phaseList = xmlDoc.SelectNodes(@"/SystemFunctionalTest/Phase");
                for (int i = 0; i < phaseList.Count; i++)
                {
                    if (phaseList[i].Attributes["Name"].Value.Equals(CurrentPhase))
                    {
                        XmlNodeList menuNode = phaseList[i].SelectNodes(@"TestMenu/MenuItem");
                        for (int j = 0; j < menuNode.Count; j++)
                        {
                            if (menuNode[j].Attributes["Name"].Value.Equals(testName))
                            {
                                //Add parser to combine a single version of SFTConfig.xml file to cover both classic version and UWP app
                                switch (testName)
                                {
                                    case "Battery":
                                    {
                                        XmlNode argNode = menuNode[j].SelectSingleNode(@"Threshold");
                                        if (argNode != null)
                                        {
                                            XmlAttribute attr = (null == argNode) ? null : argNode.Attributes["MinCapacity"];
                                            result = (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                @" ""NULL""" : @" """ + attr.Value + @"""";
                                        }
                                        break;
                                    }
                                    case "Touch":
                                    {
                                        int multiPointCount = 2;
                                        int multiDrawCount = 1;
                                        XmlNodeList propertyNodes = menuNode[j].SelectNodes(@"Property");
                                        for (int k = 0; null != propertyNodes && k < propertyNodes.Count; k++)
                                        {
                                            XmlAttribute mpcAttr = propertyNodes[k].Attributes["MultiPointCount"];
                                            int mpc = (null == mpcAttr) ? 0 : Convert.ToInt32(mpcAttr.Value);
                                            multiPointCount = (mpc >= 2 && mpc <= 10) ? mpc : multiPointCount;
                                            XmlAttribute mdcAttr = propertyNodes[k].Attributes["MultiDrawCount"];
                                            int mdc = (null == mdcAttr) ? 0 : Convert.ToInt32(mdcAttr.Value);
                                            multiDrawCount = (mdc >= 1 && mdc <= 3) ? mdc : multiDrawCount;
                                        }
                                        result = @" """ + multiPointCount + @"""";
                                        result += @" """ + multiDrawCount + @"""";
                                        break;
                                    }
                                    case "FrontCamera":
                                    {
                                            XmlNodeList colorTestNodes = menuNode[j].SelectNodes(@"BackColor");
                                            XmlAttribute attr = colorTestNodes[0].Attributes["Color"];
                                            result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                "" : @" """ + attr.Value + @"""";
                                            result = (0 == result.Length) ? @"" : result;
                                            break;
                                    }
                                    case "RearCamera":
                                    {
                                            XmlNodeList colorTestNodes = menuNode[j].SelectNodes(@"BackColor");
                                            XmlAttribute attr = colorTestNodes[0].Attributes["Color"];
                                            result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                "" : @" """ + attr.Value + @"""";
                                            result = (0 == result.Length) ? @"" : result;
                                            break;
                                    }
                                    case "FrontCameraRec":
                                    {
                                            XmlNodeList colorTestNodes = menuNode[j].SelectNodes(@"BackColor");
                                            XmlAttribute attr = colorTestNodes[0].Attributes["Color"];
                                            result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                "" : @" """ + attr.Value + @"""";
                                            result = (0 == result.Length) ? @"" : result;
                                            break;
                                    }
                                    case "RearCameraRec":
                                    {
                                            XmlNodeList colorTestNodes = menuNode[j].SelectNodes(@"BackColor");
                                            XmlAttribute attr = colorTestNodes[0].Attributes["Color"];
                                            result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                "" : @" """ + attr.Value + @"""";
                                            result = (0 == result.Length) ? @"" : result;
                                            break;
                                    }
                                    case "Display":
                                    {
                                            XmlNodeList colorTestNodes = menuNode[j].SelectNodes(@"ColorTest");
                                            for (int k = 0; null != colorTestNodes && k < colorTestNodes.Count; k++)
                                            {
                                                XmlAttribute attr = colorTestNodes[k].Attributes["Color"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    "" : @" """ + attr.Value + @"""";
                                            }
                                            result = (0 == result.Length) ? @"" : result;
                                            break;
                                        }
                                    case "WiFi":
                                        {
                                            XmlNode node = menuNode[j].SelectSingleNode(@"ConnectionName");
                                            result += (null == node || null == node.InnerText || 0 == node.InnerText.Length) ?
                                                @" ""NULL""" : @" """ + node.InnerText + @"""";
                                            int signalStrength = 50;
                                            XmlNodeList thresholdNodes = menuNode[j].SelectNodes(@"Threshold");
                                            for (int k = 0; null != thresholdNodes && k < thresholdNodes.Count; k++)
                                            {
                                                XmlAttribute attr = thresholdNodes[k].Attributes["SignalQuality"];
                                                int sq = (null == attr) ? 0 : Convert.ToInt32(attr.Value);
                                                signalStrength = (sq >= 0 && sq <= 100) ? sq : signalStrength;
                                            }
                                            result += @" """ + signalStrength + @"""";
                                            XmlNodeList avaliableNameNodes = menuNode[j].SelectNodes(@"AvailableName");
                                            for (int k = 0; null != avaliableNameNodes && k < avaliableNameNodes.Count; k++)
                                            {
                                                node = avaliableNameNodes[k];
                                                if (0 != node.InnerText.Length)
                                                    result += @" """ + node.InnerText + @"""";
                                                /*
                                                result += (null == node || null == node.InnerText || 0 == node.InnerText.Length) ?
                                                    @" "" """ : @" """ + node.InnerText + @"""";
                                                */
                                            }
                                            break;
                                        }
                                    case "Bluetooth":
                                        {
                                            XmlNode node = menuNode[j].SelectSingleNode(@"ScanTime");
                                            if (node != null)
                                                result += GetResultString(node);

                                            XmlNodeList avaliableNameNodes = menuNode[j].SelectNodes(@"AvailableName");
                                            for (int k = 0; null != avaliableNameNodes && k < avaliableNameNodes.Count; k++)
                                            {
                                                node = avaliableNameNodes[k];

                                                if (0 != node.InnerText.Length)
                                                    result += @" """ + node.InnerText + @"""";
                                                /*
                                                result += (null == node || null == node.InnerText || 0 == node.InnerText.Length) ?
                                                     @" "" """ : @" """ + node.InnerText + @"""";
                                                */
                                            }

                                            break;
                                        }
                                    case "GPS":
                                        {
                                            XmlNode argNode = menuNode[j].SelectSingleNode(@"Threshold");
                                            if (argNode != null)
                                            {
                                                XmlAttribute attr = argNode.Attributes["Tolerance"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argNode) ? null : argNode.Attributes["LocationMatch"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argNode) ? null : argNode.Attributes["AllowedMaxPosFixTime"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                            }
                                            break;
                                        }
                                    case "Speaker":
                                    case "Headset":
                                        {
                                            string path = null;
                                            XmlNodeList nodes = menuNode[j].SelectNodes(@"Property");
                                            for (int k = 0; null != nodes && k < nodes.Count; k++)
                                            {
                                                XmlAttribute attr = nodes[k].Attributes["AudioOutSource"];
                                                path = (null == attr) ? path : attr.Value;
 
                                            }
                                            result = (null == path || 0 == path.Length) ? @" ""NULL""" : @" """ + path + @"""";
                                            break;
                                        }
                                    case "Keypad":
                                        {
                                            string path = null;
                                            XmlNodeList nodes = menuNode[j].SelectNodes(@"Property");
                                            for (int k = 0; null != nodes && k < nodes.Count; k++)
                                            {
                                                XmlAttribute attr = nodes[k].Attributes["RepeatCount"];
                                                path = (null == attr) ? path : attr.Value;

                                            }
                                            result = (null == path || 0 == path.Length) ? @" ""NULL""" : @" """ + path + @"""";

                                            nodes = menuNode[j].SelectNodes(@"Button");
                                            for (int k = 0; null != nodes && k < nodes.Count; k++)
                                            {
                                                XmlNode node = nodes[k];
                                                result += (null == node || null == node.InnerText || 0 == node.InnerText.Length) ?
                                                    "" : @" """ + node.InnerText + @"""";

                                            }

                                            break;
                                        }
                                    case "Keyboard":
                                        {

                                            XmlNodeList nodes = menuNode[j].SelectNodes(@"Key");
                                            for (int k = 0; null != nodes && k < nodes.Count; k++)
                                            {
                                                XmlNode node = nodes[k];
                                                XmlAttribute attr = (null == node) ? null : node.Attributes["Name"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length || 0 == attr.Value.Length) ?
                                                    "" : @" """ + attr.Value + @"""";
                                            }

                                            break;
                                        }
                                    case "ExtDisplay":
                                        {
                                            XmlNodeList nodes = menuNode[j].SelectNodes(@"Filename");
                                            for (int k = 0; null != nodes && k < nodes.Count; k++)
                                            {
                                                XmlNode node = nodes[k];
                                                result += (null == node || null == node.InnerText || 0 == node.InnerText.Length) ?
                                                    "" : @" """ + node.InnerText + @"""";
                                            }
                                            result = (0 == result.Length) ? @" ""NULL""" : result;
                                            break;
                                        }
                                    case "Informacoes":
                                        {
                                            XmlNode node = menuNode[j].SelectSingleNode(@"BIOS_VERSION");
                                            if (node != null)
                                                result += GetResultString(node);
                                            node = menuNode[j].SelectSingleNode(@"DISK_SIZE");
                                            if (node != null)
                                                result += GetResultString(node);
                                            node = menuNode[j].SelectSingleNode(@"MEMORY_SIZE");
                                            if (node != null)
                                                result += GetResultString(node);
                                            node = menuNode[j].SelectSingleNode(@"CPU_TYPE");
                                            if (node != null)
                                                result += GetResultString(node);
                                            node = menuNode[j].SelectSingleNode(@"DISK_SIZE_TOLERANCE_PERCENTAGE");
                                            if (node != null)
                                                result += GetResultString(node);

                                            break;
                                        }
                                    case "Light":
                                        {
                                            XmlNode argnode = menuNode[j].SelectSingleNode(@"Threshold");
                                            if (argnode != null)
                                            {
                                                XmlAttribute attr = argnode.Attributes["MinLux"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argnode) ? null : argnode.Attributes["MinWaitTime"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                            }
                                            break;
                                        }
                                    case "Accelerometer":
                                        {
                                            XmlNode argnode = menuNode[j].SelectSingleNode(@"Threshold");
                                            if (argnode != null)
                                            {
                                                XmlAttribute attr = argnode.Attributes["AccelerationZ"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argnode) ? null : argnode.Attributes["AccelerationY"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argnode) ? null : argnode.Attributes["AccelerationX"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argnode) ? null : argnode.Attributes["MinWaitTime"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                            }
                                            break;
                                        }
                                    case "Gyrometer":
                                        {
                                            XmlNode argnode = menuNode[j].SelectSingleNode(@"Threshold");
                                            if (argnode != null)
                                            {
                                                XmlAttribute attr = argnode.Attributes["AngularVelocityX"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argnode) ? null : argnode.Attributes["AngularVelocityZ"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argnode) ? null : argnode.Attributes["AngularVelocityY"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argnode) ? null : argnode.Attributes["MinWaitTime"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argnode) ? null : argnode.Attributes["Tolerance"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                            }
                                            break;
                                        }
                                    case "LAN":
                                        {
                                            XmlNode node;
                                            XmlNode argNode = menuNode[j].SelectSingleNode(@"Threshold");
                                            if (argNode == null)
                                                result = @" ""NULL""";
                                            else
                                            {
                                                XmlAttribute attr = argNode.Attributes["MaxTimeOut"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                            }

                                            XmlNodeList avaliableNameNodes = menuNode[j].SelectNodes(@"IPAddress");
                                            for (int k = 0; null != avaliableNameNodes && k < avaliableNameNodes.Count; k++)
                                            {
                                                node = avaliableNameNodes[k];

                                                if (0 != node.InnerText.Length)
                                                    result += @" """ + node.InnerText + @"""";
                                            }

                                            break;
                                        }
                                    case "SerialPort":
                                        {
                                            XmlNode argNode = menuNode[j].SelectSingleNode(@"Threshold");
                                            if (argNode == null)
                                                result = @" ""NULL""";
                                            else
                                            {
                                                XmlAttribute attr = argNode.Attributes["MinWaitTime"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                            }

                                            XmlNode node = menuNode[j].SelectSingleNode(@"BaudRate");
                                            if (node != null)
                                                result += GetResultString(node);


                                            XmlNodeList avaliableNameNodes = menuNode[j].SelectNodes(@"ComPort");
                                            for (int k = 0; null != avaliableNameNodes && k < avaliableNameNodes.Count; k++)
                                            {
                                                node = avaliableNameNodes[k];

                                                if (0 != node.InnerText.Length)
                                                    result += @" """ + node.InnerText + @"""";
                                            }

                                            break;
                                        }
                                    case "Biometric":
                                        {
                                            XmlNode argnode = menuNode[j].SelectSingleNode(@"Threshold");
                                            if (argnode != null)
                                            {
                                                XmlAttribute attr = argnode.Attributes["MinWaitTime"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                /*
                                                XmlAttribute attr = argnode.Attributes["SwipeCount"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                attr = (null == argnode) ? null : argnode.Attributes["MinWaitTime"];
                                                result += (null == attr || null == attr.Value || 0 == attr.Value.Length) ?
                                                    @" "" """ : @" """ + attr.Value + @"""";
                                                */
                                            }
                                            break;
                                        }
                                    case "IRCamera":
                                        {
                                            XmlNode node = menuNode[j].SelectSingleNode(@"IRRefImage");
                                            if (node != null)
                                                result += GetResultString(node);

                                            XmlNodeList avaliableNameNodes = menuNode[j].SelectNodes(@"IRSubImage");
                                            for (int k = 0; null != avaliableNameNodes && k < avaliableNameNodes.Count; k++)
                                            {
                                                node = avaliableNameNodes[k];

                                                if (0 != node.InnerText.Length)
                                                    result += @" """ + node.InnerText + @"""";
                                            }
                                            XmlNodeList avaliableNameNodes_1 = menuNode[j].SelectNodes(@"FrameServerSupport");
                                            for (int k = 0; null != avaliableNameNodes_1 && k < avaliableNameNodes_1.Count; k++)
                                            {
                                                node = avaliableNameNodes_1[k];

                                                if (0 != node.InnerText.Length)
                                                    result += @" """ + node.InnerText + @"""";
                                            }

                                            break;
                                        }
									  case "Upload":
                                        {
                                            break;

                                        }
                                    default:
                                    {
                                        XmlNodeList argNodes = menuNode[j].ChildNodes;
                                        for (int k = 0; k < argNodes.Count; k++)
                                        {
                                            if (argNodes[k].NodeType != XmlNodeType.Comment)
                                            {
                                                result += (argNodes[k].InnerText.Length > 0) ? (@" """ + argNodes[k].InnerText + @"""") : @" ""NULL""";
                                            }

                                        }
                                        break;
                                    }
                                }
                                return lang + result;
                            }
                        }
                    }
                }
            }
            catch (IOException)
            {
                Log.LogError("GetTestArguments: Cannot read XML file: " + xmlPath);
            }
            catch (Exception e)
            {
                Log.LogError("GetTestArguments: " + e.ToString());
            }

            return lang + result;
        }

        /// <summary>
        /// Gets String from xml file
        /// </summary>
        public static string GetResultString(XmlNode _xmlnode)
        {
            string xmlstr = string.Empty; ;
            if (_xmlnode.InnerText.Length > 0)
                xmlstr = @" """ + _xmlnode.InnerText + @"""";
            else
                xmlstr = @" ""NULL""";

            return xmlstr;
        }
        /// <summary>
        /// Gets location path to save results xml file
        /// </summary>
        public static string GetResultPath()
        {
            string xmlPath = Program.TestSettingsFile;
            string result = string.Empty;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode settingNode = xmlDoc.SelectSingleNode(@"/SystemFunctionalTest/TestSettings/ResultFile");
                result = settingNode.InnerText;
            }
            catch (IOException)
            {
                Log.LogError("GetResultPath: Cannot read XML file: " + xmlPath);
            }
            catch (Exception e)
            {
                Log.LogError("GetResultPath: " + e.ToString());
            }

            return result;
        }

        /// <summary>
        /// Gets location path to the execution file of test [testName]
        /// </summary>
        public static string GetTestExes(string testName)
        {
            string xmlPath = Program.TestSettingsFile;
            string result = string.Empty;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlPath);
                XmlNode settingNode = xmlDoc.SelectSingleNode(@"/SystemFunctionalTest/TestPath/" + testName);
                result = settingNode.InnerText;
            }
            catch (IOException)
            {
                Log.LogError("GetTestExes: Cannot read XML file: " + xmlPath);
            }
            catch (Exception e)
            {
                Log.LogError("GetTestExes: " + e.ToString());
            }
            return result;
        }
    }
}
