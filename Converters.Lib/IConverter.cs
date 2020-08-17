using System.Windows.Controls;

namespace Converters.Lib {
    public interface IConverter {
        public string GetName();
        public Page GetMainView();
        public Page GetSettingsView();
        public Page GetAboutView();
    }
}
