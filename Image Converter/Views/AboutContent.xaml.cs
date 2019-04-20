using System.Diagnostics;
using System.IO;
using System.Media;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Image_Converter.Views {
    /// <summary>
    /// Interaction logic for AboutContent.xaml
    /// </summary>
    public partial class AboutContent : UserControl {
        public AboutContent() {
            InitializeComponent();
        }

        private void FinishedSoundButton_Click(object sender, System.Windows.RoutedEventArgs e) {
            SoundPlayer player = new SoundPlayer("finished.wav");
            player.Play();
        }

        private void KennyLinkButton_Click(object sender, System.Windows.RoutedEventArgs e) => Process.Start("https://www.kenney.nl/");
    }
}
