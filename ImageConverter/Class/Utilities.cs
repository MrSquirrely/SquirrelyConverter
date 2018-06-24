using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using ConverterUtilities;
using ImageConverter.View;

namespace ImageConverter.Class {
    public class ImageUtilities {
        public static List<string> DroppedFiles = new List<string>();
        public static List<string> Files = new List<string>();
        public static List<string> Directories = new List<string>();
        public static ObservableCollection<NewFile> ImagesCollection = new ObservableCollection<NewFile>();
        public static ListView ImageListView;
        public static ImageView ImageView;
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
            }
        }

        private static void StartConvertWebP() => Converter.ConvertWebP(Files);
        private static void StartConvertPng() => Converter.ConvertPng(Files);
        private static void StartConvertJpeg() => Converter.ConvertJpeg(Files);

        private static void ConvertWebP() {
            if (!NullCheck()) {
                CUtilities.Converting = true;
                ThreadStart starter = StartConvertWebP;
                starter += () => {
                    CUtilities.Converting  = false;
                };

                Thread threadEncode = new Thread(starter);
                threadEncode.SetApartmentState(ApartmentState.STA);
                threadEncode.IsBackground = true;
                threadEncode.Start();
            }
        }

        private static void ConvertPng() {
            if (!NullCheck()) {
                CUtilities.Converting  = true;
                ThreadStart starter = StartConvertPng;
                starter += () => {
                    CUtilities.Converting  = false;
                };

                Thread threadEncode = new Thread(starter);
                threadEncode.SetApartmentState(ApartmentState.STA);
                threadEncode.IsBackground = true;
                threadEncode.Start();
            }
        }

        private static void ConvertJpeg() {
            if (!NullCheck()) {
                CUtilities.Converting  = true;
                ThreadStart starter = StartConvertJpeg;
                starter += () => {
                    CUtilities.Converting  = false;
                };

                Thread threadEncode = new Thread(starter);
                threadEncode.SetApartmentState(ApartmentState.STA);
                threadEncode.IsBackground = true;
                threadEncode.Start();
            }
        }

        public static bool NullCheck() {
            if (DroppedFiles == null || CUtilities.Converting) {
                return true;
            }

            return false;
        }

        private static void GetImages(string file, bool scanDirectory) {
            string name = CUtilities.GetFileName(file, Enums.FileExtension.Yes);
            string type = CUtilities.GetFileType(file);
            string location = CUtilities.GetFileDirectory(file);
            FileAttributes attributes = File.GetAttributes(file);
            if (Enums.ImageFormats.Contains(type)) {
                ImagesCollection.Add(new NewFile { Name = name, Type = type, Converted = Queued, Location = location});
                Files.Add(file);
            }
            if (scanDirectory) {
                if ((attributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    Directories.Add($"{file}\\");
                }
            }
        }
    }
}
