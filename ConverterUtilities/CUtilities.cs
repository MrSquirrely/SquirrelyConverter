using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Threading;
using static ConverterUtilities.Enums;
using System.Net;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using Exception = System.Exception;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;

namespace ConverterUtilities {
    /// <summary>
    /// Almost everything has to have an explanation, if it doesn't yell at me
    /// Everything needs to go into a region, I don't care if you like them or not
    /// Always be verbose, no 'var'
    /// Todo comment everything
    /// </summary>
    public static class CUtilities {

        public static int Threads;

        

        #region Open Webpages
        /// <summary>
        /// The url for the issues page
        /// </summary>
        private static readonly string Github = "https://github.com/MrSquirrelyNet/SquirrelyConverter/issues";

        /// <summary>
        /// Opens the browser to the issues page on github.com
        /// </summary>
        public static void OpenGithub() => Process.Start(Github);

        private const string Download = "https://github.com/MrSquirrelyNet/SquirrelyConverter/releases";
        private static void OpenDownload(object sender, RoutedEventArgs e) => Process.Start(Download);
        #endregion

        #region Versions
        private const string VersionUrl = "https://raw.githubusercontent.com/MrSquirrelyNet/SquirrelyConverter/master/version.json";
        private const string VersionName = "version.json";
        private const string CurrentName = "current.json";
        public static string MainUrl { get; private set; }
        public static string ImageUrl { get; private set; }
        public static string VideoUrl {get; private set; }
        public const string MainName = "Squirrelys_Converters_Main.DontDownload.zip";
        public const string ImageName = "Squirrelys_Converters_Image.DontDownload.zip";
        public const string VideoName = "Squirrelys_Converters_Video.DontDownload.zip";
        public static readonly string DefaultInstall = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\MrSquirrelyNet\\SquirrelyConverters";

        /// <summary>
        /// This is just the WebClient used to download things
        /// </summary>
        private static WebClient _webClient;

        public static void StartWebClient() => _webClient = new WebClient();

        private static Update _currentVersion;
        private static Update _updateVersion;

        public static void GetVersionJson() {
            if (File.Exists(VersionName)) {
                File.Delete(VersionName);
            }

            _webClient = new WebClient();
            _webClient.DownloadFile(VersionUrl, VersionName);

            StreamReader updateReader = new StreamReader($"{GetWorkingDir()}\\{VersionName}");
            StreamReader currentReader = new StreamReader($"{GetWorkingDir()}\\{CurrentName}");

            JsonSerializer jsonSerializer = new JsonSerializer();

            _currentVersion = (Update) jsonSerializer.Deserialize(currentReader, typeof(Update));
            _updateVersion = (Update) jsonSerializer.Deserialize(updateReader, typeof(Update));

            MainUrl = _updateVersion.MainVersionUrl;
            ImageUrl = _updateVersion.ImageVersionUrl;
            VideoUrl = _updateVersion.VideoVersionUrl;
        }

        public static void GetMainVersion() => _webClient.DownloadFile(_updateVersion.MainVersionUrl, MainName);
        public static void DeleteMainVersion() => File.Delete(MainName);
        public static void GetImageVersion() => _webClient.DownloadFile(_updateVersion.ImageVersionUrl, MainName);
        public static void DeleteImageVersion() => File.Delete(ImageName);
        public static void GetVideoVersion() => _webClient.DownloadFile(_updateVersion.VideoVersionUrl, MainName);
        public static void DeleteVideoVersion() => File.Delete(VideoName);

        private static Button ButtonDownload() {
            Button button = new Button() {
                Name = "RightWindowDownload",
                ToolTip = "There are updates. Click here to go to download page",
                Content = new PackIcon() { Kind = PackIconKind.Download, Foreground = Brushes.LimeGreen }
            };
            button.Click += OpenDownload;
            return button;
        }

        /// <summary>
        /// Checks if you have the current version...
        /// We then need to download them if they say yes.
        /// Todo implement dialog
        /// </summary>
        public static void CheckUpdate() {
            try {
                if (_currentVersion.MainVersion < _updateVersion.MainVersion) {
                    Toast.Update("Main Program", _currentVersion.MainVersion, _updateVersion.MainVersion);
                    MainWindow.RightWindowCommands.Items.Add(ButtonDownload());
                    //DownloadButton.IsEnabled = true;
                    //DownloadButton.Visibility = Visibility.Visible;
                    //switch (UpdateResult("Main Program", _versionUpdate.MainVersion, _updateUpdate.MainVersion)) {
                    //    case DialogBox.ResultEnum.LeftButtonClicked:
                    //        Console.WriteLine("Left Clicked");
                    //        break;
                    //    case DialogBox.ResultEnum.RightButtonClicked:
                    //        Console.WriteLine("Right Clicked");
                    //        break;
                    //    default:
                    //        throw new ArgumentOutOfRangeException();
                    //}
                }

                if (_imageLoaded &&_currentVersion.ImageVersion < _updateVersion.ImageVersion ) {
                        Toast.Update("Image Converter", _currentVersion.ImageVersion, _updateVersion.ImageVersion);
                        //switch (UpdateResult("Iamge Converter", _versionUpdate.ImageVersion, _updateUpdate.ImageVersion)) {
                        //    case DialogBox.ResultEnum.LeftButtonClicked:
                        //        Console.WriteLine("Left Clicked");
                        //        break;
                        //    case DialogBox.ResultEnum.RightButtonClicked:
                        //        Console.WriteLine("Right Clicked");
                        //        break;
                        //    default:
                        //        throw new ArgumentOutOfRangeException();
                        //}
                }

                if (_videoLoaded && _currentVersion.VideoVersion < _updateVersion.VideoVersion) {
                        Toast.Update("Video Converter", _currentVersion.VideoVersion, _updateVersion.VideoVersion);
                        //switch (UpdateResult("Video Converter", _versionUpdate.VideoVersion, _updateUpdate.VideoVersion)) {
                        //    case DialogBox.ResultEnum.LeftButtonClicked:
                        //        Console.WriteLine("Left Clicked");
                        //        break;
                        //    case DialogBox.ResultEnum.RightButtonClicked:
                        //        Console.WriteLine("Right Clicked");
                        //        break;
                        //    default:
                        //        throw new ArgumentOutOfRangeException();
                        //}
                }
            }
            catch (Exception ex) {
                Toast.UpdateCheckFail();
                Logger.LogError("Update check  failed");
                Logger.LogError(ex);
            }
        }

