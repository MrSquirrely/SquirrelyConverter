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
using Mr_Squirrely_Converters.Class;

namespace Mr_Squirrely_Converters.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Utils._ImageItems = ImageFiles;
            Utils._VideoItems = VideoFiles;
            Utils.CheckForUpdate(false);
        }

        private void RightWindowSettings_Click(object sender, RoutedEventArgs e) => Utils.OpenSettings();
        private void RightWindowAbout_Click(object sender, RoutedEventArgs e) => Utils.OpenAbout();
        private void RightWindowGithub_Click(object sender, RoutedEventArgs e) => Utils.OpenGithub();
        private void MetroWindow_Closed(object sender, EventArgs e) => Utils.CloseWindows();

        private void ImageFiles_Drop(object sender, DragEventArgs e) => Utils.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[], Types.Images());
        private void VideoFiles_Drop(object sender, DragEventArgs e) => Utils.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[], Types.Videos());

        private void ImageConvertButton_Click(object sender, RoutedEventArgs e) => Utils.Convert(ImageFormatSelector.SelectedIndex, Types.Images());
        private void VideoConvertButton_Click(object sender, RoutedEventArgs e) => Utils.Convert(ImageFormatSelector.SelectedIndex, Types.Videos());

        //private void HamburgerButton_Click(object sender, RoutedEventArgs e) => HamburgerButton.ContextMenu.IsOpen = true;
        private void SettingsMenu_Click(object sender, RoutedEventArgs e) => Utils.OpenSettings();
        private void AboutMenu_Click(object sender, RoutedEventArgs e) => Utils.OpenAbout();
        private void ClearMenu_Click(object sender, RoutedEventArgs e) => Utils.Clear();
        private void UpdateMenu_Click(object sender, RoutedEventArgs e) => Utils.CheckForUpdate(true);

        private void ConverterTabs_SelectionChanged(object sender, SelectionChangedEventArgs e) => Utils.UpdateTitle(ConverterTabs.SelectedIndex);
    }
}
