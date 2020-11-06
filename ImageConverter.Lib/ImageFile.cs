using System;
using System.Collections.Generic;
using System.Text;

namespace ImageConverter.Lib {
    public class ImageFile {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public bool Converted { get; set; }
        public string Location { get; set; }
    }
}
