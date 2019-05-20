using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter_Utilities.API;
using ImageMagick;

namespace ImageConverter.API {
    internal class Converter {
        private static MagickFormat WEBP = MagickFormat.WebP;
        private static MagickFormat JPEG = MagickFormat.Jpeg;
        private static MagickFormat PNG = MagickFormat.Png;
        public static string SelectedType { get; set; }
        internal static void StartConvert() {
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
            foreach (InfoFile infoFile in Utilities.ImageCollection) {
                string imageExt = $"{infoFile.FileLocation}\\{infoFile.FileName}{infoFile.FileType}";
                string image = $"{infoFile.FileLocation}\\{infoFile.FileName}";

                try {
                    if (infoFile.FileType != ".gif" && infoFile.FileType != ".webp") {
                        MagickImage magickImage = new MagickImage(imageExt);
                        magickImage.Settings.SetDefine(WEBP, "-lossless", Properties.Settings.Default.WebP_Lossless);
                        magickImage.Settings.SetDefine(WEBP, "-emulate-jpeg-size", Properties.Settings.Default.WebP_EmulateJpeg);
                        magickImage.Settings.SetDefine(WEBP, "-alpha", Properties.Settings.Default.WebP_RemoveAlpha);
                        magickImage.Settings.SetDefine(WEBP, "-quality", $"{Properties.Settings.Default.WebP_Quality}");
                        magickImage.Format = WEBP;
                        magickImage.Write($"{image}.webp");
                    }
                    else if (infoFile.FileType == ".gif") {
                        Process gifProcess = new Process() {
                            StartInfo = {
                                FileName = "cmd.exe",
                                RedirectStandardInput = true,
                                RedirectStandardOutput = true,
                                CreateNoWindow = true,
                                UseShellExecute = false
                            }
                        };

                        gifProcess.Start();
                        gifProcess.StandardInput.WriteLine($"cd {Environment.CurrentDirectory}");
                        gifProcess.StandardInput.WriteLine($"gif2webp.exe {Properties.Settings.Default.WebP_Quality} \"{imageExt}\" -o \"{image}.webp\"");
                        gifProcess.StandardInput.Flush();
                        gifProcess.StandardInput.Close();
                        gifProcess.WaitForExit();
                    }
                    RemoveImage($"{infoFile.FileName}{infoFile.FileType}", infoFile.FileLocation);
                }
                catch (Exception ex) {
                    Logger.Instance("WebPConverter").LogError(ex);
                }
            }
        }
        private static void JpegConvert() {
            foreach (InfoFile info in Utilities.ImageCollection) {
                string imageExt = $"{info.FileLocation}\\{info.FileName}{info.FileType}";
                string image = $"{info.FileLocation}\\{info.FileName}";

                try {
                    if (info.FileType != ".jpg" && info.FileType != ".jpeg" && info.FileType != ".gif") {
                        MagickImage magickImage = new MagickImage(imageExt);
                        magickImage.Settings.SetDefine(JPEG, "-quality", $"{Properties.Settings.Default.Jpeg_Quality}");
                        magickImage.Format = JPEG;
                        magickImage.Write($"{image}.jpeg");

                        RemoveImage($"{info.FileName}{info.FileType}", info.FileLocation);
                    }
                }
                catch (Exception ex) {
                    Logger.Instance("JpegConverter").LogError(ex);
                }
            }
        }

        private static void PngConvert() {
            foreach (InfoFile info in Utilities.ImageCollection) {
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
                    Logger.Instance("PngConverter").LogError(ex);
                }
            }
        }

        private static void RemoveImage(string image, string imageLocation) {

        }
    }
}
