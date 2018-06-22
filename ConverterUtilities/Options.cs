using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConverterUtilities.Properties;
using NLog;

namespace ConverterUtilities {
    // Todo: Change options from "Do, Where, etc" to get, confusing and not needed
    public static class Options {

        public static void SetGeneralSettings() {
            SetCreateTemp(Settings.Default.CreateTemp);
            SetTempLocation(Settings.Default.TempLocation);
            SetDeleteTemp(Settings.Default.DeleteTemp);
        }

        public static void SetImageSettings() {
            SetWebPLossless(Settings.Default.WebPLossless);
            SetWebPEmulateJPEG(Settings.Default.WebPEmulateJPEG);
            SetWebPQuality(Settings.Default.WebPQuality);
            SetWebPRemoveAlpha(Settings.Default.WebPRemoveAlpha);

            SetJPEGQuality(Settings.Default.JPEGQuality);

            SetPNGLossless(Settings.Default.PNGLossless);
            SetPNGQuality(Settings.Default.PNGQuality);
            SetPNGRemoveAlpha(Settings.Default.PNGRemoveAlpha);
        }

        public static void SetVideoSettings() {
            SetVideoChangeSize(Settings.Default.VideoChangeSize);
            SetVideoWidth(Settings.Default.VideoWidth);
            SetVideoHeight(Settings.Default.VideoHeight);
        }

        #region General Settings
        private static bool createTemp;
        private static string tempLocation;
        private static bool deleteTemp;
        
        public static bool DoCreateTemp() => createTemp;
        public static void SetCreateTemp(bool value) => createTemp = value;

        public static string WhereTempLocation() => tempLocation;
        public static void SetTempLocation(string value) => tempLocation = value;

        public static bool DoDeleteTemp() => deleteTemp;
        public static void SetDeleteTemp(bool value) => deleteTemp = value;
        #endregion

        #region Image Settings
        #region WebP
        private static bool webPLossless;
        private static bool webPEmulateJPEG;
        private static double webPQuality;
        private static bool webPRemoveAlpha;

        public static bool IsWebPLossless() => webPLossless;
        public static void SetWebPLossless(bool value) => webPLossless = value;

        public static bool DoWebPEmulateJPEG() => webPEmulateJPEG;
        public static void SetWebPEmulateJPEG(bool value) => webPEmulateJPEG = value;

        public static double GetWebPQuality() => webPQuality;
        public static void SetWebPQuality(double value) => webPQuality = value;

        public static bool DoWebPRemoveAlpha() => webPRemoveAlpha;
        public static void SetWebPRemoveAlpha(bool value) => webPRemoveAlpha = value;
        #endregion

        #region JPEG
        private static double jpegQuality;

        public static double GetJPEGQuality() => jpegQuality;
        public static void SetJPEGQuality(double value) => jpegQuality = value;
        #endregion

        #region PNG
        private static bool pngLossless;
        private static double pngQuality;
        private static bool pngRemoveAlpha;

        public static bool IsPNGLossless() => pngLossless;
        public static void SetPNGLossless(bool value) => pngLossless = value;

        public static double GetPNGQuality() => pngQuality;
        public static void SetPNGQuality(double value) => pngQuality = value;

        public static bool DoPNGRemoveAlpha() => pngRemoveAlpha;
        public static void SetPNGRemoveAlpha(bool value) => pngRemoveAlpha = value;
        #endregion
        #endregion

        #region Video Settings
        private static bool videoChangeSize;
        private static int videoWidth;
        private static int videoHeight;
        private static bool videoRemoveAudio;

        public static bool DoVideoChangeSize() => videoChangeSize;
        public static void SetVideoChangeSize(bool value) => videoChangeSize = value;

        public static int GetVideoWidth() => videoWidth;
        public static void SetVideoWidth(int value) => videoWidth = value;

        public static int GetVideoHeight() => videoHeight;
        public static void SetVideoHeight(int value) => videoHeight = value;
        
        [Obsolete("Not Implemented")]
        public static bool DoVideoRemoveAudio() => videoRemoveAudio;
        [Obsolete("Not Implemented")]
        public static void SetVideoRemoveAudi(bool value) => videoRemoveAudio = value;
        #endregion
    }
}
