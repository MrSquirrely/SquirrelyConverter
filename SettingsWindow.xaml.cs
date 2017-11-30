using Microsoft.WindowsAPICodePack.Dialogs;
using Notifications.Wpf;
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

namespace SquirrelyConverter
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        NotificationManager toast = new NotificationManager();

        public SettingsWindow() {
            InitializeComponent();
            TempFolderBox.Text = $"{Utils.WorkingDir}/{Utils.tempDir}";
            DeleteTemp.IsChecked = Options.DeleteTemp;
            Quality.Value = Options.WebPQuality;
            Lossless.IsChecked = Options.WebPLossless;
            NoAlpha.IsChecked = Options.WebPNoAlpha;
            SaveEXIF.IsChecked = Options.WebPCopyMeta;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            //Options.TempDir = TempFolderBox.Text;
            //Options.OutDir = OutputFolderBox.Text; //This doesn't work at the moment so it does nothing!
            Options.DeleteTemp = DeleteTemp.IsChecked.GetValueOrDefault();
            Options.WebPQuality = Quality.Value;
            Options.WebPLossless = Lossless.IsChecked.GetValueOrDefault();
            Options.WebPNoAlpha = NoAlpha.IsChecked.GetValueOrDefault();
            Options.WebPCopyMeta = SaveEXIF.IsChecked.GetValueOrDefault();
            Options.Save();

            toast.Show(new NotificationContent {
                Title = "Settings Saved",
                Message = "The settings were saved.",
                Type = NotificationType.Success
            }, expirationTime: TimeSpan.FromSeconds(6));
            Close();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) {
            //Default Settings
            OutputFolderBox.Text = "N/A";
            TempFolderBox.Text = Utils.WorkingDir + Utils.tempDir;
            DeleteTemp.IsChecked = false;

            //WebP Settings
            NoAlpha.IsChecked = false;
            SaveEXIF.IsChecked = false;
            Lossless.IsChecked = false;
            Quality.Value = 80;
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void TempButton_Click(object sender, RoutedEventArgs e) {

            var dlg = new CommonOpenFileDialog();
            dlg.Title = "Choose Temp Folder";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = Utils.WorkingDir;
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = Utils.WorkingDir;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok) {
                TempFolderBox.Text = dlg.FileName;
            }
            Topmost = true;
            Topmost = false;

            //This is for later. When the Material Design Extensions is updated.
            //OpenFolder folder = new OpenFolder();
            //folder.ShowDialog();
        }
    }
}
