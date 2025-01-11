using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabaat.APIs.Controllers;
using Talabaat.APIs.Dtos;
using Talabaat.Core.Entity;
using Talabaat.Core.Reposatory.Interfaces;
using Talabaat.Core.Specification;

namespace Talabaat.Tests
{
    public class Tests
    {
        private Mock<IGenericReository<Product>> _mockProductRepo;
        private Mock<IMapper> _mockMapper;
        private ProductsController _controller;
        private Mock<IGenericReository<ProductBrand>> _mockBrandRepo;


        [SetUp]
        public void Setup()
        {
            _mockProductRepo = new Mock<IGenericReository<Product>>();
            _mockBrandRepo = new Mock<IGenericReository<ProductBrand>>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ProductsController(_mockProductRepo.Object, _mockMapper.Object, _mockBrandRepo.Object);
        }

       [Test]
       public async Task GetProductById_2_ShouldReturnCorrectMappedProduct()
       {
           var product = new Product
           {
               Id = 1,
               Name = "Laptop1",
               Description = "High-performance laptop",
               ImageUrl = "http://example.com/laptop1.jpg",
               Price = 1200.99m,
               BrandId = 2,
               Brand = new ProductBrand { Id = 2, Name = "TechBrand" },
               CatogryId = 3,
               Catogry = new ProductCatogry { Id = 3, Name = "Electronics" }
           };
           _mockProductRepo.Setup(x => x.GetWithSpectAsync(It.IsAny<ISpecifications<Product>>()))
               .ReturnsAsync(product);
           _mockMapper.Setup(m => m.Map<Product, ProductToReturnDto>(It.IsAny<Product>()))
               .Returns(new ProductToReturnDto
               {
                   Name = product.Name,
                   Description = product.Description,
                   ImageUrl = product.ImageUrl,
                   Price = product.Price,
                   BrandId = product.BrandId,
                   Brand = product.Brand.Name,
                   CatogryId = product.CatogryId,
                   Catogry = product.Catogry.Name
               });
           var result = await _controller.GetProductById(2);
           var okResult = result.Result as OkObjectResult;
           Assert.That(okResult, Is.Not.Null);
           Assert.That(okResult.Value, Is.InstanceOf<ProductToReturnDto>());
           var mappedProduct = okResult.Value as ProductToReturnDto;
           Assert.That(mappedProduct.Name, Is.EqualTo("Laptop1"));
           Assert.That(mappedProduct.Description, Is.EqualTo("High-performance laptop"));
           Assert.That(mappedProduct.ImageUrl, Is.EqualTo("http://example.com/laptop1.jpg"));
           Assert.That(mappedProduct.Price, Is.EqualTo(1200.99m));
           Assert.That(mappedProduct.BrandId, Is.EqualTo(2));
           Assert.That(mappedProduct.Brand, Is.EqualTo("TechBrand"));
           Assert.That(mappedProduct.CatogryId, Is.EqualTo(3));
           Assert.That(mappedProduct.Catogry, Is.EqualTo("Electronics"));
       }



        [Test]
        public async Task GetProducts_ShouldReturnMappedProductDtos()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Laptop1",
                    Description = "High-performance laptop",
                    ImageUrl = "http://example.com/laptop1.jpg",
                    Price = 1200.99m,
                    BrandId = 2,
                    Brand = new ProductBrand { Id = 2, Name = "TechBrand" },
                    CatogryId = 3,
                    Catogry = new ProductCatogry { Id = 3, Name = "Electronics" }
                },
                new Product
                {
                    Id = 2,
                    Name = "Laptop2",
                    Description = "Another high-performance laptop",
                    ImageUrl = "http://example.com/laptop2.jpg",
                    Price = 1300.99m,
                    BrandId = 2,
                    Brand = new ProductBrand { Id = 2, Name = "TechBrand" },
                    CatogryId = 3,
                    Catogry = new ProductCatogry { Id = 3, Name = "Electronics" }
                }
            };

            var productDtos = new List<ProductToReturnDto>
            {
                new ProductToReturnDto
                {
                    Name = "Laptop1",
                    Description = "High-performance laptop",
                    ImageUrl = "http://example.com/laptop1.jpg",
                    Price = 1200.99m,
                    BrandId = 2,
                    Brand = "TechBrand",
                    CatogryId = 3,
                    Catogry = "Electronics"
                },
                new ProductToReturnDto
                {
                    Name = "Laptop2",
                    Description = "Another high-performance laptop",
                    ImageUrl = "http://example.com/laptop2.jpg",
                    Price = 1300.99m,
                    BrandId = 2,
                    Brand = "TechBrand",
                    CatogryId = 3,
                    Catogry = "Electronics"
                }
            };
            _mockProductRepo
                .Setup(repo => repo.GetAllWithSpectAsync(It.IsAny<ISpecifications<Product>>()))
                .ReturnsAsync(products);

            _mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products))
                .Returns(productDtos);

            // Act
            var result = await _controller.GetProducts();
            var okResult = result.Result as OkObjectResult;

            // Assert
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(productDtos));
        }
        
    }
}
