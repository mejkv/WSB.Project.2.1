using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.ExternalServices.Services.Rest.RestExceptions
{
    [Serializable]
    public class RestClientException : Exception
    {
        public RestClientException(string message, HttpStatusCode statusCode, string responseBody) : base(message)
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }

        protected RestClientException(SerializationInfo info, StreamingContext context)
        : base(info, context)
        {
        }

        public HttpStatusCode StatusCode { get; }
        public string? ResponseBody { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
