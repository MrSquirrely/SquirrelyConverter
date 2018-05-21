using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Mr_Squirrely_Converters.Properties;
using Mr_Squirrely_Converters.Views;

namespace Mr_Squirrely_Converters.Class {
    // TODO:
    //  Comment what everything is for and does.
    //  Clean code up a bit. It's still not the best looking.
    //  Adding things to there own class.
    class Utils {

        internal static List<string> _DroppedFiles = new List<string>();
        internal static List<string> _Files = new List<string>();
        internal static List<string> _Dirs = new List<string>();
        internal static ObservableCollection<NewFile> _Images = new ObservableCollection<NewFile>(); // I'm keeping both for a reason that might come later.
        internal static ObservableCollection<NewFile> _Videos = new ObservableCollection<NewFile>();
        internal static string _CurrentVersion = "1.0rc1";
        internal static string _UpdateVerstion { get; set; }
        internal static bool _FirstClicked = true;

        internal static string _WorkingDir { get; set; }
        internal static bool _IsFolder { get; set; }
        internal static bool _IsWorking { get; set; }

        internal static ListView _ImageItems;
        internal static ListView _VideoItems;
        internal static DialogHost _VideoDialog;

        internal static WebClient _WebClient = new WebClient();
        private static readonly string _VERSION_URL = "https://raw.githubusercontent.com/MrSquirrelyNet/SquirrelyConverter/master/current.version";
        private static string _VERSION_FILENAME = "current.version";
        private static string _README_URL = "https://raw.githubusercontent.com/MrSquirrelyNet/SquirrelyConverter/master/README.md";
        private readonly static string _README_FILENAME = "README.md";
        internal static string _WebPLocation = $"{Directory.GetCurrentDirectory()}/Files/gif2webp.exe";

        #region Windows
        private static string _Github = "https://github.com/MrSquirrelyNet/SquirrelyConverter/issues";
        internal static MetroWindow _MainWindow;
        internal static MainPage _MainPage = new MainPage();
        internal static SettingsPage _SettingsPage;
        internal static void OpenSettings() => _SettingsPage = new SettingsPage();
        internal static void Dispose() {
            Toast.Dispose();
            DeleteWebP();
            File.Delete("current.version");
        }
        internal static void OpenGithub() => Process.Start(_Github);

        #endregion

        #region Utilities
        internal static string GetTempDir() => $"{_WorkingDir}\\{(Options.CreateTemp ? $"{Options.TempLocation}" : $"image_temp")}"; //Gets the temp directory
        internal static string GetFileName(string file) => Path.GetFileName(file); // Gets the file name
        internal static string GetFileNameWithoutExtension(string file) => Path.GetFileNameWithoutExtension(file); //Gets the file name without extension Ex: example.txt would be 'example'
        internal static string GetFileType(string file) => Path.GetExtension(file); //Gets the extension of a file
        internal static string GetFileDirectory(string file) => Path.GetDirectoryName(file); //Gets the directory
        internal static string GetFileLocation(string file) => Path.GetFullPath(file); //Gets the location of the file

        internal static void ExtractWebP() {
            Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\Files");
            using (Stream commpressedWeBP = Assembly.GetExecutingAssembly().GetManifestResourceStream(Resources.WebPGif))
            using (BinaryReader binaryReader = new BinaryReader(commpressedWeBP))
            using (FileStream fileStream = new FileStream(_WebPLocation, FileMode.OpenOrCreate))
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                binaryWriter.Write(binaryReader.ReadBytes((int)commpressedWeBP.Length));
            
        }

        internal static void DeleteWebP() {
            if (File.Exists(_WebPLocation)) {
                File.Delete(_WebPLocation);
                Directory.Delete($"{Directory.GetCurrentDirectory()}/Files");
            }
        }

        internal static void UpdateTitle(int SelectedIndex) {
            switch (SelectedIndex) {
                case 0:
                    _MainWindow.Title = "Mr. Squirrely's Image Converter";
                    break;
                case 1:
                    _MainWindow.Title = "Mr. Squirrely's Video Converter";
                    if (_FirstClicked == true) {
                        Toast.VideoMessage2();
                        _FirstClicked = false;
                    }
                    break;
            }
        }

        internal static void DownloadFiles() {
            try {
                if (File.Exists(@"current.version")) File.Delete(@"current.version");
                _WebClient.DownloadFile(_VERSION_URL, _VERSION_FILENAME);
            }
            catch (Exception ex) {
                Console.WriteLine("versions not found");
                Console.WriteLine(ex.Source);
                Console.WriteLine(ex.Message);
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
            switch (selectedIndex) {
                case 0:
                    ConvertWebM();
                    break;
                case 1:
                    ConvertMP4();
                    break;
            }
        }
        #endregion

        #region Video Converters
        private static void StartConvertWebM() => Converter.ConvertWebM(_Files);
        private static void StartConvertMP4() => Converter.ConvertMP4(_Files);

        private static void ConvertWebM() {
            if (_DroppedFiles == null) return;
            if (_IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            _IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart _Starter = StartConvertWebM;
            _Starter += () => {
                Toast.ConvertFinished();
                _IsWorking = false;
            };

            _ThreadEncode = new Thread(_Starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }

        private static void ConvertMP4() {
            if (_DroppedFiles == null) return;
            if (_IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            _IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart _Starter = StartConvertMP4;
            _Starter += () => {
                Toast.ConvertFinished();
                _IsWorking = false;
            };

            _ThreadEncode = new Thread(_Starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
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
                _Images.Add(new NewFile { Name = fileName, Type = fileType, Converted = "Queued", Location = fileLocation});
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
