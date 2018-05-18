using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Mr_Squirrely_Converters.Views;
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
            Utils.DownloadFiles();
            Utils.OpenSettings();
            Utils._MainWindow = this;
            
            Utils._MainWindow.Content = Utils._MainPage;
            Utils._WorkingDir = Directory.GetCurrentDirectory();
        }

        private void RightWindowSettings_Click(object sender, RoutedEventArgs e) => Utils._MainWindow.Content = Utils._SettingsPage;
        private void RightWindowGithub_Click(object sender, RoutedEventArgs e) => Utils.OpenGithub();
        private void MetroWindow_Closed(object sender, EventArgs e) => Utils.Dispose();

        private void HamburgerButton_Click(object sender, RoutedEventArgs e) => HamburgerButton.ContextMenu.IsOpen = true;
        private void SettingsMenu_Click(object sender, RoutedEventArgs e) => Utils._MainWindow.Content = Utils._SettingsPage;
        private void ClearMenu_Click(object sender, RoutedEventArgs e) => Utils.Clear();
        private void UpdateMenu_Click(object sender, RoutedEventArgs e) => Utils.CheckForUpdate(true);

    }
}
