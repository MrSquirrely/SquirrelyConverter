using System;
using System.Windows;
using System.Windows.Input;
using ConverterUtilities;
using System.Text.RegularExpressions;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage {
        private SettingsWindow _parentWindow;
        private bool _reset;

        public SettingsPage() {
            InitializeComponent();
            SetPages();
            StartSetValues();
        }

        private void SetPages() {
            if (!CUtilities.IsImageLoaded) {
                Control.Items.Remove(ImageTab);
            }

            if (!CUtilities.IsVideoLoaded) {
                Control.Items.Remove(VideoTab);
            }
        }

        private void StartSetValues() {
            #region General
            CreateTemp.IsChecked = Options.GetCreateTemp();
            LocationText.Text = Options.GetTempLocation();
            DeleteTemp.IsChecked = Options.GetDeleteTemp();
            #endregion

            #region Image
            //WebP
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
            #endregion

            #region Video
            VideoWidth.Text = Options.GetVideoWidth().ToString();
            VideoHeight.Text = Options.GetVideoHeight().ToString();
            ChangeSize.IsChecked = Options.GetVideoChangeSize();
            #endregion
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            if (!_reset) {
                SetValues(false);
            }
            Options.SaveSettings();
            StartSetValues();
            _reset = false;
            Toast.SettingsSaved();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) {
            SetValues(true);
            StartSetValues();
            _reset = true;
            Toast.SettingsReset();
        }

        private void SetValues(bool resetValues) {
            //General
            Options.SetCreateTemp(resetValues ? false : CreateTemp.IsChecked.Value);
            Options.SetDeleteTemp(resetValues ? true :  DeleteTemp.IsChecked.Value);
            Options.SetTempLocation(resetValues ? "./Converter_temp" : LocationText.Text);

            //Image
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

            //video
            Options.SetVideoChangeSize(resetValues ? false : ChangeSize.IsChecked.Value);
            Options.SetVideoWidth(resetValues ? 800 : Convert.ToInt32(VideoWidth.Text));
            Options.SetVideoHeight(resetValues ? 450 : Convert.ToInt32(VideoHeight.Text));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) => CloseWindow();

        private void CloseWindow() {
            CUtilities.MainWindow.IsEnabled = true;
            _parentWindow.Close();
        } 

        public void SetParent(SettingsWindow value) => _parentWindow = value;

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
