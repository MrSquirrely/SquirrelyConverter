namespace ImageConverter {
    internal class Options {
        // Todo: change things to exspressive
        internal static bool CreateTemp {
            get => Properties.Settings.Default.Create_Temp;
            set => Properties.Settings.Default.Create_Temp = value;
        }
        internal static string TempLocation {
            get => Properties.Settings.Default.Temp_Location;
            set => Properties.Settings.Default.Temp_Location = value;
        }
        internal static bool ImagesDelete {
            get => Properties.Settings.Default.Images_Delete;
            set => Properties.Settings.Default.Images_Delete = value;
        }
        internal static bool WebPLossless {
            get => Properties.Settings.Default.WebP_Lossess;
            set => Properties.Settings.Default.WebP_Lossess = value;
        }
        internal static bool WebPEmulateJpeg {
            get => Properties.Settings.Default.WebP_Emulate_JPEG;
            set => Properties.Settings.Default.WebP_Emulate_JPEG = value;
        }
        internal static double WebPQuality {
            get => Properties.Settings.Default.WebP_Quality;
            set => Properties.Settings.Default.WebP_Quality = value;
        }
        internal static bool WebPRemoveAlpha {
            get => Properties.Settings.Default.WebP_RemoveAlpha;
            set => Properties.Settings.Default.WebP_RemoveAlpha = value;
        }
        internal static double JpegQuality {
            get => Properties.Settings.Default.JPEG_Quality;
            set => Properties.Settings.Default.JPEG_Quality = value;
        }
        internal static bool PngLossless {
            get => Properties.Settings.Default.PNG_Lossess;
            set => Properties.Settings.Default.PNG_Lossess = value;
        }
        internal static double PngQuality {
            get => Properties.Settings.Default.PNG_Quality;
            set => Properties.Settings.Default.PNG_Quality = value;
        }
        internal static bool PngRemoveAlpha {
            get => Properties.Settings.Default.PNG_RemoveAlpha;
            set => Properties.Settings.Default.PNG_RemoveAlpha = value;
        }

        internal static void Save() => Properties.Settings.Default.Save();

        internal static string GetWebPRemoveAlpha() => WebPRemoveAlpha ? "remove" : "set";

        internal static string GetPngRemoveAlpha() => PngRemoveAlpha ? "remove" : "set";
    }
}
