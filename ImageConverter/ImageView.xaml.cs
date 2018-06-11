using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using ConverterUtilities;


namespace ImageConverter {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ImageView : UserControl {
        public ImageView() {
            InitializeComponent();
            ImageUtilities.imageListView = ImageFiles;
            ImageUtilities.imageView = this;
        }

        public void UpdateView() => ImageFiles.Items.Refresh();

        private void ImageConvertButton_Click(object sender, RoutedEventArgs e) => ImageUtilities.Convert(ImageFormatSelector.SelectedIndex);
        private void ClearMenu_Click(object sender, RoutedEventArgs e) { }
        private void ImageFiles_Drop(object sender, DragEventArgs e) => ImageUtilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[]);
    }
}
