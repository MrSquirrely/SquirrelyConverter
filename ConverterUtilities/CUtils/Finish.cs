using System.IO;

namespace ConverterUtilities.CUtils {
    public class Finish {
        public static void Clean(string file) {
            if (Options.GetCreateTemp()) {
                if (!Directory.Exists(CUtilities.GetTempDir(Options.GetCreateTemp(), Options.GetTempLocation()))) {
                    Directory.CreateDirectory(CUtilities.GetTempDir(Options.GetCreateTemp(), Options.GetTempLocation()));
                }
                File.Copy(file, CUtilities.GetTempDir(Options.GetCreateTemp(), Options.GetTempLocation()));

            }
            File.Delete(file);
            if (Options.GetDeleteTemp() && Directory.Exists(Options.GetTempLocation())) {
                Directory.Delete(Options.GetTempLocation());
            }
        }
    }
}
