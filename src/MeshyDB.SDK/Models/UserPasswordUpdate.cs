using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining the ability to update the users password
    /// </summary>
    internal class UserPasswordUpdate
    {
        /// <summary>
        /// Defines the new password to use
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Defines the previous password to valdiate it was correct
        /// </summary>
        public string PreviousPassword { get; set; }
    }
}
