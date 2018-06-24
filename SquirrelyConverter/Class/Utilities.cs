using System.Windows.Controls;
using ConverterUtilities;
using Dragablz;
using ImageConverter.View;
using VideoConverter.View;

namespace Mr_Squirrely_Converters.Class {
    internal static class Utilities {

        private static int Tabs { get; set; }
        public static TabablzControl ConverterTabs { get; set; }

        public static void AddImageTab() {
            Logger.LogDebug("Adding Image Tab");
            Tabs = ConverterTabs.Items.Count;
            TabItem imageTab = new TabItem {
                Content = new ImageView(),
                Header = "Image Converter"
            };
            ConverterTabs.Items.Insert(Tabs, imageTab);
        }

        public static void AddVideoTab() {
            Logger.LogDebug("Adding Video Tab");
            Tabs = ConverterTabs.Items.Count;
            TabItem videoTab = new TabItem {
                Content = new VideoView(),
                Header = "Video Converter"
            };
            ConverterTabs.Items.Insert(Tabs, videoTab);
        }
    }
}
