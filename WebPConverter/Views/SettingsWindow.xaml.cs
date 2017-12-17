using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using WebPConverter.Class;

namespace WebPConverter.Views {
    public partial class SettingsWindow
    {

        public SettingsWindow() {
            InitializeComponent();
            TempFolderBox.Text = $"{Utils.WorkingDir}/{Utils.TempDir}";
            DeleteTemp.IsChecked = Options.DeleteTemp;
            ChangeOutput.IsChecked = Options.SetCustomOutput;
            if (Options.SetCustomOutput) {
                OutputFolderBox.Text = Options.OutDir;
                OutputFolderBox.IsEnabled = true;
                OutputButton.IsEnabled = true;
            }
            if (Options.ChangeTemp) {
                TempFolderBox.Text = Options.TempDir;
                TempFolderBox.IsEnabled = true;
                TempButton.IsEnabled = true;
            }
            ChangeTemp.IsChecked = Options.ChangeTemp;
            Quality.Value = Options.Quality;
            Lossless.IsChecked = Options.Lossless;
            NoAlpha.IsChecked = Options.NoAlpha;
            SaveExif.IsChecked = Options.CopyMeta;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            Options.TempDir = TempFolderBox.Text;
            Options.OutDir = OutputFolderBox.Text;
            Options.DeleteTemp = DeleteTemp.IsChecked.GetValueOrDefault();
            Options.SetCustomOutput = ChangeOutput.IsChecked.GetValueOrDefault();
            Options.ChangeTemp = ChangeTemp.IsChecked.GetValueOrDefault();
            Options.Quality = (int)Quality.Value;
            Options.Lossless = Lossless.IsChecked.GetValueOrDefault();
            Options.NoAlpha = NoAlpha.IsChecked.GetValueOrDefault();
            Options.CopyMeta = SaveExif.IsChecked.GetValueOrDefault();
            Options.Save();
            Utils.ShowToast(Toast.SettingsSaved);
            Close();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) {
            //Default Settings
            OutputFolderBox.Text = "";
            TempFolderBox.Text = $"{Utils.WorkingDir}/{Utils.TempDir}";
            DeleteTemp.IsChecked = false;
            ChangeOutput.IsChecked = false;
            OutputFolderBox.IsEnabled = false;
            OutputButton.IsEnabled = false;


            //WebP Settings
            NoAlpha.IsChecked = false;
            SaveExif.IsChecked = false;
            Lossless.IsChecked = false;
            Quality.Value = 80;
            
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) {
            Close();
        }

        private void OpenFolder(object sender, RoutedEventArgs e) {

            var dlg = new CommonOpenFileDialog
            {
                Title = "Choose Folder",
                IsFolderPicker = true,
                InitialDirectory = Utils.WorkingDir,
                AddToMostRecentlyUsedList = false,
                AllowNonFileSystemItems = false,
                DefaultDirectory = Utils.WorkingDir,
                EnsureFileExists = true,
                EnsurePathExists = true,
                EnsureReadOnly = false,
                EnsureValidNames = true,
                Multiselect = false,
                ShowPlacesList = true
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok) {
                if(Equals(sender, TempButton)) TempFolderBox.Text = dlg.FileName;
                if (Equals(sender, OutputButton)) OutputFolderBox.Text = dlg.FileName;
            }
            Topmost = true;
            Topmost = false;
        }

        private void ChangeOutput_Click(object sender, RoutedEventArgs e) {
            if (ChangeOutput.IsChecked == true) { OutputFolderBox.IsEnabled = true; OutputButton.IsEnabled = true; }
            else if (ChangeOutput.IsChecked != true) { OutputFolderBox.IsEnabled = false; OutputButton.IsEnabled = false; }
        }

        private void ChangeTemp_Click(object sender, RoutedEventArgs e) {
            if (ChangeTemp.IsChecked == true) { OutputFolderBox.IsEnabled = true; ChangeTemp.IsEnabled = true; }
            else if (ChangeTemp.IsChecked != true) { OutputFolderBox.IsEnabled = false; ChangeTemp.IsEnabled = false; }
        }
    }
}
