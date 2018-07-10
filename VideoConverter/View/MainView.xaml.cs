using System.Windows;
using VideoConverter.Class;

namespace VideoConverter.View {
    public partial class MainView {
        public MainView() {
            InitializeComponent();
            VideoUtilities.VideoListView = VideoFiles;
            VideoUtilities.VideoView = this;
        }

        private void VideoConvertButton_OnClick(object sender, RoutedEventArgs e) => VideoUtilities.Convert(VideoFormatSelector.SelectedIndex);
        private void VideoFiles_OnDrop(object sender, DragEventArgs e) => VideoUtilities.PopulateList(e.Data.GetData(DataFormats.FileDrop) as string[]);
        private void ClearMenu_OnClick(object sender, RoutedEventArgs e) => VideoUtilities.Clear();

        private void MainView_OnLoaded(object sender, RoutedEventArgs e) {
            //ImageFormatSelector.Margin = new Thickness(0, 0, ImageConvertButton.Margin.Right + ImageConvertButton.ActualWidth + 12 ,10);
            VideoFormatSelector.Margin = new Thickness(0, 0, VideoConvertButton.Margin.Right + VideoConvertButton.ActualWidth + 12, 10);
        }
    }
}
