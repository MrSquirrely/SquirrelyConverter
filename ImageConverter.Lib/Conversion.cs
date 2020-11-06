using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using HandyControl.Controls;
using ImageMagick;


namespace ImageConverter.Lib {
    class Conversion {
        private static int _numberToConvert;
        private static int _numberConverted = 0;
        private static ImageTypes _convertToType;
        private static List<Task> _tasks = new List<Task>();
        private static Action<object> _action = (object file) => {
            ImageFile imageFile = (ImageFile)file;
            MagickImage image = new MagickImage($"{imageFile.Location}/{imageFile.Name}{imageFile.Type}");
            switch (_convertToType) {
                case ImageTypes.JPEG:
                    break;
                case ImageTypes.PNG:
                    break;
                case ImageTypes.WEBP:
                    image.Format = MagickFormat.WebP;
                    image.Settings.SetDefine(MagickFormat.WebP, "-lossless", PropertiesModel.Config.WebPLosses);
                    image.Settings.SetDefine(MagickFormat.WebP, "-emulate-jpeg-size", PropertiesModel.Config.WebPEmulateJpeg);
                    image.Settings.SetDefine(MagickFormat.WebP, "-alpha", PropertiesModel.Config.WebPRemoveAlpha);
                    image.Settings.SetDefine(MagickFormat.WebP, "-quality", PropertiesModel.Config.WebPQuality.ToString());
                    image.Write($"{imageFile.Location}/Converted/{imageFile.Name}.webp");
                    image.Dispose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (ImageFile imageFile1 in Reference.ImageCollection) {
                if (imageFile1 == imageFile) {
                    imageFile1.Converted = true;
                }
            }

            Reference.ImageListView.Dispatcher.Invoke(() => {
                Reference.ImageListView.Items.Refresh();
            }, DispatcherPriority.Background);

            _numberConverted++;
            if (_numberConverted == _numberToConvert) {
                Reference.ImageListView.Dispatcher.Invoke(() => {
                    Growl.InfoGlobal("Done Converting!");
                }, DispatcherPriority.Background);
            }
        };
        
        internal static void StartConversion(object convertType) {
            _convertToType = (ImageTypes)convertType;
            _numberToConvert = Reference.ImageCollection.Count;
            foreach (ImageFile imageFile in Reference.ImageCollection) {
                _tasks.Add(new Task(_action, imageFile));
            }

            foreach (Task task in _tasks) {
                task.Start();
            }
        }

    }
}
