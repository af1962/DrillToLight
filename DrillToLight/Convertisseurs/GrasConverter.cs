using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DrillToLight.Convertisseurs;

internal class GrasConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string? ligne = value as string;
        if (ligne.Contains('F') || ligne.Contains('S'))
        {
            return "Bold";
        }
        else
        {
            return "Normal";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
