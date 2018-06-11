using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Controls;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using Mr_Squirrely_Converters.Properties;
using Mr_Squirrely_Converters.Views;
using Dragablz;
using ConverterUtilities;

namespace Mr_Squirrely_Converters.Class {
    // TODO:
    //  Comment what everything is for and does.
    //  Clean code up a bit. It's still not the best looking.
    static class Utilities {

        internal static MainPage MainPage = new MainPage();
        private static int Tabs { get; set; }
        public static TabablzControl ConverterTabs { get; set; }

        public static void AddImageTab() {
            Tabs = ConverterTabs.Items.Count;
            TabItem ImageTab = new TabItem {
                Content = new ImageConverter.ImageView(),
                Header = "Convert Image"
            };
            ConverterTabs.Items.Insert(Tabs, ImageTab);
        }

        public static void AddVideoTab() {
            Tabs = ConverterTabs.Items.Count;
            TabItem VideoTab = new TabItem {
                Content = new VideoConverter.VideoView(),
                Header = "Convert Video"
            };
            ConverterTabs.Items.Insert(Tabs, VideoTab);
        }

        //Old things that might leave        

        #region Video Converters
        private static void StartConvertWebM() => Converter.ConvertWebM(Files);
        private static void StartConvertMP4() => Converter.ConvertMP4(Files);

        private static void ConvertWebM() {
            if (DroppedFiles == null) {
                return;
            }
            if (IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart _Starter = StartConvertWebM;
            _Starter += () => {
                Toast.ConvertFinished();
                IsWorking = false;
            };

            _ThreadEncode = new Thread(_Starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }

        private static void ConvertMP4() {
            if (DroppedFiles == null) {
                return;
            }
            if (IsWorking) {
                Toast.AlreadyConverting();
                return;
            }
            IsWorking = true;
            Thread _ThreadEncode;
            ThreadStart _Starter = StartConvertMP4;
            _Starter += () => {
                Toast.ConvertFinished();
                IsWorking = false;
            };

            _ThreadEncode = new Thread(_Starter);
            _ThreadEncode.SetApartmentState(ApartmentState.STA);
            _ThreadEncode.IsBackground = true;
            _ThreadEncode.Start();
        }
        #endregion
        
        #region Populate Lists
        internal static void PopulateList(string[] DroppedFiles, string Type) {
            switch (Type) {
                case "images":
                    //PopulateImages(DroppedFiles);
                    break;
                case "videos":
                    PopulateVideos(DroppedFiles);
                    break;
            }
        }

        private static void PopulateVideos(string[] DroppedFiles) {
            //ClearLists();

            //if (Utilities.DroppedFiles.Count != 0) {
            //    Utilities.DroppedFiles.Clear();
            //}

            //foreach (string dropped in DroppedFiles) {
            //    Utilities.DroppedFiles.Add(dropped);
            //}

            //foreach (string file in Utilities.DroppedFiles) {
            //    GetVideos(file, true);
            //}

            //foreach (string dir in Dirs) {
            //    string[] files = Directory.GetFiles(dir);
            //    foreach (string file in files) {
            //        GetVideos(file, false);
            //    }
            //}
            //VideoItems.ItemsSource = Videos;
        }

        private static void GetVideos(string file, bool scanDir) {
            //string fileName = Path.GetFileNameWithoutExtension(file);
            //string fileType = Path.GetExtension(file);
            //string fileLocation = Path.GetFullPath(file);
            //FileAttributes fileAttributes = File.GetAttributes(file);
            //if (Types.VideoFormats.Contains(fileType)) {
            //    Videos.Add(new NewFile { Name = fileName, Type = fileType, Converted = "Queued", Location = fileLocation });
            //    Files.Add(file);
            //}
            //if (scanDir == true) {
            //    if ((fileAttributes & FileAttributes.sDirectory) == FileAttributes.Directory) {
            //        Dirs.Add($"{file}\\");
            //    }
            //}
        }
        #endregion
    }
}
