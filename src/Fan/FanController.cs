using DllLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Fan
{
    class FanController
    {
        private string filePath = "FAN_log.txt";
        private Label lb_fan1 = new Label();
        private Label lb_fan2 = new Label();
        private Label lb_fan3 = new Label();
        private Boolean kill_test = true;

        public FanController(Label lb_fan1, Label lb_fan2, Label lb_fan3)
        {
            this.lb_fan1 = lb_fan1;
            this.lb_fan2 = lb_fan2;
            this.lb_fan3 = lb_fan3;
        }

        public void run()
        {
            try
            {
                DeleteFile();
                RunFan();
                GetLog();
            }
            catch (Exception e)
            {
                Log.LogError(e.Message);
                return;
            }
        }

        public void kill()
        {
            kill_test = false;
            Process p = null;
            try
            {
                p = new Process();
                p.StartInfo.FileName = @"C:\Windows\System32\taskkill.exe";
                p.StartInfo.Arguments = "/F /IM MFCfan.EXE /T";
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();

                while (!p.StandardOutput.EndOfStream)
                {
                    string mensagem = p.StandardOutput.ReadLine();
                    Console.WriteLine(mensagem);
                    Log.LogStart("Takskkill Fechar Fan: " + mensagem);
                }
                p.WaitForExit();
                if (p.ExitCode != 0)
                {
                    string mensagem = "Fechar Fan retornou o código de erro " + p.ExitCode + ".";
                    Console.WriteLine(mensagem);
                    Log.LogStart(mensagem);
                }
            }
            catch (Exception e)
            {
                string mensagem = "Erro na execução do fan.exe : " + e.Message;
                Console.WriteLine(mensagem);
                Log.LogError(mensagem);
            }
            finally
            {
                if (p != null)
                    p.Close();
            }
        }

        private void DeleteFile()
        {
            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private void RunFan()
        {
            string command = @"start /min C:\TEST_TOOL\fan\MFCfan.EXE";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c {command }",
                CreateNoWindow = true, // Configura para não criar uma janela visível.
                WindowStyle = ProcessWindowStyle.Hidden, // Também define o estilo da janela como oculto.
            };

            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit(); // Espere até que o processo termine, se necessário.
                }

                Console.WriteLine("fan.exe foi executado e oculto.");
            }
            catch (Exception ex)
            {
                Log.LogError($"Ocorreu um erro ao executar fan.exe: {ex.Message}");
            }
        }

        private void GetLog()
        {
            string fan1Pattern = @"Fan1 rpm=(\d+);";
            string fan2Pattern = @"Fan2 rpm=(\d+);";
            string fan3Pattern = @"Fan3 rpm=(\d+);";
            kill_test = true;

            do
            {
                try
                {
                    using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string line = null;
                        string penultimateLine = null;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (penultimateLine != null)
                            {
                                // Se tivermos uma linha anterior à atual, armazene-a como penúltima.
                                penultimateLine = line;
                            }
                            else
                            {
                                penultimateLine = line;
                            }
                        }

                        UpdateFan1Rpm(ExtractRpmValue(penultimateLine, fan1Pattern));
                        UpdateFan2Rpm(ExtractRpmValue(penultimateLine, fan2Pattern));
                        UpdateFan3Rpm(ExtractRpmValue(penultimateLine, fan3Pattern));
                    }
                }
                catch (IOException)
                {
                    // O arquivo está em uso; espere um pouco antes de tentar novamente.
                    Thread.Sleep(1000); // Aguarde 1 segundo (ou ajuste conforme necessário).
                }
            } while (kill_test);
        }

        public void UpdateFan1Rpm(int fan1Rpm)
        {
            if (lb_fan1.InvokeRequired)
            {
                lb_fan1.Invoke(new Action(() =>
                {
                    lb_fan1.Text = fan1Rpm.ToString();
                }));
            }
            else
            {
                lb_fan1.Text = fan1Rpm.ToString();
            }
        }

        public void UpdateFan2Rpm(int fan2Rpm)
        {
            if (lb_fan2.InvokeRequired)
            {
                lb_fan2.Invoke(new Action(() =>
                {
                    lb_fan2.Text = fan2Rpm.ToString();
                }));
            }
            else
            {
                lb_fan2.Text = fan2Rpm.ToString();
            }
        }

        public void UpdateFan3Rpm(int fan3Rpm)
        {
            if (lb_fan3.InvokeRequired)
            {
                lb_fan3.Invoke(new Action(() =>
                {
                    lb_fan3.Text = fan3Rpm.ToString();
                }));
            }
            else
            {
                lb_fan3.Text = fan3Rpm.ToString();
            }
        }

        private static int ExtractRpmValue(string input, string pattern)
        {
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                string rpmValue = match.Groups[1].Value;
                if (int.TryParse(rpmValue, out int rpm))
                {
                    return rpm;
                }
            }
            return -1; // Valor padrão se não for encontrado.
        }
    }
}
