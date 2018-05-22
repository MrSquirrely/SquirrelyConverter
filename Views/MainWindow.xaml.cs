﻿using System;
using System.Windows;
using Mr_Squirrely_Converters.Class;
using System.IO;

namespace Mr_Squirrely_Converters {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            Toast.CreateNotifier();
            Utilities.DownloadFiles();
            Utilities.OpenSettings();
            Utilities._MainWindow = this;

            Utilities._MainWindow.Content = Utilities._MainPage;
            Utilities._WorkingDir = Directory.GetCurrentDirectory();

            Logger.StartLogger();
            Logger.LogDebug("Started!");
        }

        private void RightWindowSettings_Click(object sender, RoutedEventArgs e) => Utilities._MainWindow.Content = Utilities._SettingsPage;
        private void RightWindowGithub_Click(object sender, RoutedEventArgs e) => Utilities.OpenGithub();
        private void MetroWindow_Closed(object sender, EventArgs e) => Utilities.Dispose();

        private void HamburgerButton_Click(object sender, RoutedEventArgs e) => HamburgerButton.ContextMenu.IsOpen = true;
        private void SettingsMenu_Click(object sender, RoutedEventArgs e) => Utilities._MainWindow.Content = Utilities._SettingsPage;
        private void ClearMenu_Click(object sender, RoutedEventArgs e) => Utilities.Clear();
        private void UpdateMenu_Click(object sender, RoutedEventArgs e) => Utilities.CheckForUpdate(true);

    }
}
