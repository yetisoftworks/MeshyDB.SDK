// <copyright file="NewUser.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines a new user.
    /// </summary>
    public class NewUser
    {
        /// <summary>
        /// Gets or sets unique identifier of user.
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
        /// Gets or sets phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets email address.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets a New Password for user to be able to log in with.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets Security Questions for a registering user to be used for password recovery if questions are configured.
        /// </summary>
        public IEnumerable<SecurityQuestion> SecurityQuestions { get; set; } = new List<SecurityQuestion>();

        /// <summary>
        /// Gets or sets a value indicating whether the user is verified.
        /// </summary>
        public bool Verified { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a collection of roles to be assigned for the user.
        /// </summary>
        public IEnumerable<UserRole> Roles { get; set; }
    }
}
