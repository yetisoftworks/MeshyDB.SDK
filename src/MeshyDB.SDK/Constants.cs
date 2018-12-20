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
        internal const string TemplateApiUrl = "https://api.meshydb.com/{tenant}";

        /// <summary>
        /// Defines Template Auth Url with tenant location for replacement when tenant is supplied
        /// </summary>
        internal const string TemplateAuthUrl = "https://auth.meshydb.com/{tenant}";

        internal const string ApiScopes = "meshy.api";
    }
}
