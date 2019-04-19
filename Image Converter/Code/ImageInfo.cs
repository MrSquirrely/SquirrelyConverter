using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Image_Converter.Code {
    public class ImageInfo {

        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        public PackIconKind FileIcon { get; set; }
        public Brush FileColor { get; set; }
        public string FileLocation { get; set; }

    }
}
