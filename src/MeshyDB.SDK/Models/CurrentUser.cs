// <copyright file="CurrentUser.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining elements for the currently authorized user.
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// Gets or sets the id of the user.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a collection of roles.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// Gets or sets unique identifier of anonymous user, such as a device id.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets a collection of permissions.
        /// </summary>
        public IEnumerable<string> Permissions { get; set; }
    }
}
