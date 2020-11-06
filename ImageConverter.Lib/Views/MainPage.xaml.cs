using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HandyControl.Controls;
using Squirrel_Sizer;
using ComboBox = System.Windows.Controls.ComboBox;
using MessageBox = HandyControl.Controls.MessageBox;
using Path = System.IO.Path;

namespace ImageConverter.Lib.Views {
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();

            Reference.ImageCollection = new ObservableCollection<ImageFile>();
            Reference.ImageListView = ImageListView;
            Reference.ImageListView.ItemsSource = Reference.ImageCollection;
        }

        private void ImageListView_OnDrop(object sender, DragEventArgs e) {
            Reference.ImageCollection.Clear();

            if(e.Data == null ) return;
            string[] data = (string[]) e.Data.GetData(DataFormats.FileDrop);
            switch (data) {
                case null:
                    break;
                default:
                    foreach (string file in data) {
                        FileInfo info = new FileInfo(file);
                        if (!Reference.ImageTypes.Contains(info.Extension.ToLower())) continue;
                        Reference.ImageCollection.Add(new ImageFile() {
                            Name = Path.GetFileNameWithoutExtension(file),
                            Type = info.Extension,
                            Location = info.DirectoryName,
                            Converted = false,
                            Size = Sizer.GetSizeSuffix(file)
                        });
                    }
                    break;
            }
        }

        private void ConvertButton_OnClick(object sender, RoutedEventArgs e) {
            Conversion.StartConversion(ImageTypeBox.SelectedItem);
        }

    }
}
