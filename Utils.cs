#region LICENSE
//WebP Converter || Convert your images to WebP
//    Copyright(C) 2017  James Ferguson<MrSquirrely.net>


//   This program is free software: you can redistribute it and/or modify

//   it under the terms of the GNU General Public License as published by

//   the Free Software Foundation, either version 3 of the License, or

//   (at your option) any later version.

//   This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Notifications.Wpf;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;

namespace SquirrelyConverter {
    class Utils {
        #region Variables
        public static string WorkingDir;
        //public static string WorkingDir;
        public static bool IsFolder;
        public static bool HasFolder = false;
        public static List<string> Dirs = new List<string>();
        public static List<string> Files = new List<string>();
        public static List<string> DecodeFiles = new List<string>();
        public static List<string> DroppedFiles = new List<string>();
        public static bool RemovedItems = true;
        public static int DirNum = -1;
        public static double FileNum;
        public static double CurrentImage;
        public static string TempDir = "/Stemp/";
        private static int _tempDirNum;
        private static string _tempDirFull;
        public static bool IsRunning = false;

        public const string Encode = "encode";
        public const string Decode = "decode";

        public static string FileName;
        public static string FileType;
        public static string FileLocation;

        private static string ErrorFile = "/Logs/Error.txt";

        public static MetroWindow Main;
        public static ListView encodeItems;

        private static readonly NotificationManager _toast = new NotificationManager();
        private static Thread _ThreadEncode;
        #endregion

