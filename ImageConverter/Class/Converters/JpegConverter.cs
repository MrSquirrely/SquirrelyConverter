using System;
using System.Globalization;
using System.Threading;
using ConverterUtilities;
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
            MagickImage.Settings.SetDefine(Jpeg,"-quality", Options.GetJpegQuality().ToString(CultureInfo.InvariantCulture));
            MagickImage.Format = Jpeg;
            MagickImage.Write($"{CUtilities.GetFileDirectory(Image)}\\{CUtilities.GetFileName(Image, Enums.FileExtension.No)}.jpeg");

            foreach (NewFile newFile in ImageUtilities.ImagesCollection) {
                int index = ImageUtilities.ImagesCollection.IndexOf(newFile);
                if (ImageUtilities.ImagesCollection[index].Location == CUtilities.GetFileLocation(Image)) {
                    ImageUtilities.ImagesCollection[index].Converted = "Converted";
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
