using HandyControl.Controls;
using ImageConverter.Lib.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Windows.Controls;
using HandyControl.Tools.Converter;

namespace ImageConverter.Lib {
    public static class Reference {

        public static Page GetMainPage => new MainPage();
        public static Page GetSettingsPage => new SettingsPage();
        public static Page GetAboutPage => new AboutPage();


        public static Drawer SettingsDrawer { get; set; }
        public static Drawer AboutDrawer { get; set; }
        public static ListView ImageListView { get; set; }
        
        public static ObservableCollection<ImageFile> ImageCollection { get; set; }
        internal static readonly List<string> ImageTypes = new List<string>() { ".png", ".jpeg", ".jpg", ".exif", ".tiff", ".bmp" };
        //public static PropertiesModel GetPropertiesModel { get; } = JsonSerializer.Deserialize<PropertiesModel>(File.ReadAllText("ImageConverter.json"));
        public static PropertiesModel GetPropertiesModel {
            get {
                GlobalDataHelper<PropertiesModel>.Init("ImageConverter.json");
                return new PropertiesModel() {
                    DeleteImage = GlobalDataHelper<PropertiesModel>.Config.DeleteImage,
                    PlaySound = GlobalDataHelper<PropertiesModel>.Config.PlaySound,
                    SoundToPlay = GlobalDataHelper<PropertiesModel>.Config.SoundToPlay,
                    WebPEmulateJpeg = GlobalDataHelper<PropertiesModel>.Config.WebPEmulateJpeg,
                    WebPLosses = GlobalDataHelper<PropertiesModel>.Config.WebPLosses,
                    WebPQuality = GlobalDataHelper<PropertiesModel>.Config.WebPQuality,
                    WebPRemoveAlpha = GlobalDataHelper<PropertiesModel>.Config.WebPRemoveAlpha
                };
            }
        }

        public static void ResetProperties() {

        }

        //public static void SaveProperties() => File.WriteAllText("ImageConverter.json", JsonSerializer.Serialize(GetPropertiesModel));
        public static void SaveProperties(PropertiesModel propertiesModel) {
            GlobalDataHelper<PropertiesModel>.Config.DeleteImage = propertiesModel.DeleteImage;
            GlobalDataHelper<PropertiesModel>.Config.PlaySound = propertiesModel.PlaySound;
            GlobalDataHelper<PropertiesModel>.Config.SoundToPlay = propertiesModel.SoundToPlay;
            GlobalDataHelper<PropertiesModel>.Config.WebPEmulateJpeg = propertiesModel.WebPEmulateJpeg;
            GlobalDataHelper<PropertiesModel>.Config.WebPLosses = propertiesModel.WebPLosses;
            GlobalDataHelper<PropertiesModel>.Config.WebPQuality = propertiesModel.WebPQuality;
            GlobalDataHelper<PropertiesModel>.Config.WebPRemoveAlpha = propertiesModel.WebPRemoveAlpha;
            GlobalDataHelper<PropertiesModel>.Save();
        }

        public static void OpenSettingsDrawer() {
            SettingsDrawer.IsOpen = !SettingsDrawer.IsOpen;
            AboutDrawer.IsOpen = false;
        }

        public static void OpenAboutDrawer() {
            AboutDrawer.IsOpen = !AboutDrawer.IsOpen;
            SettingsDrawer.IsOpen = false;
        }
    }

    // Todo: Need to find all supported image types in Image Magick
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ImageTypes {
        [Description("Jpeg")]
        JPEG,
        [Description("Png")]
        PNG,
        [Description("WebP")]
        WEBP
    }
}
