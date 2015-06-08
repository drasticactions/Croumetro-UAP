using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Croumetro.Core.Entities.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Croumetro.Core.Entities
{
    public class CroudiaAuthEntity
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [PrimaryKey]
        public long UserId { get; set; }

        [ManyToOne]
        public UserEntity User { get; set; }

        public bool IsDefault { get; set; }

        public long ExpireTime { get; set; }
    }
}
