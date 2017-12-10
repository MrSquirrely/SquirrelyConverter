using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquirrelyConverter {

    class nFile {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
    }

    enum Message {
       DirectoryExists,
       AlreadyFilesLoaded,
       WebM
    }

    class Types {
        /// <summary>
        /// A list of supported image containers. I'm not sure if this is all of them, but this is all I could find.
        /// </summary>
        public static List<string> WebPTypes = new List<string>() {
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

        /// <summary>
        /// A list of supported video containers for FFMPeg. Right now for ease I only have MP4 down
        /// </summary>
        public static List<string> WebMTypes = new List<string>() {
            ".asf",
            ".avi",
            ".f4v",
            ".flv",
            ".m2t",
            ".m2ts",
            ".mkv",
            ".mov",
            ".mp4",
            ".mpg",
            ".mts",
            ".nut",
            ".ogv",
            ".ts",
            ".vob",
            ".wmv",
            ".wtv"
        };
    }

    enum Toast {
        Finished,
        EncodeCleared,
        DecodeCleared,
        SettingsSaved
    }

}
