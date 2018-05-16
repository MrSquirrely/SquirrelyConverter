using Mr_Squirrely_Converters.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Immutable;
using System.Threading;
using System.Net;
using MahApps.Metro.Controls;

namespace Mr_Squirrely_Converters.Class {
    class Utils {

        internal static List<string> _DroppedFiles = new List<string>();
        internal static List<string> _Files = new List<string>();
        internal static List<string> _Dirs = new List<string>();
        internal static ObservableCollection<NewFile> _Images = new ObservableCollection<NewFile>(); // I'm keeping both for a reason that might come later.
        internal static ObservableCollection<NewFile> _Videos = new ObservableCollection<NewFile>();
        internal static string _CurrentVersion = "2018.1b";
        internal static string _UpdateVerstion { get; set; }

        internal static string _WorkingDir { get; set; }
        internal static bool _IsFolder { get; set; }
        internal static bool _IsWorking { get; set; } = false;

        internal static ListView _ImageItems;
        internal static ListView _VideoItems;
        internal static MetroWindow _MainWindow;
        internal static MainPage _MainPage = new MainPage();
        internal static SettingsPage _SettingsPage = new SettingsPage();

        internal static WebClient _WebClient = new WebClient();
        private static string _VERSION_URL = "https://raw.githubusercontent.com/MrSquirrelyNet/SquirrelyConverter/master/current.version";
        private static string _VERSION_FILENAME = "current.version";
        private  static string _README_URL = "https://raw.githubusercontent.com/MrSquirrelyNet/SquirrelyConverter/master/README.md";
        private readonly static string _README_FILENAME = "README.md";

        #region Windows
        private static About _Aboutwindow;
        private static Settings _SettingsWindow;
        private static String _Github = "https://github.com/MrSquirrelyNet/SquirrelyConverter/issues";

        internal static void OpenSettings() {
            _SettingsWindow = new Settings();
            _SettingsWindow.Show();
        }

        internal static void CloseSettings() {
            try {
                _SettingsWindow.Close();
            }
            catch (Exception) {

            }
                
        }

        internal static void OpenAbout() {
            _Aboutwindow = new About();
            _Aboutwindow.Show();
        }

        internal static void CloseAbout() {
            try {
                _Aboutwindow.Close();
            }
            catch (Exception) {
            }
                
        }

        internal static void CloseWindows() {
            CloseSettings();
            CloseAbout();
            Toast.Dispose();
        }

        internal static void OpenGithub() => Process.Start(_Github);

        #endregion

        #region Utilities
        internal static void UpdateTitle(int SelectedIndex) {
            switch (SelectedIndex) {
                case 0:
                    _MainWindow.Title = "Mr. Squirrely's Image Converter";
                    break;
                case 1:
                    _MainWindow.Title = "Mr. Squirrely's Video Converter";
                    break;
            }
        }

        internal static void DownloadFiles() {
            try {
                if (File.Exists(Utils._README_FILENAME)) File.Delete(Utils._README_FILENAME);
                _WebClient.DownloadFile(Utils._README_URL, Utils._README_FILENAME);
                if (File.Exists(@"current.version")) File.Delete(@"current.version");
                _WebClient.DownloadFile(_VERSION_URL, _VERSION_FILENAME);
            }
            catch (Exception) {
            }
        }
        internal static void CheckForUpdate(bool ShowSuccess) {
            try {
                StreamReader streamReader = new StreamReader(@"current.version");
                _UpdateVerstion = streamReader.ReadLine();
                Console.WriteLine(_CurrentVersion);
                Console.WriteLine(_UpdateVerstion);
                if (_CurrentVersion != _UpdateVerstion) Toast.Update();
                else if (ShowSuccess) Toast.NoUpdate();
                streamReader.Dispose();
            }
            catch (Exception) {
            }
        }
        
        internal static void Clear() {
            ClearLists();
            if (_Images.Count != 0) {
                _Images.Clear();
                _ImageItems.Items.Refresh();
            }
            if (_Videos.Count != 0) {
                _Videos.Clear();
                _VideoItems.Items.Refresh();
            }
        }

