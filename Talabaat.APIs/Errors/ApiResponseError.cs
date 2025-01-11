
namespace Talabaat.APIs.Errors
{
    public class ApiResponseError
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponseError(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message??GetDefualtMessageFromStatusCode( statusCode);
        }

        private string? GetDefualtMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request - The server could not understand the request due to invalid syntax.",
                401 => "Unauthorized - The client must authenticate itself to get the requested response.",
                403 => "Forbidden - The client does not have access rights to the content.",
                404 => "Not Found - The server can not find the requested resource.",
                500 => "Internal Server Error - The server has encountered a situation it doesn't know how to handle.",
                _ => null
            };
        }
    }
}
