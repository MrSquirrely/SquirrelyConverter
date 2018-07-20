using System.IO;
using System.Net;
using ConverterUtilities.Configs;
using Newtonsoft.Json;

namespace ConverterUtilities.CUtils {
    public class Downloader {

        private static WebClient _webClient;
        private const string DownloadUrl = "https://raw.githubusercontent.com/MrSquirrelyNet/ConverterRepo/master/";

        public static void Download(string downloadUrl, string converterName) {
            string converterDirectory = $"{DirectoryInfos.WorkingDirectory}\\Converters\\{converterName}\\";
            if (!File.Exists($"{converterDirectory}{converterName}.converter")) {
                _webClient = new WebClient();
                _webClient.DownloadFile(downloadUrl, $"{converterDirectory}{converterName}.converter");
            }
            else {
                //Todo: Throw a message that it already exists

            }
        }

        public static void DownloadRepo() {
            //_webClient = new WebClient();
            //_webClient.DownloadFile("https://raw.githubusercontent.com/MrSquirrelyNet/ConverterRepo/master/Converters.list", "Converters.list");

            //StreamReader reader = new StreamReader($"{DirectoryInfos.WorkingDirectory}\\Converters.list");
            //JsonSerializer serializer = new JsonSerializer();
            //Utilities.SetList((ConverterList) serializer.Deserialize(reader, typeof(ConverterList)));

            //foreach (string listConverter in Utilities.GetList().Converters) {
            //    _webClient.DownloadFile($"{DownloadUrl}{listConverter}", $"{DirectoryInfos.WorkingDirectory}\\{listConverter}");
            //    StreamReader streamReader = new StreamReader($"{DirectoryInfos.WorkingDirectory}\\{listConverter}");
            //    Utilities.SetConverterInfos((ConverterInfo) serializer.Deserialize(streamReader, typeof(ConverterInfo)));
            //}
        }
    }
}
