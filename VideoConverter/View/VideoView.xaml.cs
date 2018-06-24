using System.Windows;
using VideoConverter.Class;

namespace VideoConverter.View {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class VideoView {
        public VideoView() {
            InitializeComponent();
            VideoUtilities.VideoListView = VideoFiles;
            VideoUtilities.VideoView = this;
        }

        private void VideoConvertButton_OnClick(object sender, RoutedEventArgs e) => VideoUtilities.Convert(VideoFormatSelector.SelectedIndex);

        private void VideoFiles_OnDrop(object sender, DragEventArgs e) => VideoUtilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[]);

        private void ClearMenu_OnClick(object sender, RoutedEventArgs e) => VideoFiles.Items.Clear();
    }
}
