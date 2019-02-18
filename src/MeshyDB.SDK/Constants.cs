namespace MeshyDB.SDK
{
    /// <summary>
    /// Class containing any static text used within the SDK
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Defines Template Api Url with client location for replacement when client key is supplied
        /// </summary>
        internal const string TemplateApiUrl = "https://api.meshydb.com/{clientKey}";

        /// <summary>
        /// Defines Template Auth Url with client location for replacement when client key is supplied
        /// </summary>
        internal const string TemplateAuthUrl = "https://auth.meshydb.com/{clientKey}";

        internal const string ApiScopes = "meshy.api offline_access";
    }
}
