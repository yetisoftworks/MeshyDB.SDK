// <copyright file="SecurityQuestionHash.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that identifies the Security Question for a user.
    /// </summary>
    public class SecurityQuestionHash
    {
        /// <summary>
        /// Gets or sets the question to use as a security hint.
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        /// Gets or sets the answer hash to use for user verification.
        /// </summary>
        public string AnswerHash { get; set; }
    }
}
