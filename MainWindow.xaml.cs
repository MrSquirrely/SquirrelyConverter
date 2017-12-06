#region LICENSE
//WebP Converter || Convert your images to WebP
//    Copyright(C) 2017  James Ferguson<MrSquirrely.net>


//   This program is free software: you can redistribute it and/or modify

//   it under the terms of the GNU General Public License as published by

//   the Free Software Foundation, either version 3 of the License, or

//   (at your option) any later version.

//   This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.

//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Windows;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.Specialized;

namespace SquirrelyConverter {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {

        public MainWindow() {
            InitializeComponent();
            ((INotifyCollectionChanged)EncodeItems.Items).CollectionChanged += EncodeItems_DataContextChanged;
            Options.FirstRun();
            var left = Mouse.LeftButton;
        }

        private void ItemsDropped(object sender, DragEventArgs e) => Utils.ItemsDropped(EncodeItems, e.Data.GetData(DataFormats.FileDrop) as string[]);
        private void MoveWindow_MouseDown(object sender, MouseButtonEventArgs e) { if (e.ChangedButton == MouseButton.Left) DragMove(); }
        private void EncodeItems_DataContextChanged(object sender, NotifyCollectionChangedEventArgs e) => ItemsLoadedLabel.Content = $"Items Loaded: {EncodeItems.Items.Count}";
        private void ClearButton_Click(object sender, RoutedEventArgs e) => EncodeItems.Items.Clear();
        private void MetroWindow_Closed(object sender, EventArgs e) => Utils.DisposeToast();
        private void SettingsButton_Click(object sender, RoutedEventArgs e) => Utils.OpenSettings();
        private void MetroWindow_KeyDown(object sender, KeyEventArgs e) => Utils.ClearItems(EncodeItems.SelectedIndex, EncodeItems, e);
        private void ReportBug_OnClick(object sender, RoutedEventArgs e) => Process.Start("https://github.com/MrSquirrely/SquirrelyConverter/issues/new");
        private void EncodeButton_Click(object sender, RoutedEventArgs e) => Utils.StartEncode();

    }

}
