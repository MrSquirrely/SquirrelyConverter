using System;
using ConverterUtilities.Properties;

namespace ConverterUtilities.CUtils {
    public static class Options {

        private static bool _imageStarted;
        private static bool _videoStarted;

        public static void SaveSettings() {
            Settings.Default.CreateTemp = _createTemp;
            Settings.Default.TempLocation = _tempLocation;
            Settings.Default.DeleteTemp = _deleteTemp;
            if (_imageStarted) {
                Settings.Default.WebPLossless = _webPLossless;
                Settings.Default.WebPEmulateJPEG = _webPEmulateJpeg;
                Settings.Default.WebPQuality = _webPQuality;
                Settings.Default.WebPRemoveAlpha = _webPRemoveAlpha;

                Settings.Default.JPEGQuality = _jpegQuality;

                Settings.Default.PNGLossless = _pngLossless;
                Settings.Default.PNGQuality = _pngQuality;
                Settings.Default.PNGRemoveAlpha = _pngRemoveAlpha;
            }

            if (_videoStarted) {
                Settings.Default.VideoChangeSize = _videoChangeSize;
                Settings.Default.VideoWidth = _videoWidth;
                Settings.Default.VideoHeight = _videoHeight;
            }
            Settings.Default.Save();
        }

        public static void StartGeneralSettings() {
            SetCreateTemp(Settings.Default.CreateTemp);
            SetTempLocation(Settings.Default.TempLocation);
            SetDeleteTemp(Settings.Default.DeleteTemp);
        }

        public static void StartImageSettings() {
            _imageStarted = true;
            SetWebPLossless(Settings.Default.WebPLossless);
            SetWebPEmulateJpeg(Settings.Default.WebPEmulateJPEG);
            SetWebPQuality(Settings.Default.WebPQuality);
            SetWebPRemoveAlpha(Settings.Default.WebPRemoveAlpha);

            SetJpegQuality(Settings.Default.JPEGQuality);

            SetPngLossless(Settings.Default.PNGLossless);
            SetPngQuality(Settings.Default.PNGQuality);
            SetPngRemoveAlpha(Settings.Default.PNGRemoveAlpha);
        }

        public static void StartVideoSettings() {
            _videoStarted = true;
            SetVideoChangeSize(Settings.Default.VideoChangeSize);
            SetVideoWidth(Settings.Default.VideoWidth);
            SetVideoHeight(Settings.Default.VideoHeight);
        }

        public static void SetGeneralSettings(bool createTemp, string tempLocation, bool deleteTemp) {
            SetCreateTemp(createTemp);
            SetTempLocation(tempLocation);
            SetDeleteTemp(deleteTemp);
        }

        public static void SetImageSettings(bool webPLossless, bool weBpEmulateJpeg, double webPQuality, bool webPRemoveAlpha, 
                                            double jpegQuality, 
                                            bool pngLossless, double pngQuality, bool pngRemoveAlpha) {
            SetWebPLossless(webPLossless);
            SetWebPEmulateJpeg(weBpEmulateJpeg);
            SetWebPQuality(webPQuality);
            SetWebPRemoveAlpha(webPRemoveAlpha);

            SetJpegQuality(jpegQuality);

            SetPngLossless(pngLossless);
            SetPngQuality(pngQuality);
            SetPngRemoveAlpha(pngRemoveAlpha);
        }

        public static void SetVideoSettings(bool changeSize, int width, int height) {
            SetVideoChangeSize(changeSize);
            SetVideoWidth(width);
            SetVideoHeight(height);
        }

        [Obsolete("Remove Audio Not Implemented")]
        public static void SetVideoSettings(bool changeSize, int width, int height, bool removeAudio) {
            SetVideoChangeSize(changeSize);
            SetVideoWidth(width);
            SetVideoHeight(height);
            SetVideoRemoveAudi(removeAudio);
        }

        #region General Settings
        private static bool _createTemp;
        private static string _tempLocation;
        private static bool _deleteTemp;
        
        public static bool GetCreateTemp() => _createTemp;
        public static void SetCreateTemp(bool value) => _createTemp = value;

        public static string GetTempLocation() => _tempLocation;
        public static void SetTempLocation(string value) => _tempLocation = value;

        public static bool GetDeleteTemp() => _deleteTemp;
        public static void SetDeleteTemp(bool value) => _deleteTemp = value;
        #endregion

        #region Image Settings
        #region WebP
        private static bool _webPLossless;
        private static bool _webPEmulateJpeg;
        private static double _webPQuality;
        private static bool _webPRemoveAlpha;

        public static bool GetWebPLossless() => _webPLossless;
        public static void SetWebPLossless(bool value) => _webPLossless = value;

        public static bool GetWebPEmulateJpeg() => _webPEmulateJpeg;
        public static void SetWebPEmulateJpeg(bool value) => _webPEmulateJpeg = value;

        public static double GetWebPQuality() => _webPQuality;
        public static void SetWebPQuality(double value) => _webPQuality = value;

        public static bool GetWebPRemoveAlpha() => _webPRemoveAlpha;
        public static void SetWebPRemoveAlpha(bool value) => _webPRemoveAlpha = value;
        #endregion

        #region JPEG
        private static double _jpegQuality;

        public static double GetJpegQuality() => _jpegQuality;
        public static void SetJpegQuality(double value) => _jpegQuality = value;
        #endregion

        #region PNG
        private static bool _pngLossless;
        private static double _pngQuality;
        private static bool _pngRemoveAlpha;

        public static bool GetPngLossless() => _pngLossless;
        public static void SetPngLossless(bool value) => _pngLossless = value;

        public static double GetPngQuality() => _pngQuality;
        public static void SetPngQuality(double value) => _pngQuality = value;

        public static bool GetPngRemoveAlpha() => _pngRemoveAlpha;
        public static void SetPngRemoveAlpha(bool value) => _pngRemoveAlpha = value;
        #endregion
        #endregion

        #region Video Settings
        private static bool _videoChangeSize;
        private static int _videoWidth;
        private static int _videoHeight;
        private static bool _videoRemoveAudio;

        public static bool GetVideoChangeSize() => _videoChangeSize;
        public static void SetVideoChangeSize(bool value) => _videoChangeSize = value;

        public static int GetVideoWidth() => _videoWidth;
        public static void SetVideoWidth(int value) => _videoWidth = value;

        public static int GetVideoHeight() => _videoHeight;
        public static void SetVideoHeight(int value) => _videoHeight = value;
        
        [Obsolete("Not Implemented")]
        public static bool GetVideoRemoveAudio() => _videoRemoveAudio;
        [Obsolete("Not Implemented")]
        public static void SetVideoRemoveAudi(bool value) => _videoRemoveAudio = value;
        #endregion
    }
}
