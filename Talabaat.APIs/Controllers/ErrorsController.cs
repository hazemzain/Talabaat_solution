using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabaat.APIs.Errors;

namespace Talabaat.APIs.Controllers
{
    [Route("Errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
       // [HttpGet]
        public ActionResult Error(int code)
        {
            return NotFound(new ApiResponseError(code));
        }
    }
}
