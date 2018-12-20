using MeshyDB.SDK.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MeshyDB.SDK.Models
{
    public class HttpServiceRequest
    {
        public Uri RequestUri { get; set; }
        public HttpMethod Method { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
        public RequestDataFormat RequestDataFormat { get; set; } = RequestDataFormat.Json;
        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}
