using System;
using System.Globalization;
using System.Threading;
using ConverterUtilities;
using ConverterUtilities.Configs;
using ConverterUtilities.CUtils;
using ConverterUtilities.Interfaces;
using ImageMagick;

namespace ImageConverter.Class.Converters {
    class PngConverter : IConverter {
        public string Image { get; set; }
        public Thread Thread { get; set; }
        public ThreadStart ThreadStart { get; set; }
        public MagickImage MagickImage;
        public MagickFormat Png = MagickFormat.Png;

        public void StartConvert() {
            ThreadStart = Convert;
            ThreadStart += () => {
                Utilities.RemoveThread();
                if (Utilities.GetThreads() == 0) {
                    Toast.ConvertFinished();
                    Utilities.SetConverting(false);
                }
            };
            Thread = new Thread(ThreadStart);
            Thread.Start();
        }

        public void Convert() {
            FileInfos infos = new FileInfos(Image);
            MagickImage = new MagickImage(Image);
            MagickImage.Settings.SetDefine(Png, "-lossless", Options.GetPngLossless());
            MagickImage.Settings.SetDefine(Png, "-alpha", Options.GetPngRemoveAlpha());
            MagickImage.Settings.SetDefine(Png, "-quality", Options.GetPngQuality().ToString(CultureInfo.InvariantCulture));
            MagickImage.Format = Png;
            MagickImage.Write($"{infos.FileDirectory()}\\{infos.FileNameWithoutExtension()}.png");

            foreach (NewFile newFile in ImageUtilities.ImagesCollection) {
                int index = ImageUtilities.ImagesCollection.IndexOf(newFile);
                if (ImageUtilities.ImagesCollection[index].Location == infos.FileDirectory()) {
                    ImageUtilities.ImagesCollection[index].Converted = "Converted";
                    UpdateView();
                }
            }
            Finish();
        }

        public void Finish() => ConverterUtilities.CUtils.Finish.Clean(Image);

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
