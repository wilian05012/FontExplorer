using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Text;
using System.Windows.Media;
using FontExplorer.Properties;
using System.Collections;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace FontExplorer {
    public class MainWindowDataContext : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _fontFilter;
        public string FontFilter {
            get => _fontFilter;
            set {
                if(_fontFilter != value) {
                    _fontFilter = value;
                    NotifyPropertyChanged(nameof(InstalledFonts));
                    NotifyPropertyChanged(nameof(FontFilter));
                    NotifyPropertyChanged(nameof(IsFilterEmpty));
                    NotifyPropertyChanged(nameof(IsFilterNotEmpty));
                }
            }
        }

        public bool IsFilterEmpty => String.IsNullOrWhiteSpace(FontFilter);
        public bool IsFilterNotEmpty => !IsFilterEmpty;

        public IEnumerable<FontFamily> InstalledFonts =>
           new InstalledFontCollection().Families
            .Where(f => String.IsNullOrWhiteSpace(FontFilter) || f.Name.ToUpper().Contains(FontFilter.ToUpper()))
            .Select(font => new FontFamily(font.Name));

        private FontFamily _selectedFont;
        public FontFamily SelectedFont {
            get => _selectedFont;
            set {
                if (_selectedFont != value) {
                    _selectedFont = value;
                    NotifyPropertyChanged(nameof(FontSizes));
                    NotifyPropertyChanged(nameof(SelectedFont));
                    NotifyPropertyChanged(nameof(IsSelectedFontNull));
                    NotifyPropertyChanged(nameof(IsSelectedFontNotNull));
                }
            }
        }

        public bool IsSelectedFontNull => SelectedFont is null;
        public bool IsSelectedFontNotNull => !IsSelectedFontNull;

        private Color _color;
        public Color Color {
            get => _color;
            set {
                if(_color != value) {
                    _color = value;
                    NotifyPropertyChanged(nameof(Color));
                    NotifyPropertyChanged(nameof(FontSizes));
                }
            }
        }

        public const int MIN_FONT_SIZE = 10;
        public const int MAX_FONT_SIZE = 64;

        private int _fontSize;
        public int FontSize {
            get => _fontSize;
            set {
                if (_fontSize != value) {
                    _fontSize = value;
                    NotifyPropertyChanged(nameof(FontSize));
                }
            }
        }

        private string _sampleText;
        public string SampleText {
            get => _sampleText;
            set {
                if (!String.IsNullOrWhiteSpace(value) && _sampleText != value) {
                    _sampleText = value;
                    NotifyPropertyChanged(nameof(SampleText));
                }
            }
        }

        public IEnumerable<int> FontSizes => IsSelectedFontNull ? new int[] { } : Enumerable.Range(MIN_FONT_SIZE, MAX_FONT_SIZE);

        private void NotifyPropertyChanged(string propertyName) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowDataContext() {
            FontSize = Settings.Default.FontSize;
            SampleText = Settings.Default.SampleText;
            Color = Color.FromArgb(
                a: Settings.Default.Color.A,
                r: Settings.Default.Color.R,
                g: Settings.Default.Color.G,
                b: Settings.Default.Color.B);
        }

        public void ClearSelectedFont() { SelectedFont = null; }
        public void ClearFilter() { FontFilter = String.Empty; }

        private string GetHtmlColor(Color color) {
            return $"rgba({color.R}, {color.G}, {color.B}, {(double)color.A / 255})";
        }

        public string GetHtmlFor(FontFamily fontFamily) {
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine($"<p style='font-family: {fontFamily}; font-size: {FontSize}pt; color: {GetHtmlColor(Color)}'>");
            strBuilder.AppendLine(SampleText);
            strBuilder.AppendLine("</p>");

            return strBuilder.ToString();
        }

        public void SaveToSettings() {
            Settings.Default.FontSize = FontSize;
            Settings.Default.SampleText = SampleText;
            Settings.Default.Color = System.Drawing.Color.FromArgb(
                alpha: Color.A, 
                red: Color.R, 
                green: Color.G, 
                blue: Color.B);

            Settings.Default.Save();
        }
    }
}
