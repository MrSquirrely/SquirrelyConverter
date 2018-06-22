using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using ConverterUtilities;
using ImageMagick;

namespace ImageConverter.Class {
    class Converter {

        public static void ConvertWebP(List<string> files) {
            try {
                foreach (string file in files) {
                    if (CUtilities.GetFileType(file) != ".gif") {
                        MagickImage image = new MagickImage(file);
                        
                        image.Settings.SetDefine(MagickFormat.WebP, "-lossless", Options.IsWebPLossless());
                        image.Settings.SetDefine(MagickFormat.WebP, "-emulate-jpeg-size", Options.DoWebPEmulateJPEG());
                        image.Settings.SetDefine(MagickFormat.WebP, "-alpha", Options.DoWebPRemoveAlpha());
                        image.Settings.SetDefine(MagickFormat.WebP, "-quality", Options.GetWebPQuality().ToString(CultureInfo.InvariantCulture));
                        image.Format = MagickFormat.WebP;
                        image.Write($"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, Enums.FileExtension.No)}.webp");
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
                process.StandardInput.WriteLine($"gif2webp {Options.GetWebPQuality()} \"{file}\" -o \"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, Enums.FileExtension.No)}.webp\"");
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
                    image.Settings.SetDefine(MagickFormat.Jpeg, "-quality", Options.GetJPEGQuality().ToString(CultureInfo.InvariantCulture));
                    image.Format = MagickFormat.Jpeg;
                    image.Write($"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, Enums.FileExtension.No)}.jpeg");

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
                    image.Settings.SetDefine(MagickFormat.Png, "-lossless", Options.IsPNGLossless());
                    image.Settings.SetDefine(MagickFormat.Png, "-alpha", Options.DoPNGRemoveAlpha());
                    image.Settings.SetDefine(MagickFormat.Png, "-quality", Options.GetPNGQuality().ToString(CultureInfo.InvariantCulture));
                    image.Format = MagickFormat.Png;
                    image.Write($"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, Enums.FileExtension.No)}.png");

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
            if (Options.DoCreateTemp()) {
                if (!Directory.Exists($"{CUtilities.GetTempDir(Options.DoCreateTemp(), Options.WhereTempLocation())}")) {
                    Directory.CreateDirectory($"{CUtilities.GetTempDir(Options.DoCreateTemp(), Options.WhereTempLocation())}");
                }
                File.Copy(file, $"{CUtilities.GetTempDir(Options.DoCreateTemp(), Options.WhereTempLocation())}");
            }
        }

        private static void DeleteFile(string file) {
            if (Options.DoDeleteTemp()) {
                Directory.Delete(Options.WhereTempLocation());
            }
        }

    }
}
