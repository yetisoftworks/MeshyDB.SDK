// <copyright file="UserSecurityQuestionUpdate.cs" company="Yeti Softworks LLC">
// Copyright (c) Yeti Softworks LLC. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining the ability to update the users security questions.
    /// </summary>
    public class UserSecurityQuestionUpdate
    {
        /// <summary>
        /// Gets or sets collection of new security questions to be replaced for user.
        /// </summary>
        public IEnumerable<SecurityQuestion> SecurityQuestions { get; set; }
    }
}
