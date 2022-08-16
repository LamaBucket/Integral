using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Exceptions
{
    public class WebRequestException : Exception
    {
        public WebRequestException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public WebRequestException(string? message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public WebRequestException(string? message, Exception? innerException, HttpStatusCode statusCode) : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}
