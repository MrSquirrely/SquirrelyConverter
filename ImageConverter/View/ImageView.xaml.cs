using System.Windows;
using ImageConverter.Class;

namespace ImageConverter.View {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ImageView {
        public ImageView() {
            InitializeComponent();
            ImageUtilities.ImageListView = ImageFiles;
            ImageUtilities.ImageView = this;
        }

        private void ImageConvertButton_Click(object sender, RoutedEventArgs e) => ImageUtilities.Convert(ImageFormatSelector.SelectedIndex);
        private void ClearMenu_Click(object sender, RoutedEventArgs e) => ImageUtilities.Clear();
        private void ImageFiles_Drop(object sender, DragEventArgs e) => ImageUtilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[]);
    }
}
