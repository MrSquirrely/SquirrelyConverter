namespace SquirrelyConverter
{
    internal class Options
    {
        //Default
        public static string TempDir { get; set; }
        public static string OutDir { get; set; }
        public static bool DeleteTemp { get; set; }
        public static bool SetCustomOutput { get; set; }
        public static bool ChangeTemp { get; set; }
        //WeBP
        public static double WebPQuality { get; set; }
        public static bool WebPLossless { get; set; }
        public static bool WebPNoAlpha { get; set; }
        public static bool WebPCopyMeta { get; set; }

        public static void FirstRun() {
            TempDir = Properties.Settings.Default.TempDir;
            OutDir = Properties.Settings.Default.OutDir;
            DeleteTemp = Properties.Settings.Default.DeleteTempDir;
            SetCustomOutput = Properties.Settings.Default.SetCustomOutput;
            ChangeTemp = Properties.Settings.Default.ChangeTemp;
            WebPQuality = Properties.Settings.Default.WebPQuality;
            WebPLossless = Properties.Settings.Default.WebPLossless;
            WebPNoAlpha = Properties.Settings.Default.WebPNoAlpha;
            WebPCopyMeta = Properties.Settings.Default.WebPCopyMeta;
        }

        public static void Save() {
            Properties.Settings.Default.TempDir = TempDir;
            Properties.Settings.Default.OutDir = OutDir;
            Properties.Settings.Default.SetCustomOutput = SetCustomOutput;
            Properties.Settings.Default.DeleteTempDir = DeleteTemp;
            Properties.Settings.Default.ChangeTemp = ChangeTemp;
            Properties.Settings.Default.WebPQuality = WebPQuality;
            Properties.Settings.Default.WebPLossless = WebPLossless;
            Properties.Settings.Default.WebPNoAlpha = WebPNoAlpha;
            Properties.Settings.Default.WebPCopyMeta = WebPCopyMeta;
            Properties.Settings.Default.Save();
        }
    }
}
