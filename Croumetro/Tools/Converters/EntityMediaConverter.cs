using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Croumetro.Core.Entities.Status;

namespace Croumetro.Tools.Converters
{
    public class EntityMediaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var entity = value as Entities;
            return entity?.Media?.MediaUrlHttps;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
