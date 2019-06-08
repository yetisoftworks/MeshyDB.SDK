// <copyright file="Constants.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

namespace MeshyDB.SDK
{
    /// <summary>
    /// Class containing any static text used within the SDK.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Defines Template Api Url with client location for replacement when account name is supplied.
        /// </summary>
        internal const string TemplateApiUrl = "https://api.meshydb.com/{accountName}";

        /// <summary>
        /// Defines Template Auth Url with client location for replacement when account name is supplied.
        /// </summary>
        internal const string TemplateAuthUrl = "https://auth.meshydb.com/{accountName}";

        /// <summary>
        /// Defines Api scoes for user when generating token.
        /// </summary>
        internal const string ApiScopes = "meshy.api offline_access openid";
    }
}
