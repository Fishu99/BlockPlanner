using BlockPlanner.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BlockPlanner.Converters
{
    public class SaveAndExitModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
            {
                throw new InvalidOperationException("Target must be a button type.");
            }

            var settingsMode = (int)value;
            var buttonParameter = (string)parameter;

            if (buttonParameter == "addButton")
            {
                return settingsMode == (int)PlanCreatorMode.Add ? "Visible" : "Hidden";
            }
            else
            {
                return settingsMode == (int)PlanCreatorMode.Modify ? "Visible" : "Hidden";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
