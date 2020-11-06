using Converters.Lib;
using System.Windows.Controls;

namespace ImageConverter.Lib {
    class Converter : IConverter {
        private const string Name = "Image Converter";
        private readonly Page _mainPage = new Views.MainPage();
        private readonly Page _settingsPage = new Views.SettingsPage();
        private readonly Page _aboutPage = new Views.AboutPage();

        string IConverter.GetName() => Name;
        //Page IConverter.GetMainView() => _mainPage;
        //Page IConverter.GetSettingsView() => _settingsPage;
        //Page IConverter.GetAboutView() => _aboutPage;

        public object GetMainView() => _mainPage;
        public object GetSettingsView() => _settingsPage;
        public object GetAboutView() => _aboutPage;

    }
}
