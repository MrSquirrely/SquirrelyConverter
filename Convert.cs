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
                        var image = new WebP
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
                        var image = new WebP
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
                        var inputFile = new MediaFile { Filename = file};
                        var outputFile = new MediaFile {
                            Filename = Options.SetCustomOutput
                            ? $"{Options.OutDir}/{Utils.FileName}.webm"
                            : $"{Utils.FileLocation}/{Utils.FileName}.webm"
                        };

                        var engine = new Engine();
                        engine.Convert(inputFile, outputFile);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        private static async void WebMEncodeAsync(string file) {
            //string outPut = Options.SetCustomOutput
            //                ? $"{Options.OutDir}/{Utils.FileName}.webm"
            //                : $"{Utils.FileLocation}/{Utils.FileName}.webm";
            //IConversion conversion = new Conversion();
            //bool conversionResult = await conversion.SetInput(file).SetOutput(outPut).Start();
        }
        #endregion
        #endregion

    }
}
