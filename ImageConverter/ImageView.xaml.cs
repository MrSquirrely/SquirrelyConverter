using System.Windows;
using ConverterUtilities;


namespace ImageConverter {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ImageView {
        public ImageView() {
            InitializeComponent();
            ImageUtilities.ImageListView = ImageFiles;
            ImageUtilities.ImageView = this;
        }

        public void UpdateView() => ImageFiles.Items.Refresh();

        private void ImageConvertButton_Click(object sender, RoutedEventArgs e) => ImageUtilities.Convert(ImageFormatSelector.SelectedIndex);
        private void ClearMenu_Click(object sender, RoutedEventArgs e) { }
        private void ImageFiles_Drop(object sender, DragEventArgs e) => ImageUtilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[]);

        private void ImageView_OnGotFocus(object sender, RoutedEventArgs e) => CUtilities.UpdateTitle("Image Converter");
    }
}
