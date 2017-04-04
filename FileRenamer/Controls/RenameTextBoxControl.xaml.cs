using System;
using System.Collections.Generic;
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

namespace FileRenamer.Controls
{
    /// <summary>
    /// RenameTextBoxControl.xaml 的交互逻辑
    /// </summary>
    public partial class RenameTextBoxControl : UserControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header), typeof (string), typeof (RenameTextBoxControl), new PropertyMetadata(default(string)));

        public string Header
        {
            get { return (string) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof (string), typeof (RenameTextBoxControl), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TipProperty = DependencyProperty.Register(
            "Tip", typeof (string), typeof (RenameTextBoxControl), new PropertyMetadata(default(string)));

        public string Tip
        {
            get { return (string) GetValue(TipProperty); }
            set { SetValue(TipProperty, value); }
        }

        public static readonly DependencyProperty TimeFormatTextProperty = DependencyProperty.Register(
            "TimeFormatText", typeof (string), typeof (RenameTextBoxControl), new PropertyMetadata(default(string)));

        public string TimeFormatText
        {
            get { return (string) GetValue(TimeFormatTextProperty); }
            set { SetValue(TimeFormatTextProperty, value); }
        }
        
        public RenameTextBoxControl()
        {
            InitializeComponent();

            TipBorder.Visibility = Visibility.Collapsed;

            RenameTextBox.GotKeyboardFocus += (sender, args) => TipBorder.Visibility = Visibility.Visible;
            RenameTextBox.LostKeyboardFocus += (sender, args) => TipBorder.Visibility = Visibility.Collapsed;
            TimeFormatTextBox.GotKeyboardFocus += (sender, args) => TipBorder.Visibility = Visibility.Visible;
            TimeFormatTextBox.LostKeyboardFocus += (sender, args) => TipBorder.Visibility = Visibility.Collapsed;
            RenameTextBox.TextChanged += RenameTextBoxOnTextChanged;
        }

        private void RenameTextBoxOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            
        }
    }
}
