using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class defining a New User to be created
    /// </summary>
    public class RegisterUser
    {
        /// <summary>
        /// Instantiates a new instance of a new user
        /// </summary>
        /// <param name="username">Username for user to log in with</param>
        /// <param name="newPassword">Password for user to log in with</param>
        /// <param name="phoneNumber">Phone number of user to verify against</param>
        public RegisterUser(string username, string newPassword, string phoneNumber)
        {
            this.Username = username;
            this.NewPassword = newPassword;
            this.PhoneNumber = phoneNumber;
        }

        /// <summary>
        /// Defines the user name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Defines the first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Defines the last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Defines the phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Defines a New Password for user to be able to log in with
        /// </summary>
        public string NewPassword { get; set; }
    }
}
