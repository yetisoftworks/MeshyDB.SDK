using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models.Authentication
{
    /// <summary>
    /// Class to identifiy what token to revoke
    /// </summary>
    internal class TokenRevocation
    {
        /// <summary>
        /// Token to revoke
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Type of token to revoke
        /// </summary>
        /// <remarks>Revoking refresh token</remarks>
        [JsonProperty("token_type_hint")]
        public string TokenTypeHint { get; set; }

        /// <summary>
        /// Public identifier of client to revoke on behalf of
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
    }
}
