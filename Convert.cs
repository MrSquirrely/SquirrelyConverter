using System;
using System.IO;


namespace SquirrelyConverter
{
    internal class Convert
    {
        #region WebP
        public static void WebP(string coding) {
            if (Utils.IsRunning) return;
            switch (coding) {
                case "encode":
                    try {
                        WebPEncode();
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                    break;
                case "decode":
                    try {
                        WebPDecode();
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                    break;
            }
            Utils.IsRunning = true;
        }


        #region Encode
        private static void WebPEncode() {
            try {
                foreach (var file in Utils.Files) {
                    Utils.FileName = Path.GetFileNameWithoutExtension(file);
                    Utils.FileType = Path.GetExtension(file)?.ToLower();
                    Utils.FileLocation = Path.GetDirectoryName(file);

                    if (Utils.FileType == ".gif") {
                        var image = new WebP
                        {
                            WebPImage = file,
                            WebPOutput = Options.SetCustomOutput
                                ? $"{Options.OutDir}/{Utils.FileName}.webp"
                                : $"{Utils.FileLocation}/{Utils.FileName}.webp"
                        };
                        image.EnocdeGif();
                        Utils.FileNum++;
                        File.Delete(file);
                    }
                    else {
                        var image = new WebP
                        {
                            WebPImage = file,
                            WebPQuality = Options.WebPQuality,
                            WebPCopyMeta = Options.WebPCopyMeta,
                            WebPNoAlpha = Options.WebPNoAlpha,
                            WebPLossless = Options.WebPLossless,
                            WebPOutput = Options.SetCustomOutput
                                ? $"{Options.OutDir}/{Utils.FileName}.webp"
                                : $"{Utils.FileLocation}/{Utils.FileName}.webp"
                        };
                        image.Encode();
                        Utils.FileNum++;
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
                foreach (var file in Utils.Files) {
                    Utils.FileName = Path.GetFileNameWithoutExtension(file);
                    Utils.FileLocation = Path.GetDirectoryName(file);

                    var image = new WebP
                    {
                        WebPImage = file,
                        WebPOutput = Utils.FileLocation + "/" + Utils.FileName + ".png"
                    };
                    image.Decode();
                    File.Delete(file);
                }
                Utils.IsRunning = false;
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        #endregion

    }
}
