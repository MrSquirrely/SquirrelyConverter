using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace Mr_Squirrely_Converters.Class
{
    class Converter
    {
        //If alpha true = remove else = set
        internal static void ConvertWebP(List<string> files) {
            foreach (string file in files) {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fileLocation = Path.GetDirectoryName(file);
                MagickImage image = new MagickImage(file);
                image.Settings.SetDefine(MagickFormat.WebP, "lossless", true);
                image.Settings.SetDefine(MagickFormat.WebP, "emulate-jpeg-size", false);
                image.Format = MagickFormat.WebP;
                image.Write($"{fileLocation}\\{fileName}.webp");
            }
        }

        internal static void ConvertJPEG(List<string> files) {
            foreach (string file in files) {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fileLocation = Path.GetDirectoryName(file);
                MagickImage image = new MagickImage(file);
                image.Format = MagickFormat.Jpeg;
                image.Write($"{fileLocation}\\{fileName}.Jpeg");
            }
        }

        internal static void ConvertPNG(List<string> files) {
            foreach (string file in files) {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fileLocation = Path.GetDirectoryName(file);
                MagickImage image = new MagickImage(file);
                image.Format = MagickFormat.Png;
                image.Write($"{fileLocation}\\{fileName}.Png");
            }
        }
    }
}
