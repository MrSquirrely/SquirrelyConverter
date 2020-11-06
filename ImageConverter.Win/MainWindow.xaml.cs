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

namespace ImageConverter.Win {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            MainFrame.Content = Lib.Reference.GetMainPage;
            AboutFrame.Content = Lib.Reference.GetAboutPage;
            SettingsFrame.Content = Lib.Reference.GetSettingsPage;

            Lib.Reference.SettingsDrawer = SettingsDrawer;
            Lib.Reference.AboutDrawer = AboutDrawer;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e) => Lib.Reference.OpenSettingsDrawer();

        private void AboutButton_Click(object sender, RoutedEventArgs e) => Lib.Reference.OpenAboutDrawer();

        private void BugButton_Click(object sender, RoutedEventArgs e) {
            //Todo: implement
        }
    }
}
