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
                        
                        image.Settings.SetDefine(MagickFormat.WebP, "-lossless", Options.GetWebPLossless());
                        image.Settings.SetDefine(MagickFormat.WebP, "-emulate-jpeg-size", Options.GetWebPEmulateJpeg());
                        image.Settings.SetDefine(MagickFormat.WebP, "-alpha", Options.GetWebPRemoveAlpha());
                        image.Settings.SetDefine(MagickFormat.WebP, "-quality", Options.GetWebPQuality().ToString(CultureInfo.InvariantCulture));
                        image.Format = MagickFormat.WebP;
                        image.Write($"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, Enums.FileExtension.No)}.webp");
                    }
                    else if (CUtilities.GetFileType(file) == ".gif") {
                        ConvertWebPGif(file);
                    }

                    // I need help on this. There has to be a better way to do this?
                    foreach (NewFile newFile in ImageUtilities.ImagesCollection) {
                        
                        int index = ImageUtilities.ImagesCollection.IndexOf(newFile);
                        Console.WriteLine(ImageUtilities.ImagesCollection[index].Location);
                        Console.WriteLine(newFile.Location);

                        if (ImageUtilities.ImagesCollection[index].Location == CUtilities.GetFileLocation(file)) {
                            ImageUtilities.ImagesCollection[index].Converted = "Converted";
                            Console.WriteLine(newFile.Converted);
                            UpdateView();
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
                    image.Settings.SetDefine(MagickFormat.Jpeg, "-quality", Options.GetJpegQuality().ToString(CultureInfo.InvariantCulture));
                    image.Format = MagickFormat.Jpeg;
                    image.Write($"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, Enums.FileExtension.No)}.jpeg");

                    // I need help on this. There has to be a better way to do this?
                    foreach (NewFile newFile in ImageUtilities.ImagesCollection) {
                        int index = ImageUtilities.ImagesCollection.IndexOf(newFile);
                        if (ImageUtilities.ImagesCollection[index].Location == CUtilities.GetFileLocation(file)) {
                            ImageUtilities.ImagesCollection[index].Converted = "Converted";
                            UpdateView();
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
                    image.Settings.SetDefine(MagickFormat.Png, "-lossless", Options.GetPngLossless());
                    image.Settings.SetDefine(MagickFormat.Png, "-alpha", Options.GetPngRemoveAlpha());
                    image.Settings.SetDefine(MagickFormat.Png, "-quality", Options.GetPngQuality().ToString(CultureInfo.InvariantCulture));
                    image.Format = MagickFormat.Png;
                    image.Write($"{CUtilities.GetFileDirectory(file)}\\{CUtilities.GetFileName(file, Enums.FileExtension.No)}.png");

                    // I need help on this. There has to be a better way to do this?
                    foreach (NewFile newFile in ImageUtilities.ImagesCollection) {
                        int index = ImageUtilities.ImagesCollection.IndexOf(newFile);
                        if (ImageUtilities.ImagesCollection[index].Location == CUtilities.GetFileLocation(file)) {
                            ImageUtilities.ImagesCollection[index].Converted = "Converted";
                            UpdateView();
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
            if (Options.GetCreateTemp()) {
                if (!Directory.Exists($"{CUtilities.GetTempDir(Options.GetCreateTemp(), Options.GetTempLocation())}")) {
                    Directory.CreateDirectory($"{CUtilities.GetTempDir(Options.GetCreateTemp(), Options.GetTempLocation())}");
                }
                File.Copy(file, $"{CUtilities.GetTempDir(Options.GetCreateTemp(), Options.GetTempLocation())}");
            }
            File.Delete(file);

            if (Options.GetDeleteTemp() && Directory.Exists(Options.GetTempLocation())) {
                Directory.Delete(Options.GetTempLocation());
            }
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

    }
}
