using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mr_Squirrely_Converters.Class {
    static class Options {

        internal static bool CreateTemp { get { return Properties.Settings.Default.Create_Temp; } set { Properties.Settings.Default.Create_Temp = value; } }
        internal static string TempLocation { get { return Properties.Settings.Default.Temp_Location; } set { Properties.Settings.Default.Temp_Location = value; } }
        internal static bool ImagesDelete { get { return Properties.Settings.Default.Images_Delete; } set { Properties.Settings.Default.Images_Delete = value; } }
        internal static bool WebPLossless { get { return Properties.Settings.Default.WebP_Lossess; } set { Properties.Settings.Default.WebP_Lossess = value; } }
        internal static bool WebPEmulateJPEG { get { return Properties.Settings.Default.WebP_Emulate_JPEG; } set { Properties.Settings.Default.WebP_Emulate_JPEG = value; } }
        internal static double WebPQuality { get { return Properties.Settings.Default.WebP_Quality; } set { Properties.Settings.Default.WebP_Quality = value; } }
        internal static bool WebPRemoveAlpha { get { return Properties.Settings.Default.WebP_RemoveAlpha; } set { Properties.Settings.Default.WebP_RemoveAlpha = value; } }
        internal static double JPEGQuality { get { return Properties.Settings.Default.JPEG_Quality; } set { Properties.Settings.Default.JPEG_Quality = value; } }
        internal static bool PNGLossless { get { return Properties.Settings.Default.PNG_Lossess; } set { Properties.Settings.Default.PNG_Lossess = value; } }
        internal static double PNGQuality { get { return Properties.Settings.Default.PNG_Quality; } set { Properties.Settings.Default.PNG_Quality = value; } }
        internal static bool PNGRemoveAlpha { get { return Properties.Settings.Default.PNG_RemoveAlpha; } set { Properties.Settings.Default.PNG_RemoveAlpha = value; } }
        internal static bool VideoChangeSize { get { return Properties.Settings.Default.Video_ChangeSize; } set { Properties.Settings.Default.Video_ChangeSize = value; } }
        internal static int VideoWidth { get { return Properties.Settings.Default.Video_Width; } set { Properties.Settings.Default.Video_Width = value; } }
        internal static int VideoHeight { get { return Properties.Settings.Default.Video_Height; } set { Properties.Settings.Default.Video_Height = value; } }
        internal static bool VideoRemoveAudio { get { return Properties.Settings.Default.Video_RemoveAudio; } set { Properties.Settings.Default.Video_RemoveAudio = value; } }

        internal static void Save() => Properties.Settings.Default.Save();

        internal static string GetWebPRemoveAlpha() {
            if (WebPRemoveAlpha) return "remove"; else return "set";
        }

        internal static string GetPNGRemoveAlpha() {
            if (PNGRemoveAlpha) return "remove"; else return "set";
        }
    }
}
