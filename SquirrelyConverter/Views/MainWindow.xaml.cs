using System;
using System.Windows;
using Mr_Squirrely_Converters.Class;
using System.IO;
using System.Reflection;
using ConverterUtilities;

namespace Mr_Squirrely_Converters {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            Toast.Instance.CreateNotifier();
            //Utilities.DownloadFiles();
            //Utilities.OpenSettings();
            Utilities.MainWindow = this;
            CUtilities.MainWindow = this;
            CUtilities.Dispatcher = App.Current.Dispatcher;

            Utilities.MainWindow.Content = Utilities.MainPage;
            CUtilities.SetWorkdingDir(Directory.GetCurrentDirectory());

            Logger.instance.LogDebug("Logger started!");
        }

        private void RightWindowSettings_Click(object sender, RoutedEventArgs e) => Utilities.MainWindow.Content = Utilities.SettingsPage;
        private void RightWindowGithub_Click(object sender, RoutedEventArgs e) => Utilities.OpenGithub();
        private void MetroWindow_Closed(object sender, EventArgs e) => Utilities.Dispose();

        private void HamburgerButton_Click(object sender, RoutedEventArgs e) => HamburgerButton.ContextMenu.IsOpen = true;
        private void SettingsMenu_Click(object sender, RoutedEventArgs e) => Utilities.MainWindow.Content = Utilities.SettingsPage;
        private void ClearMenu_Click(object sender, RoutedEventArgs e) => Utilities.Clear();
        private void UpdateMenu_Click(object sender, RoutedEventArgs e) => Utilities.CheckForUpdate(true);

    }
}
