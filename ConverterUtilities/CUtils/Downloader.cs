using System.IO;
using System.Net;
using ConverterUtilities.Configs;

namespace ConverterUtilities.CUtils {
    class Downloader {

        private static WebClient _webClient;

        public static void Download(ConverterInfo converter) {
            string converterDirectory = $"{DirectoryInfo.WorkingDirectory}\\Converters\\{converter.ConverterName}\\";
            if (!File.Exists($"{converterDirectory}{converter.ConverterName}.converter")) {
                _webClient = new WebClient();
                _webClient.DownloadFile($"{converterDirectory}{converter.ConverterName}.converter", converter.ConverterDownloadUrl);
            }
            else {
                //Todo: Throw a message that it already exists

            }
        }
    }
}
