using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabaat.Core.Reposatory.Interfaces;

namespace Talabaat.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

    }
}
