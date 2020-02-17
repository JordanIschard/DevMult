using Fourplaces.Model;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    class IdToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int id_image = (int)value;
            return Path.IMAGES + "/" + id_image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
