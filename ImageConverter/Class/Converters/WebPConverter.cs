using System;
using System.Globalization;
using System.Threading;
using ConverterUtilities;
using ConverterUtilities.Interfaces;
using ImageMagick;

namespace ImageConverter.Class.Converters {
    class WebPConverter : IConverter {
        public string Image { get; set; }
        public Thread Thread { get; set; }
        public ThreadStart ThreadStart { get; set; }
        public MagickImage MagickImage;
        public MagickFormat WebP = MagickFormat.WebP;

        public void StartConvert() {
            ThreadStart = Convert;
            ThreadStart += () => {
                CUtilities.Threads--;
                if (CUtilities.Threads == 0) {
                    Toast.ConvertFinished();
                    CUtilities.Converting  = false;
                }
            };
            Thread = new Thread(ThreadStart);
            Thread.Start();
        }

        public void Convert() {
            MagickImage = new MagickImage(Image);
            MagickImage.Settings.SetDefine(WebP, "-lossless", Options.GetWebPLossless());
            MagickImage.Settings.SetDefine(WebP, "-emulate-jpeg-size", Options.GetWebPEmulateJpeg());
            MagickImage.Settings.SetDefine(WebP, "-alpha", Options.GetWebPRemoveAlpha());
            MagickImage.Settings.SetDefine(WebP, "-quality", Options.GetWebPQuality().ToString(CultureInfo.InvariantCulture));
            MagickImage.Format = MagickFormat.WebP;
            MagickImage.Write($"{CUtilities.GetFileDirectory(Image)}\\{CUtilities.GetFileName(Image, Enums.FileExtension.No)}.webp");

            foreach (NewFile newFile in ImageUtilities.ImagesCollection) {
                        
                int index = ImageUtilities.ImagesCollection.IndexOf(newFile);
                Console.WriteLine(ImageUtilities.ImagesCollection[index].Location);
                Console.WriteLine(newFile.Location);

                if (ImageUtilities.ImagesCollection[index].Location == CUtilities.GetFileLocation(Image)) {
                    ImageUtilities.ImagesCollection[index].Converted = "Converted";
                    Console.WriteLine(newFile.Converted);
                    UpdateView();
                }
            }
            Finish();
        }

        public void Finish() => ConverterUtilities.Finish.Clean(Image);

        public void UpdateView() {
            try {
                ImageUtilities.ImageView.Dispatcher.Invoke(() => {
                    ImageUtilities.ImageListView.Items.Refresh();
                },
                    System.Windows.Threading.DispatcherPriority.Background);
            } catch (Exception ex) {
                Logger.LogDebug(ex);
            }
        }
    }
}
