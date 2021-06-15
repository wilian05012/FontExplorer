using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FontExplorer {
    public static class MainWindowCommands {
        private static RoutedUICommand _clearSelectedFontCommand = new RoutedUICommand(
            text: "Clear Selection",
            name: "ClearSelectedFontCmd",
            ownerType: typeof(MainWindowCommands));
        private static RoutedUICommand _clearFontFilterCommand = new RoutedUICommand(
            text: "Clear Filter",
            name: "ClearFontFilterCmd",
            ownerType: typeof(MainWindowCommands));

        public static RoutedUICommand ClearSelectedFontCommand => _clearSelectedFontCommand;
        public static RoutedUICommand ClearFontFilterCommand => _clearFontFilterCommand;
    }
}
