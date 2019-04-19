﻿using Markdig;
using Neo.Markdig.Xaml;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
