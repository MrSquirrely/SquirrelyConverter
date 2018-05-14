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
using System.Threading;
using System.Net;

namespace Mr_Squirrely_Converters.Class {
    class Utils {

        public static List<string> _DroppedFiles = new List<string>();
        public static List<string> _Files = new List<string>();
        public static List<string> _Dirs = new List<string>();
        public static ObservableCollection<NewImage> _Images = new ObservableCollection<NewImage>();
        public static string _CurrentVersion = "2018.1b";
        public static string _UpdateVerstion { get; set; }

        public static string _WorkingDir { get; set; }
        public static bool _IsFolder { get; set; }
        public static bool _IsWorking { get; set; } = false;

        public static ListView _EncodeItems;
        
        
        private static Convert _Converter = new Convert();
        private static Toast _Toast = new Toast();

        internal static WebClient _WebClient = new WebClient();
        private static string _VERSION_URL = "https://raw.githubusercontent.com/MrSquirrelyNet/SquirrelyConverter/master/current.version";
        private static string _VERSION_FILENAME = "current.version";
        internal static string _README_URL = "https://raw.githubusercontent.com/MrSquirrelyNet/SquirrelyConverter/master/README.md";
        internal static string _README_FILENAME = "README.md";

        #region Windows
        private static About _Aboutwindow;
        private static Settings _SettingsWindow;
        private static String _Github = "https://github.com/MrSquirrelyNet/SquirrelyConverter";

        public static void OpenSettings() {
            _SettingsWindow = new Settings();
            _SettingsWindow.Show();
        }

        public static void CloseSettings() {
            try {
                _SettingsWindow.Close();
            }
            catch (Exception) {

            }
                
        }
        
        public static void OpenAbout() {
            _Aboutwindow = new About();
            _Aboutwindow.Show();
        }
        public static void CloseAbout() {
            try {
                _Aboutwindow.Close();
            }
            catch (Exception) {
            }
                
        }

        public static void CloseWindows() {
            CloseSettings();
            CloseAbout();
            _Toast.Dispose();
        }

        public static void OpenGithub() => Process.Start(_Github);

        #endregion

        internal static void DownloadFiles() {
            try {
                if (File.Exists(Utils._README_FILENAME)) File.Delete(Utils._README_FILENAME);
                _WebClient.DownloadFile(Utils._README_URL, Utils._README_FILENAME);
                if (File.Exists(@"current.version")) File.Delete(@"current.version");
                _WebClient.DownloadFile(_VERSION_URL, _VERSION_FILENAME);
            }
            catch (Exception) {

                throw;
            }
        }
        
        public static void CheckForUpdate(bool ShowSuccess) {
            try {
                StreamReader streamReader = new StreamReader(@"current.version");
                _UpdateVerstion = streamReader.ReadLine();
                Console.WriteLine(_CurrentVersion);
                Console.WriteLine(_UpdateVerstion);
                if (_CurrentVersion != _UpdateVerstion) _Toast.Update();
                else if (ShowSuccess) _Toast.NoUpdate();
            }
            catch (Exception) {

                throw;
            }
        }

        public static void Clear() {
            if (_DroppedFiles.Count != 0) _DroppedFiles.Clear();
            if (_Files.Count != 0) _Files.Clear();
            if (_Dirs.Count != 0) _Dirs.Clear();
            if (_Images.Count != 0) _Images.Clear();
            _EncodeItems.Items.Refresh();
        }

        public static void Convert(int selectedIndex) {
            Console.WriteLine($"Selected index:{selectedIndex}");
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
                default:
                    break;
            }
        }

        private static void StartConvertJPEG() => _Converter.ConvertJPEG(_Files);
        private static void StartConvertPNG() => _Converter.ConvertPNG(_Files);
        private static void StartConvertWebP() => _Converter.ConvertWebP(_Files);

        private static void ConvertJPEG() {
            if (_DroppedFiles == null) return;
            if (_IsWorking) {
                _Toast.AlreadyConverting();
                return;
            }
            _IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertJPEG;
            starter += () => {
                _Toast.ConvertFinished();
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
                _Toast.AlreadyConverting();
                return;
            }
            _IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertPNG;
            starter += () => {
                _Toast.ConvertFinished();
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
                _Toast.AlreadyConverting();
                return;
            }
            _IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertWebP;
            starter += () => {
                _Toast.ConvertFinished();
                _IsWorking = false;
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
            
        }

        public static void PopulateList(string[] DroppedFiles) {
            foreach(string dropped in DroppedFiles) {
                _DroppedFiles.Add(dropped);
            }

            foreach (string file in _DroppedFiles) {
                GetFiles(file, true);
            }
            foreach (string dir in _Dirs) {
                string[] files = Directory.GetFiles(dir);
                foreach(string file in files) {
                    GetFiles(file, false);
                }
            }
            _EncodeItems.ItemsSource = _Images;
        }

        public static void GetFiles(string file, bool scanDir) {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string fileType = Path.GetExtension(file);
            string fileLocation = Path.GetFullPath(file);
            FileAttributes fileAttributes = File.GetAttributes(file);
            if (Types.ImageFormats.Contains(fileType)) {
                _Images.Add(new NewImage { Name = fileName, Type = fileType, Converted = "Queued", Location = fileLocation });
                _Files.Add(file);
            }
            if (scanDir == true) {
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    _Dirs.Add($"{file}\\");
                }
            }
        }

    }
}
