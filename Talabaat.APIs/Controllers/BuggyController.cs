using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabaat.APIs.Errors;
using Talabaat.Repository.Data;

namespace Talabaat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {
        private readonly StoreDBContext _dbContext;

        public BuggyController(StoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Products.Find(-1);
            if (product == null) return NotFound(new ApiResponseError(404));

            return Ok(product);
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            try
            {
                var product = _dbContext.Products.Find(-1);
                var productToReturn = product.ToString(); // This will throw a NullReferenceException if product is null
                return Ok(productToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An internal server error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponseError(400));
        }
        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int ?id) //for validation Error
        {
            //if (id <= 0) return BadRequest(new { Message = "Invalid ID. ID must be greater than zero." });

            return Ok();
        }
        [HttpGet("unauthorized")]
        public ActionResult GetUnauthorizedError()
        {
            return Unauthorized(new ApiResponseError(401));
        }
        
    }
}
