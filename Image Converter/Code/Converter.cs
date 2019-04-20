using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace Image_Converter.Code {
    internal class Converter {
        private static MagickFormat WEBP = MagickFormat.WebP;
        private static MagickFormat JPEG = MagickFormat.Jpeg;
        private static MagickFormat PNG = MagickFormat.Png;

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
                        magickImage.Settings.SetDefine(WEBP, "-lossless", Properties.Settings.Default.WebP_Lossless);
                        magickImage.Settings.SetDefine(WEBP, "-emulate-jpeg-size", Properties.Settings.Default.WebP_EmulateJpeg);
                        magickImage.Settings.SetDefine(WEBP, "-alpha", Properties.Settings.Default.WebP_RemoveAlpha);
                        magickImage.Settings.SetDefine(WEBP, "-quality", $"{Properties.Settings.Default.WebP_Quality}");
                        magickImage.Format = WEBP;
                        magickImage.Write($"{image}.webp");

                        RemoveImage($"{info.FileName}{info.FileType}", info.FileLocation);
                    }
                    else if (info.FileType != ".webp") {
                        //Todo: add gif conversion
                    }
                }
                catch (Exception ex) {
                    Logger.Instance.LogError(ex);
                }

            }
        }

        private static void JpegConvert() {
            foreach (ImageInfo info in Utilities.ImageCollection) {
                string imageExt = $"{info.FileLocation}\\{info.FileName}{info.FileType}";
                string image = $"{info.FileLocation}\\{info.FileName}";

                try{
                    if (info.FileType != ".jpg" && info.FileType != ".jpeg" && info.FileType != ".gif") {
                        MagickImage magickImage = new MagickImage(imageExt);
                        magickImage.Settings.SetDefine(JPEG, "-quality", $"{Properties.Settings.Default.Jpeg_Quality}");
                        magickImage.Format = JPEG;
                        magickImage.Write($"{image}.jpeg");

                        RemoveImage($"{info.FileName}{info.FileType}", info.FileLocation);
                    }
                }
                catch (Exception ex) {
                    Logger.Instance.LogError(ex);
                }
            }
        }

        private static void PngConvert() {
            foreach (ImageInfo info in Utilities.ImageCollection) {
                string imageExt = $"{info.FileLocation}\\{info.FileName}{info.FileType}";
                string image = $"{info.FileLocation}\\{info.FileName}";

                try {
                    if (info.FileType != ".png" && info.FileType != ".gif") {
                        MagickImage magickImage = new MagickImage(imageExt);
                        magickImage.Settings.SetDefine(PNG, "-lossless", Properties.Settings.Default.Png_Lossless);
                        magickImage.Settings.SetDefine(PNG, "-alpha", Properties.Settings.Default.Png_RemoveAlpha);
                        magickImage.Settings.SetDefine(PNG, "-quality", $"{Properties.Settings.Default.Png_Quality}");
                        magickImage.Format = PNG;
                        magickImage.Write($"{image}.png");

                        RemoveImage($"{info.FileName}{info.FileType}", info.FileLocation);
                    }
                }
                catch (Exception ex) {
                    Logger.Instance.LogError(ex);
                }
            }
        }

        private static void RemoveImage(string image, string imageLocation) {
            if (Properties.Settings.Default.General_Backup) {
                if (Properties.Settings.Default.General_CustomBackupFolder && Properties.Settings.Default.General_BackupFolder != "BACKUP") {
                    if (!Directory.Exists($"{Properties.Settings.Default.General_BackupFolder}")) {
                        Directory.CreateDirectory($"{Properties.Settings.Default.General_BackupFolder}");
                    }
                    File.Move($"{imageLocation}\\{image}", $"{Properties.Settings.Default.General_BackupFolder}\\{image}");
                }
                else {
                    if (!Directory.Exists($"{imageLocation}\\BACKUP")) {
                        Directory.CreateDirectory($"{imageLocation}\\BACKUP");
                    }
                    File.Move($"{imageLocation}\\{image}", $"{imageLocation}\\BACKUP\\{image}");
                }
            }
            else {
                File.Delete($"{imageLocation}\\{image}");
            }
        }
    }
}
