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
using System.Threading;
using System.Text;
using Notifications.Wpf;

namespace SquirrelyConverter
{
    class Utils
    {
        public static string Dir;
        public static string WorkingDir;
        public static bool isFolder;
        public static bool hasFolder = false;
        public static List<string> Dirs = new List<string>();
        public static List<string> Files = new List<string>();
        public static List<string> droppedFiles = new List<string>();
        public static int dirNum;
        public static double fileNum;
        public static double currentImage;
        public static string tempDir = "/Stemp/";
        private static int tempDirNum;
        private static string tempDirFull;
        public static bool isRunning = false;

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
            Console.WriteLine(Dir + tempDir);
            if (Directory.Exists(Dir + tempDir)) {
                
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
                        tempDirNum = new Random().Next(0,100);
                        Directory.CreateDirectory(Dir + "/" + tempDirNum.ToString() + "Stemp/");
                        tempDirFull = Dir + "/" + tempDirNum.ToString() + tempDir;
                        break;
                    case MessageBoxResult.OK:
                        DeleteFolder(Dir + tempDir);
                        Directory.CreateDirectory(Dir + tempDir);
                        tempDirFull = Dir + tempDir;
                        break;
                    default:
                        Console.WriteLine("Something Broke!");
                        break;
                }
            }
            else {
                Console.WriteLine("Creating" + Dir + tempDir);
                Directory.CreateDirectory(Dir + tempDir);
                tempDirFull = Dir + tempDir;
            }

        }

        private static void DeleteFolder(string folder) {
            if (Directory.GetFiles(folder).Length > 0) {
                foreach (string file in Directory.GetFiles(folder)) {
                    File.Delete(file);
                }
            }
            Directory.Delete(folder);
        }
        #endregion

        #region Backup Files
        public static void BackupFiles() {

            foreach (string file in droppedFiles) {
                string NType = Path.GetExtension(file);
                if (NType == ".jpg" || NType == ".png" || NType == ".jpeg" || NType == ".gif") {
                    string filename = Path.GetFileName(file);
                    File.Copy(file, tempDirFull + filename);
                }
            }

            try {
                foreach (string folder in Dirs) {
                    if (folder != Dir) {
                        foreach (string file in Directory.GetFiles(folder, "*.jpg")) {
                            string filename = Path.GetFileName(file);
                            File.Copy(file, tempDirFull + filename);
                        }

                        foreach (string file in Directory.GetFiles(folder, "*.png")) {
                            string filename = Path.GetFileName(file);
                            File.Copy(file, tempDirFull + filename);
                        }

                        foreach (string file in Directory.GetFiles(folder, "*.jpeg")) {
                            string filename = Path.GetFileName(file);
                            File.Copy(file, tempDirFull + filename);
                        }

                        foreach (string file in Directory.GetFiles(folder, "*.gif")) {
                            string filename = Path.GetFileName(file);
                            File.Copy(file, tempDirFull + filename);
                        }
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

        }
        #endregion

       
    }
}
