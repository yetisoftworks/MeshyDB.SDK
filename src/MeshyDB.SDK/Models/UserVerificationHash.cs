using System;
using System.Collections.Generic;
using System.Text;

namespace MeshyDB.SDK.Models
{
    public class UserVerificationHash : UserVerification
    {
        public string Hash { get; set; }

        public DateTimeOffset Expires { get; set; }

        public string Hint { get; set; }
    }
}
