using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using ConverterUtilities;
using ImageMagick;
using static ConverterUtilities.Enums;

namespace ImageConverter {
    class Converter {

        public static void ConvertWebP(List<string> files) {
            try {
                foreach (string file in files) {
                    if (CUtilities.GetFileType(file) != ".gif") {
                        MagickImage image = new MagickImage(file);
                        image.Settings.SetDefine(MagickFormat.WebP, "-lossless", Options.WebPLossless);
                        image.Settings.SetDefine(MagickFormat.WebP, "-emulate-jpeg-size", Options.WebPEmulateJpeg);
                        image.Settings.SetDefine(MagickFormat.WebP, "-alpha", Options.GetWebPRemoveAlpha());
                        image.Settings.SetDefine(MagickFormat.WebP, "-quality", Options.WebPQuality.ToString(CultureInfo.InvariantCulture));
                        image.Format = MagickFormat.WebP;
                        image.Write($"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, FileExtension.No)}.webp");
                    }
                    else if (CUtilities.GetFileType(file) == ".gif") {
                        ConvertWebPGif(file);
                    }

                    // I need help on this. There has to be a better way to do this?
                    foreach (NewFile newFile in ImageUtilities.Images) {
                        int index = ImageUtilities.Images.IndexOf(newFile);
                        if (ImageUtilities.Images[index].Location == CUtilities.GetFileLocation(file)) {
                            ImageUtilities.Images[index].Converted = "Converted";
                        }
                    }
                    Finish(file);
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
            
        }

        private static void ConvertWebPGif(string file) {
            try {
                Process process = new Process {
                    StartInfo = {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true,
                        UseShellExecute = false
                    }
                };
                process.Start();
                process.StandardInput.WriteLine($"cd {Directory.GetCurrentDirectory()}");
                process.StandardInput.WriteLine($"gif2webp {Options.WebPQuality} \"{file}\" -o \"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, FileExtension.No)}.webp\"");
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
        }

        public static void ConvertJpeg(List<string> files) {
            try {
                foreach (string file in files) {
                    if (CUtilities.GetFileType(file) == ".gif" || CUtilities.GetFileType(file) == ".jpg" || CUtilities.GetFileType(file) == ".jpeg") {
                        continue;
                    }
                    MagickImage image = new MagickImage(file);
                    image.Settings.SetDefine(MagickFormat.Jpeg, "-quality", Options.WebPQuality.ToString(CultureInfo.InvariantCulture));
                    image.Format = MagickFormat.Jpeg;
                    image.Write($"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, FileExtension.No)}.jpeg");

                    // I need help on this. There has to be a better way to do this?
                    foreach (NewFile newFile in ImageUtilities.Images) {
                        int index = ImageUtilities.Images.IndexOf(newFile);
                        if (ImageUtilities.Images[index].Location == CUtilities.GetFileLocation(file)) {
                            ImageUtilities.Images[index].Converted = "Converted";
                        }
                    }
                    Finish(file);
                }
                
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
        }

        public static void ConvertPng(List<string> files) {
            try {
                foreach (string file in files) {
                    if (CUtilities.GetFileType(file) == ".gif" || CUtilities.GetFileType(file) == ".png") {
                        continue;
                    }

                    MagickImage image = new MagickImage(file);
                    image.Settings.SetDefine(MagickFormat.Png, "-lossless", Options.PngLossless);
                    image.Settings.SetDefine(MagickFormat.Png, "-alpha", Options.GetPngRemoveAlpha());
                    image.Settings.SetDefine(MagickFormat.Png, "-quality", Options.PngQuality.ToString(CultureInfo.InvariantCulture));
                    image.Format = MagickFormat.Png;
                    image.Write($"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, FileExtension.No)}.png");

                    // I need help on this. There has to be a better way to do this?
                    foreach (NewFile newFile in ImageUtilities.Images) {
                        int index = ImageUtilities.Images.IndexOf(newFile);
                        if (ImageUtilities.Images[index].Location == CUtilities.GetFileLocation(file)) {
                            ImageUtilities.Images[index].Converted = "Converted";
                        }
                    }
                    Finish(file);
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
        }
        
        private static void Finish(string file) {
            UpdateView();
            CopyFile(file);
            DeleteFile(file);
        }

        private static void UpdateView() {
            try {
                ImageUtilities.ImageView.Dispatcher.Invoke(() => {
                    ImageUtilities.ImageListView.Items.Refresh();
                },
                System.Windows.Threading.DispatcherPriority.Background);
            }
            catch (Exception ex) {
                Logger.LogDebug(ex);
            }
        }

        private static void CopyFile(string file) {
            if (Options.CreateTemp) {
                if (!Directory.Exists($"{CUtilities.GetTempDir(Options.CreateTemp, Options.TempLocation)}")) {
                    Directory.CreateDirectory($"{CUtilities.GetTempDir(Options.CreateTemp, Options.TempLocation)}");
                }
                File.Copy(file, $"{CUtilities.GetTempDir(Options.CreateTemp, Options.TempLocation)}");
            }
        }

        private static void DeleteFile(string file) {
            if (Options.ImagesDelete) {
                File.Delete(file);
            }
        }

    }
}