        // todo Implement a dialog to show
        //private static DialogBox.ResultEnum UpdateResult(string versionName, double currentVersion, double updateVersion) {
        //    DialogBox.ResultEnum result = DialogBox.Show(
        //        $"{versionName}\b Has an update", 
        //        $"There is an update to {versionName}, Your version is {currentVersion} and the update version is {updateVersion}" +
        //        $" Do you want to download the update? This will close the program and re-open once installed.", 
        //        "Yes", 
        //        "No");
        //    //MessageDialogResult result = await MainWindow.ShowMessageAsync($"{versionName} Has an update", $"There is an update to {versionName}, Your version is {currentVersion} and the update version is {updateVersion}" +
        //    //                                                                                               $"Do you want to download the update? This will close the program and reopen once installed.");
        //    return result;
        //}
        #endregion

        #region Window Things
        /// <summary>
        /// Gets or sets weather we are currently converting something
        /// </summary>
        public static bool Converting { get; set; }
        /// <summary>
        /// Is the image view loaded
        /// </summary>
        private static bool _imageLoaded;
        /// <summary>
        /// Is the video view loaded
        /// </summary>
        private static bool _videoLoaded;
        /// <summary>
        /// Returns if the image view is loaded or not
        /// </summary>
        public static bool IsImageLoaded { get => _imageLoaded; set => _imageLoaded = value; }
        /// <summary>
        /// Returns if the video view is loaded or not
        /// </summary>
        public static bool IsVideoLoaded { get => _videoLoaded; set => _videoLoaded = value; }
        /// <summary>
        /// Gets or sets the Main Window for other projects to update various items
        /// </summary>
        public static MetroWindow MainWindow { get; set; }
        public static Window SettingsWindow { get; set; }
        /// <summary>
        /// Gets or sets the dispatcher
        /// </summary>
        public static Dispatcher Dispatcher { get; set; }
        /// <summary>
        /// Disposes of things during shutdown
        /// </summary>
        public static void Dispose() {
            try {
                Toast.Dispose();
            }
            catch(Exception ex) {
                Logger.LogError("Couldn't Stop Toast");
                Logger.LogError(ex);
            }

            try {
            Logger.Dispose();
            }
            catch(Exception ex) {
                Logger.LogError("Couldn't Stop Logger");
                Logger.LogError(ex);
            }

            if (SettingsWindow.IsLoaded) {
                SettingsWindow.Close();
            }
        }
        #endregion

        #region Get File Methods
        /// <summary>
        /// Gets the file name of a given file
        /// </summary>
        /// <param name="file">The file in which you want to get the name</param>
        /// <param name="extension">Weather or not </param>
        /// <returns></returns>
        public static string GetFileName(string file, FileExtension extension) {
            string fileName = "null";
            switch (extension) {
                case FileExtension.Yes:
                    fileName = Path.GetFileName(file);
                    break;
                case FileExtension.No:
                    fileName = Path.GetFileNameWithoutExtension(file);
                    break;
            }
            return fileName;
        }
        /// <summary>
        /// Gets the file type of a given file
        /// </summary>
        /// <param name="file">The file in which you want to get the name type</param>
        /// <returns></returns>
        public static string GetFileType(string file) => Path.GetExtension(file);
        /// <summary>
        /// Gets the directory name of a file
        /// </summary>
        /// <param name="file">The file in which you want to get the directory</param>
        /// <returns></returns>
        public static string GetFileDirectory(string file) => Path.GetDirectoryName(file);
        /// <summary>
        /// Gets the full path including the file name of a file
        /// </summary>
        /// <param name="file">The file in which you want to get the path</param>
        /// <returns></returns>
        public static string GetFileLocation(string file) => Path.GetFullPath(file);
        #endregion

        #region Directory Info
        private static string _workingDir;
        /// <summary>
        /// Gets the working directory
        /// </summary>
        public static string GetWorkingDir() => _workingDir ?? throw new Exception("No directory specified");
        /// <summary>
        /// Sets the working directory
        /// </summary>
        /// <param name="value">The directory to set it to</param>
        public static void SetWorkingDir(string value) => _workingDir = value;
        /// <summary>
        /// Gets the temp directory to store temp files
        /// </summary>
        /// <param name="createTemp">Weather or not we are creating a custom temp directory</param>
        /// <param name="tempLocation">The name of the custom temp directory</param>
        /// <returns></returns>
        public static string GetTempDir(bool createTemp, string tempLocation) => $"{_workingDir}\\{(createTemp ? $"{tempLocation}" : "converter_temp")}";
        public static ObservableCollection<Object> Views = new ObservableCollection<Object>();
        public static void AddView(Object view) => Views.Add(view);
        #endregion
    }
}
