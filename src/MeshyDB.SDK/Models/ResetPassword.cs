// <copyright file="ResetPassword.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class the allows a password to be reset when forgotten.
    /// </summary>
    public class ResetPassword : UserVerificationCheck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPassword"/> class.
        /// </summary>
        public ResetPassword()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPassword"/> class.
        /// </summary>
        /// <param name="userVerificationHash">User verification hash to copy into a new instance.</param>
        public ResetPassword(UserVerificationHash userVerificationHash)
        {
            this.Username = userVerificationHash.Username;
            this.Expires = userVerificationHash.Expires;
            this.Hint = userVerificationHash.Hint;
            this.Hash = userVerificationHash.Hash;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPassword"/> class.
        /// </summary>
        /// <param name="userVerificationCheck">User verification check to copy into a new instance.</param>
        public ResetPassword(UserVerificationCheck userVerificationCheck)
        {
            this.Username = userVerificationCheck.Username;
            this.Expires = userVerificationCheck.Expires;
            this.Hint = userVerificationCheck.Hint;
            this.Hash = userVerificationCheck.Hash;
            this.VerificationCode = userVerificationCheck.VerificationCode;
        }

        /// <summary>
        /// Gets or sets the new password for the user to be able to log in.
        /// </summary>
        public string NewPassword { get; set; }
    }
}
