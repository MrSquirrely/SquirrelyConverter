using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConverterUtilities;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using static ConverterUtilities.Enums;

namespace VideoConverter {
    class Converter {
        public static void ConvertMP4(List<string> files) {
            try {
                foreach (string file in files) {
                    string fileName = CUtilities.GetFileName(file, FileExtension.No);
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
                    string fileName = CUtilities.GetFileName(file, FileExtension.No);
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
