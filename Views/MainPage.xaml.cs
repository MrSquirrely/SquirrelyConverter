using System.Windows;
using System.Windows.Controls;
using Mr_Squirrely_Converters.Class;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();
            Utils._ImageItems = ImageFiles;
            Utils._VideoItems = VideoFiles;
            Utils.CheckForUpdate(false);
        }

        private void ImageFiles_Drop(object sender, DragEventArgs e) => Utils.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[], Types.Images());
        private void VideoFiles_Drop(object sender, DragEventArgs e) => Utils.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[], Types.Videos());

        private void ImageConvertButton_Click(object sender, RoutedEventArgs e) => Utils.Convert(ImageFormatSelector.SelectedIndex, Types.Images());
        private void VideoConvertButton_Click(object sender, RoutedEventArgs e) => Utils.Convert(ImageFormatSelector.SelectedIndex, Types.Videos());
        private void ClearMenu_Click(object sender, RoutedEventArgs e) => Utils.Clear();

        private void ConverterTabs_SelectionChanged(object sender, SelectionChangedEventArgs e) => Utils.UpdateTitle(ConverterTabs.SelectedIndex);
    }
}
