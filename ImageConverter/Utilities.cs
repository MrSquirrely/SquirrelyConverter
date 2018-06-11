using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using ConverterUtilities;
using static ConverterUtilities.Enums;

namespace ImageConverter {
    public class ImageUtilities {
        public static List<string> droppedFiles = new List<string>();
        public static List<string> files = new List<string>();
        public static List<string> directories = new List<string>();
        public static ObservableCollection<NewFile> images = new ObservableCollection<NewFile>();
        public static ListView imageListView;
        public static ImageView imageView;
        public static bool converting = false;
        private static readonly string Queued = "Queued";
        
        public static void PopulateList(string[] DroppedFiles) {
            droppedFiles.Clear();

            foreach (string droppedFile in DroppedFiles) {
                droppedFiles.Add(droppedFile);
            }

            foreach (string file in droppedFiles) {
                GetImages(file, true);
            }

            foreach (string directory in directories) {
                string[] filesInDirectory = Directory.GetFiles(directory);
                foreach (string file in filesInDirectory) {
                    GetImages(file, false);
                }
            }
            imageListView.ItemsSource = images;
        }

        internal static void Convert(int selectedIndex) {
            switch (selectedIndex) {
                case 0:
                    ConvertWebP();
                    break;
                case 1:
                    ConvertPNG();
                    break;
                case 2:
                    ConvertJPEG();
                    break;
            }
        }

        private static void StartConvertWebP() => Converter.ConvertWebP(files);
        private static void StartConvertPNG() => Converter.ConvertPNG(files);
        private static void StartConvertJPEG() => Converter.ConvertJPEG(files);

        private static void ConvertWebP() {
            NullCheck();
            converting = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertWebP;
            starter += () => {
                converting = false;
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }

        private static void ConvertPNG() {
            NullCheck();
            converting = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertPNG;
            starter += () => {
                converting = false;
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }

        private static void ConvertJPEG() {
            NullCheck();
            converting = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertJPEG;
            starter += () => {
                converting = false;
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();

        }

        public static bool NullCheck() {
            if (droppedFiles == null || converting == true) {
                return true;
            }
            else {
                return false;
            }
        }

        private static void GetImages(string file, bool scanDirectory) {
            string Name = CUtilities.GetFileName(file, FileExtension.yes);
            string Type = CUtilities.GetFileType(file);
            string Location = CUtilities.GetFileDirectory(file);
            FileAttributes Attributes = File.GetAttributes(file);
            if (ImageFormats.Contains(Type)) {
                images.Add(new NewFile { Name = Name, Type = Type, Converted = Queued, Location = Location});
                files.Add(file);
            }
            if (scanDirectory) {
                if ((Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    directories.Add($"{file}\\");
                }
            }
        }
    }
}
