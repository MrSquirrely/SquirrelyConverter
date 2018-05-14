using System;
using System.IO;

namespace WebPConverter.Class {
    internal class Convert {
        public static void StartEncode() {
            try {
                foreach (var file in Utils.Files) {
                    int fileNum = 0;
                    Utils.FileName = Path.GetFileNameWithoutExtension(file);
                    Utils.FileType = Path.GetExtension(file)?.ToLower();
                    Utils.FileLocation = Path.GetDirectoryName(file);

                    for (int i = 0; i < Utils.NFiles.Count; i++) {
                        if (Utils.NFiles[i].Name == Utils.FileName) {
                            Utils.NFiles[i].Converted = "Converting";
                            try {
                                Utils.UpdateView();
                            }
                            catch (Exception e) {
                                Console.WriteLine(e.Message);
                            }
                            fileNum = i;
                        }
                    }

                    if (Utils.FileType == ".gif") {
                        WebP image = new WebP {
                            Input = file,
                            Output = Options.SetCustomOutput
                                ? $"{Options.OutDir}/{Utils.FileName}.webp"
                                : $"{Utils.FileLocation}/{Utils.FileName}.webp"
                        };
                        image.EnocdeGif();
                        Utils.FileNum++;
                        File.Delete(file);
                    }
                    else if (Types.WebP.Contains(Utils.FileType) && Utils.FileType != ".gif") {
                        WebP image = new WebP {
                            Input = file,
                            Quality = Options.Quality,
                            CopyMeta = Options.CopyMeta,
                            NoAlpha = Options.NoAlpha,
                            Lossless = Options.Lossless,
                            MultiThreading = Utils.MultiCore,
                            Output = Options.SetCustomOutput
                                ? $"{Options.OutDir}/{Utils.FileName}.webp"
                                : $"{Utils.FileLocation}/{Utils.FileName}.webp"
                        };
                        image.Encode();
                        Utils.FileNum++;
                        File.Delete(file);
                    }
                    try {
                        Utils.NFiles[fileNum].Converted = "converted";
                        Utils.UpdateView();
                    }
                    catch (Exception e) {
                        Utils.LogMessage(e);
                    }
                }
            }
            catch (Exception e) {
                Utils.LogMessage(e);
            }
        }
    }
}
