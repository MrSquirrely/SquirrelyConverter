using System;
using System.Diagnostics;

namespace SquirrelyConverter
{
    public class WebP
    {
        public double WebPQuality { get; set; } = 80;
        public bool WebPNoAlpha { get; set; } = false;
        public bool WebPCopyMeta { get; set; } = false;
        public bool WebPLossless { get; set; } = false;
        public string WebPImage { get; set; }
        public string WebPOutput { get; set; }

        private const string NoAlpha = " -noalpha";
        private const string CopyMeta = " -metadata all";
        private const string Lossless = " -lossless";

        private bool _ready;
        private string _cmdText;

        #region Encode
        public void Encode() {
            //These are remnants from when this was going to be a library. I still want to make it one, but for now it'll stay.
            if (WebPImage == null) throw new ArgumentException("Input must not be empty!");  //Checks if the input is null and if so throws exception
            if (WebPOutput == null) throw new ArgumentException("WebPOutput must not be empty!"); //Checks if the output is null and if so throws exception
            if (WebPImage != null && WebPOutput != null) _ready = true; else throw new ArgumentException("There has to be an input and an output!"); //Just a final test to be safe.

            if (!_ready) return;
            try {
                _cmdText = $"cwebp {WebPQuality}";
                if (WebPNoAlpha) _cmdText = _cmdText.Insert(_cmdText.Length, NoAlpha);
                if (WebPCopyMeta) _cmdText = _cmdText.Insert(_cmdText.Length, CopyMeta);
                if (WebPLossless) _cmdText = _cmdText.Insert(_cmdText.Length, Lossless);
                _cmdText = _cmdText.Insert(_cmdText.Length, " "+ $"\"{WebPImage}\"" + " -o " + $"\"{WebPOutput}\" ");

                Console.WriteLine(_cmdText);

                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
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
        #endregion

        #region Encode GIF
        public void EnocdeGif() {
            if (WebPImage == null) throw new ArgumentException("Input must not be empty!");  //Checks if the input is null and if so throws exception
            if (WebPOutput == null) throw new ArgumentException("WebPOutput must not be empty!"); //Checks if the output is null and if so throws exception
            if (WebPImage != null && WebPOutput != null) _ready = true; else throw new ArgumentException("There has to be an input and an output!"); //Just a final test to be safe.

            if (!_ready) return;
            try {
                _cmdText = $"gif2webp {WebPImage} -o {WebPOutput}";
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
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
        #endregion

        #region Decode
        public void Decode() {
            if (WebPImage == null) throw new ArgumentException("Input must not be empty!"); //Checks if the input is null
            if (WebPOutput == null) throw new ArgumentException("WebPOutput must not be empty!"); //Checks if the output is null
            if (WebPImage != null && WebPOutput != null) _ready = true; else throw new ArgumentException("There has to be an input and an output!"); //Just a final test to be safe.

            if (!_ready) return;
            try {
                _cmdText = $"dwebp {WebPImage} -o {WebPOutput}";
                var process = new Process
                {
                    StartInfo =
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    }
                };
                process.Start();

                process.StandardInput.WriteLine(_cmdText);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();

                _ready = false;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

    }
}
