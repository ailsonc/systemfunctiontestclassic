using System;
using System.Management;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security;


namespace Chassis
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Chassis();
        }

        public void Chassis()
        {
            try
            {
                EventLog eventLog = new EventLog("Security");

                foreach (EventLogEntry entry in eventLog.Entries)
                {
                    if (entry.InstanceId == 601 || entry.InstanceId == 602)
                    {

                        MessageBox.Show("Evento: " + entry.EntryType);
                        MessageBox.Show("Descrição: " + entry.Message);
                        MessageBox.Show("Data e Hora: " + entry.TimeGenerated);
                    }
                }



                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Chassis");
                ManagementObjectCollection collection = searcher.Get();

                foreach (ManagementObject obj in collection)
                {
                    string chassisType = obj["ChassisTypes"].ToString();
                    int chassisStatus = (int)obj["ChassisStatus"];

                    if (chassisType == "3") // O valor "3" geralmente indica que o gabinete está aberto.
                    {
                        if (chassisStatus == 3)
                        {
                            MessageBox.Show("O gabinete está aberto.");
                            Console.WriteLine("O gabinete está aberto.");
                        }
                        else
                        {
                            MessageBox.Show("O gabinete pode estar aberto. O status não pode ser determinado com certeza.");
                            Console.WriteLine("O gabinete pode estar aberto. O status não pode ser determinado com certeza.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("O gabinete está fechado.");
                        Console.WriteLine("O gabinete está fechado.");
                    }
                }
            }
            catch (ManagementException e)
            {
                MessageBox.Show("Erro: " + e.Message);
                Console.WriteLine("Erro: " + e.Message);
            }
        }
    }
}
