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

            if (ligne.Contains("F"))
            {
                return "blue";
            }
            if(ligne.Contains("S"))
            {
                return "red";
            }
            return "black";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
