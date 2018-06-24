using System;
using System.Collections.Generic;
using System.IO;
using ConverterUtilities;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace VideoConverter.Class {
    class Converter {
        public static void ConvertMp4(List<string> files) {
            try {
                foreach (string file in files) {
                    string fileName = CUtilities.GetFileName(file, Enums.FileExtension.No);
                    string fileLocation = CUtilities.GetFileLocation(file);
                    Engine engine = new Engine();
                    MediaFile inputFile = new MediaFile {Filename = file};
                    MediaFile outputFile = new MediaFile {Filename = $"{fileLocation}\\{fileName}.mp4"};
                    if (Options.ChangeSize) {
                        ConversionOptions conversionOptions = new ConversionOptions {
                            CustomWidth = Options.Width,
                            CustomHeight = Options.Height
                        };
                        engine.Convert(inputFile, outputFile, conversionOptions);
                    }
                    else {
                        engine.Convert(inputFile, outputFile);
                    }

                    foreach (NewFile newFile in VideoUtilities.VideosCollection) {
                        int index = VideoUtilities.VideosCollection.IndexOf(newFile);
                        if (VideoUtilities.VideosCollection[index].Location == fileLocation) {
                            VideoUtilities.VideosCollection[index].Converted = "Converted";
                        }
                    }
                    engine.Dispose();
                    Copyfile(file);
                    DeleteFile(file);
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
        }
        public static void ConvertWebM(List<string> files) {
            try {
                foreach (string file in files) {
                    string fileName = CUtilities.GetFileName(file, Enums.FileExtension.No);
                    string fileLocation = CUtilities.GetFileLocation(file);
                    Engine engine = new Engine();
                    MediaFile inputFile = new MediaFile {Filename = file};
                    MediaFile outputFile = new MediaFile {Filename = $"{fileLocation}\\{fileName}.webm"};
                    if (Options.ChangeSize) {
                        ConversionOptions conversionOptions = new ConversionOptions {
                            CustomWidth = Options.Width,
                            CustomHeight = Options.Height
                        };
                        engine.Convert(inputFile, outputFile, conversionOptions);
                    }
                    else {
                        engine.Convert(inputFile, outputFile);
                    }

                    foreach (NewFile newFile in VideoUtilities.VideosCollection) {
                        int index = VideoUtilities.VideosCollection.IndexOf(newFile);
                        if (VideoUtilities.VideosCollection[index].Location == fileLocation) {
                            VideoUtilities.VideosCollection[index].Converted = "Converted";
                        }
                    }
                    engine.Dispose();
                    Copyfile(file);
                    DeleteFile(file);
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
        }

        private static void Copyfile(string file) {
            if (Options.CreateTemp) {
                if (!Directory.Exists($"{CUtilities.GetTempDir(Options.CreateTemp, Options.TempLocation)}")) {
                    Directory.CreateDirectory($"{CUtilities.GetTempDir(Options.CreateTemp, Options.TempLocation)}");
                }
                File.Copy(file, $"{CUtilities.GetTempDir(Options.CreateTemp, Options.TempLocation)}");
            }
        }

        private static void DeleteFile(string file) {
            if (Options.DeleteFile) {
                File.Delete(file);
            }
        }
    }
}
