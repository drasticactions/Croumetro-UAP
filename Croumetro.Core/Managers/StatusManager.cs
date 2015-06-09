using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
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

        public async Task<StatusEntity> UpdateStatusWithMedia(string status, string path, IRandomAccessStream stream, long? inReply,
             bool? trim)
        {
            var url = EndPoints.StatusUpdateWithMedia + "?";
            if (inReply != null) url += "&in_reply_to_status_id=" + inReply;
            if (trim != null) url += "&trim_user=" + trim;

            var imageData = new byte[stream.Size];
            for (int i = 0; i < imageData.Length; i++)
            {
                imageData[i] = (byte)stream.AsStreamForRead().ReadByte();
            }

            Stream imageStream = new MemoryStream(imageData);
            var form = new MultipartFormDataContent {{new StringContent(status), @"""status"""}};
            var t = new StreamContent(imageStream);
            t.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            form.Add(t, @"""media""", "upload.jpeg");

            var result = await _webManager.PostData(new Uri(url), form);
            return JsonConvert.DeserializeObject<StatusEntity>(result.ResultJson);
        }
    }
}
