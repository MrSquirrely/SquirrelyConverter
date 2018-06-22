using System;
using System.IO;
using System.Windows.Threading;
using static ConverterUtilities.Enums;
using MahApps.Metro.Controls;
using System.Net;
using System.Diagnostics;

namespace ConverterUtilities {
    /// <summary>
    /// Almost everything has to have an explanation, if it doesn't yell at me
    /// Everything needs to go into a region, I don't care if you like them or not
    /// Always be verbose, no 'var'
    /// </summary>
    public static class CUtilities {

        #region Open Webpages
        /// <summary>
        /// The url for the issues page
        /// </summary>
        private static readonly string Github = "https://github.com/MrSquirrelyNet/SquirrelyConverter/issues";
        /// <summary>
        /// Opens the browser to the issues page on github.com
        /// </summary>
        public static void OpenGithub() => Process.Start(Github);
        #endregion

        #region Versions
        public static StreamReader streamReader;

        /// <summary>
        /// Various variables for things
        /// *Version tags are the current version of said code
        /// *Ver tags are the file names of the download version 
        /// *Update tags are the download version
        /// *URL tags are the urls for the *Ver file
        /// </summary>
        private const string MainVersion = "1.0";
        private const  string MainVer = "main.ver";
        private static string MainUpdate { get; set; }
        private const string MainUrl = "https://gist.githubusercontent.com/MrSquirrely/bff5b9882c96d41e522963dff089d918/raw/d211cf300bfcfc608b98e9519a9a7ebac3e22da5/main.ver";
        private const string ImageVersion = "1.0";
        private const string ImageVer = "image.ver";
        private static string ImageUpdate { get; set; }
        private const string ImageUrl = "https://gist.githubusercontent.com/MrSquirrely/011d504b44da238b95a8c831d9936215/raw/fd059936794116ea37089a4de36b6e77f902fa01/image.ver";
        private const string VideoVersion = "1.0";
        private const string VideoVer = "video.ver";
        private static string VideoUpdate { get; set; }
        private const  string VideoUrl = "https://gist.githubusercontent.com/MrSquirrely/593bbe37c01fb30a8888870637aeae4c/raw/dd5d1755940fd1f7baa636e36b29c1e3e301e838/video.ver";

        /// <summary>
        /// This is just the webclient used to download things
        /// </summary>
        private static WebClient webClient;

        /// <summary>
        /// Checks the version based on what you want
        /// Todo: implement
        /// </summary>
        /// <param name="main">True if you want to check for update of the main program</param>
        /// <param name="image">True if you want to check for update to the image conversion library</param>
        /// <param name="video">True if you want to check for update to the video conversion library</param>
        public static void CheckVersion(bool main, bool image, bool video) {
            if (main) {
                File.Delete(MainVer);
                webClient = new WebClient();
                webClient.DownloadFile(MainUrl, MainVer);
                streamReader = new StreamReader(MainVer);
                MainUpdate = streamReader.ReadLine();
                if (MainVersion != MainUpdate) {
                    Toast.Update(MainVersion, MainUpdate);
                }
                else {
                    Toast.NoUpdate();
                }
                streamReader.Dispose();
                webClient.Dispose();
            }
            if (image) {
                File.Delete(ImageVer);
                webClient = new WebClient();
                webClient.DownloadFile(ImageUrl, ImageVer);
                streamReader = new StreamReader(ImageVer);
                ImageUpdate = streamReader.ReadLine();
                if (ImageVersion != ImageUpdate) {
                    Toast.Update(ImageVersion, ImageUpdate);
                }
                else {
                    Toast.NoUpdate();
                }
                streamReader.Dispose();
                webClient.Dispose();
            }
            if (video) {
                File.Delete(VideoVer);
                webClient = new WebClient();
                webClient.DownloadFile(VideoUrl, VideoVer);
                streamReader = new StreamReader(VideoVer);
                VideoUpdate = streamReader.ReadLine();
                if (VideoVersion != VideoUpdate) {
                    Toast.Update(VideoVersion, VideoUpdate);
                }
                else {
                    Toast.NoUpdate();
                }
                streamReader.Dispose();
                webClient.Dispose();
            }
        }
        #endregion

        #region Window Things
        /// <summary>
        /// Gets or sets wether we are currently converting something
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
        /// <summary>
        /// Gets or sets the dispatcher
        /// </summary>
        public static Dispatcher Dispatcher { get; set; }
        /// <summary>
        /// Updates the title of the Main Window
        /// </summary>
        /// <param name="title">Title to update window to</param>
        public static void UpdateTitle(string title) => MainWindow.Title = title;
        /// <summary>
        /// Disposes of things during shutdown
        /// </summary>
        public static void Dispose() {
            try {
                Toast.Dispose();
            }
            catch {

            }

            try {
            Logger.Dispose();
            }
            catch {

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
        private static string _workdingDir;
        /// <summary>
        /// Gets the working directory
        /// </summary>
        public static string GetWorkdingDir() => _workdingDir ?? throw new Exception("No directory specified");
        /// <summary>
        /// Sets the working directry
        /// </summary>
        /// <param name="value">The directory to set it to</param>
        public static void SetWorkdingDir(string value) => _workdingDir = value;
        /// <summary>
        /// Gets the temp directory to store temp files
        /// </summary>
        /// <param name="createTemp">Wethere or not we are creating a custom temp directory</param>
        /// <param name="tempLocation">The name of the custom temp directory</param>
        /// <returns></returns>
        public static string GetTempDir(bool createTemp, string tempLocation) => $"{_workdingDir}\\{(createTemp ? $"{tempLocation}" : "converter_temp")}";
        #endregion
    }
}
