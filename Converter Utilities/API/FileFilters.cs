using System.Collections.Generic;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Converter_Utilities.API {
    public class FileFilters {
        public static CommonFileDialogFilter ImageFilters => new CommonFileDialogFilter("Common Images", "*.png;*.jpeg;*.jpg;*.exif;*.tiff;*.gif;*.bmp");
        public static CommonFileDialogFilter WebPFilter => new CommonFileDialogFilter("Webp Images", "*.webp");

        public static readonly List<string> ImageTypes = new List<string>() { ".png", ".jpeg", ".jpg", ".exif", ".tiff", ".gif", ".bmp" };
    }
}
