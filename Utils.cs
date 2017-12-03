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

namespace SquirrelyConverter
{
    class Utils
    {
        public static string Dir;
        public static string WorkingDir;
        public static bool IsFolder;
        public static bool HasFolder = false;
        public static List<string> Dirs = new List<string>();
        public static List<string> Files = new List<string>();
        public static List<string> DroppedFiles = new List<string>();
        public static int DirNum;
        public static double FileNum;
        public static double CurrentImage;
        public static string TempDir = "/Stemp/";
        private static int _tempDirNum;
        private static string _tempDirFull;
        public static bool IsRunning = false;

        public static string FileName;
        public static string FileType;
        public static string FileLocation;

        private static string ErrorFile = "/Logs/Error.txt";


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
                Console.WriteLine(Dir + TempDir);
                if (Directory.Exists(Dir + TempDir)) {

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
                            Directory.CreateDirectory(Dir + "/" + _tempDirNum + "Stemp/");
                            _tempDirFull = Dir + "/" + _tempDirNum + TempDir;
                            break;
                        case MessageBoxResult.OK:
                            DeleteFolder(Dir + TempDir);
                            Directory.CreateDirectory(Dir + TempDir);
                            _tempDirFull = Dir + TempDir;
                            break;
                    }
                }
                else {
                    Directory.CreateDirectory(Dir + TempDir);
                    _tempDirFull = Dir + TempDir;
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
                        if (folder != Dir) {
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
                        if (folder != Dir) {
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

       
    }
}
