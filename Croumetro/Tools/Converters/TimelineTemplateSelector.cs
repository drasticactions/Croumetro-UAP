using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Croumetro.Core.Entities.Status;

namespace Croumetro.Tools.Converters
{
    public class TimelineTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TimelineActivityDataTemplate { get; set; }

        public DataTemplate SpreadTimelineDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item,
            DependencyObject container)
        {
            var status = item as StatusEntity;
            if (status == null) return null;
            return status.IsSpreaded ? SpreadTimelineDataTemplate : TimelineActivityDataTemplate;
        }
    }
}
