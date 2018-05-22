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
            Utilities._ImageItems = ImageFiles;
            Utilities._VideoItems = VideoFiles;
            Utilities.CheckForUpdate(false);
        }

        private void ImageFiles_Drop(object sender, DragEventArgs e) => Utilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[], Types.Images());
        private void VideoFiles_Drop(object sender, DragEventArgs e) => Utilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[], Types.Videos());

        private void ImageConvertButton_Click(object sender, RoutedEventArgs e) => Utilities.Convert(ImageFormatSelector.SelectedIndex, Types.Images());
        private void VideoConvertButton_Click(object sender, RoutedEventArgs e) => Utilities.Convert(ImageFormatSelector.SelectedIndex, Types.Videos());
        private void ClearMenu_Click(object sender, RoutedEventArgs e) => Utilities.Clear();

        private void ConverterTabs_SelectionChanged(object sender, SelectionChangedEventArgs e) => Utilities.UpdateTitle(ConverterTabs.SelectedIndex);
    }
}
