using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabaat.Core.Entity;
using Talabaat.Core.Reposatory.Interfaces;
using Talabaat.Repository.Data;
using Talabaat.Core.Specification;
using Talabaat.Core.Specification.ProductSpecifications;
using AutoMapper;
using Talabaat.APIs.Dtos;

namespace Talabaat.APIs.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IGenericReository<Product> _productRepo;
        private readonly IMapper _mapper;
        private IGenericReository<ProductBrand> _BrandRepo;

        public ProductsController(IGenericReository<Product>ProductRepo,IMapper mapper,IGenericReository<ProductBrand>BrandRepo) {
            _productRepo = ProductRepo;
            _mapper = mapper;
            _BrandRepo=BrandRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec = new ProductAndBrindAndCatogrySpecifications();
            var products = await _productRepo.GetAllWithSpectAsync(Spec);
            var result = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
            return Ok(result);


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id) {
            var Spec = new ProductAndBrindAndCatogrySpecifications(id);

            var producta = await _productRepo.GetWithSpectAsync(Spec);
            if (producta == null) {
                return NotFound(new {message ="Not Found", StatusCode=404});
            }
           var result_map= _mapper.Map<Product,ProductToReturnDto>(producta);
            return Ok(result_map);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrands()
        {
            var brands = await _BrandRepo.GetAllAsync();
            return Ok(brands);
        }
    }
}