        private static void ClearLists() {
            if (_DroppedFiles.Count != 0) _DroppedFiles.Clear();
            if (_Files.Count != 0) _Files.Clear();
            if (_Dirs.Count != 0) _Dirs.Clear();
        }
        #endregion

        #region Start Converters
        internal static void Convert(int selectedIndex, string Type) {
            switch (Type) {
                case "images":
                    ConvertImage(selectedIndex);
                    break;
                case "videos":
                    ConvertVideo(selectedIndex);
                    break;
            }
            
        }

        private static void ConvertImage(int selectedIndex) {
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

        private static void ConvertVideo(int selectedIndex) {
            //Add a switch case here for videos
        }
        #endregion

        #region Image Converters
        private static void StartConvertJPEG() => Converter.ConvertJPEG(_Files);
        private static void StartConvertPNG() => Converter.ConvertPNG(_Files);
        private static void StartConvertWebP() => Converter.ConvertWebP(_Files);

        private static void ConvertJPEG() {
            if (_DroppedFiles == null) return;
            if (_IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            _IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertJPEG;
            starter += () => {
                Toast.ConvertFinished();
                _IsWorking = false;
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }

        private static void ConvertPNG() {
            if (_DroppedFiles == null) return;
            if (_IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            _IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertPNG;
            starter += () => {
                Toast.ConvertFinished();
                _IsWorking = false;
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }

        private static void ConvertWebP() {
            if (_DroppedFiles == null) return;
            if (_IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            _IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertWebP;
            starter += () => {
                Toast.ConvertFinished();
                _IsWorking = false;
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
            
        }
        #endregion

        #region Populate Lists
        internal static void PopulateList(string[] DroppedFiles, string Type) {
            switch (Type) {
                case "images":
                    PopulateImages(DroppedFiles);
                    break;
                case "videos":
                    PopulateVideos(DroppedFiles);
                    break;
            }
        }

        private static void PopulateImages(string[] DroppedFiles) {
            ClearLists();

            foreach (string dropped in DroppedFiles) {
                _DroppedFiles.Add(dropped);
            }

            foreach (string file in _DroppedFiles) {
                GetImages(file, true);
            }

            foreach (string dir in _Dirs) {
                string[] files = Directory.GetFiles(dir);
                foreach (string file in files) {
                    GetImages(file, false);
                }
            }
            _ImageItems.ItemsSource = _Images;
        }

        private static void PopulateVideos(string[] DroppedFiles) {
            ClearLists();

            if (_DroppedFiles.Count != 0) _DroppedFiles.Clear();

            foreach (string dropped in DroppedFiles) {
                _DroppedFiles.Add(dropped);
            }

            foreach (string file in _DroppedFiles) {
                GetVideos(file, true);
            }

            foreach (string dir in _Dirs) {
                string[] files = Directory.GetFiles(dir);
                foreach(string file in files) {
                    GetVideos(file, false);
                }
            }
            _VideoItems.ItemsSource = _Videos;
        }

        private static void GetImages(string file, bool scanDir) {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string fileType = Path.GetExtension(file);
            string fileLocation = Path.GetFullPath(file);
            FileAttributes fileAttributes = File.GetAttributes(file);
            if (Types.ImageFormats.Contains(fileType)) {
                _Images.Add(new NewFile { Name = fileName, Type = fileType, Converted = "Queued", Location = fileLocation });
                _Files.Add(file);
            }
            if (scanDir == true) {
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    _Dirs.Add($"{file}\\");
                }
            }
        }

        private static void GetVideos(string file, bool scanDir) {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string fileType = Path.GetExtension(file);
            string fileLocation = Path.GetFullPath(file);
            FileAttributes fileAttributes = File.GetAttributes(file);
            if (Types.VideoFormats.Contains(fileType)) {
                _Videos.Add(new NewFile { Name = fileName, Type = fileType, Converted = "Queued", Location = fileLocation });
                _Files.Add(file);
            }
            if (scanDir == true) {
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    _Dirs.Add($"{file}\\");
                }
            }
        }
        #endregion

    }
}
