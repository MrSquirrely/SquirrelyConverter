using System;
using System.Collections.Generic;
using System.IO;
using ConverterUtilities;
using ConverterUtilities.Configs;
using ConverterUtilities.CUtils;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace VideoConverter.Class {
    class Converter {
        private static Engine _engine;

        public static void ConvertMp4(List<string> files) {
            try {
                foreach (string file in files) {
                    string fileName = CUtilities.GetFileName(file, Enums.FileExtension.No);
                    string fileLocation = CUtilities.GetFileDirectory(file);
                    _engine = new Engine();
                    _engine.ConversionCompleteEvent += EngineOnConversionCompleteEvent;
                    MediaFile inputFile = new MediaFile { Filename = file };
                    MediaFile outputFile = new MediaFile { Filename = $"{fileLocation}\\{fileName}.mp4" };
                    if (Options.GetVideoChangeSize()) {
                        ConversionOptions conversionOptions = new ConversionOptions {
                            CustomWidth = Options.GetVideoWidth(),
                            CustomHeight = Options.GetVideoHeight()
                        };
                        _engine.Convert(inputFile, outputFile, conversionOptions);
                    }
                    else {
                        _engine.Convert(inputFile, outputFile);
                    }

                    foreach (NewFile newFile in VideoUtilities.VideosCollection) {
                        int index = VideoUtilities.VideosCollection.IndexOf(newFile);
                        if (VideoUtilities.VideosCollection[index].Location == fileLocation) {
                            VideoUtilities.VideosCollection[index].Converted = "Converted";
                        }
                    }
                    Finish(file);
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
                    string fileLocation = CUtilities.GetFileDirectory(file);
                    _engine = new Engine();
                    _engine.ConversionCompleteEvent += EngineOnConversionCompleteEvent;
                    MediaFile inputFile = new MediaFile {Filename = file};
                    MediaFile outputFile = new MediaFile {Filename = $"{fileLocation}\\{fileName}.webm"};
                    if (Options.GetVideoChangeSize()) {
                        ConversionOptions conversionOptions = new ConversionOptions {
                            CustomWidth = Options.GetVideoWidth(),
                            CustomHeight = Options.GetVideoHeight()
                        };
                        _engine.Convert(inputFile, outputFile, conversionOptions);
                    }
                    else {
                        _engine.Convert(inputFile, outputFile);
                    }

                    foreach (NewFile newFile in VideoUtilities.VideosCollection) {
                        int index = VideoUtilities.VideosCollection.IndexOf(newFile);
                        if (VideoUtilities.VideosCollection[index].Location == fileLocation) {
                            VideoUtilities.VideosCollection[index].Converted = "Converted";
                        }
                    }
                    Finish(file);
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
        }

        private static void EngineOnConversionCompleteEvent(object sender, ConversionCompleteEventArgs e) => _engine.Dispose();

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
    }
}
