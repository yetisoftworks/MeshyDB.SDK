using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeshyDb.SDK.Services
{
    /// <summary>
    /// Defines methods for authentication tokens
    /// </summary>
    internal interface ITokenService
    {
        /// <summary>
        /// Gets OAuth token for currently established client credentials
        /// </summary>
        /// <returns>Current token for client credentials</returns>
        Task<string> GetOAuthTokenAsync();
    }
}
