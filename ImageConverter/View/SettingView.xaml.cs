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
using ConverterUtilities.CUtils;
using ConverterUtilities.Interfaces;

namespace ImageConverter.View {
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView : ISettings {
        public bool Reset { get; set; }
        public Window ParentWindow { get; set; }

        public SettingView() {
            InitializeComponent();
            StartValues();
            ParentWindow = CUtilities.SettingsWindow;
        }

        public void StartValues() {
            WebPLossless.IsChecked = Options.GetWebPLossless();
            WebPRemoveAlpha.IsChecked = Options.GetWebPRemoveAlpha();
            WebPEmulateJpeg.IsChecked = Options.GetWebPEmulateJpeg();
            WebpQuality.Value = Options.GetWebPQuality();

            //Jpeg
            JpegQuality.Value = Options.GetJpegQuality();

            //Png
            PngLossless.IsChecked = Options.GetPngLossless();
            PngQuality.Value = Options.GetPngQuality();
            PngRemoveAlpha.IsChecked = Options.GetPngRemoveAlpha();
        }

        public void SetValues(bool resetValues) {
            //WebP
            Options.SetWebPQuality(resetValues ? 80 : WebpQuality.Value);
            Options.SetWebPEmulateJpeg(resetValues ? false : WebPEmulateJpeg.IsChecked.Value);
            Options.SetWebPLossless(resetValues ? true : WebPLossless.IsChecked.Value);
            Options.SetWebPRemoveAlpha(resetValues ? false : WebPRemoveAlpha.IsChecked.Value);
            //Jpeg
            Options.SetJpegQuality(resetValues ? 80 : JpegQuality.Value);
            //Png
            Options.SetPngLossless(resetValues ? true : PngLossless.IsChecked.Value);
            Options.SetPngQuality(resetValues ? 80 : PngQuality.Value);
            Options.SetPngRemoveAlpha(resetValues ? false : PngRemoveAlpha.IsChecked.Value);
        }

        public void CloseWindow() {
            CUtilities.MainWindow.IsEnabled = true;
            ParentWindow.Close();
        }

        public void SaveButton_Click(object sender, RoutedEventArgs e) {
            if (!Reset) {
                SetValues(false);
            }
            Options.SaveSettings();
            StartValues();
            Reset = false;
            Toast.SettingsSaved();
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e) => CloseWindow();

        public void ResetButton_Click(object sender, RoutedEventArgs e) {
            SetValues(true);
            StartValues();
            Reset = true;
            Toast.SettingsReset();
        }
    }
}
