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
using Notifications.Wpf;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;
using MahApps.Metro.Controls;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using WebPConverter.Views;
using NLog;

namespace WebPConverter.Class {
    class Utils {
        #region Variables
        public static string WorkingDir;
        public static bool HasFolder = false;
        public static List<string> Dirs = new List<string>();
        public static List<string> Files = new List<string>();
        public static List<string> DecodeFiles = new List<string>();
        public static List<string> DroppedFiles = new List<string>();
        public static bool RemovedItems = true;
        public static int DirNum = -1;
        public static double FileNum;
        public static string TempDir = "/Stemp/";
        private static int _tempDirNum;
        private static string _tempDirFull;
        public static bool IsRunning;
        public static bool MultiCore = false;

        public const string Encode = "encode";
        public const string Decode = "decode";

        public static string FileName;
        public static string FileType;
        public static string FileLocation;

        public static MetroWindow Main;
        public static ListView EncodeItems;

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static readonly NotificationManager Toast = new NotificationManager();
        public static ObservableCollection<NFile> NFiles = new ObservableCollection<NFile>();
        public static SettingsWindow Sets = new SettingsWindow();
        private static Thread _threadEncode;
        #endregion

        #region Start Encode
        public static void StartEncodeAsync() {
            if (DroppedFiles == null) return;
            if (Options.ChangeTemp && !Directory.Exists(Options.TempDir)) MessageBox.Show("Cannot find the temp folder, make sure it exists!", "Missing Temp Folder", MessageBoxButton.OK, MessageBoxImage.Warning);
            if (Options.SetCustomOutput && !Directory.Exists(Options.OutDir)) MessageBox.Show("Cannot find the temp folder, make sure it exists!", "Missing Output Folder", MessageBoxButton.OK, MessageBoxImage.Warning);
            CheckFolder();
            BackupFiles();

            ThreadStart starter = EncodeStarter;
            starter += () => {
                ShowToast(Class.Toast.Finished);
                IsRunning = false;
                if (!Options.DeleteTemp) return;
                if (!Options.ChangeTemp) DeleteFolder($"{WorkingDir}/{TempDir}");
            };

            _threadEncode = new Thread(starter);
            _threadEncode.SetApartmentState(ApartmentState.STA);
            _threadEncode.IsBackground = true;
            _threadEncode.Start();
        }
        private static void EncodeStarter() => Convert.StartEncode();
        #endregion

        #region Update View
        public static void UpdateView() => Main.Dispatcher.Invoke(() => { EncodeItems.Items.Refresh(); }, DispatcherPriority.ContextIdle);
        #endregion

        #region Clear Items
        internal static void ClearItems(int selectedIndex, KeyEventArgs e) {
            if (e.Key != Key.Delete) return;
            if (selectedIndex <= -1) return;
            Files.RemoveAt(selectedIndex);
            NFiles.RemoveAt(selectedIndex);
            EncodeItems.Items.Refresh();
        }
        #endregion

        #region Open Settings Window
        public static void OpenSettings() => Sets.ShowDialog();
        #endregion

        #region Items Dropped
        public static void ItemsDroppedAsync(string[] files) {
            if (!IsRunning) {
                if (EncodeItems.HasItems) {
                    MessageBoxResult result = MessageBox.Show("There are already files loaded, do you wish to delete them?", "Already File Are Loaded", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (result) {
                        case MessageBoxResult.No:
                            RemovedItems = false;
                            break;
                        case MessageBoxResult.Yes:
                            ClearDropped();
                            RemovedItems = true;
                            break;
                    }
                }
                DroppedFiles.Clear();
                foreach (string file in files) {
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
                    if (Types.WebP.Contains(nType)) {
                        NFiles.Add(new NFile { Name = nName, Type = nType, Converted = "queued", Location = nLocation });
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
                            if (Types.WebP.Contains(nType2)) {
                                NFiles.Add(new NFile { Name = nName2, Type = nType2, Converted = "queued", Location = nLocation2 });
                                Files.Add(file2);
                            }
                        }
                    }
                }
                EncodeItems.ItemsSource = NFiles;
            }
            catch (Exception e) {
                LogMessage(e);
            }
        }
        #endregion

        #region Clear Dropped
        public static void ClearDropped() => NFiles.Clear();
        #endregion

        #region Create Log File
        public static void LogMessage(Exception ex) {
            logger.Log(LogLevel.Error, $"{ex.Source} \n\n {ex.TargetSite} \n\n {ex.Message}");
            logger.Log(LogLevel.Debug, $"{ex.Source} \n\n {ex.TargetSite} \n\n {ex.Message}");
        }
        #endregion

        #region Check Folder
        public static void CheckFolder() {
            if (!Options.ChangeTemp) {
                Console.WriteLine(WorkingDir + TempDir);
                if (Directory.Exists(WorkingDir + TempDir)) {
                     MessageBoxResult result = MessageBox.Show("The temp folder already exists, do you want to delete it?", "Temp Folder Exists", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    switch (result) {
                        case MessageBoxResult.Yes:
                            DeleteFolder(WorkingDir + TempDir);
                            Directory.CreateDirectory(WorkingDir + TempDir);
                            _tempDirFull = WorkingDir + TempDir;
                            break;
                        case MessageBoxResult.No:
                            _tempDirNum = new Random().Next(0, 100);
                            Directory.CreateDirectory(WorkingDir + "/" + _tempDirNum + "Stemp/");
                            _tempDirFull = WorkingDir + "/" + _tempDirNum + TempDir;
                            break;
                    }
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
                        string nType = Path.GetExtension(file)?.ToLower();
                        if (Types.WebP.Contains(nType)) {
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
                                if (Types.WebP.Contains(nType)) {
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
                LogMessage(e);
                }

        }
        #endregion

        #region Show Toast
        public static void ShowToast(Toast toast) {
            switch (toast) {
                case Class.Toast.Finished:
                    Toast.Show(new NotificationContent {
                        Title = "Finished",
                        Message = "Finished encoding the images.",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(6));
                    break;
                case Class.Toast.EncodeCleared:
                    Toast.Show(new NotificationContent {
                        Title = "Encode Items Cleared",
                        Message = "The items in the encode list were cleared.",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(6));
                    break;
                case Class.Toast.DecodeCleared:
                    Toast.Show(new NotificationContent {
                        Title = "Decode Items Cleared",
                        Message = "The items in the decode list were cleared.",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(6));
                    break;
                case Class.Toast.SettingsSaved:
                    Toast.Show(new NotificationContent {
                        Title = "Settings Saved",
                        Message = "The settings were saved.",
                        Type = NotificationType.Success
                    }, expirationTime: TimeSpan.FromSeconds(6));
                    break;
            }
        }
        internal static void DisposeToast() { Toast.Dispose(); }
        #endregion

    }
}
