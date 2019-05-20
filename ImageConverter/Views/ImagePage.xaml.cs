using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;
using Converter_Utilities.API;
using ImageConverter.API;
using MahApps.Metro.IconPacks;

namespace ImageConverter.Views {
    /// <summary>
    /// Interaction logic for ImagePage.xaml
    /// </summary>
    public partial class ImagePage : Page {
        private bool IsFinished = false;
        private string SelectedType { get; set; }

        private ThreadStart ConvertThreadStart { get; set; }
        private Thread ConvertThread { get; set; }
        private ThreadStart UpdateThreadStart { get; set; }
        private Thread UpdateThread { get; set; }
        public ImagePage() {
            InitializeComponent();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e) {
            IsFinished = false;
            ComboBoxItem selectedItem = (ComboBoxItem)TypeSelector.SelectedItem;
            SelectedType = selectedItem.Content.ToString().ToLower();
            API.Converter.SelectedType = SelectedType;
            ConvertThreadStart = API.Converter.StartConvert;
            ConvertThreadStart += () => {
                IsFinished = true;
                Snackbar.SendSnackbarMessage("Conversion Finished");
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

        private void Update() {
            while (!IsFinished) {
                foreach (InfoFile info in Utilities.ImageCollection) {
                    if (File.Exists($"{info.FileLocation}\\{info.FileName}.{SelectedType}")) {
                        info.FileIcon = PackIconMaterialKind.Check;
                        info.FileColor = Brushes.Green;
                        Utilities.ImageListView.Dispatcher.Invoke(() => { Utilities.ImageListView.Items.Refresh(); }, DispatcherPriority.Background);
                    }
                }
            }
        }

        private void ImageList_Drop(object sender, DragEventArgs e) {
            Utilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[]);
            Utilities.ImageListView.ItemsSource = Utilities.ImageCollection;
        }
    }
}
