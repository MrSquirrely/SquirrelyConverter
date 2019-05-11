using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter_Utilities {
    public class FileFilters {
        public static CommonFileDialogFilter ImageFilters => new CommonFileDialogFilter("Common Images", "*.png;*.jpeg;*.jpg;*.exif;*.tiff;*.gif;*.bmp");
        public static CommonFileDialogFilter WebPFilter => new CommonFileDialogFilter("Webp Images", "*.webp");

        public static readonly List<string> ImageTypes = new List<string>() { ".png", ".jpeg", ".jpg", ".exif", ".tiff", ".gif", ".bmp" };
    }
}
