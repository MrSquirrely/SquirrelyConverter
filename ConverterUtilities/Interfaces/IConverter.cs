using System.Threading;

namespace ConverterUtilities.Interfaces {
    public interface IConverter {

        string Image { get; set; }
        Thread Thread { get; set; }
        ThreadStart ThreadStart { get; set; }

        void StartConvert();
        void Convert();
        void Finish();
        void UpdateView();
    }
}
