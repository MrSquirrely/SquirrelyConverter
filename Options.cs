using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelyConverter
{
    class Options
    {
        public static string TempDir { get; set; }
        public static string OutDir { get; set; }
        public static bool DeleteTemp { get; set; }
        public static double WebPQuality { get; set; }
        public static bool WebPLossless { get; set; }
        public static bool WebPNoAlpha { get; set; }
        public static bool WebPCopyMeta { get; set; }

        public static void FirstRun() {
            TempDir = Properties.Settings.Default.TempDir;
            OutDir = Properties.Settings.Default.OutDir;
            DeleteTemp = Properties.Settings.Default.DeleteTempDir;
            WebPQuality = Properties.Settings.Default.WebPQuality;
            WebPLossless = Properties.Settings.Default.WebPLossless;
            WebPNoAlpha = Properties.Settings.Default.WebPNoAlpha;
            WebPCopyMeta = Properties.Settings.Default.WebPCopyMeta;
        }

        public static void Save() {
            Properties.Settings.Default.TempDir = TempDir;
            //Properties.Settings.Default.OutDir = OutDir; //Until I get this working it'll stay here.
            Properties.Settings.Default.DeleteTempDir = DeleteTemp;
            Properties.Settings.Default.WebPQuality = WebPQuality;
            Properties.Settings.Default.WebPLossless = WebPLossless;
            Properties.Settings.Default.WebPNoAlpha = WebPNoAlpha;
            Properties.Settings.Default.WebPCopyMeta = WebPCopyMeta;
        }
    }
}
