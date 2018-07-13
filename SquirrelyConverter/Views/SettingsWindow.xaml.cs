using System;
using System.Windows;
using ConverterUtilities;
using ConverterUtilities.Interfaces;

namespace Mr_Squirrely_Converters.Views {
    public partial class SettingsWindow : ISettings {
        public bool Reset { get; set; }
        public Window ParentWindow { get; set; }

        public SettingsWindow() {
            InitializeComponent();
            StartValues();
        }

        private void SettingsWindow_OnClosed(object sender, EventArgs e) {
            CUtilities.MainWindow.IsEnabled = true;
            Owner.Effect = null;
        }

        public void StartValues() {
            CreateTemp.IsChecked = Options.GetCreateTemp();
            LocationText.Text = Options.GetTempLocation();
            DeleteTemp.IsChecked = Options.GetDeleteTemp();
        }

        public void SetValues(bool resetValues) {
            Options.SetCreateTemp(resetValues ? false : CreateTemp.IsChecked.Value);
            Options.SetDeleteTemp(resetValues ? true :  DeleteTemp.IsChecked.Value);
            Options.SetTempLocation(resetValues ? "./Converter_temp" : LocationText.Text);

        }
        
        public void CloseWindow() {
            ParentWindow.IsEnabled = true;
            Close();
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
