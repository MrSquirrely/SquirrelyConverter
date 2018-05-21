using System.Collections.Generic;

namespace Mr_Squirrely_Converters.Class {
    static class Types {
        internal static string Images() { return "images"; }
        internal static string Videos() { return "videos"; }

        //Doesn't support GIF Images
        internal readonly static List<string> ImageFormats = new List<string>{
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

        //This list is rather large and will possibly shorten at some point
        internal readonly static List<string> VideoFormats = new List<string>{
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
