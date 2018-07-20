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
using static ConverterUtilities.CUtils.Downloader;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for ConverterAdd.xaml
    /// </summary>
    public partial class ConverterAdd  {

        public string DownloadUrl;

        public ConverterAdd() {
            InitializeComponent();
        }

        private void ConverterDownload_OnClick(object sender, RoutedEventArgs e) => Download(ConverterName.Content.ToString(), DownloadUrl);
    }
}
