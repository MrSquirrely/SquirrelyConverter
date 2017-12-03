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
using System.Windows;
using System.Threading;
using Notifications.Wpf;
using System.Net;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;

namespace SquirrelyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private const string Encode = "encode";
        private const string Decode = "decode";


        Thread _threadEncode;
        readonly NotificationManager _toast = new NotificationManager();

        public MainWindow() {
            InitializeComponent();
            Utils.WorkingDir = Directory.GetCurrentDirectory();
            Options.FirstRun();


        }

        #region Encode Items Dropped
        private void EncodeItems_Drop(object sender, DragEventArgs e) {
            //This checks if there are already files in the list and if there are removes them.
            if (Utils.IsRunning != true) {
                if (EncodeItems.HasItems) {

                    var msg = new CustomMaterialMessageBox {
                        TxtMessage = { Text = "There are already files loaded, Do you wish to delete them?" },
                        TxtTitle = { Text = "Already Files In Loaded" },
                        BtnOk = { Content = "Yes" },
                        BtnCancel = { Content = "No" }
                    };
                    msg.Show();
                    var result = msg.Result;

                    switch (result) {
                        case MessageBoxResult.OK:
                            EncodeItems.Items.Clear();
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
                Utils.DroppedFiles.Clear();
                foreach (var file in (string[])e.Data.GetData(DataFormats.FileDrop)) {
                    Utils.DroppedFiles.Add(file);
                }
                GetDirectory(Utils.DroppedFiles);
                DecodeItems.Items.Clear();
                GetFiles(Utils.Dir, Encode);
            }else if (Utils.IsRunning == true) {
                var msg = new CustomMaterialMessageBox {
                    TxtMessage = { Text = "We are already running! Cannot add items while we are running!" },
                    TxtTitle = { Text = "WE ARE RUNNING!" },
                };

                msg.Show();
                var result = msg.Result;
            }
        }
        #endregion

        #region Decode Items Dropped
        private void DecodeItems_Drop(object sender, DragEventArgs e) {

            if (!Utils.IsRunning) {
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

                Utils.DroppedFiles.Clear();
                foreach (var file in (string[])e.Data.GetData(DataFormats.FileDrop)) {
                    Utils.DroppedFiles.Add(file);
                }
                GetDirectory(Utils.DroppedFiles); // Needs to be looked at!! I'm not sure what I'm doing with it? It looks like nothing...
                EncodeItems.Items.Clear();
                GetFiles(Utils.Dir, Decode);
            }
            else if (Utils.IsRunning) {
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
        private static void GetDirectory(List<string> value) {
            Utils.Dir = Path.GetDirectoryName(value[value.Count - 1]);

            foreach (var item in Utils.DroppedFiles) {
                Console.WriteLine(item);
                var attr = File.GetAttributes(item);
                Utils.IsFolder = (attr & FileAttributes.Directory) == FileAttributes.Directory;
                Console.WriteLine(Utils.IsFolder);
                if (Utils.IsFolder) {
                    Utils.HasFolder = true;
                    Utils.Dirs.Add(item + "/");
                    Utils.DirNum++;
                    Console.WriteLine(Utils.DirNum);
                    try {
                        Console.WriteLine(Utils.Dirs[Utils.DirNum -1]);
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
        private void EncodeButton_Click(object sender, RoutedEventArgs e) {
            if (Utils.DroppedFiles == null) return;
            Utils.CheckFolder();
            Utils.BackupFiles();

            ThreadStart starter = EncodeStarter;

            starter += () => {
                _toast.Show(new NotificationContent {
                    Title = "Finished",
                    Message = "Finished encoding the images.",
                    Type = NotificationType.Success
                }, expirationTime: TimeSpan.FromSeconds(6));

                Utils.IsRunning = false;

                if (!Options.DeleteTemp) return;
                if (!Options.ChangeTemp) Utils.DeleteFolder($"{Utils.Dir}/{Utils.TempDir}");
            };
            _threadEncode = new Thread(starter);
            _threadEncode.SetApartmentState(ApartmentState.STA);
            _threadEncode.IsBackground = true;
            _threadEncode.Start();
        }

        
        private void EncodeStarter() {
            Convert.WebP(Encode);
        }
        #endregion

        #region Decode Button Click
        //Starts the encoding process.
        //private void EncodeButton_Click(object sender, RoutedEventArgs e) => Utils.StartEncode();
        private void DecodeButton_Click(object sender, RoutedEventArgs e) {

        }
        #endregion

        #region Get Files
        //Get all files in directory and sets them to an array;
        private void GetFiles(string value, string coding) {
            Utils.Files.Clear();

            try {
                foreach (var file in Utils.DroppedFiles) {
                    var nName = Path.GetFileName(file);
                    var nType = Path.GetExtension(file.ToLower());
                    var nLocation = Path.GetDirectoryName(file);
                    if (nType == ".jpg" || nType == ".png" || nType == ".jpeg" || nType == ".gif") {
                        switch (coding) {
                            case "encode":
                                EncodeItems.Items.Add(new Tools { Name = nName, Type = nType, Saved = "Queued", Location = nLocation });
                                break;
                            case "decode":
                                DecodeItems.Items.Add(new Tools { Name = nName, Type = nType, Saved = "Queued", Location = nLocation });
                                break;
                            default:
                                break;;
                        }
                        Utils.Files.Add(file);
                    }
                }

                Console.WriteLine(Utils.IsFolder);

                if (!Utils.HasFolder) return;
                {
                    Console.WriteLine("Scanning Folders!");
                    for (var i = 0; i < Utils.Dirs.Count; i++) {
                        foreach (string file in Directory.GetFiles(Utils.Dirs[i +1])) {
                            var nName = Path.GetFileName(file);
                            var nType = Path.GetExtension(file.ToLower());
                            var nLocation = Path.GetDirectoryName(file);
                            if (nType == ".jpg" || nType == ".png" || nType == ".jpeg" || nType == ".gif") {
                                EncodeItems.Items.Add(new Tools { Name = nName, Type = nType, Saved = "Queued", Location = nLocation });
                                Utils.Files.Add(file);
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

        private void MetroWindow_Closed(object sender, EventArgs e) { _toast.Dispose(); }

        private void SettingsButton_Click(object sender, RoutedEventArgs e) {
            var sets = new SettingsWindow();
            sets.ShowDialog();
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;
            if (EncodeItems.SelectedIndex <= -1) return;
            Utils.Files.RemoveAt(EncodeItems.SelectedIndex);
            EncodeItems.Items.RemoveAt(EncodeItems.SelectedIndex);
            EncodeItems.Items.Refresh();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e) { ClearItems(true, true); }

        private void ClearItems(bool itemsEncode, bool itemsDecode) {
            if (itemsEncode) EncodeItems.Items.Clear();
            if (itemsDecode) DecodeItems.Items.Clear();
            Utils.Files.Clear();
        }

        private void ReportBug_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/MrSquirrely/SquirrelyConverter/issues/new");
        }
    }

}
