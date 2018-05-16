using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xaml;
using XamlReader = System.Windows.Markup.XamlReader;
using Mr_Squirrely_Converters.Class;
using Markdig;
using Markdig.Wpf;
using System.Reflection;
using System.Diagnostics;

namespace Mr_Squirrely_Converters.Views {
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About {
        public About() {
            InitializeComponent();
        }

        private static MarkdownPipeline BuildPipeline() {
            return new MarkdownPipelineBuilder()
                .UseSupportedExtensions().
                Build();
        }

        private void OnLoad(object sender, RoutedEventArgs e) {
            StreamReader streamReader = new StreamReader(@"README.md");
            string Markdown = streamReader.ReadToEnd();
            string XAML = Markdig.Wpf.Markdown.ToXaml(Markdown, BuildPipeline());
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(XAML))) {
                XamlXmlReader reader = new XamlXmlReader(stream, new MyXamlSchemaContext());
                FlowDocument document = XamlReader.Load(reader) as FlowDocument;
                if (document != null) {
                    Viewer.Document = document;
                }
            }
            streamReader.Dispose();
        }

        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e) {
            Process.Start(e.Parameter.ToString());
        }
    }

    

    class MyXamlSchemaContext : XamlSchemaContext {
        public override bool TryGetCompatibleXamlNamespace(string xamlNamespace, out string compatibleNamespace) {
            if (xamlNamespace.Equals("clr-namespace:Markdig.Wpf")) {
                compatibleNamespace = $"clr-namespace:Markdig.Wpf;assembly={Assembly.GetAssembly(typeof(Markdig.Wpf.Styles)).FullName}";
                return true;
            }
            return base.TryGetCompatibleXamlNamespace(xamlNamespace, out compatibleNamespace);
        }
    }
}
