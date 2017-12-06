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
using BespokeFusion;
using System.Collections.Generic;
using System.Text;
using Notifications.Wpf;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading;

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

        private static readonly NotificationManager _toast = new NotificationManager();
        private static Thread _ThreadEncode;
        #endregion

        #region Start Encode
        public static void StartEncode() {
            if (DroppedFiles == null) return;
            CheckFolder();
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
        internal static void ClearItems(int selectedIndex,ListView encodeItems , KeyEventArgs e) {
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

        #region Remove Dropped Items
        public static void RemoveDropped(ListView encodeItems) {
            CustomMaterialMessageBox msg = new CustomMaterialMessageBox {
                TxtMessage = { Text = "There are already files loaded, Do you wish to delete them?" },
                TxtTitle = { Text = "Already Files In Loaded" },
                BtnOk = { Content = "Yes" },
                BtnCancel = { Content = "No" }
            };
            msg.Show();
            MessageBoxResult result = msg.Result;
            switch (result) {
                case MessageBoxResult.OK:
                    ClearDropped(encodeItems);
                    RemovedItems = true;
                    break;
                case MessageBoxResult.Cancel:
                    Utils.RemovedItems = false;
                    break;
            }
        }
        #endregion

        #region Items Dropped
        public static void ItemsDropped(ListView encodeItems, string[] files) {
            if (!IsRunning) {
                if (encodeItems.HasItems) RemoveDropped(encodeItems);
                DroppedFiles.Clear();
                foreach (string file in files) DroppedFiles.Add(file);
                WorkingDir = Path.GetDirectoryName(DroppedFiles[DroppedFiles.Count - 1]);
                GetFiles(encodeItems);
            }
        }
        #endregion

        #region Get Files
        public static void GetFiles(ListView encodeItems) {
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
        public static void ClearDropped(ListView encodeItems) {
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
        public static void CheckFolder() {
            if (!Options.ChangeTemp) {
                Console.WriteLine(WorkingDir + TempDir);
                if (Directory.Exists(WorkingDir + TempDir)) {

                    var msg = new CustomMaterialMessageBox {
                        TxtMessage = { Text = "The temp folder already exists, do you want to delete it?" },
                        TxtTitle = { Text = "Temp Folder Exists" },
                        BtnOk = { Content = "Yes" },
                        BtnCancel = { Content = "No" }
                    };
                    msg.Show();
                    var result = msg.Result;


                    switch (result) {
                        case MessageBoxResult.Cancel:
                            _tempDirNum = new Random().Next(0, 100);
                            Directory.CreateDirectory(WorkingDir + "/" + _tempDirNum + "Stemp/");
                            _tempDirFull = WorkingDir + "/" + _tempDirNum + TempDir;
                            break;
                        case MessageBoxResult.OK:
                            DeleteFolder(WorkingDir + TempDir);
                            Directory.CreateDirectory(WorkingDir + TempDir);
                            _tempDirFull = WorkingDir + TempDir;
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
            if (Options.ChangeTemp) {
                try {
                    foreach (string file in DroppedFiles) {
                        var nType = Path.GetExtension(file)?.ToLower();
                        if (nType == ".jpg" || nType == ".png" || nType == ".jpeg" || nType == ".gif") {
                            var filename = Path.GetFileName(file);
                            File.Copy(file, $"{Options.TempDir}/{filename}");
                        }
                    }
                    foreach (var folder in Dirs) {
                        if (folder != WorkingDir) {
                            foreach (var file in Directory.GetFiles(folder, "*.jpg")) {
                                var filename = Path.GetFileName(file);
                                File.Copy(file, $"{Options.TempDir}/{filename}");
                            }

                            foreach (var file in Directory.GetFiles(folder, "*.png")) {
                                var filename = Path.GetFileName(file);
                                File.Copy(file, $"{Options.TempDir}/{filename}");
                            }

                            foreach (var file in Directory.GetFiles(folder, "*.jpeg")) {
                                var filename = Path.GetFileName(file);
                                File.Copy(file, $"{Options.TempDir}/{filename}");
                            }

                            foreach (var file in Directory.GetFiles(folder, "*.gif")) {
                                var filename = Path.GetFileName(file);
                                File.Copy(file, $"{Options.TempDir}/{filename}");
                            }
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            else {
                try {
                    foreach (var file in DroppedFiles) {
                        var nType = Path.GetExtension(file)?.ToLower();
                        if (nType == ".jpg" || nType == ".png" || nType == ".jpeg" || nType == ".gif") {
                            var filename = Path.GetFileName(file);
                            File.Copy(file, _tempDirFull + filename);
                        }
                    }
                    foreach (var folder in Dirs) {
                        if (folder != WorkingDir) {
                            foreach (var file in Directory.GetFiles(folder, "*.jpg")) {
                                var filename = Path.GetFileName(file);
                                File.Copy(file, _tempDirFull + filename);
                            }

                            foreach (var file in Directory.GetFiles(folder, "*.png")) {
                                var filename = Path.GetFileName(file);
                                File.Copy(file, _tempDirFull + filename);
                            }

                            foreach (var file in Directory.GetFiles(folder, "*.jpeg")) {
                                var filename = Path.GetFileName(file);
                                File.Copy(file, _tempDirFull + filename);
                            }

                            foreach (var file in Directory.GetFiles(folder, "*.gif")) {
                                var filename = Path.GetFileName(file);
                                File.Copy(file, _tempDirFull + filename);
                            }
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
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
