using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using BlockPlanner.Models;

namespace BlockPlanner.Converters
{
    public class ButtonSelectionColorConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
            {
                throw new InvalidOperationException("Target must be a brush type.");
            }

            var buttonWeekDayId = (int)value;
            var buttonParameter = (string)parameter;
            var parameterWeekDayId = WeekDayMethods.TryGetWeekDay(buttonParameter).GetId();

            return buttonWeekDayId == parameterWeekDayId ? "#FF90EE90" : //LightGreen
                "#FFD3D3D3"; //LightGrey
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
