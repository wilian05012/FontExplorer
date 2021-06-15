using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace FontExplorer.CustomConverters {
    public class ColorToSolidBrush : IValueConverter {
        private readonly static SolidColorBrush DEFAULT_BRUSH = new SolidColorBrush(SystemColors.ControlTextColor);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try { 
                return new SolidColorBrush((Color)value); 
            } catch {
                return DEFAULT_BRUSH;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return Binding.DoNothing;
        }
    }
}
