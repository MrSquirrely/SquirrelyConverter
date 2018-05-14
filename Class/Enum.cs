using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mr_Squirrely_Converters.Class
{
    class Enum{ }

    class NewImage {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Converted { get; set; }
        public string Location { get; set; }
    }

    //Currently doesn't support GIF Images
    // ".gif",
    class Types {
        public static List<string> ImageFormats = new List<string>() {
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
    }
}
