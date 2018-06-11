using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageConverter {
    class Options {
        public static bool CreateTemp { get { return Properties.Settings.Default.Create_Temp; } set { Properties.Settings.Default.Create_Temp = value; } }
        public static string TempLocation { get { return Properties.Settings.Default.Temp_Location; } set { Properties.Settings.Default.Temp_Location = value; } }
        public static bool ImagesDelete { get { return Properties.Settings.Default.Images_Delete; } set { Properties.Settings.Default.Images_Delete = value; } }
        public static bool WebPLossless { get { return Properties.Settings.Default.WebP_Lossess; } set { Properties.Settings.Default.WebP_Lossess = value; } }
        public static bool WebPEmulateJPEG { get { return Properties.Settings.Default.WebP_Emulate_JPEG; } set { Properties.Settings.Default.WebP_Emulate_JPEG = value; } }
        public static double WebPQuality { get { return Properties.Settings.Default.WebP_Quality; } set { Properties.Settings.Default.WebP_Quality = value; } }
        public static bool WebPRemoveAlpha { get { return Properties.Settings.Default.WebP_RemoveAlpha; } set { Properties.Settings.Default.WebP_RemoveAlpha = value; } }
        public static double JPEGQuality { get { return Properties.Settings.Default.JPEG_Quality; } set { Properties.Settings.Default.JPEG_Quality = value; } }
        public static bool PNGLossless { get { return Properties.Settings.Default.PNG_Lossess; } set { Properties.Settings.Default.PNG_Lossess = value; } }
        public static double PNGQuality { get { return Properties.Settings.Default.PNG_Quality; } set { Properties.Settings.Default.PNG_Quality = value; } }
        public static bool PNGRemoveAlpha { get { return Properties.Settings.Default.PNG_RemoveAlpha; } set { Properties.Settings.Default.PNG_RemoveAlpha = value; } }

        public static void Save() => Properties.Settings.Default.Save();

        public static string GetWebPRemoveAlpha() {
            if (WebPRemoveAlpha) {
                return "remove";
            }
            else {
                return "set";
            }
        }

        public static string GetPNGRemoveAlpha() {
            if (PNGRemoveAlpha) {
                return "remove";
            }
            else {
                return "set";
            }
        }
    }
}
