using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WifiPerf
{
    class Connect
    {
        public static int SetWifi(string ssid)
        {
            ProcessStartInfo psi = new ProcessStartInfo("netsh", $"wlan connect name=\"{ssid}\"");
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;

            // Inicia o processo
            Process process = Process.Start(psi);
            if (process != null)
            {
                // Aguarda até que o processo seja concluído
                process.WaitForExit();

                // Verifica se a conexão foi bem-sucedida
                if (process.ExitCode == 0)
                {
                    return 0;
                }
            }

            return 0;
        }


    }
}
