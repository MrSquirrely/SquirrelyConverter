using System.Collections.Generic;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Image_Converter.Code {
    internal class Filters {
        public static CommonFileDialogFilter ImageFilters => new CommonFileDialogFilter("Common Images", "*.png;*.jpeg;*.jpg;*.exif;*.tiff;*.gif;*.bmp");
        public static CommonFileDialogFilter WebPFilter => new CommonFileDialogFilter("Webp Images", "*.webp");

        public static readonly List<string> ImageTypes = new List<string>() { ".png", ".jpeg", ".jpg", ".exif", ".tiff", ".gif", ".bmp" };
    }
}
