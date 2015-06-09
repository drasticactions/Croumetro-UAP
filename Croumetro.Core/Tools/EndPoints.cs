using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Croumetro.Core.Tools
{
    public class EndPoints
    {
        private static readonly string Croudia = "https://api.croudia.com/";

        public static readonly string OauthToken = Croudia + "oauth/token";

        public static readonly string OauthAuthorize = Croudia + "oauth/authorize";

        //Statuses
        public static readonly string StatusUpdate = Croudia + "statuses/update.json";

        public static readonly string StatusUpdateWithMedia = Croudia + "statuses/update_with_media.json";

        public static readonly string StatusDestroy = Croudia + "statuses/destroy/{0}.json";

        public static readonly string StatusShow = Croudia + "statuses/show/{0}.json";

        //Timeline
        public static readonly string PublicTimeline = Croudia + "statuses/public_timeline.json";

        //Account
        public static readonly string AccountVerify = Croudia + "account/verify_credentials.json";
    }
}
