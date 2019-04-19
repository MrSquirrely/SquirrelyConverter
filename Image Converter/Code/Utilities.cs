using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Image_Converter.Code {
    public class Utilities {

        public static ObservableCollection<ImageInfo> ImageCollection { get; } = new ObservableCollection<ImageInfo>();
        public static ListView ImageListView { get; set; }

        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public static string SizeSuffix(Int64 value, int decimalPlaces = 1) {
            if (value < 0) {
                return $"-{SizeSuffix(-value)}";
            }

            int i = 0;
            decimal dValue = (decimal)value;
            while (Math.Round(dValue, decimalPlaces) >= 1000) {
                dValue /= 1024;
                i++;
            }
            return string.Format("{0:n" + decimalPlaces + "} {1}", dValue, SizeSuffixes[i]);
        }

        public static void PopulateList(string[] droppedFiles) {
            ImageCollection.Clear();
            foreach (string file in droppedFiles) {
                FileInfo fileInfo = new FileInfo(file);
                if (Filters.ImageTypes.Contains(fileInfo.Extension)) {
                    ImageCollection.Add(new ImageInfo() {
                        FileName = Path.GetFileNameWithoutExtension(file),
                        FileType = fileInfo.Extension,
                        FileSize = SizeSuffix(fileInfo.Length),
                        FileIcon = PackIconKind.Close,
                        FileColor = Brushes.Red,
                        FileLocation = fileInfo.DirectoryName
                    });
                }
            }
        }

    }
}
