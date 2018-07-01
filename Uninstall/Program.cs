using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uninstall {
    class Program {
        static void Main(string[] args) {
            List<string> files = new List<string>() {
                "ControlzEx.dll",
                "ConverterUtilities.dll",
                "ConverterUtilities.dll.config",
                "Dragablz.dll",
                "Dragablz.xml",
                "ffmpeg.exe",
                "gif2webp.exe",
                "icon.ico",
                "ImageConverter.dll",
                "ImageConverter.dll.config",
                "Magick.NET-Q8-AnyCPU.dll",
                "Magick.NET-Q8-AnyCPU.xml",
                "MahApps.Metro.dll",
                "MahApps.Metro.xml",
                "MaterialDesignColors.dll",
                "MaterialDesignThemes.MahApps.dll",
                "MaterialDesignThemes.Wpf.dll",
                "MaterialDesignThemes.Wpf.xml",
                "MediaToolkit.dll",
                "MrSquirrelysConverters.exe",
                "MrSquirrelysConverters.exe.config",
                "NLog.dll",
                "NLog.xml",
                "System.Windows.Interactivity.dll",
                "ToastNotifications.dll",
                "ToastNotifications.Messages.dll",
                "VideoConverter.dll",
                "VideoConverter.dll.config"
            };

            foreach (string file in files) {
                File.Delete($"{Directory.GetCurrentDirectory()}\\{file}");
                Console.WriteLine($"Deleted: {file}");
            }

            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            File.Delete($"{desktop}\\Squirrely Converters.lnk");
#if (DEBUG)
            Console.ReadKey();
#endif
        }
        
    }
}
