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

        public static SnackbarMessageQueue messageQueue { get; set; }

        public static string SizeSuffix(long value, int decimalPlaces = 1) {
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

        public static void SendSnackbarMessage(string message) => Task.Factory.StartNew(() => messageQueue.Enqueue(message));

        private static string a01 = "“I recommend you take care of the minutes and the hours will take care of themselves.” – Earl of Chesterfield";
        private static string a02 = "“To do two things at once is to do neither.” – Publilius Syrus";
        private static string a03 = "“You will never find time for anything. If you want time you must make it.” – Charles Buxton";
        private static string a04 = "“A plan is what, a schedule is when. It takes both a plan and a schedule to get things done.” – Peter Turla";
        private static string a05 = "“There is nothing so useless as doing efficiently that which should not be done at all.” – Peter F. Drucker";
        private static string a06 = "“If you find yourself in a hole, stop digging.” – Will Rogers";
        private static string a07 = "“Take a rest. A field that has rested yields a beautiful crop.” – Ovid";
        private static string a08 = "“There is more to life than increasing its speed.” – Mohandas K. Gandhi";
        private static string a09 = "“If you win the rat race, you’re still a rat.” – Lily Tomlin";
        private static string a10 = "“Focused action beats brilliance.” – Mark Sanborn";
        private static string a11 = "“Begin with the end in mind.” – Steven Covey";
        private static string a12 = "“Much of the stress that people feel doesn’t come from having too much to do. It comes from not finishing what they started.” – David Allen";
        private static string a13 = "“It’s not enough to be busy, so are the ants. The question is, what are we busy about?” – Henry David Thoreau";
        private static string a14 = "“You’ve got to think about big things while you’re doing small things, so that all the small things go in the right direction.” – Alvin Toffler";
        private static string a15 = "“In the long run, we shape our lives, and we shape ourselves. The process never ends until we die. And the choices we make are ultimately our own responsibility.” ― Eleanor Roosevelt";

        internal static string GetQuote() {
            int random = new Random().Next(15);
            switch (random) {
                case 0:
                    return a01;
                case 1:
                    return a01;
                case 2:
                    return a02;
                case 3:
                    return a03;
                case 4:
                    return a04;
                case 5:
                    return a05;
                case 6:
                    return a06;
                case 7:
                    return a07;
                case 8:
                    return a08;
                case 9:
                    return a09;
                case 10:
                    return a10;
                case 11:
                    return a11;
                case 12:
                    return a12;
                case 13:
                    return a13;
                case 14:
                    return a14;
                case 15:
                    return a15;
                default:
                    return a01;
            }
        }
    }
}
