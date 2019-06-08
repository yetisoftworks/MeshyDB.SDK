// <copyright file="UserVerificationCheck.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining elements for user verification check.
    /// </summary>
    public class UserVerificationCheck : UserVerificationHash
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserVerificationCheck"/> class.
        /// </summary>
        public UserVerificationCheck()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserVerificationCheck"/> class.
        /// </summary>
        /// <param name="userVerificationHash">User verfication hash to copy into a new instance.</param>
        public UserVerificationCheck(UserVerificationHash userVerificationHash)
        {
            this.Username = userVerificationHash.Username;
            this.Expires = userVerificationHash.Expires;
            this.Hint = userVerificationHash.Hint;
            this.Hash = userVerificationHash.Hash;
            this.Attempt = userVerificationHash.Attempt;
        }

        /// <summary>
        /// Gets or sets verification code.
        /// </summary>
        public string VerificationCode { get; set; }
    }
}
