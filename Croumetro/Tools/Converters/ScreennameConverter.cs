using System;
using Windows.UI.Xaml.Data;

namespace Croumetro.Tools.Converters
{
    public class ScreennameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value == null ? null : $"@{value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
