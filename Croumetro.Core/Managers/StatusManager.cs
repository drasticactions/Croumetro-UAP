using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Core.Entities.Status;
using Croumetro.Core.Interfaces;
using Croumetro.Core.Tools;
using Newtonsoft.Json;

namespace Croumetro.Core.Managers
{
    public class StatusManager
    {
        private IWebManager _webManager;

        public StatusManager(IWebManager webManager)
        {
            _webManager = webManager;
        }

        public StatusManager()
    : this(new WebManager())
        {
        }

        public async Task<List<StatusEntity>> GetHomeTimeline(bool? trim, long? sinceId, long? maxId, int? count)
        {
            var url = EndPoints.PublicTimeline + "?";
            if (sinceId != null) url += "&since_id=" + sinceId;
            if (maxId != null) url += "&max_id=" + maxId;
            if (count != null) url += "&count=" + count;

            var result = await _webManager.GetData(new Uri(url));
            return JsonConvert.DeserializeObject<List<StatusEntity>>(result.ResultJson);
        }
    }
}
