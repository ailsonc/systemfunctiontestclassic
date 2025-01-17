﻿//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using DllComponent;
using DllLog;
using System;
using System.Globalization;
using System.Management;
using System.Windows.Forms;

namespace GenerateSystemSettings
{
    public partial class SystemSettings : Form
    {
        private const string flaotingValue = "1";

        public SystemSettings()
        {
            InitializeComponent();
            InitializeSystemInfo();
            InitializeCameraDevice();
        }

        /// <summary>
        /// Initialize SystemInfo config string
        /// </summary>
        private void InitializeSystemInfo()
        {
            //Sample:
            //<BIOS_VERSION> LENOVO INKT26AUS, 8 / 22 / 2014 </BIOS_VERSION>
            //<DISK_SIZE> 931GB </DISK_SIZE>
            //<MEMORY_SIZE> 4GB </MEMORY_SIZE>
            //<CPU_TYPE> Intel(R) Core(TM) i5 - 4460T CPU @ 1.90GHz </CPU_TYPE>
            //<DISK_SIZE_TOLERANCE_PERCENTAGE> 6 </DISK_SIZE_TOLERANCE_PERCENTAGE>

            this.SystemInfoConfig.Text = String.Format(
                "<BIOS_VERSION>{0}</BIOS_VERSION>\r\n<DISK_SIZE>{1}</DISK_SIZE>\r\n<MEMORY_SIZE>{2}</MEMORY_SIZE>\r\n<CPU_TYPE>{3}</CPU_TYPE>\r\n<DISK_SIZE_TOLERANCE_PERCENTAGE>{4}</DISK_SIZE_TOLERANCE_PERCENTAGE>",
                 GetBios(), GetDiskSize(), GetRAM(), GetCPU(), flaotingValue
                );
           
            Log.LogComment(Log.LogLevel.Info, this.SystemInfoConfig.Text);
        }

        /// <summary>
        /// Initialize Camera config string
        /// </summary>
        private void InitializeCameraDevice()
        {
            CoreComponent component = null;
            string cameraDevices = "";

            //get camera friendly names from 
            try
            {
                component = new CoreComponent();
                cameraDevices = component.GetCameraDevice();
            }
            catch (Exception ex)
            {
                Log.LogError("Cannot get Camera devices: " + ex.ToString());
            }
            finally
            {
                if (component != null)
                {
                    component.Dispose();
                    component = null;
                }
            }
            CameraConfig.Text = cameraDevices;
        }

        /// <summary>
        /// Gets system Bios info.
        /// Return: (string) Bios info
        /// </summary>
        private static string GetBios()
        {
            string biosManufacturer = "";
            string biosVersion = "";
            DateTime biosReleaseDate = DateTime.Now;

            System.Management.ManagementObjectSearcher search1 = new ManagementObjectSearcher("select * from Win32_BIOS");

            foreach (ManagementObject obj in search1.Get())
            {
                biosManufacturer = obj["Manufacturer"].ToString();
                biosVersion = obj["SMBIOSBIOSVersion"].ToString();
                string releaseDate = obj["ReleaseDate"].ToString().Substring(0, 8);
                biosReleaseDate = DateTime.ParseExact(releaseDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            }

            string result = string.Format("{0} {1}, {2}", biosManufacturer, biosVersion, biosReleaseDate.ToString("M/dd/yyyy"));
            return result;
        }


        /// <summary>
        /// Gets system CPU info.
        /// Return: (string) CPU info
        /// </summary>
        private static string GetCPU()
        {
            string cpuName = "";

            ManagementObjectSearcher search1 = new ManagementObjectSearcher("select * from Win32_Processor");

            foreach (ManagementObject obj in search1.Get())
            {
                cpuName = obj["Name"].ToString();

            }

            string result = cpuName;
            return result;
        }

        /// <summary>
        /// Gets system RAM info.
        /// Return: (string) RAM info
        /// </summary>
        private static string GetRAM()
        {
            ulong ramCapacity = 0;

            ManagementObjectSearcher search1 = new ManagementObjectSearcher("select * from Win32_PhysicalMemory");

            foreach (ManagementObject obj in search1.Get())
            {
                ramCapacity += (ulong)obj["Capacity"];
            }

            string result = (ramCapacity / (1024 * 1024 * 1024) + "GB").ToString();
            return result;
        }

        /// <summary>
        /// Gets system disk size
        /// Return: (string) system disk size
        /// </summary>
        private static string GetDiskSize()
        {
            ManagementObject disk = new
            ManagementObject(@"Win32_DiskDrive.DeviceID='\\.\PHYSICALDRIVE0'");
            disk.Get();

            string result = ((ulong)disk["Size"]) / (1024 * 1024 * 1024) + "GB";
            return result;
        }

    }
}
