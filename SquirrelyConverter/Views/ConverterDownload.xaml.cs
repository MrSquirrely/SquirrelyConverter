using System;
using System.Collections.Generic;
using System.IO;
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
using ConverterUtilities.Configs;
using ConverterUtilities.CUtils;
using FileStyleUriParser = System.FileStyleUriParser;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for ConverterDownload.xaml
    /// </summary>
    public partial class ConverterDownload : Window {
        public ConverterDownload() {
            InitializeComponent();
            Downloader.DownloadRepo();
            foreach (ConverterInfo converterInfo in Utilities.GetConverterInfos()) {
                ConverterList.Items.Add(new ConverterAdd() {
                    ConverterName = { Content = converterInfo.ConverterName },
                    ConverterDiscription = { Text = converterInfo.ConverterDescription }
                });
            }
        }

        private void ConverterDownload_OnClosed(object sender, EventArgs e) {
            File.Delete("ImageConverter.converter");
            File.Delete("VideoConverter.converter");
        }

        private void ConverterList_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            foreach (ConverterInfo converterInfo in Utilities.GetConverterInfos()) {
                ConverterAdd added = (ConverterAdd) ConverterList.SelectedItem;
                if (converterInfo.ConverterName != null && (string)added.ConverterName.Content == converterInfo.ConverterName) {
                    ConverterInfoName.Content = converterInfo.ConverterName;
                    ConverterInfoDiscription.Text = converterInfo.ConverterDescription;
                    ConverterInfoAuthor.Content = converterInfo.Author;
                    ConverterInfoVersion.Content = converterInfo.ConverterVersion;
                }
            }
        }
    }
}
