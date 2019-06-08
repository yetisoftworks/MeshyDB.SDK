// <copyright file="UserVerificationHash.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining user verification hash.
    /// </summary>
    public class UserVerificationHash : UserVerification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserVerificationHash"/> class.
        /// </summary>
        public UserVerificationHash()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserVerificationHash"/> class.
        /// </summary>
        /// <param name="userVerificationHash">User verfication hash to copy into a new instance.</param>
        public UserVerificationHash(UserVerificationHash userVerificationHash)
        {
            this.Username = userVerificationHash.Username;
            this.Expires = userVerificationHash.Expires;
            this.Hint = userVerificationHash.Hint;
            this.Hash = userVerificationHash.Hash;
            this.Attempt = userVerificationHash.Attempt;
        }

        /// <summary>
        /// Gets or sets hash.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Gets or sets expires.
        /// </summary>
        public DateTimeOffset Expires { get; set; }

        /// <summary>
        /// Gets or sets hint.
        /// </summary>
        public string Hint { get; set; }
    }
}
