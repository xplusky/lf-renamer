using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FileRenamer.Modules;
using FileRenamer.Properties;

namespace FileRenamer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<FileItem> FileItems { get; } = new ObservableCollection<FileItem>();

        public MainWindow()
        {
            InitializeComponent();
            InitializeData();
            AttachEvents();
        }

        private void InitializeData()
        {
            //FileItems = new ObservableCollection<FileItem>();
            MainListView.ItemsSource = FileItems;
            InfoTextBlock.Text = "ver. " + Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
            OptionRadioButtonOnChecked(this, null);
        }

        private void AttachEvents()
        {
            MainListView.Drop += MainListViewOnDrop;
            MainListView.KeyDown += MainListViewOnKeyDown;
            ApplyButton.Click += ApplyButtonOnClick;
            ClearButton.Click += ClearButtonOnClick;
            DeleteSameNameButton.Click += DeleteSameNameButtonOnClick;
            MainTabControl.SelectionChanged += MainTabControlOnSelectionChanged;
            VisitWebsiteButton.Click += VisitWebsiteButtonOnClick;

            SpecificRenameRadioButton.Checked += OptionRadioButtonOnChecked;
            RegularRnameRadioButton.Checked += OptionRadioButtonOnChecked;
        }

        private void OptionRadioButtonOnChecked(object sender, RoutedEventArgs routedEventArgs)
        {
            foreach (FrameworkElement child in OptionStackPanel.Children)
            {
                child.Visibility = Visibility.Collapsed;
            }
            if (SpecificRenameRadioButton.IsChecked == true) SpecificRenameStackPanel.Visibility = Visibility.Visible;
            else if(RegularRnameRadioButton.IsChecked == true) RegularRnameStackPanel.Visibility = Visibility.Visible;
            
        }

        private void MainTabControlOnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            RenameTabItemHeaderStackPanel.Visibility = RenameTabItem.IsSelected ? Visibility.Visible : Visibility.Collapsed;
            OptionRaidoButtomGroupStackPanel.Visibility = RenameOptionTabItem.IsSelected? Visibility.Visible: Visibility.Collapsed;
        }
        
        private void DeleteSameNameButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            foreach (var item in FileItems
                .Where(item => item.HasSameNameFile)
                .Where(item => item.OriginFile.Exists))
            {
                try
                {
                    item.OriginFile.Delete();
                    item.Message = "已删除";
                }
                catch (Exception e)
                {
                    item.Message = $"删除失败:{e.Message}";
                }
            }
            MainListView.Items.Refresh();
        }

        private void MainListViewOnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            var index = MainListView.SelectedIndex;
            if (keyEventArgs.Key == Key.Delete && index != -1)
            {
                FileItems.RemoveAt(index);
            }
            if (index == FileItems.Count)
            {
                MainListView.SelectedIndex = index - 1;
            }
            else
            {
                MainListView.SelectedIndex = index;
            }
            
        }

        private void ClearButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            FileItems.Clear();
        }

        private void ApplyButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            foreach (var item in FileItems)
            {
                if (!string.IsNullOrWhiteSpace(item.Message))
                {
                    continue;
                }
                if(!item.CanRename || item.IsRenamed)continue;
                try
                {
                    File.Move(item.OriginFile.FullName, System.IO.Path.Combine(item.Directory,item.AlterFileName));
                }
                catch (Exception ex)
                {
                    item.Message = ex.Message;
                    continue;
                }

                item.IsRenamed = true;
                item.Message = "成功";

            }

            MainListView.Items.Refresh();
            //MessageBox.Show("OKKKKK");
            //FileItems.Clear();
        }

        private async void MainListViewOnDrop(object sender, DragEventArgs dragEventArgs)
        {
            var paths = (string[])dragEventArgs.Data.GetData(DataFormats.FileDrop);
            if (paths == null) return;
            var sb = (Storyboard) Resources["LoadingSpinStoryboard"];
            sb.Begin();
            var list = await Task.Run(() =>
            {
                var list2 = new List<FileItem>();
                foreach (var path in paths
                    .Where(File.Exists))
                {
                    var fi = new FileItem(path);
                    list2.Add(fi);
                    fi.RenameReview();
                }
                return list2;
            });
            foreach (var item in list)
            {
                FileItems.Add(item);
            }
            sb.Stop();
        }

        private void VisitWebsiteButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Process.Start("http://leaful.com/file-renamer");
        }
    }
}
