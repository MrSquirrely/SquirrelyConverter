using Image_Converter.Code;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Image_Converter.Views {
    /// <summary>
    /// Interaction logic for SettingsContent.xaml
    /// </summary>
    public partial class SettingsContent : UserControl {
        public SettingsContent() {
            InitializeComponent();
            //! General Settings
            GeneralBackup.IsChecked = Properties.Settings.Default.General_Backup;
            GeneralUseCustomFolder.IsChecked = Properties.Settings.Default.General_CustomBackupFolder;
            GeneralCustomFolder.Text = Properties.Settings.Default.General_BackupFolder;
            GeneralPlaySound.IsChecked = Properties.Settings.Default.General_PlaySound;
            //!WebP Settings
            WebPLosses.IsChecked = Properties.Settings.Default.WebP_Lossless;
            WebPEmulateJpeg.IsChecked = Properties.Settings.Default.WebP_EmulateJpeg;
            WebPRemoveAlpha.IsChecked = Properties.Settings.Default.WebP_RemoveAlpha;
            WebPQuality.Value = Properties.Settings.Default.WebP_Quality;
            //!Jpeg Settings
            JpegQuality.Value = Properties.Settings.Default.Jpeg_Quality;
            //!Png Settings
            PngLossess.IsChecked = Properties.Settings.Default.Png_Lossless;
            PngRemoveAlpha.IsChecked = Properties.Settings.Default.Png_RemoveAlpha;
            PngQuality.Value = Properties.Settings.Default.Png_Quality;
        }

        private void GernalBrowseButton_Click(object sender, RoutedEventArgs e) {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog {
                InitialDirectory = Environment.CurrentDirectory,
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok) {
                GeneralCustomFolder.Text = dialog.FileName;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            //! General Settings
            Properties.Settings.Default.General_Backup = GeneralBackup.IsChecked.Value;
            Properties.Settings.Default.General_CustomBackupFolder = GeneralUseCustomFolder.IsChecked.Value;
            Properties.Settings.Default.General_BackupFolder = GeneralCustomFolder.Text;
            Properties.Settings.Default.General_PlaySound = GeneralPlaySound.IsChecked.Value;
            //! WebP Settings
            Properties.Settings.Default.WebP_Lossless = WebPLosses.IsChecked.Value;
            Properties.Settings.Default.WebP_EmulateJpeg = WebPEmulateJpeg.IsChecked.Value;
            Properties.Settings.Default.WebP_RemoveAlpha = WebPRemoveAlpha.IsChecked.Value;
            Properties.Settings.Default.WebP_Quality = (int)WebPQuality.Value;
            //! Jpeg Settings
            Properties.Settings.Default.Jpeg_Quality = (int)JpegQuality.Value;
            //! Png Settings
            Properties.Settings.Default.Png_Lossless = PngLossess.IsChecked.Value;
            Properties.Settings.Default.Png_RemoveAlpha = PngRemoveAlpha.IsChecked.Value;
            Properties.Settings.Default.Png_Quality = (int)PngQuality.Value;
            //! Save the settings
            Properties.Settings.Default.Save();

            Utilities.flyout.IsOpen = false;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e) {
            GeneralBackup.IsChecked = false;
            GeneralUseCustomFolder.IsChecked = false;
            GeneralCustomFolder.Text = "BACKUP";
            GeneralPlaySound.IsChecked = false;
            //!WebP Settings
            WebPLosses.IsChecked = true;
            WebPEmulateJpeg.IsChecked = false;
            WebPRemoveAlpha.IsChecked = false;
            WebPQuality.Value = 80;
            //!Jpeg Settings
            JpegQuality.Value = 80;
            //!Png Settings
            PngLossess.IsChecked = true;
            PngRemoveAlpha.IsChecked = false;
            PngQuality.Value = 80;
        }
    }
}
