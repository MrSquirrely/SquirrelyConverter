using System;
using System.Globalization;
using System.Threading;
using ConverterUtilities;
using ConverterUtilities.Configs;
using ConverterUtilities.CUtils;
using ConverterUtilities.Interfaces;
using ImageMagick;

namespace ImageConverter.Class.Converters {
    class JpegConverter : IConverter{
        public string Image { get; set; }
        public Thread Thread { get; set; }
        public ThreadStart ThreadStart { get; set; }
        public MagickImage MagickImage;
        public MagickFormat Jpeg = MagickFormat.Jpeg;
        
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
            MagickImage.Settings.SetDefine(Jpeg,"-quality", Options.GetJpegQuality().ToString(CultureInfo.InvariantCulture));
            MagickImage.Format = Jpeg;
            MagickImage.Write($"{infos.FileDirectory()}\\{infos.FileNameWithoutExtension()}.jpeg");

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
