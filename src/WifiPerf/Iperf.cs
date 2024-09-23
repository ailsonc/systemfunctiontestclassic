using DllLog;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WifiPerf
{
    class Iperf
    {
        private Label lb_transfer = new Label();
        private Label lb_bandwidth = new Label();
        private Chart chart_tb = new Chart();
        private Button PassBtn = new Button();
        private ArrayList transferList = new ArrayList();
        private ArrayList bandwidthList = new ArrayList();
        private double transferLimit, bandwidthLimit;

        public Iperf(Label lb_transfer, Label lb_bandwidth, Chart chart, Button passBtn, double transferLimit, double bandwidthLimit)
        {
            this.lb_transfer = lb_transfer;
            this.lb_bandwidth = lb_bandwidth;
            this.chart_tb = chart;
            this.PassBtn = passBtn;
            this.transferLimit = transferLimit;
            this.bandwidthLimit = bandwidthLimit;
        }

        public void Run()
        {
            try
            {
                string iperfPath = "iperf/iperf.exe";
                //string arguments = " -c 10.1.5.16 -P 1 -i 1 -p 5001 -f k -t 10 -R";
                string arguments = " -c 192.168.1.2 -P 1 -i 1 -p 5001 -f k -t 10 -R";

                PassBtnVisible(false);

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = iperfPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    int i = 1;

                    process.OutputDataReceived += (sender, e) =>
                    {
                        // Manipula os dados de saída aqui (pode ser um método separado)
                        if (e.Data != null)
                        {
                            string line = e.Data;

                            i = HandleOutputData(line, i);
                        }
                    };

                    process.EnableRaisingEvents = true;
                    process.Exited += (sender, e) =>
                    {
                        if(!ValidateData())
                        {
                            FailTest();
                        }
                        else
                        {
                            PassBtnVisible(true);
                        }                       
                    };

                    process.Start();

                    // Inicia a leitura assíncrona da saída
                    process.BeginOutputReadLine();

                    // Aguarda o término do processo
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private int HandleOutputData(string line, int i)
        {
            Console.WriteLine($"{line}");

            string patternKBytes = @"(\d+)\s+KBytes\s+(\d+)\s+Kbits\/sec";
            Match matchKBytes = Regex.Match(line, patternKBytes);

            if (matchKBytes.Success)
            {
                if (i <= 10)
                {
                    double transfer = (double.Parse(matchKBytes.Groups[1].Value) / 1.0); // KB to Bytes
                    double bandwidth = double.Parse(matchKBytes.Groups[2].Value);

                    AddTransfer(transfer.ToString());
                    AddBandwidth(bandwidth.ToString());

                    AddChat(0, i, transfer);
                    AddChat(1, i, bandwidth);

                    this.transferList.Add(transfer);
                    this.bandwidthList.Add(bandwidth);
                }
                
                i++;
            }

            string patternGBytes = @"(\d+)\s+GBytes\s+(\d+)\s+Kbits\/sec";
            Match matchKGBytes = Regex.Match(line, patternGBytes);

            if (matchKGBytes.Success)
            {
                double transfer = (double.Parse(matchKBytes.Groups[1].Value) * 1024.0); // KB to Bytes
                double bandwidth = double.Parse(matchKBytes.Groups[2].Value);

                AddTransfer(transfer.ToString());
                AddBandwidth(bandwidth.ToString());

                AddChat(0, i, transfer);
                AddChat(1, i, bandwidth);

                i++;
            }

            return i;
        }

        private bool ValidateData()
        {
            if (transferList.Count.Equals(0))
                return false;

            if (bandwidthList.Count.Equals(0))
                return false;

            for (int i = 0; i < transferList.Count; i++)
            {
                Log.LogComment(DllLog.Log.LogLevel.Info, $"Transfer {(double)transferList[i]}");
            }

            for (int i = 0; i < bandwidthList.Count; i++)
            {
                Log.LogComment(DllLog.Log.LogLevel.Info, $"Bandwidth {(double)bandwidthList[i]}: Fail");
            }

            for (int i = 0; i < transferList.Count; i++)
            {
                if ((double)transferList[i] < this.transferLimit)
                {
                    Log.LogComment(DllLog.Log.LogLevel.Error, "Transfer below the specified limit : Fail");
                    return false; 
                }
            }

            for (int i = 0; i < bandwidthList.Count; i++)
            {
                if ((double)bandwidthList[i] < this.bandwidthLimit)
                {
                    Log.LogComment(DllLog.Log.LogLevel.Error, "Bandwidth below specified limit : Fail");
                    return false; 
                }
            }

            return true;
        }

        private void AddChat(int series, double xValue, double yValue)
        {
            try
            {
                this.chart_tb.BeginInvoke((MethodInvoker)delegate
                {
                    chart_tb.Series[series].Points.AddXY(xValue, yValue);
                });
            }
            catch (Exception ex)
            {
                Log.LogComment(DllLog.Log.LogLevel.Error, $"AddChat {ex.Message}: Fail");
            }
        }

        private void AddTransfer(String lbTransfer)
        {
            try
            {
                this.lb_transfer.BeginInvoke((MethodInvoker)delegate
                {
                    this.lb_transfer.Text = lbTransfer;
                });
            }
            catch (Exception ex)
            {
                Log.LogComment(DllLog.Log.LogLevel.Error, $"AddTransfer {ex.Message}: Fail");
            }
        }

        private void AddBandwidth(String lbBandwidth)
        {
            try
            {
                this.lb_bandwidth.BeginInvoke((MethodInvoker)delegate
                {
                    this.lb_bandwidth.Text = lbBandwidth;
                });
            }
            catch (Exception ex)
            {
                Log.LogComment(DllLog.Log.LogLevel.Error, $"AddBandwidth {ex.Message}: Fail");
            }
        }

        private void FailTest()
        {
            try
            {
                this.lb_transfer.BeginInvoke((MethodInvoker)delegate
                {
                    this.lb_transfer.ForeColor = Color.Red;
                });
                this.lb_bandwidth.BeginInvoke((MethodInvoker)delegate
                {
                    this.lb_bandwidth.ForeColor = Color.Red;
                });
            }
            catch (Exception ex)
            {
                Log.LogComment(DllLog.Log.LogLevel.Error, $"FailTest {ex.Message}: Fail");
            }
        }

        private void PassBtnVisible(bool visible)
        {
            try
            {
                this.lb_transfer.BeginInvoke((MethodInvoker)delegate
                {
                    this.lb_transfer.ForeColor = Color.Green;
                });
                this.lb_bandwidth.BeginInvoke((MethodInvoker)delegate
                {
                    this.lb_bandwidth.ForeColor = Color.Green;
                });
                this.PassBtn.BeginInvoke((MethodInvoker)delegate
                {
                    this.PassBtn.Visible = visible;
                });
            }
            catch (Exception ex)
            {
                Log.LogComment(DllLog.Log.LogLevel.Error, $"PassBtnVisible {ex.Message}: Fail");
            }
        }
    }
}
