using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Pekalicious.SrTracker.ViewModels
{
    public class CutoffConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Direction > 0)
            {
                return ((int)value) > int.Parse(parameter.ToString());
            }
            else if (Direction < 0)
            {
                return ((int)value) < int.Parse(parameter.ToString());
            }
            return ((int)value) == int.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public int Cutoff { get; set; }
        public int Direction { get; set; }
    }
}
