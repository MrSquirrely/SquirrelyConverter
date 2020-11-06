using System;
using System.Collections.Generic;
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

namespace ImageConverter.Lib.Views {
    /// <summary>
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page {
        public SettingsPage() {
            InitializeComponent();
            Property.SelectedObject = Reference.GetPropertiesModel;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            Reference.SettingsDrawer.IsOpen = false;
            //Reset view code
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e) {
            Reference.SaveProperties((PropertiesModel)Property.SelectedObject);
        }
    }
}
