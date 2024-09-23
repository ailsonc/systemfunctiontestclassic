
using System;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;

namespace AudioTest
{
    class Speak
    {
        private string text;
        private Boolean sleep = true;

        public Speak(string text)
        {
            this.text = text;
            this.sleep = true;
        }

        public void Sleep()
        {
            sleep = false;
        }

        public void Run()
        {
            try
            {
                SpeechSynthesizer synth = new SpeechSynthesizer();
                synth.SetOutputToDefaultAudioDevice();
                synth.Volume = 100;
                while (sleep)
                {
                    synth.Speak(Convert.ToString($"Digite o número {this.text}"));
                    Thread.Sleep(1000);
                }
            }
            catch (ThreadAbortException abortException)
            {
                Task.Delay(10000);
                Console.WriteLine($"Aborto: {(string)abortException.ExceptionState}");
            }
        }
    }
}
