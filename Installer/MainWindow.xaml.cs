using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows;
using ConverterUtilities;
using IWshRuntimeLibrary;
using Microsoft.WindowsAPICodePack.Dialogs;
using File = System.IO.File;

namespace Installer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            CUtilities.SetWorkingDir(Directory.GetCurrentDirectory());
            CUtilities.GetVersionJson();
            LocationText.Text = CUtilities.DefaultInstall;
        }
        
        private void InstallButton_OnClick(object sender, RoutedEventArgs e) {
            if ((string) InstallButton.Content == "Close") {
                Close();
            }
            else {
                using (WebClient webClient = new WebClient()) {
                    if (!Directory.Exists(LocationText.Text)) {
                        Directory.CreateDirectory(LocationText.Text);
                    }

                    if (File.Exists($"{LocationText.Text}\\Uninstall.exe")) {
                        File.Delete("Uninstall.exe");
                    }
                    
                    webClient.DownloadFile(CUtilities.MainUrl, CUtilities.MainName);
                    ZipFile.ExtractToDirectory(CUtilities.MainName, LocationText.Text);

                    if (ImageInstall.IsChecked != null && ImageInstall.IsChecked.Value) {
                        webClient.DownloadFile(CUtilities.ImageUrl, CUtilities.ImageName);
                        ZipFile.ExtractToDirectory(CUtilities.ImageName, LocationText.Text);
                    }

                    if (VideoInstall.IsChecked != null && VideoInstall.IsChecked.Value) {
                        webClient.DownloadFile(CUtilities.VideoUrl, CUtilities.VideoName);
                        ZipFile.ExtractToDirectory(CUtilities.VideoName, LocationText.Text);
                    }
                }
                InstallButton.Content = "Close";
                if (DesktopShortcut.IsChecked != null && DesktopShortcut.IsChecked.Value) {
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


        private void CreateShortcut() {
            object shDesktop = "Desktop";
            WshShell shell = new WshShell();
            string shortcutAddress = (string) shell.SpecialFolders.Item(ref shDesktop) + @"\Squirrely Converters.lnk";
            IWshShortcut shortcut = (IWshShortcut) shell.CreateShortcut(shortcutAddress);
            shortcut.Description = "Easy converters for everyone";
            shortcut.TargetPath = $"{LocationText.Text}\\MrSquirrelysConverters.exe";
            shortcut.WorkingDirectory = $"{LocationText.Text}";
            shortcut.IconLocation = $"{LocationText.Text}\\icon.ico";
            shortcut.Save();
        }
        
        
        //Todo: Write open on close code
        private void MainWindow_OnClosed(object sender, EventArgs e) {
            CUtilities.DeleteImageVersion();
            CUtilities.DeleteMainVersion();
            CUtilities.DeleteVideoVersion();
            File.Delete("version.json");
            //if (OpenOnClose.IsChecked.Value) {
            //    Process.Start($"{LocationText.Text}\\MrSquirrelysConverters.exe");
            //}
        }
    }
}
