using System;
using System.Windows;
using Mr_Squirrely_Converters.Class;
using System.IO;
using ConverterUtilities;

namespace Mr_Squirrely_Converters {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            Toast.CreateNotifier();
            Logger.StartLogger();
            CUtilities.MainWindow = this;
            Content = Utilities.MainPage;
            CUtilities.SetWorkdingDir(Directory.GetCurrentDirectory());
            Logger.LogDebug("Logger started!");
        }

        private void RightWindowSettings_Click(object sender, RoutedEventArgs e) {
            //todo open settings window
        }

        private void RightWindowGithub_Click(object sender, RoutedEventArgs e) => CUtilities.OpenGithub();
        private void MetroWindow_Closed(object sender, EventArgs e) => CUtilities.Dispose();

    }
}
