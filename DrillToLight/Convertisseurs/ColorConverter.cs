using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DrillToLight.Convertisseurs
{
    internal class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ligne = (string)value;

            if (ligne.Contains("Z-") || ligne.Contains("(") || ligne.Contains("%") || ligne.Contains("M") || ligne.Contains("G21") || ligne.Contains("80"))
            {
                return "gray";
            }
            if (ligne.Contains("G00 Z"))
            {
                return "blue";
            }
            else
            {
                return "black";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
