namespace WebPConverter.Class {
    internal class Options
    {
        //Default
        public static string TempDir { get; set; }
        public static string OutDir { get; set; }
        public static bool DeleteTemp { get; set; }
        public static bool SetCustomOutput { get; set; }
        public static bool ChangeTemp { get; set; }
        //WeBP
        public static int Quality { get; set; }
        public static bool Lossless { get; set; }
        public static bool NoAlpha { get; set; }
        public static bool CopyMeta { get; set; }

        public static void FirstRun() {
            TempDir = Properties.Settings.Default.TempDir;
            OutDir = Properties.Settings.Default.OutDir;
            DeleteTemp = Properties.Settings.Default.DeleteTempDir;
            SetCustomOutput = Properties.Settings.Default.SetCustomOutput;
            ChangeTemp = Properties.Settings.Default.ChangeTemp;
            Quality = Properties.Settings.Default.Quality;
            Lossless = Properties.Settings.Default.Lossless;
            NoAlpha = Properties.Settings.Default.NoAlpha;
            CopyMeta = Properties.Settings.Default.CopyMeta;
        }

        public static void Save() {
            Properties.Settings.Default.TempDir = TempDir;
            Properties.Settings.Default.OutDir = OutDir;
            Properties.Settings.Default.SetCustomOutput = SetCustomOutput;
            Properties.Settings.Default.DeleteTempDir = DeleteTemp;
            Properties.Settings.Default.ChangeTemp = ChangeTemp;
            Properties.Settings.Default.Quality = Quality;
            Properties.Settings.Default.Lossless = Lossless;
            Properties.Settings.Default.NoAlpha = NoAlpha;
            Properties.Settings.Default.CopyMeta = CopyMeta;
            Properties.Settings.Default.Save();
        }
    }
}
