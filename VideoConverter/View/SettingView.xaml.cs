using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using ConverterUtilities.Interfaces;

namespace VideoConverter.View {
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
            VideoWidth.Text = Options.GetVideoWidth().ToString();
            VideoHeight.Text = Options.GetVideoHeight().ToString();
            ChangeSize.IsChecked = Options.GetVideoChangeSize();
        }

        public void SetValues(bool resetValues) {
            Options.SetVideoChangeSize(resetValues ? false : ChangeSize.IsChecked.Value);
            Options.SetVideoWidth(resetValues ? 800 : Convert.ToInt32(VideoWidth.Text));
            Options.SetVideoHeight(resetValues ? 450 : Convert.ToInt32(VideoHeight.Text));
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

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e) {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
