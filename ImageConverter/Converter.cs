using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter_Utilities;

namespace ImageConverter {
    public class Converter : IConverter {
        public object MainPage => new Views.ImagePage();

        public object SEttingsPage => new Views.ImageSettingsPage();
    }
}
