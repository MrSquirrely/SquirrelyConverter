using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using ConverterUtilities;
using ConverterUtilities.Configs;
using ConverterUtilities.CUtils;
using Humanizer;
using MahApps.Metro.Controls;
using Mr_Squirrely_Converters.Class;
using Exception = System.Exception;

namespace Mr_Squirrely_Converters.Views {
    public partial class MainWindow {

        private bool _firstViewed = true;
        private SettingsWindow _settingsWindow;

        public MainWindow() {
            //There is german translation in here but it will not be included in the release,
            //This is because I used Google translate to translate them. That is not the way you should do it
            //and I only did it that way to test responsiveness and how to do localization
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE"); //This is here for testing only, don't use this.
            InitializeComponent();

            Utilities.Startup(this, Application.Current.Dispatcher, Directory.GetCurrentDirectory());
            //Logger.StartLogger();
            //Logger.LogDebug("Logger Started");

            //Toast.CreateNotifier();
            //Logger.LogDebug("Toast Notifier Created");

            //Utilities.SetMainWindow(this);
            //Logger.LogDebug($"Main Window set to {Utilities.GetMainWindow()}");

            //Utilities.SetDispatcher(Application.Current.Dispatcher);
            //Logger.LogDebug($"Dispatcher set to {Utilities.GetDispatcher()}");

            //DirectoryInfos.WorkingDirectory = Directory.GetCurrentDirectory();
            //Logger.LogDebug($"Working Directoory set to {DirectoryInfos.WorkingDirectory}");
            MainUtilities.ConverterTabs = ConverterTabs;
            Options.StartGeneralSettings();
        }

        

        private void LoadViews() {
            MainUtilities.LoadAssemblies();
            //MainUtilities.AddViews();
        }

        private void RightWindowSettings_Click(object sender, RoutedEventArgs e) {
            Utilities.GetMainWindow().IsEnabled = false;
            BlurEffect myBlur = new BlurEffect {
                Radius = 5
            };
            Effect = myBlur;
            _settingsWindow = new SettingsWindow();
            
            Utilities.SetSettingsWindow(_settingsWindow);
            MainUtilities.SettingsTabs = _settingsWindow.SettingsTab;
            //MainUtilities.AddSettingViews();

            _settingsWindow.Owner = this;
            _settingsWindow.ParentWindow = this;
            _settingsWindow.Show();
        }

        //private void RightWindowUpdate_OnClick(object sender, RoutedEventArgs e) => CUtilities.CheckUpdate();
        private void RightWindowUpdate_OnClick(object sender, RoutedEventArgs e) {
            ConverterDownload converterDownload = new ConverterDownload();
            converterDownload.Show();
        }

        private void RightWindowGithub_OnClick(object sender, RoutedEventArgs e) => Webpages.OpenWebpage(Enums.Webpage.Github);
        //private void RightWindowDownload_OnClick(object sender, RoutedEventArgs e) => CUtilities.OpenDownload();
        private void MetroWindow_Closed(object sender, EventArgs e) => Utilities.Dispose();

        private void ConverterTabs_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {

            //TabItem tabItem = MainUtilities.ConverterTabs.SelectedItem as TabItem;
            //foreach (ConverterInfo converter in Utilities.GetConverterInfos()) {
            //    if (tabItem?.Header != null && (string)tabItem?.Header == converter.ConverterName) {
            //        Title = $"{converter.Author.Humanize()}'s {converter.ConverterName.Humanize()}";
            //    }
            //}

            //if (Title == "Mr. Squirrely's Video Converter" && _firstViewed) {
            //    Toast.VideoMessage();
            //    _firstViewed = false;
            //} Todo: Implement a on viewed for the video converter
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            //CUtilities.CheckUpdate();
            try {
                LoadViews();
                Toast.PreviewRelease();
                for (int i = 0; i < 2; i++) {
                    ConverterTabs.SelectedIndex = 0;
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
            }
        }
        
    }
}
