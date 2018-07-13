using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ConverterUtilities;
using ConverterUtilities.Interfaces;

namespace ImageConverter.Class.Converters {
    class WebPGifConverter : IConverter {
        public string Image { get; set; }
        public Thread Thread { get; set; }
        public ThreadStart ThreadStart { get; set; }

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
            Process process = new Process() {
                StartInfo = {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            process.Start();
            process.StandardInput.WriteLine($"cd {Directory.GetCurrentDirectory()}");
            process.StandardInput.WriteLine($"gif2webp {Options.GetWebPQuality()} \"{Image}\" -o \"{CUtilities.GetFileDirectory(Image)}\\{CUtilities.GetFileName(Image, Enums.FileExtension.No)}.webp\"");
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();

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