        #region Start Encode
        public static async void StartEncodeAsync() {
            if (DroppedFiles == null) return;
            if (Options.ChangeTemp && !Directory.Exists(Options.TempDir)) await DialogManager.ShowMessageAsync(Main, "Missing Temp Folder", "Cannot find the temp folder, make sure it exists!", MessageDialogStyle.Affirmative);
            if(Options.SetCustomOutput && !Directory.Exists(Options.OutDir)) await DialogManager.ShowMessageAsync(Main, "Missing Output Folder", "Cannot find the output folder, make sure it exists!", MessageDialogStyle.Affirmative);
            CheckFolderAsync();
            BackupFiles();

            ThreadStart starter = EncodeStarter;
            starter += () => {
                ShowToast(Toast.Finished);
                IsRunning = false;
                if (!Options.DeleteTemp) return;
                if (!Options.ChangeTemp) DeleteFolder($"{WorkingDir}/{Utils.TempDir}");
            };

            _ThreadEncode = new Thread(starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }
        private static void EncodeStarter() => Convert.WebPEncode();
        #endregion

        #region Clear Items
        internal static void ClearItems(int selectedIndex, KeyEventArgs e) {
            if (e.Key != Key.Delete) return;
            if (selectedIndex <= -1) return;
            Files.RemoveAt(selectedIndex);
            encodeItems.Items.RemoveAt(selectedIndex);
            encodeItems.Items.Refresh();
        }
        #endregion

        #region Open Settings Window
        public static void OpenSettings() {
            SettingsWindow sets = new SettingsWindow();
            sets.ShowDialog();
        }
        #endregion

        #region Items Dropped
        public static async void ItemsDroppedAsync(string[] files) {
            if (!IsRunning) {
                if (encodeItems.HasItems) {
                    MessageDialogResult result = await DialogManager.ShowMessageAsync(Main, "Already Files In Loaded", "There are already files loaded, Do you wish to delete them?", MessageDialogStyle.AffirmativeAndNegative);

                    switch (result) {
                        case MessageDialogResult.Negative:
                            RemovedItems = false;
                            break;
                        case MessageDialogResult.Affirmative:
                            ClearDropped();
                            RemovedItems = true;
                            break;
                    }
                }
                DroppedFiles.Clear();
                foreach (string file in files) {
                    string nType = Path.GetExtension(file);
                    if (Types.WebMTypes.Contains(nType)) await DialogManager.ShowMessageAsync(Main, "WebM Files Take A While", "Video files take a while to convert, you may want to convert them seperatly.", MessageDialogStyle.Affirmative);
                    DroppedFiles.Add(file);
                }
                WorkingDir = Path.GetDirectoryName(DroppedFiles[DroppedFiles.Count - 1]);
                GetFiles();
            }
        }
        #endregion

        #region Get Files
        public static void GetFiles() {
            if (RemovedItems) Files.Clear();
            try {
                foreach (string file in DroppedFiles) {
                    string nName = Path.GetFileNameWithoutExtension(file);
                    string nType = Path.GetExtension(file.ToLower());
                    string nLocation = Path.GetDirectoryName(file);
                    FileAttributes attr = File.GetAttributes(file);
                    if (Types.WebPTypes.Contains(nType) || Types.WebMTypes.Contains(nType)) {
                        encodeItems.Items.Add(new nFile { Name = nName, Type = nType, Location = nLocation });
                        Files.Add(file);
                    }
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory) {
                        Dirs.Add($"{file}\\");
                        DirNum++;

                        string[] files = Directory.GetFiles(Dirs[DirNum]);

                        foreach (string file2 in files) {
                            string nName2 = Path.GetFileNameWithoutExtension(file2);
                            string nType2 = Path.GetExtension(file2.ToLower());
                            string nLocation2 = Path.GetDirectoryName(file2);
                            if (Types.WebPTypes.Contains(nType2) || Types.WebMTypes.Contains(nType2)) {
                                encodeItems.Items.Add(new nFile { Name = nName2, Type = nType2, Location = nLocation2 });
                                Files.Add(file2);
                            }
                        }
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Clear Dropped
        public static void ClearDropped() {
            encodeItems.Items.Clear();
        }
        #endregion

        #region Create Error File
        public void CreateError(string message) {
            string error = WorkingDir + ErrorFile;

            if (File.Exists(error)) {
                File.Delete(error);
            }

            FileStream fs = File.Create(error);
            Byte[] bs = new UTF8Encoding(true).GetBytes(message);
            fs.Write(bs, 0, bs.Length);
        }
        #endregion

        #region Check Folder
        public static async void CheckFolderAsync() {
            if (!Options.ChangeTemp) {
                Console.WriteLine(WorkingDir + TempDir);
                if (Directory.Exists(WorkingDir + TempDir)) {
                    MessageDialogResult result = await DialogManager.ShowMessageAsync(Main, "Temp Folder Exists", "The temp folder already exists, do you want to delete it?", MessageDialogStyle.AffirmativeAndNegative);

                    switch (result) {
                        case MessageDialogResult.Negative:
                            _tempDirNum = new Random().Next(0, 100);
                            Directory.CreateDirectory(WorkingDir + "/" + _tempDirNum + "Stemp/");
                            _tempDirFull = WorkingDir + "/" + _tempDirNum + TempDir;
                            break;
                        case MessageDialogResult.Affirmative:
                            DeleteFolder(WorkingDir + TempDir);
                            Directory.CreateDirectory(WorkingDir + TempDir);
                            _tempDirFull = WorkingDir + TempDir;
                            break;
                    };
                }
                else {
                    Directory.CreateDirectory(WorkingDir + TempDir);
                    _tempDirFull = WorkingDir + TempDir;
                }
            }
        }

        public static void DeleteFolder(string folder) {
            if (!Directory.Exists(folder)) return;
            if (Directory.GetFiles(folder).Length > 0) {
                foreach (var file in Directory.GetFiles(folder)) {
                    File.Delete(file);
                }
            }
            Directory.Delete(folder);
        }
        #endregion

        #region Backup Files
        public static void BackupFiles() {
                try {
                    foreach (string file in DroppedFiles) {
                        string nType = Path.GetExtension(file).ToLower();
                        if (Types.WebPTypes.Contains(nType) || Types.WebMTypes.Contains(nType)) {
                            string filename = Path.GetFileName(file);
                            File.Copy(file, Options.ChangeTemp?
                                $"{Options.TempDir}/{filename}"
                                : $"{_tempDirFull}/{filename}");
                            
                        }
                    }
                    foreach (string folder in Dirs) {
                        if (folder != WorkingDir) {
                            foreach (var file in Directory.GetFiles(folder)) {
                                string nType = Path.GetExtension(file).ToLower();
                                if (Types.WebPTypes.Contains(nType) || Types.WebMTypes.Contains(nType)) {
                                    string filename = Path.GetFileName(file);
                                    File.Copy(file, Options.ChangeTemp ?
                                $"{Options.TempDir}/{filename}"
                                : $"{_tempDirFull}/{filename}");
                                }
                            }
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }

        }
        #endregion

        #region Show Toast
        public static void ShowToast(Toast toast) {
            switch (toast) {
                case Toast.Finished:
                    _toast.Show(new NotificationContent {
                        Title = "Finished",
                        Message = "Finished encoding the images.",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(6));
                    break;
                case Toast.EncodeCleared:
                    _toast.Show(new NotificationContent {
                        Title = "Encode Items Cleared",
                        Message = "The items in the encode list were cleared.",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(6));
                    break;
                case Toast.DecodeCleared:
                    _toast.Show(new NotificationContent {
                        Title = "Decode Items Cleared",
                        Message = "The items in the decode list were cleared.",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(6));
                    break;
                case Toast.SettingsSaved:
                    _toast.Show(new NotificationContent {
                        Title = "Settings Saved",
                        Message = "The settings were saved.",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(6));
                    break;
                default:
                    break;
            }
        }

        internal static void DisposeToast() { _toast.Dispose(); }
        #endregion
        
    }
}
