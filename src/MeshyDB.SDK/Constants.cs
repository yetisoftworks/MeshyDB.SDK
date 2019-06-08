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
        internal const string TemplateApiUrl = "http://localhost:59487/{accountName}";

        /// <summary>
        /// Defines Template Auth Url with client location for replacement when account name is supplied.
        /// </summary>
        internal const string TemplateAuthUrl = "http://localhost:15333/{accountName}";

        /// <summary>
        /// Defines Api scoes for user when generating token.
        /// </summary>
        internal const string ApiScopes = "meshy.api offline_access openid";
    }
}
