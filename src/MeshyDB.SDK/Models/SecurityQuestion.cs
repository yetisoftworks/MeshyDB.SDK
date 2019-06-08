// <copyright file="SecurityQuestion.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that identifies the Security Question for a user.
    /// </summary>
    public class SecurityQuestion
    {
        /// <summary>
        /// Gets or sets the question to use as a security hint.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the answer to use for user verification.
        /// </summary>
        public string Answer { get; set; }
    }
}