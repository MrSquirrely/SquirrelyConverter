using MahApps.Metro.Controls;
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
        public static Flyout flyout { get; set; }

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

        private static List<string> Quotes = new List<string>() {
            "“I recommend you take care of the minutes and the hours will take care of themselves.” – Earl of Chesterfield",
            "“To do two things at once is to do neither.” – Publilius Syrus",
            "“You will never find time for anything. If you want time you must make it.” – Charles Buxton",
            "“A plan is what, a schedule is when. It takes both a plan and a schedule to get things done.” – Peter Turla",
            "“There is nothing so useless as doing efficiently that which should not be done at all.” – Peter F. Drucker",
            "“If you find yourself in a hole, stop digging.” – Will Rogers",
            "“Take a rest. A field that has rested yields a beautiful crop.” – Ovid",
            "“There is more to life than increasing its speed.” – Mohandas K. Gandhi",
            "“If you win the rat race, you’re still a rat.” – Lily Tomlin",
            "“Focused action beats brilliance.” – Mark Sanborn",
            "“Begin with the end in mind.” – Steven Covey",
            "“Much of the stress that people feel doesn’t come from having too much to do. It comes from not finishing what they started.” – David Allen",
            "“It’s not enough to be busy, so are the ants. The question is, what are we busy about?” – Henry David Thoreau",
            "“You’ve got to think about big things while you’re doing small things, so that all the small things go in the right direction.” – Alvin Toffler",
            "“In the long run, we shape our lives, and we shape ourselves. The process never ends until we die. And the choices we make are ultimately our own responsibility.” ― Eleanor Roosevelt"
            };

        internal static string GetQuote() {
            int random = new Random().Next(Quotes.Count);
            return Quotes[random];
        }
    }
}