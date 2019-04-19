using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Image_Converter.Code {
    internal class Filters {
        public static CommonFileDialogFilter ImageFilters => new CommonFileDialogFilter("Common Images", "*.png;*.jpeg;*.jpg;*.exif;*.tiff;*.gif;*.bmp");
        public static CommonFileDialogFilter WebPFilter => new CommonFileDialogFilter("Webp Images", "*.webp");

        public static List<string> ImageTypes = new List<string>() { ".png", ".jpeg", ".jpg", ".exif", ".tiff", ".gif", ".bmp" };

    }
}
