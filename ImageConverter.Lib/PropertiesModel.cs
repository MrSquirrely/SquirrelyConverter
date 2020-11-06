using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Media;
using HandyControl.Controls;
using ImageConverter.Lib.Lang;

namespace ImageConverter.Lib {
    public class PropertiesModel : GlobalDataHelper<PropertiesModel> {
        // General Settings Start
        [Category("General")]
        [DisplayName("Delete Image")]
        [Description("Wether or not to delete the original image")]
        [DefaultValue(true)]
        public bool DeleteImage { get; set; } = true;

        [Category("General")]
        [DisplayName("Play Sound")]
        [Description("Wether or not to play a sound when finished converting")]
        [DefaultValue(true)]
        public bool PlaySound { get; set; } = true;

        [Category("General")]
        [DisplayName("Sound")]
        [Description("The sound that plays at the end of the conversion")]
        [DefaultValue(Sound.Hit)]
        public Sound SoundToPlay { get; set; } = Sound.Hit;
        // General Settings End

        // Jpeg Settings Start
        //[Category("Jpeg Settings")]
        //[DisplayName("This is the display name")]
        //[Description("This is the description")]
        //public int Integer { get; set; }
        // Jpeg Settings End

        // PNG Settings Start
        //[Category("PNG Settings")]
        //[DisplayName("This is the display name")]
        //[Description("This is the description")]
        //public bool Boolean { get; set; }
        // PNG Settings End

        // WebP Settings Start
        [Category("WebP Settings")]
        [DisplayName("Lossless")]
        [Description("Whether or not to use lossless compression")]
        [DefaultValue(true)]
        public bool WebPLosses { get; set; } = true;

        [Category("WebP Settings")]
        [DisplayName("Remove Alpha")]
        [Description("Whether or not to remove the alpha channel/layer")]
        [DefaultValue(false)]
        public bool WebPRemoveAlpha { get; set; } = false;

        [Category("WebP Settings")]
        [DisplayName("Emulate Jpeg Size")]
        [Description("Whether or not to try and emulate Jpeg size")]
        [DefaultValue(false)]
        public bool WebPEmulateJpeg { get; set; } = false;

        [Category("WebP Settings")]
        [DisplayName("Quality")]
        [Description("The quality of the compression")]
        [DefaultValue(80)]
        public int WebPQuality { get; set; } = 80;
        //WebP Settings End

    }

    public enum Sound {
        Hit,
        Pizza,
        Sax,
        Steel
    }
}
