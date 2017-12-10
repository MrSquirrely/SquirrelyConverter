using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.IO;

namespace SquirrelyConverter
{
    internal class Convert
    {
        #region WebP
        #region Encode
        public static void WebPEncode() {
            try {
                foreach (var file in Utils.Files) {
                    Utils.FileName = Path.GetFileNameWithoutExtension(file);
                    Utils.FileType = Path.GetExtension(file)?.ToLower();
                    Utils.FileLocation = Path.GetDirectoryName(file);

                    if (Utils.FileType == ".gif") {
                        WebP image = new WebP
                        {
                            WebPImage = file,
                            WebPOutput = Options.SetCustomOutput
                                ? $"{Options.OutDir}/{Utils.FileName}.webp"
                                : $"{Utils.FileLocation}/{Utils.FileName}.webp"
                        };
                        image.EnocdeGif();
                        Utils.FileNum++;
                        File.Delete(file);
                    }
                    else if(Types.WebPTypes.Contains(Utils.FileType) && Utils.FileType != ".gif"){
                        WebP image = new WebP
                        {
                            WebPImage = file,
                            WebPQuality = Options.WebPQuality,
                            WebPCopyMeta = Options.WebPCopyMeta,
                            WebPNoAlpha = Options.WebPNoAlpha,
                            WebPLossless = Options.WebPLossless,
                            WebPOutput = Options.SetCustomOutput
                                ? $"{Options.OutDir}/{Utils.FileName}.webp"
                                : $"{Utils.FileLocation}/{Utils.FileName}.webp"
                        };
                        image.Encode();
                        Utils.FileNum++;
                        File.Delete(file);
                    }
                    else if (Types.WebMTypes.Contains(Utils.FileType)) {
                        WebM video = new WebM {
                            WebMInput = file,
                            WebMOutput = Options.SetCustomOutput
                            ? $"{Options.OutDir}/{Utils.FileName}.webm"
                            : $"{Utils.FileLocation}/{Utils.FileName}.webm"
                        };
                        video.Encode();
                        File.Delete(file);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        #endregion

    }
}
