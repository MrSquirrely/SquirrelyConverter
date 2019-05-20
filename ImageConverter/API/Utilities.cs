using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Converter_Utilities.API;

namespace ImageConverter.API {
    internal class Utilities {
        internal static ObservableCollection<InfoFile> ImageCollection { get; } = new ObservableCollection<InfoFile>();
        internal static ListView ImageListView { get; set; }

        internal static void PopulateList(string[] droppedFiles) {
            ImageCollection.Clear();
            foreach (string droppedFile in droppedFiles) {
                FileInfo info = new FileInfo(droppedFile);
                if (FileFilters.ImageTypes.Contains(info.Extension)) {
                    ImageCollection.Add(new InfoFile(){
                        FileName = Path.GetFileNameWithoutExtension(droppedFile),
                        FileType = info.Extension,
                        FileSize = Sizer.SizeSuffix(info.Length),
                        FileIcon = PackIconMaterialKind.Close,
                        FileColor = Brushes.Red,
                        FileLocation = info.DirectoryName
                    });
                }
            }
        }
    }
}
