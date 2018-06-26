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
using System.Windows.Shapes;
using ConverterUtilities;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow  {
        public SettingsWindow() {
            InitializeComponent();
        }

        private void SettingsWindow_OnClosed(object sender, EventArgs e) {
            CUtilities.MainWindow.IsEnabled = true;
        }
    }
}
