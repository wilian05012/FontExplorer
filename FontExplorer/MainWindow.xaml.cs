using FontExplorer.Properties;
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
using FontExplorer.Infrastructure;

namespace FontExplorer {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public new MainWindowDataContext DataContext {
            get => base.DataContext as MainWindowDataContext;
            set {
                base.DataContext = value;
            }
        }
        public MainWindow() {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            DataContext.SaveToSettings();
        }

        private void ClearSelectionCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = DataContext.SelectedFont != null;
        }

        private void ClearSelectionCmd_Executed(object sender, ExecutedRoutedEventArgs e) {
            DataContext.ClearSelectedFont();
        }

        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e) {
            FontFamily curFontFamily = e.Parameter as FontFamily;
            if(curFontFamily != null) {
                string html = DataContext.GetHtmlFor(curFontFamily);
                ClipboardHelper.CopyToClipboard(html, DataContext.SampleText);

                System.Diagnostics.Debug.Assert(Clipboard.ContainsData(DataFormats.Html));
                MessageBox.Show("Formatted text has been copied to the clipboard.", Settings.Default.ApplicationTitle, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ClearFilterCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e) { e.CanExecute = !String.IsNullOrWhiteSpace(DataContext.FontFilter); }

        private void ClearFilterCmd_Executed(object sender, ExecutedRoutedEventArgs e) { DataContext.ClearFilter(); }
    }
}
