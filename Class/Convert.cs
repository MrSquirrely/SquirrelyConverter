using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ImageMagick;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace Mr_Squirrely_Converters.Class {
    class Converter {
        #region Video Converters
        internal static void ConvertWebM(List<string> files) {
            try {
                foreach (string file in files) {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string fileLocation = Path.GetDirectoryName(file);
                    string fileType = Path.GetExtension(file);
                    Engine engine = new Engine();
                    MediaFile inputFile = new MediaFile { Filename = file };
                    MediaFile outputFile = new MediaFile { Filename = $"{fileLocation}\\{fileName}.webm" };

                    if (Options.VideoChangeSize) {
                        ConversionOptions conversionOptions = new ConversionOptions {
                            CustomWidth = Options.VideoWidth,
                            CustomHeight = Options.VideoHeight
                        };
                        engine.Convert(inputFile, outputFile, conversionOptions);
                    }
                    else {
                        engine.Convert(inputFile, outputFile);
                    }
                }
            }
            catch (Exception) {
                //Todo
            }
        }

        internal static void ConvertMP4(List<string> files) {
            try {
                foreach (string file in files) {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    string fileLocation = Path.GetDirectoryName(file);
                    string fileType = Path.GetExtension(file);
                    Engine engine = new Engine();
                    MediaFile inputFile = new MediaFile { Filename = file };
                    MediaFile outputFile = new MediaFile { Filename = $"{fileLocation}\\{fileName}.mp4" };

                    if (Options.VideoChangeSize) {
                        ConversionOptions conversionOptions = new ConversionOptions {
                            CustomWidth = Options.VideoWidth,
                            CustomHeight = Options.VideoHeight
                        };
                        engine.Convert(inputFile, outputFile, conversionOptions);
                    }
                    else {
                        engine.Convert(inputFile, outputFile);
                    }
                }
            }
            catch (Exception) {
                //Todo
            }
        }
        #endregion

        #region Image Converters
        internal static void ConvertWebP(List<string> files) {
            try {
                foreach (string file in files) {
                    if (Utils.GetFileType(file) != ".gif") {
                        MagickImage image = new MagickImage(file);
                        image.Settings.SetDefine(MagickFormat.WebP, "-lossless", Options.WebPLossless);
                        image.Settings.SetDefine(MagickFormat.WebP, "-emulate-jpeg-size", Options.WebPEmulateJPEG);
                        image.Settings.SetDefine(MagickFormat.WebP, "-alpha", Options.GetWebPRemoveAlpha());
                        image.Settings.SetDefine(MagickFormat.WebP, "-quality", Options.WebPQuality.ToString());
                        image.Format = MagickFormat.WebP;
                        image.Write($"{Utils.GetFileDirectory(file)}\\{Utils.GetFileNameWithoutExtension(file)}.webp");
                    }
                    else if (Utils.GetFileType(file) == ".gif") {
                        ConvertWebPGif(file);
                    }

                    //I need help on this. There has to be a better way to do this?!
                    foreach (NewFile newFile in Utils._Images) {
                        int index = Utils._Images.IndexOf(newFile);
                        if (Utils._Images[index].Location == Utils.GetFileLocation(file)) {
                            Utils._Images[index].Converted = "Converted";
                        }
                    }
                    UpdateView();
                    CopyFile(file);
                    DeleteFile(file);
                }
            }
            catch (Exception) {
                //Todo
            }
        }

        private static void ConvertWebPGif(string file) {
            try {
                Utils.ExtractWebP();
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
                process.StandardInput.WriteLine($"cd {Directory.GetCurrentDirectory()}\\Files");
                process.StandardInput.WriteLine($"gif2webp {Options.WebPQuality} \"{file}\" -o \"{Utils.GetFileDirectory(file)}\\{Utils.GetFileNameWithoutExtension(file)}.webp\"");
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
            }
            catch (Exception) {
                //todo
            }
        }

        internal static void ConvertJPEG(List<string> files) {
            try {
                foreach (string file in files) {
                    if (Utils.GetFileType(file) == ".gif" || Utils.GetFileType(file) == ".jpg" || Utils.GetFileType(file) == ".jpeg")
                        continue;
                    MagickImage image = new MagickImage(file);
                    image.Settings.SetDefine(MagickFormat.Jpeg, "-quality", Options.WebPQuality.ToString());
                    image.Format = MagickFormat.Jpeg;
                    image.Write($"{Utils.GetFileDirectory(file)}\\{Utils.GetFileNameWithoutExtension(file)}.jpeg");
                    //I need help on this. There has to be a better way to do this?!
                    foreach (NewFile newFile in Utils._Images) {
                        int index = Utils._Images.IndexOf(newFile);
                        if (Utils._Images[index].Location == Utils.GetFileLocation(file)) {
                            Utils._Images[index].Converted = "Converted";
                        }
                    }
                    UpdateView();
                    CopyFile(file);
                    DeleteFile(file);
                }
            }
            catch (Exception) {
                //todo
            }
        }

        internal static void ConvertPNG(List<string> files) {
            try {
                foreach (string file in files) {
                    if (Utils.GetFileType(file) == ".gif" || Utils.GetFileType(file) == ".png")
                        continue;
                    MagickImage image = new MagickImage(file);
                    image.Settings.SetDefine(MagickFormat.Png, "-lossless", Options.WebPLossless);
                    image.Settings.SetDefine(MagickFormat.Png, "-alpha", Options.GetWebPRemoveAlpha());
                    image.Settings.SetDefine(MagickFormat.Png, "-quality", Options.WebPQuality.ToString());
                    image.Format = MagickFormat.Png;
                    image.Write($"{Utils.GetFileDirectory(file)}\\{Utils.GetFileNameWithoutExtension(file)}.png");
                    //I need help on this. There has to be a better way to do this?!
                    foreach (NewFile newFile in Utils._Images) {
                        int index = Utils._Images.IndexOf(newFile);
                        if (Utils._Images[index].Location == Utils.GetFileLocation(file)) {
                            Utils._Images[index].Converted = "Converted";
                        }
                    }
                    UpdateView();
                    CopyFile(file);
                    DeleteFile(file);
                }
            }
            catch (Exception) {
                //todo
            }
        }
        #endregion

        private static void UpdateView() => Utils._MainPage.Dispatcher.Invoke(() => { Utils._MainPage.ImageFiles.Items.Refresh(); }, System.Windows.Threading.DispatcherPriority.Background);

        private static void CopyFile(string file) {
            if (Options.CreateTemp) {
                if (!Directory.Exists($"{Utils.GetTempDir()}"))
                    Directory.CreateDirectory($"{Utils.GetTempDir()}");
                File.Copy(file, $"{Utils.GetTempDir()}");
            }
        }

        private static void DeleteFile(string file) {
            if (Options.ImagesDelete)
                File.Delete(file);
        }

    }
}
