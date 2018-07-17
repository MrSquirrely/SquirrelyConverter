using System;
using System.Diagnostics;
using ConverterUtilities.Configs;

namespace ConverterUtilities.CUtils {
    class Webpages {

        private const string Github = "https://github.com/MrSquirrelyNet/SquirrelyConverter/issues";
        private const string Download = "https://github.com/MrSquirrelyNet/SquirrelyConverter/releases";

        public static void OpenWebpage(Enums.Webpage webpage) {
            switch (webpage) {
                case Enums.Webpage.Github:
                    Process.Start(Github);
                    break;
                case Enums.Webpage.Download:
                    Process.Start(Download);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(webpage), webpage, null);
            }
        }

    }
}
