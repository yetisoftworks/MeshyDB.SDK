// <copyright file="UserVerificationCheck.cs" company="Yetisoftworks LLC">
// Copyright (c) Yetisoftworks LLC. All rights reserved.
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
        /// <param name="userVerificationCheck">User verfication hash to copy into a new instance.</param>
        public UserVerificationCheck(UserVerificationCheck userVerificationCheck)
        {
            this.Username = userVerificationCheck.Username;
            this.Expires = userVerificationCheck.Expires;
            this.Hint = userVerificationCheck.Hint;
            this.Hash = userVerificationCheck.Hash;
            this.VerificationCode = userVerificationCheck.VerificationCode;
        }

        /// <summary>
        /// Gets or sets verification code.
        /// </summary>
        public int VerificationCode { get; set; }
    }
}
