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
    static class Utilities {
        internal static List<string> DroppedFiles = new List<string>();
        internal static List<string> Files = new List<string>();
        internal static List<string> Dirs = new List<string>();
        internal static ObservableCollection<NewFile> Images = new ObservableCollection<NewFile>(); // I'm keeping both for a reason that might come later.
        internal static ObservableCollection<NewFile> Videos = new ObservableCollection<NewFile>();
        internal static string CurrentVersion = "1.0rc1";
        internal static string UpdateVerstion { get; set; }
        internal static bool FirstClicked = true;

        internal static string WorkingDir { get; set; }
        internal static bool IsFolder { get; set; }
        internal static bool IsWorking { get; set; }
        internal static ListView ImageItems;
        internal static ListView VideoItems;

        internal static WebClient WebClient = new WebClient();
        private static readonly string VERSION_URL = "https://raw.githubusercontent.com/MrSquirrelyNet/SquirrelyConverter/master/current.version";
        private static readonly string VERSION_FILENAME = "current.version";
        internal static readonly string WEBPLOCATION = $"{Directory.GetCurrentDirectory()}/Files/gif2webp.exe";
        private static readonly string GITHUB = "https://github.com/MrSquirrelyNet/SquirrelyConverter/issues";
        internal static MetroWindow MainWindow;
        internal static MainPage MainPage = new MainPage();
        internal static SettingsPage SettingsPage;
        internal static void OpenSettings() => SettingsPage = new SettingsPage();
        internal static void Dispose() {
            Toast.Dispose();
            File.Delete("current.version");
            Logger.Dispose();
        }
        internal static void OpenGithub() => Process.Start(GITHUB);

        #region Utilities
        internal static string GetTempDir() => $"{WorkingDir}\\{(Options.CreateTemp ? $"{Options.TempLocation}" : $"image_temp")}"; //Gets the temp directory
        internal static string GetFileName(string file) => Path.GetFileName(file); // Gets the file name
        internal static string GetFileNameWithoutExtension(string file) => Path.GetFileNameWithoutExtension(file); //Gets the file name without extension Ex: example.txt would be 'example'
        internal static string GetFileType(string file) => Path.GetExtension(file); //Gets the extension of a file
        internal static string GetFileDirectory(string file) => Path.GetDirectoryName(file); //Gets the directory
        internal static string GetFileLocation(string file) => Path.GetFullPath(file); //Gets the location of the file

        internal static void ExtractWebP() {
            Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\Files");
            using (Stream commpressedWeBP = Assembly.GetExecutingAssembly().GetManifestResourceStream(Resources.WebPGif))
            using (BinaryReader binaryReader = new BinaryReader(commpressedWeBP))
            using (FileStream fileStream = new FileStream(WEBPLOCATION, FileMode.OpenOrCreate))
            using (BinaryWriter binaryWriter = new BinaryWriter(fileStream)) {
                binaryWriter.Write(binaryReader.ReadBytes((int)commpressedWeBP.Length));
            }
        }

        internal static void DeleteWebP() {
            if (File.Exists(WEBPLOCATION)) {
                File.Delete(WEBPLOCATION);
                Directory.Delete($"{Directory.GetCurrentDirectory()}/Files");
            }
        }

        internal static void UpdateTitle(int SelectedIndex) {
            switch (SelectedIndex) {
                case 0:
                    MainWindow.Title = "Mr. Squirrely's Image Converter";
                    break;
                case 1:
                    MainWindow.Title = "Mr. Squirrely's Video Converter";
                    Toast.VideoMessage();
                    break;
            }
        }

        internal static void DownloadFiles() {
            try {
                if (File.Exists(@"current.version")) {
                    File.Delete(@"current.version");
                }

                WebClient.DownloadFile(VERSION_URL, VERSION_FILENAME);
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
        }

        internal static void CheckForUpdate(bool ShowSuccess) {
            try {
                StreamReader streamReader = new StreamReader(@"current.version");
                UpdateVerstion = streamReader.ReadLine();
                Console.WriteLine(CurrentVersion);
                Console.WriteLine(UpdateVerstion);
                if (CurrentVersion != UpdateVerstion) {
                    Toast.Update();
                }
                else if (ShowSuccess) {
                    Toast.NoUpdate();
                }

                streamReader.Dispose();
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
        }

        internal static void Clear() {
            ClearLists();
            if (Images.Count != 0) {
                Images.Clear();
                ImageItems.Items.Refresh();
            }
            if (Videos.Count != 0) {
                Videos.Clear();
                VideoItems.Items.Refresh();
            }
        }

        private static void ClearLists() {
            if (DroppedFiles.Count != 0) { DroppedFiles.Clear(); }
            if (Files.Count != 0) { Files.Clear(); }
            if (Dirs.Count != 0) { Dirs.Clear(); }
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
        private static void StartConvertWebM() => Converter.ConvertWebM(Files);
        private static void StartConvertMP4() => Converter.ConvertMP4(Files);

        private static void ConvertWebM() {
            if (DroppedFiles == null) {
                return;
            }
            if (IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart _Starter = StartConvertWebM;
            _Starter += () => {
                Toast.ConvertFinished();
                IsWorking = false;
            };

            _ThreadEncode = new Thread(_Starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }

        private static void ConvertMP4() {
            if (DroppedFiles == null) {
                return;
            }
            if (IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart _Starter = StartConvertMP4;
            _Starter += () => {
                Toast.ConvertFinished();
                IsWorking = false;
            };

            _ThreadEncode = new Thread(_Starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }
        #endregion

        #region Image Converters
        private static void StartConvertJPEG() => Converter.ConvertJPEG(Files);
        private static void StartConvertPNG() => Converter.ConvertPNG(Files);
        private static void StartConvertWebP() => Converter.ConvertWebP(Files);

        private static void ConvertJPEG() {
            if (DroppedFiles == null) {
                return;
            }
            if (IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertJPEG;
            starter += () => {
                Toast.ConvertFinished();
                IsWorking = false;
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }

        private static void ConvertPNG() {
            if (DroppedFiles == null) {
                return;
            }
            if (IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertPNG;
            starter += () => {
                Toast.ConvertFinished();
                IsWorking = false;
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }

        private static void ConvertWebP() {
            if (DroppedFiles == null) {
                return;
            }
            if (IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart starter = StartConvertWebP;
            starter += () => {
                Toast.ConvertFinished();
                IsWorking = false;
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
                Utilities.DroppedFiles.Add(dropped);
            }

            foreach (string file in Utilities.DroppedFiles) {
                GetImages(file, true);
            }

            foreach (string dir in Dirs) {
                string[] files = Directory.GetFiles(dir);
                foreach (string file in files) {
                    GetImages(file, false);
                }
            }
            ImageItems.ItemsSource = Images;
        }

        private static void PopulateVideos(string[] DroppedFiles) {
            ClearLists();

            if (Utilities.DroppedFiles.Count != 0) {
                Utilities.DroppedFiles.Clear();
            }

            foreach (string dropped in DroppedFiles) {
                Utilities.DroppedFiles.Add(dropped);
            }

            foreach (string file in Utilities.DroppedFiles) {
                GetVideos(file, true);
            }

            foreach (string dir in Dirs) {
                string[] files = Directory.GetFiles(dir);
                foreach (string file in files) {
                    GetVideos(file, false);
                }
            }
            VideoItems.ItemsSource = Videos;
        }

        private static void GetImages(string file, bool scanDir) {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string fileType = Path.GetExtension(file);
            string fileLocation = Path.GetFullPath(file);
            FileAttributes fileAttributes = File.GetAttributes(file);
            if (Types.ImageFormats.Contains(fileType)) {
                Images.Add(new NewFile { Name = fileName, Type = fileType, Converted = "Queued", Location = fileLocation });
                Files.Add(file);
            }
            if (scanDir == true) {
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    Dirs.Add($"{file}\\");
                }
            }
        }

        private static void GetVideos(string file, bool scanDir) {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string fileType = Path.GetExtension(file);
            string fileLocation = Path.GetFullPath(file);
            FileAttributes fileAttributes = File.GetAttributes(file);
            if (Types.VideoFormats.Contains(fileType)) {
                Videos.Add(new NewFile { Name = fileName, Type = fileType, Converted = "Queued", Location = fileLocation });
                Files.Add(file);
            }
            if (scanDir == true) {
                if ((fileAttributes & FileAttributes.Directory) == FileAttributes.Directory) {
                    Dirs.Add($"{file}\\");
                }
            }
        }
        #endregion
    }
}
