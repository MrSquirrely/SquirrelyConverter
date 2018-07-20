using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ConverterUtilities;
using ConverterUtilities.Configs;
using ConverterUtilities.CUtils;
using ConverterUtilities.Interfaces;

namespace ImageConverter.Class.Converters {
    class WebPGifConverter : IConverter {
        public string Image { get; set; }
        public Thread Thread { get; set; }
        public ThreadStart ThreadStart { get; set; }

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
            Process process = new Process() {
                StartInfo = {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            Logger.LogError(" ");
            Logger.LogError(" ");
            Logger.LogError(" ");
            Logger.LogError(" ");
            Logger.LogError($"cd {DirectoryInfos.WorkingDirectory}\\Converters\\Image Converter");
            Logger.LogError(" ");
            Logger.LogError(" ");
            Logger.LogError(" ");
            Logger.LogError(" ");
            Logger.LogError($"gif2webp.exe {Options.GetWebPQuality()} \"{Image}\" -o \"{infos.FileDirectory()}\\{infos.FileNameWithoutExtension()}.webp\"");

            process.Start();
            process.StandardInput.WriteLine($"cd {DirectoryInfos.WorkingDirectory}\\Converters\\Image Converter");
            process.StandardInput.WriteLine($"gif2webp.exe {Options.GetWebPQuality()} \"{Image}\" -o \"{infos.FileDirectory()}\\{infos.FileNameWithoutExtension()}.webp\"");
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.WaitForExit();

            foreach (NewFile newFile in ImageUtilities.ImagesCollection) {
                        
                int index = ImageUtilities.ImagesCollection.IndexOf(newFile);

                if (ImageUtilities.ImagesCollection[index].Location == infos.FileDirectory()) {
                    ImageUtilities.ImagesCollection[index].Converted = "Converted";
                    Console.WriteLine(newFile.Converted);
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
