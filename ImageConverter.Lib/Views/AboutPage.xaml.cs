﻿using Markdig;
using Neo.Markdig.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImageConverter.Lib.Views {
    /// <summary>
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    public partial class AboutPage : Page {
        public AboutPage() {
            InitializeComponent();
            AboutViewer.Document = MarkdownXaml.ToFlowDocument(File.ReadAllText($"{Environment.CurrentDirectory}/about.md"), new MarkdownPipelineBuilder().UseXamlSupportedExtensions().Build());
        }
    }
}
