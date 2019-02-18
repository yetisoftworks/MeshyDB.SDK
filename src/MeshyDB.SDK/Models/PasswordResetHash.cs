using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines the object required to change a new password when it was forgotten
    /// </summary>
    public class PasswordResetHash
    {
        /// <summary>
        /// Defines the User name of the user that was requested
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Defines when the hash expires and a new request must be made
        /// </summary>
        public DateTimeOffset Expires { get; set; }

        /// <summary>
        /// Defines a hashed value to ensure the request was requested on behalf the correct person
        /// </summary>
        public string Hash { get; set; }
    }
}
