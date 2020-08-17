using Converters.Lib;
using HandyControl.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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

namespace Converters.Win {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();

            Config.LoadPlugins();
            foreach (IConverter converter in Config.Converters) {

                SideMenuItem mainItem = new SideMenuItem();
                    mainItem.Header = "Home";
                    mainItem.Selected += delegate (object sender, RoutedEventArgs e) { SideMenuItem_Selected(sender, e, converter, converter.GetMainView()); };
                SideMenuItem settingsItem = new SideMenuItem();
                    settingsItem.Header = "Settings";
                    settingsItem.Selected += delegate (object sender, RoutedEventArgs e) { SideMenuItem_Selected(sender, e, converter, converter.GetSettingsView()); };
                SideMenuItem aboutItem = new SideMenuItem();
                    aboutItem.Header = "About";
                    aboutItem.Selected += delegate (object sender, RoutedEventArgs e) { SideMenuItem_Selected(sender, e, converter, converter.GetAboutView()); };
                
                SideMenuItem headerItem = new SideMenuItem();
                    headerItem.Header = converter.GetName();
                    headerItem.Items.Add(mainItem);
                    headerItem.Items.Add(settingsItem);
                    headerItem.Items.Add(aboutItem);

                ContentHeader.Items.Add(headerItem);
            }

            //for (int i = 1; i < 5; i++ ) {
            //    SideMenuItem sideMenuItem = new SideMenuItem();
            //    sideMenuItem.Header = $"Header {i}";
            //    sideMenuItem.Selected += delegate (object sender, RoutedEventArgs e) { SideMenuItem_Selected(sender,e, $"You selected header {i}"); };
            //    ContentHeader.Items.Add(sideMenuItem);
            //}


            //< !--< hc:SideMenuItem Header = "Image Converter"
            //                 Command = "{Binding NewCommand}"
            //                 Selected = ""
            //                 CommandParameter = "ThisIsANewCommand" >
            //    < hc:SideMenuItem Header = "About" />

            //     < hc:SideMenuItem Header = "About" />

            //  </ hc:SideMenuItem > -->

            //Config.LoadPlugins();
            //foreach (IConverter converter in Config.Converters) {
            //    PluginsTab.Items.Add(
            //        new MetroTabItem {
            //            Header = converter.GetName(),
            //            Content = new Frame {
            //                Content = converter.GetMainView()
            //            }
            //        });
            //    PluginsTab.Items.Refresh();
            //}
        }

        private void SideMenuItem_Selected(object sender, RoutedEventArgs e, IConverter converter, Page page) {
            if (Config.Converters.Contains(converter)) {
                //if (page == converter.GetMainView()) {
                //    Title = converter.GetName();
                //}
                Title = $"{converter.GetName()} - {page.Title}";
                ContentFrame.Content = page;
            }
        }

        //private void PluginHamburger_ItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args) {

        //}
    }
}
