using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConverterUtilities.Interfaces
{
    public interface ISettings {
        bool Reset { get; set; }
        void StartValues();
        void SetValues(bool resetValues);
        void CloseWindow();
        void SaveButton_Click(object sender, RoutedEventArgs e);
        void CancelButton_Click(object sender, RoutedEventArgs e);
        void ResetButton_Click(object sender, RoutedEventArgs e);
    }
}
