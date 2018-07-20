using System.IO;

namespace ConverterUtilities.CUtils {
    public class Finish {
        public static void Clean(string file) {
            if (Options.GetCreateTemp()) {
                if (!Directory.Exists(DirectoryInfos.GetTempDir(Options.GetCreateTemp(), Options.GetTempLocation()))) {
                    Directory.CreateDirectory(DirectoryInfos.GetTempDir(Options.GetCreateTemp(), Options.GetTempLocation()));
                }
                File.Copy(file, DirectoryInfos.GetTempDir(Options.GetCreateTemp(), Options.GetTempLocation()));

            }
            File.Delete(file);
            if (Options.GetDeleteTemp() && Directory.Exists(Options.GetTempLocation())) {
                Directory.Delete(Options.GetTempLocation());
            }
        }
    }
}
