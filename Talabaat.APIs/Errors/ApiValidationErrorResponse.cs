namespace Talabaat.APIs.Errors
{
    public class ApiValidationErrorResponse:ApiResponseError
    {
       public IEnumerable<String> Errors { get; set; }

        public ApiValidationErrorResponse():base(400)
        {
            
        }
        
    }
}
