using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;

namespace Uninstall {
    internal static class Program {
        internal static void Main() {
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
            
            ProcessStartInfo startInfo = new ProcessStartInfo("cmd.exe", "/C ping 1.1.1.1 -n 1 -w 1000 > Nul & Del " + $"{Directory.GetCurrentDirectory()}\\Uninstall.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            Process.Start(startInfo);
            
            Environment.Exit(0);
#if (DEBUG)
            Console.ReadKey();
#endif
        }
        
    }
}
