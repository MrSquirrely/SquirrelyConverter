using Image_Converter.Code;
using Image_Converter.Views;
using MaterialDesignThemes.Wpf;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Image_Converter {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        private double FlyoutWidth => (Width / 3) + (Width / 6);
        private bool IsFinished = false;
        private string SelectedType { get; set; }

        private ThreadStart ConvertThreadStart { get; set; }
        private Thread ConvertThread { get; set; }
        private ThreadStart UpdateThreadStart { get; set; }
        private Thread UpdateThread { get; set; }

        public MainWindow() {
            InitializeComponent();
            Utilities.ImageListView = ImageList;
            Utilities.messageQueue = SnackbarToaster.MessageQueue;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e) => OpenFlyout(new SettingsContent(), Properties.Resources.Settings);
        private void AboutButton_Click(object sender, RoutedEventArgs e) => OpenFlyout(new AboutContent(), Properties.Resources.About);
        private void BugButton_Click(object sender, RoutedEventArgs e) => OpenFlyout(new BugContent(), "Bugs or Features");

        private void OpenFlyout(UserControl content, string header) {
            content.Width = FlyoutWidth;
            content.Height = FlyoutControl.Height;
            FlyoutControl.Content = content;
            FlyoutControl.Header = header;
            FlyoutControl.Width = FlyoutWidth;
            FlyoutControl.IsOpen = true;
        }

        private void FileButton_Click(object sender, RoutedEventArgs e) => FileButton.ContextMenu.IsOpen = true;

        private void OpenFolderMenu_Click(object sender, RoutedEventArgs e) {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog {
                InitialDirectory = Environment.CurrentDirectory,
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                List<string> files = new List<string>();
                foreach (string type in Filters.ImageTypes) {
                    IEnumerable<string> scannedFiles = Directory.EnumerateFiles(dialog.FileName, $"*{type}", SearchOption.TopDirectoryOnly);
                    foreach (string file in scannedFiles) {
                        files.Add(file);
                    }
                }
                Utilities.PopulateList(files.ToArray());
                Utilities.ImageListView.ItemsSource = Utilities.ImageCollection;
            }
        }

        private void OpenFileMenu_Click(object sender, RoutedEventArgs e) {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog {
                InitialDirectory = Environment.CurrentDirectory,
                Multiselect = true
            };
            dialog.Filters.Add(Filters.ImageFilters);
            dialog.Filters.Add(Filters.WebPFilter);
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                Utilities.ImageCollection.Clear();
                foreach (string file in dialog.FileNames) {
                    FileInfo fileInfo = new FileInfo(file);
                    Utilities.ImageCollection.Add(new ImageInfo() {
                        FileName = Path.GetFileNameWithoutExtension(file),
                        FileType = fileInfo.Extension,
                        FileSize = Utilities.SizeSuffix(fileInfo.Length),
                        FileIcon = PackIconKind.Close,
                        FileColor = Brushes.Red,
                        FileLocation = fileInfo.DirectoryName
                    });
                }
                Utilities.ImageListView.ItemsSource = Utilities.ImageCollection;
            }

        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private void ImageList_Drop(object sender, DragEventArgs e) {
            Utilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[]);
            Utilities.ImageListView.ItemsSource = Utilities.ImageCollection;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            IsFinished = false;
            ConvertThreadStart = Update;
            ConvertThreadStart += () => {
                IsFinished = true;
            };
            Thread thread = new Thread(ConvertThreadStart);
            thread.Start();
        }

        private void Update() {
            while (!IsFinished) {
                foreach (ImageInfo info in Utilities.ImageCollection) {
                    if (File.Exists($"{info.FileLocation}\\{info.FileName}.{SelectedType}")) {
                        info.FileIcon = PackIconKind.Check;
                        info.FileColor = Brushes.Green;
                        Utilities.ImageListView.Dispatcher.Invoke(() => { Utilities.ImageListView.Items.Refresh(); }, DispatcherPriority.Background);
                    }
                }
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e) {
            IsFinished = false;
            ComboBoxItem selectedItem = (ComboBoxItem)TypeSelector.SelectedItem;
            SelectedType = selectedItem.Content.ToString().ToLower();
            Converter.SelectedType = SelectedType;
            ConvertThreadStart = Converter.StartConvert;
            ConvertThreadStart += () => {
                IsFinished = true;
                Utilities.SendSnackbarMessage("Conversion Finished");
                if (Properties.Settings.Default.General_PlaySound) {
                    SoundPlayer player = new SoundPlayer("finished.wav");
                    player.Play();
                }
            };
            ConvertThread = new Thread(ConvertThreadStart);
            ConvertThread.Start();

            UpdateThreadStart = Update;
            UpdateThread = new Thread(UpdateThreadStart);
            UpdateThread.Start();
        }
    }
}
