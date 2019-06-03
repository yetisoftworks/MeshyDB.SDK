// <copyright file="RegisterUser.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining a New User to be created.
    /// </summary>
    public class RegisterUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUser"/> class.
        /// </summary>
        /// <param name="username">Username for user to log in with.</param>
        /// <param name="newPassword">Password for user to log in with.</param>
        /// <param name="phoneNumber">Phone number of user to verify against.</param>
        public RegisterUser(string username, string newPassword, string phoneNumber)
        {
            this.Username = username;
            this.NewPassword = newPassword;
            this.PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Gets or sets username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets hone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets a New Password for user to be able to log in with.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
