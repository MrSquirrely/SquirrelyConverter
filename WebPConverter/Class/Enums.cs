using System.Collections.Generic;

namespace WebPConverter.Class {

    class NFile {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Converted { get; set; }
        public string Location { get; set; }
    }

    enum Message {
       DirectoryExists,
       AlreadyFilesLoaded,
       WebM
    }
    
    enum From {
        Gif,
        Image
    }

    
    class Types {
        /// <summary>
        /// A list of supported image containers. I'm not sure if this is all of them, but this is all I could find.
        /// </summary>
        public static List<string> WebP = new List<string>() {
            ".png",
            ".jpg",
            ".jpeg",
            ".jpe",
            ".jif",
            ".jfif",
            ".jfi",
            ".gif",
            ".tiff",
            ".tif"
        };

        //public static List<string> WebMTypes = new List<string>() {
        //    ".asf",
        //    ".avi",
        //    ".f4v",
        //    ".flv",
        //    ".m2t",
        //    ".m2ts",
        //    ".mkv",
        //    ".mov",
        //    ".mp4",
        //    ".mpg",
        //    ".mts",
        //    ".nut",
        //    ".ogv",
        //    ".ts",
        //    ".vob",
        //    ".wmv",
        //    ".wtv"
        //};
    }

    enum Toast {
        Finished,
        EncodeCleared,
        DecodeCleared,
        SettingsSaved
    }

}
