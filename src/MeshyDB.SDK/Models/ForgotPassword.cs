using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining elements required while forgetting a password
    /// </summary>
    internal class ForgotPassword
    {
        /// <summary>
        /// User name that is requested which a password was forgotten
        /// </summary>
        public string Username { get; set; }
    }
}
