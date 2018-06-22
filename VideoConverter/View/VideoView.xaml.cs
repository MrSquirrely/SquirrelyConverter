using System;
using System.Windows;
using System.Windows.Controls;
using VideoConverter.Class;

namespace VideoConverter.View {
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class VideoView : UserControl {
        public VideoView() {
            InitializeComponent();
            VideoUtilities.VideoListView = VideoFiles;
            VideoUtilities.VideoView = this;
        }

        private void VideoConvertButton_OnClick(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }

        private void VideoFiles_OnDrop(object sender, DragEventArgs e) {
            throw new NotImplementedException();
        }
    }
}
