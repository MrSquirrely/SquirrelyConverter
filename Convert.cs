using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Notifications.Wpf;


namespace SquirrelyConverter
{
    class Convert
    {
        #region WebP
        public static void WebP(string coding) {
            
            if (Utils.isRunning != true) {
                switch (coding) {
                    case "encode":
                        try {
                            Console.WriteLine("Encoding");
                            WebPEncode();
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "decode":
                        try {
                            Console.WriteLine("Decoding");
                            WebPDecode();
                        }
                        catch (Exception e) {
                            Console.WriteLine(e.Message);
                        }
                        break;
                }
                Utils.isRunning = true;
            }
        }

        
        #region Encode
        private static void WebPEncode() {
            try {
                foreach (string file in Utils.Files) {
                    Utils.FileName = Path.GetFileNameWithoutExtension(file);
                    Utils.FileType = Path.GetExtension(file).ToLower();
                    Utils.FileLocation = Path.GetDirectoryName(file);

                    if (Utils.FileType == ".gif") {
                        WebP image = new WebP();
                        image.Image = file;
                        image.Output = $"{Utils.FileLocation}/{Utils.FileName}.webp";
                        image.EnocdeGIF();
                        Utils.fileNum++;
                        File.Delete(file);
                    }
                    else {
                        WebP image = new WebP();
                        image.Image = file;
                        image.Quality = Options.WebPQuality;
                        image.CopyMeta = Options.WebPCopyMeta;
                        image.NoAlpha = Options.WebPNoAlpha;
                        image.Lossless = Options.WebPLossless;
                        image.Output = $"{Utils.FileLocation}/{Utils.FileName}.webp";
                        image.Encode();
                        Utils.fileNum++;
                        File.Delete(file);
                    }                
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Decode
        private static void WebPDecode() {

            try {
                foreach (string file in Utils.Files) {
                    Utils.FileName = Path.GetFileNameWithoutExtension(file);
                    Utils.FileLocation = Path.GetDirectoryName(file);

                    WebP image = new WebP();
                    image.Image = file;
                    image.Output = Utils.FileLocation + "/" + Utils.FileName + ".png";
                    image.Decode();
                    File.Delete(file);
                }
                Utils.isRunning = false;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        #endregion

    }
}
