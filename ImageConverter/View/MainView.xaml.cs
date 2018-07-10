using System.Globalization;
using System.Threading;
using System.Windows;
using ImageConverter.Class;
using ConverterUtilities;

namespace ImageConverter.View {
    public partial class MainView {
        public MainView() {
            InitializeComponent();
            ImageUtilities.ImageListView = ImageFiles;
            ImageUtilities.ImageView = this;

        }

        private void ImageConvertButton_Click(object sender, RoutedEventArgs e) => ImageUtilities.Convert(ImageFormatSelector.SelectedIndex);
        private void ClearMenu_Click(object sender, RoutedEventArgs e) => ImageUtilities.Clear();
        private void ImageFiles_Drop(object sender, DragEventArgs e) => ImageUtilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[]);

        private void MainView_OnLoaded(object sender, RoutedEventArgs e) {
            // Some more code might go here. I still need to test things out.
            ImageFormatSelector.Margin = new Thickness(0, 0, ImageConvertButton.Margin.Right + ImageConvertButton.ActualWidth + 12 ,10);
        }
    }
}
