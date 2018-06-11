using System;
using System.Windows;
using System.Windows.Controls;
using Mr_Squirrely_Converters.Class;
using System.IO;
using ConverterUtilities;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page {
        public MainPage() {
            InitializeComponent();
            //Utilities.ImageItems = ImageFiles;
            //Utilities.VideoItems = VideoFiles;
            Utilities.ConverterTabs = ConverterTabs;

            Utilities.CheckForUpdate(false);

            Console.WriteLine($"{Directory.GetCurrentDirectory()}");

            try {
                if (File.Exists($"{CUtilities.WorkingDir}ImageConverter.dll")) {
                    Utilities.AddImageTab();
                }
            }
            catch {

            }


            try {
                if (File.Exists($"{CUtilities.WorkingDir}VideoConverter.dll")) {
                    Utilities.AddVideoTab();
                }
            }
            catch {

            }
            




        }

        private void ImageFiles_Drop(object sender, DragEventArgs e) => Class.Utilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[], Types.Images());
        private void VideoFiles_Drop(object sender, DragEventArgs e) => Class.Utilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[], Types.Videos());

        //private void ImageConvertButton_Click(object sender, RoutedEventArgs e) => Utilities.Convert(ImageFormatSelector.SelectedIndex, Types.Images());
        //private void VideoConvertButton_Click(object sender, RoutedEventArgs e) => Utilities.Convert(ImageFormatSelector.SelectedIndex, Types.Videos());
        private void ClearMenu_Click(object sender, RoutedEventArgs e) => Class.Utilities.Clear();

        private void ConverterTabs_SelectionChanged(object sender, SelectionChangedEventArgs e) => Class.Utilities.UpdateTitle(ConverterTabs.SelectedIndex);
    }
}
