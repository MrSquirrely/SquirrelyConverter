using System;
using System.Diagnostics;

namespace WebPConverter.Class {
    public class WebP {
        
        public int Quality { get; set; } = 80;
        public bool NoAlpha { get; set; } = false;
        public bool CopyMeta { get; set; } = false; 
        public bool Lossless { get; set; } = false;
        public bool MultiThreading { get; set; } = false;
        public string Input { get; set; }
        public string Output { get; set; }
        
        private const string CmdNoAlpha = " -noalpha";
        private const string CmdCopyMeta = " -metadata all";
        private const string CmdLossless = " -lossless";
        private const string CmdMultiThread = " -mt";

        private bool _ready;
        private string _cmdText;
        
        private void CheckInOut() { if (Input != null && Output != null) _ready = true; }

        #region Set Command
        private void SetCommand(From type) {
            switch (type) {
                case From.Gif:
                    _cmdText = $"cwebp {Quality}";
                    break;
                case From.Image:
                    _cmdText = $"cwebp {Quality}";
                    if (NoAlpha) _cmdText = _cmdText.Insert(_cmdText.Length, CmdNoAlpha);
                    if (CopyMeta) _cmdText = _cmdText.Insert(_cmdText.Length, CmdCopyMeta);
                    if (Lossless) _cmdText = _cmdText.Insert(_cmdText.Length, CmdLossless);
                    if (MultiThreading) _cmdText = _cmdText.Insert(_cmdText.Length, CmdMultiThread);
                    break;
            }
            _cmdText = _cmdText.Insert(_cmdText.Length, $"\"{Input}\" -o \"{Output}\"");            
        }
        #endregion

        #region Encode
        public void Encode() {
            CheckInOut();
            SetCommand(From.Image);

            if (!_ready) return;
            try {
                Process process = new Process {
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
                Utils.LogMessage(e);
            }
        }
        #endregion

        #region Encode GIF
        public void EnocdeGif() {
            CheckInOut();
            SetCommand(From.Gif);

            if (!_ready) return;
            try {
                Process process = new Process {
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
                Utils.LogMessage(e);
            }
        }
        #endregion

    }
}
