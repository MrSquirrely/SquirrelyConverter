using Converters.Lib;
using System.Windows.Controls;

namespace ImageConverter.Lib {
    class Converter : IConverter {

        private readonly string Name = "Image Converter";
        private readonly Page MainPage = new Views.MainPage();
        private readonly Page SettingsPage = new Views.SettingsPage();
        private readonly Page AboutPage = new Views.AboutPage();

        public string GetName() => Name;
        public Page GetMainView() => MainPage;
        public Page GetSettingsView() => SettingsPage;
        public Page GetAboutView() => AboutPage;
       
    }
}
