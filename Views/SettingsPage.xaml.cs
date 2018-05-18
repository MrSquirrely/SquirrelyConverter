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
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        //I need to work on this a little bit. 
        //Currently it sets the view and even if you cancel it never goes back. only a Restart would fix it.
        public SettingsPage()
        {
            InitializeComponent();
            SetValues();
        }

        private void SetValues() {
            TempCreate.IsChecked = Options.CreateTemp;
            LocationTemp.Text = Options.TempLocation;
            DeleteImages.IsChecked = Options.ImagesDelete;
            LosslessWebP.IsChecked = Options.WebPLossless;
            RemoveAlphaWebP.IsChecked = Options.WebPRemoveAlpha;
            EmulateJPEGWebP.IsChecked = Options.WebPEmulateJPEG;
            QualityWebP.Value = Options.WebPQuality;
            LosslessPNG.IsChecked = Options.PNGLossless;
            RemoveAlphaPNG.IsChecked = Options.PNGRemoveAlpha;
            QualityPNG.Value = Options.PNGQuality;
            QualityJPEG.Value = Options.JPEGQuality;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            Options.CreateTemp = TempCreate.IsChecked.Value;
            Options.TempLocation = LocationTemp.Text;
            Options.ImagesDelete = DeleteImages.IsChecked.Value;
            Options.WebPLossless = LosslessWebP.IsChecked.Value;
            Options.WebPRemoveAlpha = RemoveAlphaWebP.IsChecked.Value;
            Options.WebPEmulateJPEG = EmulateJPEGWebP.IsChecked.Value;
            Options.WebPQuality = QualityWebP.Value;
            Options.PNGLossless = LosslessPNG.IsChecked.Value;
            Options.PNGRemoveAlpha = RemoveAlphaPNG.IsChecked.Value;
            Options.PNGQuality = QualityPNG.Value;
            Options.JPEGQuality = QualityJPEG.Value;

            Toast.SettingsSaved();

            Options.Save();
            Utils._MainWindow.Content = Utils._MainPage;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) {
            Options.CreateTemp = false;
            Options.TempLocation = ".image_temp";
            Options.ImagesDelete = true;
            Options.WebPLossless = false;
            Options.WebPRemoveAlpha = false;
            Options.WebPEmulateJPEG = false;
            Options.WebPQuality = 80;
            Options.PNGLossless = true;
            Options.PNGRemoveAlpha = false;
            Options.PNGQuality = 80;
            Options.JPEGQuality = 80;
            SetValues();
            Toast.SettingsReset();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => Utils._MainWindow.Content = Utils._MainPage;
    }
}
