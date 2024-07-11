using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnBoarding.api.Application.Responses
{
    public class ApiResponse<T> : ObjectResult
    {
        /// <summary>
        /// Constructor with ApiResult value and statusCode
        /// </summary>
        /// <param name="value">value of type ApiResult</param>
        /// <param name="statusCode">statusCode from base</param>
        public ApiResponse(ApiResult<T> value, int statusCode) : base(value)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Constructor with ApiResult value and statusCode
        /// </summary>
        /// <param name="value">value of type ApiResult</param>
        /// <param name="statusCode">statusCode from base</param>
        public ApiResponse(bool status, T value, int statusCode) : this(new ApiResult<T>() { Status = status, Data = value }, statusCode) { }

        /// <summary>
        /// Constructor with base value,message,status and statusCode.Instance created for ApiResult from constructor.
        /// </summary>
        /// <param name="value">value of type ApiResult</param>
        /// <param name="message">statusCode from base</param>
        /// <param name="status">status from base</param>
        /// <param name="statusCode">statusCode from base</param>
        public ApiResponse(string message, bool status, T value, int statusCode) : this(new ApiResult<T>() { Message = message, Status = status, Data = value }, statusCode) { }

        /// <summary>
        /// Constructor with base value,message,status and statusCode.Instance created for ApiResult from constructor.
        /// </summary>
        /// <param name="value">value of type ApiResult</param>
        /// <param name="statusCode">statusCode from base</param>
        public ApiResponse(T value, int statusCode) : this(new ApiResult<T>() { Status = true, Data = value }, statusCode) { }

        public ApiResult<T> Result => Value as ApiResult<T>;
    }
}
