using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    public class UserVerificationCheck : UserVerificationHash
    {
        public UserVerificationCheck() { }

        public UserVerificationCheck(UserVerificationHash userVerificationHash)
        {
            this.Username = userVerificationHash.Username;
            this.Expires = userVerificationHash.Expires;
            this.Hint = userVerificationHash.Hint;
            this.Hash = userVerificationHash.Hash;
        }

        public int VerificationCode { get; set; }
    }
}
