using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    /// <summary>
    /// Class that defines a user
    /// </summary>
    public class User
    {
        /// <summary>
        /// Defines the id of the user
        /// </summary>
        public string Id { get; set; }

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
        /// Defines if the user is verified or not
        /// </summary>
        public bool Verified { get; set; }

        /// <summary>
        /// Defines if the user is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Defines the phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Defines the roles assigned
        /// </summary>
        public IEnumerable<string> Roles { get; set; }
    }
}
