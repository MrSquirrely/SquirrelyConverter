using Image_Converter.Code;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Image_Converter.Views {
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl {
        private bool IsFinished = false;
        private string SelectedType { get; set; }

        private ThreadStart ConvertThreadStart { get; set; }
        private Thread ConvertThread { get; set; }
        private ThreadStart UpdateThreadStart { get; set; }
        private Thread UpdateThread { get; set; }

        public MainPage() {
            InitializeComponent();
            Utilities.ImageListView = ImageList;
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

        private void ImageList_Drop(object sender, DragEventArgs e) {
            Utilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[]);
            Utilities.ImageListView.ItemsSource = Utilities.ImageCollection;
        }
    }
}
