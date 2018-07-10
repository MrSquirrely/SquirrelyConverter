using System;
using ConverterUtilities;

namespace Mr_Squirrely_Converters.Views {
    public partial class SettingsWindow  {
        public SettingsWindow() => InitializeComponent();

        private void SettingsWindow_OnClosed(object sender, EventArgs e) {
            CUtilities.MainWindow.IsEnabled = true;
            Owner.Effect = null;
        } 
    }
}
