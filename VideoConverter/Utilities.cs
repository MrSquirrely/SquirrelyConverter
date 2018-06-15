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

namespace VideoConverter {
    public class VideoUtilities {
        public static List<string> DroppedFilesList = new List<string>();
        public static List<string> FilesList = new List<string>();
        public static List<string> DirectoriesList = new List<string>();
        public static ObservableCollection<NewFile> VideosCollection = new ObservableCollection<NewFile>();
        public static ListView VideoListView;
        public static VideoView VideoView;
        private const string Queued = "Queued";

        public static void PopulateList(string[] droppedFiles) {
            DroppedFilesList.Clear();
            foreach (string droppedFile in droppedFiles) {
                DroppedFilesList.Add(droppedFile);
                GetVideos(droppedFile, true);
            }

            foreach (string directory in DirectoriesList) {
                string[] filesInDirectory = Directory.GetFiles(directory);
                foreach (string file in filesInDirectory) {
                    GetVideos(file, false);
                }
            }
        }

        internal static void Convert(int selectedIndex) {
            switch (selectedIndex) {
                    case 0:
                        break;
                    case 1:
                        break;
            }
        }

        private static void StartConvertMP4() => Converter.ConvertMP4(FilesList);
        private static void StartConvertWebM() => Converter.ConvertWebM(FilesList);

        private static void ConvertMP4() {
            if (!NullCheck()) {
                CUtilities.Converting = true;
                ThreadStart start = StartConvertMP4;
                start += () => {
                    Toast.Instance.ConvertFinished();
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
                    Toast.Instance.ConvertFinished();
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
            string name = CUtilities.GetFileName(file, FileExtension.Yes);
            string type = CUtilities.GetFileType(file);
            string location = CUtilities.GetFileDirectory(file);
            FileAttributes attributes = File.GetAttributes(file);
            if (VideoFormats.Contains(type)) {
                VideosCollection.Add(new NewFile{Name = name, Type = type, Converted = Queued, Location = location});
                FilesList.Add(file);
            }

            if (scanDirectory && (attributes & FileAttributes.Directory) == FileAttributes.Directory) {
                DirectoriesList.Add($"{file}\\");
            }
        }

        private static bool NullCheck() {
            if (FilesList == null || CUtilities.Converting == true) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
