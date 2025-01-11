namespace Talabaat.APIs.Errors
{
    public class ApiExceptionResponce : ApiResponseError
    {
        public string ?Details { get; set; }
        public ApiExceptionResponce(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details=details;
        }
    }
}
