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

namespace SquirrelyConverter
{
    class Utils
    {
        public static string Dir;
        public static bool isFolder;
        public static bool hasFolder = false;
        public static List<string> Dirs = new List<string>();
        public static int dirNum;
        public static double fileNum;
        public static double currentImage;
        private static string tempDir = "/temp/";
        private static string tempDir2 = "/temp2/";
        private static string tempDirFull;
        public static string[] droppedFiles;
        public static List<string> files = new List<string>();
        public static bool isRunning = false;

        public static string OutDir;
        public static bool ChangeOutDir;
        public static bool DeleteTempDir;
        public static double Quality;

        public static string FileName;
        public static string FileType;
        public static string FileLocation;

        //[Obsolete("Instead of using Utils.StartEncode use Convert.CONVERTTYPE instead")]
        //public static void StartEncode() {
        //    try {
        //        CheckFolder();
        //        BackupFiles();
        //        Thread thread = new Thread(StartConversion);
        //        thread.SetApartmentState(ApartmentState.STA);
        //        thread.Start();
        //        //StartConversion();

        //    }
        //    catch (Exception e) {
        //        Console.WriteLine(value: e.Message);
        //    }
        //}

        #region Finished Message
        private static void Finished() {
            var msg = new CustomMaterialMessageBox {
                TxtMessage = { Text = "We are finished with the conversion." },
                TxtTitle = { Text = "Conversion Finished" },
                BtnOk = { Content = "okay" }
            };
            msg.Show();
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
                        Directory.CreateDirectory(Dir + tempDir2);
                        tempDirFull = Dir + tempDir2;
                        break;
                    case MessageBoxResult.OK:
                        Directory.Delete(Dir + tempDir);
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
        #endregion

        #region Backup Files
        public static void BackupFiles() {

            foreach (string file in droppedFiles) {

            }

            try {
                foreach (string folder in Dirs) {

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
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

        }
        #endregion

        #region Conversion
        private static void StartConversion() {

            try {
                foreach (string file in files) {
                    FileName = Path.GetFileNameWithoutExtension(file);
                    FileType = Path.GetExtension(file);
                    FileLocation = Path.GetDirectoryName(file);

                    WebP image = new WebP();
                    image.Image = file;
                    image.Output = FileLocation + "/" + FileName + ".webp";
                    image.Quality = 80;
                    //image.CopyMeta = false;
                    //image.NoAlpha = false;
                    image.Encode();

                    //SimpleEncoder encoder = new SimpleEncoder();
                    //encoder.Encode();
                    //MagickImage image = new MagickImage(file);
                    //image.Format = MagickFormat.WebP;
                    //image.Quality = 80;
                    //image.Write(FileLocation + "/" + FileName + ".webp");
                    //File.Delete(file);
                }
                //foreach (string folder in Dirs) {

                //    foreach (string file in Directory.GetFiles(folder, "*.jpg")) {
                //        FileName = Path.GetFileNameWithoutExtension(file);
                //        FileType = Path.GetExtension(file);
                //        FileLocation = Path.GetDirectoryName(file);

                //        MagickImage image = new MagickImage(file);
                //        image.Format = MagickFormat.WebP;
                //        image.Quality = 80;
                //        image.Write(FileLocation + "/" + FileName + ".webp");
                //        File.Delete(file);
                //        currentImage++;
                //    }

                //    foreach (string file in Directory.GetFiles(folder, "*.png")) {
                //        FileName = Path.GetFileNameWithoutExtension(file);
                //        FileType = Path.GetExtension(file);
                //        FileLocation = Path.GetDirectoryName(file);

                //        MagickImage image = new MagickImage(file);
                //        image.Format = MagickFormat.WebP;
                //        image.Quality = 80;
                //        image.Write(FileLocation + "/" + FileName + ".webp");
                //        File.Delete(file);
                //        currentImage++;
                //    }

                //    foreach (string file in Directory.GetFiles(folder, "*.jpeg")) {
                //        FileName = Path.GetFileNameWithoutExtension(file);
                //        FileType = Path.GetExtension(file);
                //        FileLocation = Path.GetDirectoryName(file);

                //        MagickImage image = new MagickImage(file);
                //        image.Format = MagickFormat.WebP;
                //        image.Quality = 80;
                //        image.Write(FileLocation + "/" + FileName + ".webp");
                //        File.Delete(file);
                //        currentImage++;
                //    }

                //}
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

        }

        #endregion

        #region Settings First Run
        public static void SettingsFirstRun() {
            OutDir = Properties.Settings.Default.OutDir;
            ChangeOutDir = Properties.Settings.Default.ChangeOutDir;
            DeleteTempDir = Properties.Settings.Default.DeleteTempDir;
            Quality = Properties.Settings.Default.WebPQuality;
        }
        #endregion

        #region Set Settings
        public static void SetSetting(string outDir, bool changeOutDir, bool deleteTempDir, double quality) {
            Properties.Settings.Default.OutDir = outDir;
            Properties.Settings.Default.ChangeOutDir = changeOutDir;
            Properties.Settings.Default.DeleteTempDir = deleteTempDir;
            Properties.Settings.Default.WebPQuality = quality;

            Properties.Settings.Default.Save();
        }
        #endregion

    }
}
