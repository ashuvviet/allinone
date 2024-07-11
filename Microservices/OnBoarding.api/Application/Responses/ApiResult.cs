namespace OnBoarding.api.Application.Responses
{
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
