using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConverterUtilities {
    public class Enums {
        public enum FileExtension { yes, no }

        public readonly static List<string> ImageFormats = new List<string>{
            ".gif",
            ".png",
            ".jpg",
            ".jpeg",
            ".jpe",
            ".jif",
            ".jfif",
            ".jfi",
            ".tiff",
            ".tif"
        };

        public readonly static List<string> VideoFormats = new List<string>{
            ".gif",
            ".3gp",
            ".3g2",
            ".asf",
            ".amv",
            ".avi",
            ".drc",
            ".flv",
            ".f4v",
            ".f4p",
            ".f4a",
            ".f4b",
            ".gif",
            ".m4v",
            ".mxf",
            ".mkv",
            ".mpg",
            ".mp2",
            ".mpeg",
            ".mpe",
            ".mpv",
            ".m2v",
            ".mp4",
            ".m4p",
            ".m4v",
            ".mng",
            ".nsv",
            ".ogv",
            ".ogg",
            ".mov",
            ".qt",
            ".yuv",
            ".rm",
            ".rmvb",
            ".roq",
            ".svi",
            ".gifv",
            ".vob",
            ".webm",
            ".wmv"
        };
    }
}
