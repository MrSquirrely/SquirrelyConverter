using Markdig;
using Neo.Markdig.Xaml;
using System.IO;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Image_Converter.Views {
    /// <summary>
    /// Interaction logic for AboutContent.xaml
    /// </summary>
    public partial class AboutContent : UserControl {
        public AboutContent() {
            InitializeComponent();

            string content = File.ReadAllText("README.md");
            FlowDocument doc = MarkdownXaml.ToFlowDocument(content, new MarkdownPipelineBuilder().UseXamlSupportedExtensions().Build());
            AboutViewer.Document = doc;
        }
    }
}
