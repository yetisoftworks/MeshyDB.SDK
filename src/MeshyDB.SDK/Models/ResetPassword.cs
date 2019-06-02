using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class the allows a password to be reset when forgotten
    /// </summary>
    public class ResetPassword : UserVerificationCheck
    {
        /// <summary>
        /// Defines the new password for the user to be able to log in
        /// </summary>
        public string NewPassword { get; set; }
    }
}
