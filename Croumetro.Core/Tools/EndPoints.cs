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

        //Account
        public static readonly string AccountVerify = Croudia + "account/verify_credentials.json";
    }
}
