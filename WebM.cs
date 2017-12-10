using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace SquirrelyConverter {
    class WebM {

        public string WebMInput { get; set; }
        public string WebMOutput { get; set; }

        private bool _ready;
        private string _cmdText;

        public void Encode() {
            if (WebMInput == null) throw new ArgumentException("Input must not be empty!");
            if (WebMOutput == null) throw new ArgumentException("Output must not be empty!");
            if (WebMInput != null && WebMOutput != null) _ready = true; else throw new ArgumentException("There has to be an input and an output!");

            if (!_ready) return;
            try {
                // I might not add options, I'm not 100% at the moment
                // Possible are -crf "int" // This is quality range from 0(lossless) - 51
                // -speed encoding speed 0-8 higher being faster
                _cmdText = $"ffmpeg -i \"{WebMInput}\" -speed 8 \"{WebMOutput}\"";

                Process process = new Process {
                    StartInfo = {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = false,
                        UseShellExecute = false
                    }
                };
                process.Start();

                process.StandardInput.WriteLine(_cmdText);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();

                _ready = false;

                process.Kill();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
