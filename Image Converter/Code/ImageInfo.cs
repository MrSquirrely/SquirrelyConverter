using MaterialDesignThemes.Wpf;
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
