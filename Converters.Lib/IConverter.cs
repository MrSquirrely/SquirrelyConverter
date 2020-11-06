using System.Windows.Controls;

namespace Converters.Lib {
    public interface IConverter {
        public string GetName();
        public object GetMainView();
        public object GetSettingsView();
        public object GetAboutView();
    }
}
