using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
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
            CUtilities.MainWindow = this;
            CUtilities.DownloadButton = RightWindowDownload;
            CUtilities.Dispatcher = Application.Current.Dispatcher;
            CUtilities.SetWorkingDir(Directory.GetCurrentDirectory());
            
            Options.StartGeneralSettings();

            Logger.LogDebug($"{CUtilities.GetWorkingDir()}");

            CUtilities.GetVersionJson();
        }

        private SettingsWindow _settingsWindow;
        private SettingsPage _settingsPage;

        private void LoadViews() {
            if (File.Exists($"{CUtilities.GetWorkingDir()}\\ImageConverter.dll")) {
                Utilities.AddImageTab();
                Options.StartImageSettings();
                CUtilities.IsImageLoaded = true;
            }
            
            if (File.Exists($"{CUtilities.GetWorkingDir()}\\VideoConverter.dll")) {
                Utilities.AddVideoTab();
                Options.StartVideoSettings();
                CUtilities.IsVideoLoaded = true;
            }
        }

        private void RightWindowSettings_Click(object sender, RoutedEventArgs e) {
            CUtilities.MainWindow.IsEnabled = false;
            BlurEffect myBlur = new BlurEffect {
                Radius = 5
            };
            Effect = myBlur;
            _settingsPage = new SettingsPage();
            _settingsWindow = new SettingsWindow { Content = _settingsPage, Owner = this};
            _settingsPage.SetParent(_settingsWindow);
            _settingsWindow.Show();
        }

        private void RightWindowUpdate_OnClick(object sender, RoutedEventArgs e) => CUtilities.CheckUpdate();
        private void RightWindowGithub_OnClick(object sender, RoutedEventArgs e) => CUtilities.OpenGithub();
        private void RightWindowDownload_OnClick(object sender, RoutedEventArgs e) => CUtilities.OpenDownload();
        private void MetroWindow_Closed(object sender, EventArgs e) => CUtilities.Dispose();

        private void ConverterTabs_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            TabItem tabItem = Utilities.ConverterTabs.SelectedItem as TabItem;
            Title = $"Mr. Squirrely's {tabItem?.Header ?? "Converters"}";
            if (Title == "Mr. Squirrely's Video Converter" && _firstViewed) {
                Toast.VideoMessage();
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            //CUtilities.CheckUpdate();
            LoadViews();
            Toast.PreviewRelease();
            ConverterTabs.SelectedIndex = 0;
        }

    }
}
