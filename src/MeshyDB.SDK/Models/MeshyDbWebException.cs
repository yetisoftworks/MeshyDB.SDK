using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MeshyDB.SDK.Models
{
    public class MeshyDbWebException : HttpRequestException
    {
        public MeshyDbWebException(string message, HttpRequestException e)
            : base(message, e)
        {
        }
    }
}
