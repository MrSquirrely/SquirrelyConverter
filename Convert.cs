using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SquirrelyConverter
{
    class Convert
    {

        public static void WebP(string coding) {
            if (Utils.isRunning != true) {
                Utils.CheckFolder();
                Utils.BackupFiles();
                switch (coding) {
                    case "encode":
                        Thread encode = new Thread(WebPEncode);
                        encode.SetApartmentState(ApartmentState.STA);
                        encode.Start();
                        break;
                    case "decode":
                        Thread decode = new Thread(WebPDecode);
                        decode.SetApartmentState(ApartmentState.STA);
                        decode.Start();
                        break;
                }
                Utils.isRunning = true;
            }
        }

        #region WebP
        #region Encode
        private static void WebPEncode() {

            try {
                foreach (string file in Utils.files) {
                    Utils.FileName = Path.GetFileNameWithoutExtension(file);
                    Utils.FileType = Path.GetExtension(file).ToLower();
                    Utils.FileLocation = Path.GetDirectoryName(file);

                    if (Utils.FileType == ".gif") {
                        WebP image = new WebP();
                        image.Image = file;
                        image.Output = Utils.FileLocation + "/" + Utils.FileName + ".webp";
                        image.EnocdeGIF();
                        File.Delete(file);
                    }
                    else {
                        WebP image = new WebP();
                        image.Image = file;
                        image.Quality = 80;
                        image.CopyMeta = false;
                        image.NoAlpha = false;
                        image.Output = Utils.FileLocation + "/" + Utils.FileName + ".webp";
                        image.Encode();
                        File.Delete(file);
                    }

                    
                }
                Utils.isRunning = false;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Decode
        private static void WebPDecode() {

            try {
                foreach (string file in Utils.files) {
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
