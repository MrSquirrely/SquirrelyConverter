using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ConverterUtilities {
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
