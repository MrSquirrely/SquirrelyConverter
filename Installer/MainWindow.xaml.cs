using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using MaterialDesignThemes;
using MaterialDesignThemes.Wpf.Transitions;
using ConverterUtilities;
using IWshRuntimeLibrary;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Installer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            CUtilities.SetWorkingDir(Directory.GetCurrentDirectory());
            CUtilities.GetVersionJson();
            LocationText.Text = CUtilities.DefaultInstall;
        }

        private string InstallLocation;

        private void InstallButton_OnClick(object sender, RoutedEventArgs e) {
            if (InstallButton.Content == "Close") {
                Close();
            }
            else {
                WebClient _webClient = new WebClient();
                if (!Directory.Exists(LocationText.Text)) {
                    Directory.CreateDirectory(LocationText.Text);
                }
                CUtilities.GetMainVersion();
                _webClient.DownloadFile(CUtilities.MainUrl, CUtilities.MainName);
                ZipFile.ExtractToDirectory(CUtilities.MainName, LocationText.Text);

                if (ImageInstall.IsChecked.Value) {
                    _webClient.DownloadFile(CUtilities.ImageUrl, CUtilities.ImageName);
                    ZipFile.ExtractToDirectory(CUtilities.ImageName, LocationText.Text);
                }

                if (VideoInstall.IsChecked.Value) {
                    _webClient.DownloadFile(CUtilities.VideoUrl, CUtilities.VideoName);
                    ZipFile.ExtractToDirectory(CUtilities.VideoName, LocationText.Text);
                }
                InstallButton.Content = "Close";
                if (DesktopShortcut.IsChecked.Value) {
                    CreateShortcut();
                }
            }
        }

        private void BrowseButton_OnClick(object sender, RoutedEventArgs e) {
            CommonOpenFileDialog openFileDialog = new CommonOpenFileDialog {
                InitialDirectory = "C:\\Program Files",
                IsFolderPicker = true
            };

            if (openFileDialog.ShowDialog() == CommonFileDialogResult.Ok) {
                LocationText.Text = openFileDialog.FileName;
            }
        }

        /*
         * object shDesktop = (object)"Desktop";
           WshShell shell = new WshShell();
           string shortcutAddress = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\Notepad.lnk";
           IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutAddress);
           shortcut.Description = "New shortcut for a Notepad";
           shortcut.Hotkey = "Ctrl+Shift+N";
           shortcut.TargetPath = Environment.GetFolderPath(Environment.SpecialFolders.System) + @"\notepad.exe";
           shortcut.Save();
         */
        private void CreateShortcut() {
            object shDesktop = (object) "Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string) shell.SpecialFolders.Item(ref shDesktop) + @"\Squirrely Converters.lnk";
            IWshShortcut shortcut = (IWshShortcut) shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Easy converters for everyone";
            shortcut.TargetPath = $"{LocationText.Text}\\MrSquirrelysConverters.exe";
            shortcut.WorkingDirectory = $"{LocationText.Text}";
            shortcut.IconLocation = $"{LocationText.Text}\\icon.ico";
            shortcut.Save();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e) {
            CUtilities.DeleteImageVersion();
            CUtilities.DeleteMainVersion();
            CUtilities.DeleteVideoVersion();
            //if (OpenOnClose.IsChecked.Value) {
            //    Process.Start($"{LocationText.Text}\\MrSquirrelysConverters.exe");
            //}
        }
    }
}
