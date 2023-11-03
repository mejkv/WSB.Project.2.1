using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AirShop.ExternalServices
{
    public class RestClientException : Exception
    {
        public RestClientException(string message, HttpStatusCode statusCode, string responseBody) : base(message)
        {
            this.StatusCode = statusCode;
            this.ResponseBody = responseBody;
        }

        public HttpStatusCode StatusCode { get; }
        public string ResponseBody { get; }
    }
}
