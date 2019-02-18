using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining a New User to be created
    /// </summary>
    public class NewUser : User
    {
        /// <summary>
        /// Instantiates a new instance of a new user
        /// </summary>
        public NewUser()
        {

        }

        /// <summary>
        /// Instantiates a new instance of a new user
        /// </summary>
        /// <param name="username">Username for user to log in with</param>
        /// <param name="newPassword">Password for user to log in with</param>
        public NewUser(string username, string newPassword)
        {
            this.Username = username;
            this.NewPassword = newPassword;
        }

        /// <summary>
        /// Defines a New Password for user to be able to log in with
        /// </summary>
        public string NewPassword { get; set; }
    }
}
