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
            TabItem imageTab = new TabItem {
                Content = new ImageConverter.ImageView(),
                Header = "Convert Image"
            };
            ConverterTabs.Items.Insert(Tabs, imageTab);
        }

        public static void AddVideoTab() {
            Tabs = ConverterTabs.Items.Count;
            TabItem videoTab = new TabItem {
                Content = new VideoConverter.VideoView(),
                Header = "Convert Video"
            };
            ConverterTabs.Items.Insert(Tabs, videoTab);
        }
    }
}
