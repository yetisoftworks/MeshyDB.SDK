using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK
{
    /// <summary>
    /// Class containing any static text used within the SDK
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Defines Template Api Url with tenant location for replacement when tenant is supplied
        /// </summary>
        internal const string TemplateApiUrl = "http://api.meshydb.com/{tenant}";

        /// <summary>
        /// Defines Template Auth Url with tenant location for replacement when tenant is supplied
        /// </summary>
        internal const string TemplateAuthUrl = "http://auth.meshydb.com/{tenant}";

        internal const string ApiScopes = "meshy.api";
    }
}
