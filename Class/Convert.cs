using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using NReco.VideoConverter;

namespace Mr_Squirrely_Converters.Class
{
    class Converter
    {

        #region Video Converters
        internal static void ConvertWebM(List<string> files) {
            try {

                // Currently disabled
                //FFMpegConverter video = new FFMpegConverter();
                //foreach (string file in files) {
                //    string fileName = Path.GetFileNameWithoutExtension(file);
                //    string fileLocation = Path.GetDirectoryName(file);
                //    string fileType = Path.GetExtension(file);
                //    video.ConvertMedia(file, $"{fileLocation}\\{fileName}.webm", Format.webm);
                //}
                
            }
            catch (Exception) {

            }
        }

        internal static void ConvertMP4(List<string> files) {

        }
        #endregion

        #region Image Converters
        //If alpha true = remove else = set
        internal static void ConvertWebP(List<string> files) {
            foreach (string file in files) {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fullName = Path.GetFileName(file);
                string fileLocation = Path.GetDirectoryName(file);
                string fullLocation = Path.GetFullPath(file);
                MagickImage image = new MagickImage(file);
                image.Settings.SetDefine(MagickFormat.WebP, "-lossless", Options.WebPLossless);
                image.Settings.SetDefine(MagickFormat.WebP, "-emulate-jpeg-size", Options.WebPEmulateJPEG);
                image.Settings.SetDefine(MagickFormat.WebP, "-alpha", Options.GetWebPRemoveAlpha());
                image.Settings.SetDefine(MagickFormat.WebP, "-quality", Options.WebPQuality.ToString());
                image.Format = MagickFormat.WebP;
                image.Write($"{fileLocation}\\{fileName}.webp");
                //I need help on this. There has to be a better way to do this?!
                foreach (NewFile newFile in Utils._Images) {
                    int index = Utils._Images.IndexOf(newFile);
                    if (Utils._Images[index].Location == fullLocation) {
                        Utils._Images[index].Converted = "Converted";
                    }
                }
                Utils._MainPage.Dispatcher.Invoke(() => { Utils._MainPage.ImageFiles.Items.Refresh(); }, System.Windows.Threading.DispatcherPriority.Background);
                if (Options.CreateTemp) {
                    if (!Directory.Exists($"{Utils._WorkingDir}\\image_temp")) Directory.CreateDirectory($"{Utils._WorkingDir}\\image_temp");
                    File.Copy(file, $"{Utils._WorkingDir}\\image_temp\\{fullName}");
                }
                DeleteFile(file);
            }
        }

        internal static void ConvertJPEG(List<string> files) {
            foreach (string file in files) {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fullName = Path.GetFileName(file);
                string fileLocation = Path.GetDirectoryName(file);
                string fullLocation = Path.GetFullPath(file);
                MagickImage image = new MagickImage(file);
                image.Settings.SetDefine(MagickFormat.Jpeg, "-quality", Options.WebPQuality.ToString());
                image.Format = MagickFormat.Jpeg;
                image.Write($"{fileLocation}\\{fileName}.Jpeg");
                //I need help on this. There has to be a better way to do this?!
                foreach (NewFile newFile in Utils._Images) {
                    int index = Utils._Images.IndexOf(newFile);
                    if (Utils._Images[index].Location == fullLocation) {
                        Utils._Images[index].Converted = "Converted";
                    }
                }
                Utils._MainPage.Dispatcher.Invoke(() => { Utils._MainPage.ImageFiles.Items.Refresh(); }, System.Windows.Threading.DispatcherPriority.Background);
                if (Options.CreateTemp) {
                    if (!Directory.Exists($"{Utils._WorkingDir}\\image_temp")) Directory.CreateDirectory($"{Utils._WorkingDir}\\image_temp");
                    File.Copy(file, $"{Utils._WorkingDir}\\image_temp\\{fullName}");
                }
                DeleteFile(file);
            }
        }

        internal static void ConvertPNG(List<string> files) {
            foreach (string file in files) {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fullName = Path.GetFileName(file);
                string fileLocation = Path.GetDirectoryName(file);
                string fullLocation = Path.GetFullPath(file);
                MagickImage image = new MagickImage(file);
                image.Settings.SetDefine(MagickFormat.Png, "-lossless", Options.WebPLossless);
                image.Settings.SetDefine(MagickFormat.Png, "-alpha", Options.GetWebPRemoveAlpha());
                image.Settings.SetDefine(MagickFormat.Png, "-quality", Options.WebPQuality.ToString());
                image.Format = MagickFormat.Png;
                image.Write($"{fileLocation}\\{fileName}.Png");
                //I need help on this. There has to be a better way to do this?!
                foreach (NewFile newFile in Utils._Images) {
                    int index = Utils._Images.IndexOf(newFile);
                    if (Utils._Images[index].Location == fullLocation) {
                        Utils._Images[index].Converted = "Converted";
                    }
                }
                Utils._MainPage.Dispatcher.Invoke(() => { Utils._MainPage.ImageFiles.Items.Refresh(); }, System.Windows.Threading.DispatcherPriority.Background);
                if (Options.CreateTemp) {
                    if (!Directory.Exists($"{Utils._WorkingDir}\\image_temp")) Directory.CreateDirectory($"{Utils._WorkingDir}\\image_temp");
                    File.Copy(file, $"{Utils._WorkingDir}\\image_temp\\{fullName}");
                }
                DeleteFile(file);
            }
        }
        #endregion

        private static void DeleteFile(string file) {
            if (Options.ImagesDelete) File.Delete(file);
        }

    }
}
