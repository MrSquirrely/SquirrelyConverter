using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using ConverterUtilities;
using VideoConverter.View;

namespace VideoConverter.Class {
    public class VideoUtilities {
        private static readonly List<string> FilesList = new List<string>();
        private static readonly List<string> DirectoriesList = new List<string>();
        private static ObservableCollection<NewFile> VideosCollection = new ObservableCollection<NewFile>();
        public static ListView VideoListView;
        public static VideoView VideoView;
        private const string Queued = "Queued";

        public static void PopulateList(string[] droppedFiles) {
            foreach (string droppedFile in droppedFiles) {
                GetVideos(droppedFile, true);
            }

            foreach (string directory in DirectoriesList) {
                string[] filesInDirectory = Directory.GetFiles(directory);
                foreach (string file in filesInDirectory) {
                    GetVideos(file, false);
                }
            }

            VideoListView.ItemsSource = VideosCollection;
        }

        internal static void Convert(int selectedIndex) {
            switch (selectedIndex) {
                    case 0:
                        ConvertMp4();
                        break;
                    case 1:
                        ConvertWebM();
                        break;
            }
        }

        private static void StartConvertMp4() => Converter.ConvertMp4(FilesList);
        private static void StartConvertWebM() => Converter.ConvertWebM(FilesList);

        private static void ConvertMp4() {
            if (!NullCheck()) {
                CUtilities.Converting = true;
                ThreadStart start = StartConvertMp4;
                start += () => {
                    Toast.ConvertFinished();
                    CUtilities.Converting = false;
                };

                Thread thread = new Thread(start);
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start();
            }
            else {
                //Todo show a message that we are already converting
            }
        }

        private static void ConvertWebM() {
            if (!NullCheck()) {
                CUtilities.Converting = true;
                ThreadStart start = StartConvertWebM;
                start += () => {
                    Toast.ConvertFinished();
                    CUtilities.Converting = false;
                };

                Thread thread = new Thread(start);
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start();
            }
            else {
                //Todo show a message that we are already converting
            }
        }

        private static void GetVideos(string file, bool scanDirectory) {
            string name = CUtilities.GetFileName(file, Enums.FileExtension.Yes);
            string type = CUtilities.GetFileType(file);
            string location = CUtilities.GetFileDirectory(file);
            FileAttributes attributes = File.GetAttributes(file);
            if (Enums.VideoFormats.Contains(type)) {
                VideosCollection.Add(new NewFile{Name = name, Type = type, Converted = Queued, Location = location});
                FilesList.Add(file);
            }

            if (scanDirectory && (attributes & FileAttributes.Directory) == FileAttributes.Directory) {
                DirectoriesList.Add($"{file}\\");
            }
        }

        private static bool NullCheck() {
            if (FilesList == null || CUtilities.Converting) return true;
            else return false;
        }
    }
}
