using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace Image_Converter.Code {
    internal class Converter {

        private static bool BackupImage = true;
        
        private static MagickFormat WEBP = MagickFormat.WebP;

        public static string SelectedType { get; set; }

        public static void StartConvert() {
            switch (SelectedType) {
                case "webp":
                    WebPConvert();
                    break;
                case "jpeg":
                    JpegConvert();
                    break;
                case "png":
                    PngConvert();
                    break;
                default:
                    WebPConvert();
                    break;
            }
        }

        private static void WebPConvert() {
            foreach (ImageInfo info in Utilities.ImageCollection) {
                string imageExt = $"{info.FileLocation}\\{info.FileName}{info.FileType}";
                string image = $"{info.FileLocation}\\{info.FileName}";

                try {
                    if (info.FileType != ".gif" && info.FileType != ".webp") {
                        MagickImage magickImage = new MagickImage(imageExt);
                        magickImage.Settings.SetDefine(WEBP, "-lossless", true);
                        magickImage.Settings.SetDefine(WEBP, "-emulate-jpeg-size", false);
                        magickImage.Settings.SetDefine(WEBP, "-alpha", false);
                        magickImage.Settings.SetDefine(WEBP, "-quality", "80");
                        magickImage.Format = WEBP;
                        magickImage.Write($"{image}.webp");

                        RemoveImage($"{info.FileName}{info.FileType}", info.FileLocation);
                    }
                    else {
                        //Todo: add gif conversion
                    }
                    
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
                
            }
        }

        private static void JpegConvert() {

        }

        private static void PngConvert() {

        }

        private static void RemoveImage(string image, string imageLocation) {
            if (BackupImage) {
                if (!Directory.Exists($"{imageLocation}\\BACKUP")) {
                    Directory.CreateDirectory($"{imageLocation}\\BACKUP");
                }
                File.Move($"{imageLocation}\\{image}",$"{imageLocation}\\BACKUP\\{image}");
            }
            else {
                File.Delete($"{imageLocation}\\{image}");
            }
        }
    }
}
