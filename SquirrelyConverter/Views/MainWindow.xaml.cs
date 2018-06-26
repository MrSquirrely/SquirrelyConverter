using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ConverterUtilities;
using Mr_Squirrely_Converters.Class;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        private bool _firstViewed = true;

        public MainWindow() {
            InitializeComponent();
            Logger.StartLogger();
            Logger.LogDebug("Logger Started");
            Toast.CreateNotifier();
            Logger.LogDebug("Toast Notifier Created");
            
            Utilities.ConverterTabs = ConverterTabs;
            CUtilities.MainWindow = Application.Current.MainWindow;
            CUtilities.Dispatcher = Application.Current.Dispatcher;
            CUtilities.SetWorkdingDir(Directory.GetCurrentDirectory());
            
            Options.StartGeneralSettings();

            Logger.LogDebug($"{CUtilities.GetWorkdingDir()}");

            if (File.Exists($"{CUtilities.GetWorkdingDir()}\\ImageConverter.dll")) {
                Utilities.AddImageTab();
                Options.StartImageSettings();
                CUtilities.IsImageLoaded = true;
            }
            
            if (File.Exists($"{CUtilities.GetWorkdingDir()}\\VideoConverter.dll")) {
                Utilities.AddVideoTab();
                Options.StartVideoSettings();
                CUtilities.IsVideoLoaded = true;
            }

            //CUtilities.CheckVersion(true,true,true); Todo do this better

            //Toast.PreviewRelease();
        }

        private SettingsWindow _settingsWindow;
        private SettingsPage _settingsPage;

        private void RightWindowSettings_Click(object sender, RoutedEventArgs e) {
            CUtilities.MainWindow.IsEnabled = false;
            _settingsPage = new SettingsPage();
            _settingsWindow = new SettingsWindow { Content = _settingsPage };
            _settingsPage.SetParent(_settingsWindow);
            _settingsWindow.Show();
        }

        private void RightWindowGithub_Click(object sender, RoutedEventArgs e) => CUtilities.OpenGithub();
        private void MetroWindow_Closed(object sender, EventArgs e) => CUtilities.Dispose();

        private void ConverterTabs_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            TabItem tabItem = Utilities.ConverterTabs.SelectedItem as TabItem;
            Title = $"Mr. Squirrely's {tabItem?.Header ?? "Converters"}";
            if (Title == "Mr. Squirrely's Video Converter" && _firstViewed) {
                Toast.VideoMessage();
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            Toast.PreviewRelease();
        }
    }
}
