using Microsoft.AspNetCore.Http;

namespace OnBoarding.api.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set;  }

        public ApiException(int statusCode) { StatusCode = statusCode; }

        public ApiException(string message, int statusCode) : base(message) { StatusCode = statusCode; }

        public ApiException(string message, int statusCode, Exception innerException) : base(message, innerException) { StatusCode = statusCode; }
    }
}
