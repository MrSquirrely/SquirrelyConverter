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
using BespokeFusion;
using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace SquirrelyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        //private double OP_Quality;
        //private bool OP_DeleteTempDir;
        protected string ENCODE = "encode";
        protected string DECODE = "decode";

        public MainWindow() {
            InitializeComponent();
            //This will be added later
            //Options.FirstRun();
            //AutoDelete.IsChecked = Options.DeleteTempDir;
            //OP_DeleteTempDir = Options.DeleteTempDir;
            //Quality.Value = Options.Quality;
            //OP_Quality = Options.Quality;
        }

        #region Encode Items Dropped
        private void EncodeItems_Drop(object sender, DragEventArgs e) {
            if (Utils.isRunning != true) {
                if (EncodeItems.HasItems) {

                    var msg = new CustomMaterialMessageBox {
                        TxtMessage = { Text = "There are already images loaded. Would you like to keep them? *This will be a setting in a later update" },
                        TxtTitle = { Text = "Confirmation" },
                        BtnOk = { Content = "Yes" },
                        BtnCancel = { Content = "No" }
                    };

                    msg.Show();
                    var result = msg.Result;
                    switch (result) {
                        case MessageBoxResult.Cancel:
                            EncodeItems.Items.Clear();
                            break;
                        case MessageBoxResult.OK:
                            break;
                    }
                }

                Utils.droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                GetDirectory(Utils.droppedFiles); // Needs to be looked at!! I'm not sure what I'm doing with it? It looks like nothing...
                DecodeItems.Items.Clear();
                GetFiles(Utils.Dir, ENCODE);
            }else if (Utils.isRunning == true) {
                var msg = new CustomMaterialMessageBox {
                    TxtMessage = { Text = "We are already running! Cannot add items while we are running!" },
                    TxtTitle = { Text = "WE ARE RUNNING!" },
                };

                msg.Show();
                var result = msg.Result;
            }
        }
        #endregion

        #region Encode Items Dropped
        private void DecodeItems_Drop(object sender, DragEventArgs e) {

            if (!Utils.isRunning) {
                if (DecodeItems.HasItems) {

                    var msg = new CustomMaterialMessageBox {
                        TxtMessage = { Text = "There are already images loaded. Would you like to keep them? *This will be a setting in a later update" },
                        TxtTitle = { Text = "Confirmation" },
                        BtnOk = { Content = "Yes" },
                        BtnCancel = { Content = "No" }
                    };

                    msg.Show();
                    var result = msg.Result;
                    switch (result) {
                        case MessageBoxResult.Cancel:
                            EncodeItems.Items.Clear();
                            break;
                        case MessageBoxResult.OK:
                            break;
                    }
                }

                Utils.droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                GetDirectory(Utils.droppedFiles); // Needs to be looked at!! I'm not sure what I'm doing with it? It looks like nothing...
                EncodeItems.Items.Clear();
                GetFiles(Utils.Dir, DECODE);
            }
            else if (Utils.isRunning) {
                var msg = new CustomMaterialMessageBox {
                    TxtMessage = { Text = "We are already running! Cannot add items while we are running!" },
                    TxtTitle = { Text = "WE ARE RUNNING!" },
                };

                msg.Show();
                var result = msg.Result;
            }


        }
        #endregion

        #region Get Directory
        //Sets the directory to be used later.
        private void GetDirectory(string[] value) {
            Console.WriteLine("Start Get Directory");
            Utils.Dir = Path.GetDirectoryName(value[value.Length - 1]);
            Utils.Dirs.Add(Utils.Dir);

            foreach (string item in Utils.droppedFiles) {
                Console.WriteLine(item);
                FileAttributes attr = File.GetAttributes(item);
                Utils.isFolder = (attr & FileAttributes.Directory) == FileAttributes.Directory;
                Console.WriteLine(Utils.isFolder);
                if (Utils.isFolder == true) {
                    Utils.hasFolder = true;
                    Utils.Dirs.Add(item + "/");
                    Utils.dirNum++;
                    Console.WriteLine(Utils.dirNum);
                    try {
                        Console.WriteLine(Utils.Dirs[Utils.dirNum -1]);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                    
                }
            }
        }
        #endregion

        #region Encode Button Click
        //Starts the encoding process.
        //private void EncodeButton_Click(object sender, RoutedEventArgs e) => Utils.StartEncode();
        private void EncodeButton_Click(object sender, RoutedEventArgs e) => Convert.WebP(ENCODE);
        #endregion

        #region Encode Button Click
        //Starts the encoding process.
        //private void EncodeButton_Click(object sender, RoutedEventArgs e) => Utils.StartEncode();
        private void DecodeButton_Click(object sender, RoutedEventArgs e) => Convert.WebP(DECODE);
        #endregion

        #region Get Files
        //Get all files in directory and sets them to an array;
        private void GetFiles(string value, string coding) {

            try {

                foreach (string file in Utils.droppedFiles) {
                    string NName = Path.GetFileName(file);
                    string NType = Path.GetExtension(file.ToLower());
                    string NLocation = Path.GetDirectoryName(file);
                    if (NType == ".jpg" || NType == ".png" || NType == ".jpeg" || NType == ".gif") {
                        switch (coding) {
                            case "encode":
                                EncodeItems.Items.Add(new Tools { Name = NName, Type = NType, Saved = "Queued", Location = NLocation });
                                break;
                            case "decode":
                                DecodeItems.Items.Add(new Tools { Name = NName, Type = NType, Saved = "Queued", Location = NLocation });
                                break;
                        }
                        
                        Utils.files.Add(file);
                    }
                }

                //foreach (string file in Directory.GetFiles(value, "*.jpg")) {
                //    string NName = Path.GetFileName(file);
                //    string NType = Path.GetExtension(file.ToLower());
                //    string NLocation = Path.GetDirectoryName(file);
                //    EncodeItems.Items.Add(new Tools { Name = NName, Type = NType, Saved = "Queued", Location = NLocation });
                //}

                //foreach (string file in Directory.GetFiles(value, "*.jpeg")) {
                //    string NName = Path.GetFileName(file);
                //    string NType = Path.GetExtension(file.ToLower());
                //    string NLocation = Path.GetDirectoryName(file);
                //    EncodeItems.Items.Add(new Tools { Name = NName, Type = NType, Saved = "Queued", Location = NLocation });
                //}

                //foreach (string file in Directory.GetFiles(value, "*.png")) {
                //    string NName = Path.GetFileName(file);
                //    string NType = Path.GetExtension(file.ToLower());
                //    string NLocation = Path.GetDirectoryName(file);
                //    EncodeItems.Items.Add(new Tools { Name = NName, Type = NType, Saved = "Queued", Location = NLocation });
                //}

                Console.WriteLine(Utils.isFolder);

                if (Utils.hasFolder) {
                    Console.WriteLine("Scanning Folders!");
                    for (int i = 0; i < Utils.Dirs.Count; i++) {
                        foreach (string file in Directory.GetFiles(Utils.Dirs[i +1])) {
                            string NName = Path.GetFileName(file);
                            string NType = Path.GetExtension(file.ToLower());
                            string NLocation = Path.GetDirectoryName(file);
                            if (NType == ".jpg" || NType == ".png" || NType == ".jpeg" || NType == ".gif") {
                                EncodeItems.Items.Add(new Tools { Name = NName, Type = NType, Saved = "Queued", Location = NLocation });
                                Utils.files.Add(file);
                            }
                            //EncodeItems.Items.Add(new Tools { Name = NName, Type = NType, Saved = "Queued", Location = NLocation });
                        }

                        //foreach (string file in Directory.GetFiles(Utils.Dirs[i + 1], "*.jpeg")) {
                        //    string NName = Path.GetFileName(file);
                        //    string NType = Path.GetExtension(file.ToLower());
                        //    string NLocation = Path.GetDirectoryName(file);
                        //    EncodeItems.Items.Add(new Tools { Name = NName, Type = NType, Saved = "Queued", Location = NLocation });
                        //}

                        //foreach (string file in Directory.GetFiles(Utils.Dirs[i + 1], "*.png")) {
                        //    string NName = Path.GetFileName(file);
                        //    string NType = Path.GetExtension(file.ToLower());
                        //    string NLocation = Path.GetDirectoryName(file);
                        //    EncodeItems.Items.Add(new Tools { Name = NName, Type = NType, Saved = "Queued", Location = NLocation });
                        //}
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
        }
        #endregion

        #region Open Folder Click
        private void OpenFolderButton_Click(object sender, RoutedEventArgs e) {

        }
        #endregion

        #region Save Button Click
        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            //Console.WriteLine("Saved Settings!");
            //Console.WriteLine(AutoDelete.IsChecked.ToString());
            //Console.WriteLine(Quality.Value.ToString());
        }
        #endregion

        public void UpdateSlider() {

            //NumImagesLabel
            //NumImagesSlide

            //NumImagesSlide.Value = new Tools { NumImages = Utils.fileNum };

        }
    }

}
