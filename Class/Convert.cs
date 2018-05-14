using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;

namespace Mr_Squirrely_Converters.Class
{
    class Convert
    {

        public void ConvertWebP(List<string> files) {
            foreach (string file in files) {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fileLocation = Path.GetDirectoryName(file);
                MagickImage image = new MagickImage(file);
                image.Format = MagickFormat.WebP;
                image.Write($"{fileLocation}\\{fileName}.webp");
            }
        }

        internal void ConvertJPEG(List<string> files) {
            foreach (string file in files) {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string fileLocation = Path.GetDirectoryName(file);
                MagickImage image = new MagickImage(file);
                image.Format = MagickFormat.Jpeg;
                image.Write($"{fileLocation}\\{fileName}.Jpeg");
            }
        }

        internal void ConvertPNG(List<string> files) {
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
