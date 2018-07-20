using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using ConverterUtilities;
using ConverterUtilities.Configs;
using ConverterUtilities.CUtils;
using ImageConverter.Class.Converters;
using ImageConverter.View;

namespace ImageConverter.Class {
    public class ImageUtilities {
        public static List<string> DroppedFiles = new List<string>();
        public static List<string> Files = new List<string>();
        public static List<string> Directories = new List<string>();
        public static ObservableCollection<NewFile> ImagesCollection = new ObservableCollection<NewFile>();
        public static ListView ImageListView;
        public static MainView ImageView;
        private const string Queued = "Queued";

        public static void PopulateList(string[] droppedFiles) {
            DroppedFiles.Clear();

            foreach (string droppedFile in droppedFiles) {
                DroppedFiles.Add(droppedFile);
                GetImages(droppedFile, true);
            }

            foreach (string directory in Directories) {
                string[] filesInDirectory = Directory.GetFiles(directory);
                foreach (string file in filesInDirectory) {
                    GetImages(file, false);
                }
            }
            ImageListView.ItemsSource = ImagesCollection;
        }

        internal static void Convert(int selectedIndex) {
            if (DroppedFiles.Count < 1) {
                Toast.NothingToConvert();
            } else {
                switch (selectedIndex) {
                    case 0:
                        ConvertWebP();
                        break;
                    case 1:
                        ConvertPng();
                        break;
                    case 2:
                        ConvertJpeg();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

        }
        
        private static void ConvertWebP() {
            if (!NullCheck()) {
                Utilities.SetConverting(true);
                foreach (string file in Files) {
                    if (file != null) {
                        FileInfos infos = new FileInfos(file);
                        if (infos.FileType() == ".webp") {
                            continue;
                        }
                        if (infos.FileType() != ".gif") {
                            WebPConverter converter = new WebPConverter() { Image = file };
                            Utilities.AddThread();
                            converter.StartConvert();
                        } else {
                            WebPGifConverter gifConverter = new WebPGifConverter() { Image = file };
                            Utilities.AddThread();
                            gifConverter.StartConvert();
                        }
                    }
                }
            }
        }

        private static void ConvertPng() {
            if (!NullCheck()) {
                Utilities.SetConverting(true);
                foreach (string file in Files) {
                    FileInfos infos = new FileInfos(file);
                    if (infos.FileType() == ".gif" || infos.FileType() == ".png") {
                        continue;
                    }
                    PngConverter converter = new PngConverter(){ Image = file};
                    Utilities.AddThread();
                    converter.StartConvert();
                }
            }
        }

        private static void ConvertJpeg() {
            if (!NullCheck()) {
                Utilities.SetConverting(true);
                foreach (string file in Files) {
                    FileInfos infos = new FileInfos(file);
                    if (infos.FileType() == ".gif" || infos.FileType() == ".jpg" || infos.FileType() == ".jpeg") {
                        continue;
                    }
                    JpegConverter converter = new JpegConverter(){ Image = file};
                    Utilities.AddThread();
                    converter.StartConvert();
                }
            }
        }

        public static bool NullCheck() {
            if (DroppedFiles == null || Utilities.IsConverting()) {
                return true;
            }

            return false;
        }

        private static void GetImages(string file, bool scanDirectory) {
            FileInfos infos = new FileInfos(file);
            string name = infos.FileNameWithoutExtension();
            string type = infos.FileType();
            string location = infos.FileDirectory();
            FileAttributes attributes = File.GetAttributes(file);
            if (Enums.ImageFormats.Contains(type)) {
                ImagesCollection.Add(new NewFile { Name = name, Type = type, Converted = Queued, Location = location });
                Files.Add(file);
            }
            if (scanDirectory) {
                if ((attributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    Directories.Add($"{file}\\");
                }
            }
        }

        public static void Clear() {
            ImageListView.Items.Refresh();
            DroppedFiles.Clear();
            Files.Clear();
            Directories.Clear();
            ImagesCollection.Clear();
        }
    }
}
