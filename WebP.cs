using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelyConverter
{
    public class WebP
    {
        public double Quality { get; set; } = 80;
        public bool NoAlpha { get; set; } = false;
        public bool CopyMeta { get; set; } = false;
        public bool Lossless { get; set; } = false;
        public string Image { get; set; }
        public string Output { get; set; }

        private string _NoAlpha = " -noalpha";
        private string _CopyMeta = " -metadata all";
        private string _Lossless = " -lossless";

        private bool Ready = false;
        private string CMDText;
        private string S = " ";

        #region Encode
        public void Encode() {
            //These are remnants from when this was going to be a library. I still want to make it one, but for now it'll stay.
            if (Image == null) throw new ArgumentException("Input must not be empty!");  //Checks if the input is null and if so throws exception
            if (Output == null) throw new ArgumentException("Output must not be empty!"); //Checks if the output is null and if so throws exception
            if (Image != null && Output != null) Ready = true; else throw new ArgumentException("There has to be an input and an output!"); //Just a final test to be safe.

            if (Ready) {
                try {
                    CMDText = $"cwebp {Quality}";
                    if (NoAlpha) CMDText = CMDText.Insert(CMDText.Length, _NoAlpha);
                    if (CopyMeta) CMDText = CMDText.Insert(CMDText.Length, _CopyMeta);
                    if (Lossless) CMDText = CMDText.Insert(CMDText.Length, _Lossless);
                    CMDText = CMDText.Insert(CMDText.Length, " "+ $"\"{Image}\"" + " -o " + $"\"{Output}\" ");

                    Console.WriteLine(CMDText);

                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();

                    process.StandardInput.WriteLine(CMDText);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();
                    process.WaitForExit();

                    Ready = false;

                    process.Kill();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }

            }
        }
        #endregion

        #region Encode GIF
        public void EnocdeGIF() {
            if (Image == null) throw new ArgumentException("Input must not be empty!");  //Checks if the input is null and if so throws exception
            if (Output == null) throw new ArgumentException("Output must not be empty!"); //Checks if the output is null and if so throws exception
            if (Image != null && Output != null) Ready = true; else throw new ArgumentException("There has to be an input and an output!"); //Just a final test to be safe.

            if (Ready) {
                try {
                    CMDText = $"gif2webp {Image} -o {Output}";
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();

                    process.StandardInput.WriteLine(CMDText);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();
                    process.WaitForExit();

                    Ready = false;

                    process.Kill();
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }
        #endregion

        #region Decode
        public void Decode() {
            if (Image == null) throw new ArgumentException("Input must not be empty!"); //Checks if the input is null
            if (Output == null) throw new ArgumentException("Output must not be empty!"); //Checks if the output is null
            if (Image != null && Output != null) Ready = true; else throw new ArgumentException("There has to be an input and an output!"); //Just a final test to be safe.

            if (Ready) {
                try {
                    CMDText = $"dwebp {Image} -o {Output}";
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();

                    process.StandardInput.WriteLine(CMDText);
                    process.StandardInput.Flush();
                    process.StandardInput.Close();
                    process.WaitForExit();

                    Ready = false;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }

            }

        }
        #endregion

    }
}
