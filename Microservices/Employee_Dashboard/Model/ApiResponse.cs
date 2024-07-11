using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Employee_Dashboard.Model
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

    public record ApiResult<T>
    {
        /// <summary>
        /// Exception/Success message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Status of the request
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Any object of type can be passed as Data 
        /// </summary>
        public T Data { get; set; }
    }
}
