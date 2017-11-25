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
        public string Image { get; set; }
        public string Output { get; set; }

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
                    CMDText = "cwebp" + S + Quality + S + NoAlpha + S + CopyMeta + S + Image + S + "-o" + S + Output;
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

                    //ProcessStartInfo startInfo = new ProcessStartInfo();
                    ////startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    //startInfo.FileName = "cmd.exe";
                    //startInfo.Arguments = CMDText;
                    //process.StartInfo = startInfo;
                    //process.Start();
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
                    CMDText = "gif2webp" + S + Image + S + "-o" + S + Output;
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

        #region Decode
        public void Decode() {
            if (Image == null) throw new ArgumentException("Input must not be empty!"); //Checks if the input is null
            if (Output == null) throw new ArgumentException("Output must not be empty!"); //Checks if the output is null
            if (Image != null && Output != null) Ready = true; else throw new ArgumentException("There has to be an input and an output!"); //Just a final test to be safe.

            if (Ready) {
                try {
                    CMDText = "dwebp" + S + Image + S + "-o" + S + Output;
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
