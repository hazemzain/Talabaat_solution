using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Allure.Commons;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using NUnit.Framework.Legacy;
using Talabaat.APIs.Controllers;
using Talabaat.Core.Entity;
using Talabaat.Core.Reposatory.Interfaces;
using Talabaat.APIs.Dtos;
using Talabaat.Core.Specification;

namespace Talabaat.Tests.Test
{
    [TestFixture]
    [AllureNUnit]
    
    public class ProductEndPointTests
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
        public async Task GetBrands_CallGetBrandsEndPoint_ShoudReturnAllBrands()
        {
            // Arrange
            var mockBrands = new List<ProductBrand>
            {
                new() { Id = 1, Name = "Brand1" }, new() { Id = 2, Name = "Brand2" }
            };

            _mockBrandRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(mockBrands);

            // Act
            var result = await _controller.GetBrands();

            // Assert
            Assert.That(result.Result,Is.InstanceOf<OkObjectResult>());
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult,Is.Not.Null);
            Assert.That(okResult.StatusCode,Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<IEnumerable<ProductBrand>>());
            var returnedBrands = okResult.Value as IEnumerable<ProductBrand>;
            Assert.That(returnedBrands.Count(),Is.EqualTo(mockBrands.Count));
            CollectionAssert.AreEquivalent(mockBrands, returnedBrands);
        }

        [Test]
        public async Task GetBrand_ShouldReturnEmptyList_WhenNoBrandsAvalibleAsync()
        {
           var MockBrand = new List<ProductBrand>();
           _mockBrandRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(MockBrand);
           var result = await _controller.GetBrands();
           Assert.That(result.Value, Is.Null);
           var okResult = result.Result as OkObjectResult;
           Assert.That(okResult.StatusCode, Is.EqualTo(200));

        }
        //it will fail becouse End Point not handle Throw Exception
        // i will handle it 
        [Test]
        [AllureDescription("This test verifies that the GetBrands endpoint returns Internal Server Error When AnyException Accure in Method  .")]
        [AllureLabel("Fail","the RootCause is that i do not handle Exceptions")]

        public async Task GetBrand_ShouldReturnInternalServerError_WhenRepositoryThrowsException()
        {
           
                // Arrange
                _mockBrandRepo.Setup(repo => repo.GetAllAsync()).ThrowsAsync(new Exception("Repository error"));

                // Act
                var result = await _controller.GetBrands();

                // Assert
                var objectResult = result.Result as ObjectResult;
                Assert.That(objectResult, Is.Not.Null);
                Assert.That(objectResult.StatusCode, Is.EqualTo(500));
                Assert.That(objectResult.Value, Is.EqualTo("An error occurred while processing your request."));
            
        }

        [Test]
        public async Task GetBrand_ShouldHandleLargeNumberOfData()
        {
            var mockBrands = Enumerable.Range(1, 1000).Select(i => new ProductBrand
            {
                Id = i,
                Name = $"Brand{i}"
            }).ToList();
            _mockBrandRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(mockBrands);
            var Result = _controller.GetBrands();
            var okResult = (await Result).Result as OkObjectResult;
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<IEnumerable<ProductBrand>>());
            var returnedBrands = okResult.Value as IEnumerable<ProductBrand>;
            Assert.That(returnedBrands.Count(), Is.EqualTo(mockBrands.Count));
            CollectionAssert.AreEquivalent(mockBrands, returnedBrands);

        }

        [Test]
        public async Task GetBrand_ShouldHandleLargeNumberOfDataUsingFluentAssertions()
        {
            var mockBrands = Enumerable.Range(1, 1000).Select(i => new ProductBrand
            {
                Id = i,
                Name = $"Brand{i}"
            }).ToList();
            _mockBrandRepo.Setup(x => x.GetAllAsync()).ReturnsAsync(mockBrands);
            var Result = _controller.GetBrands();
            var okResult = (await Result).Result as OkObjectResult;
            okResult.Should().NotBe(null);
           // Assert.That(okResult, Is.Not.Null);
           okResult.StatusCode.Should().Be(200);
           okResult.Value.Should().BeAssignableTo<IEnumerable<ProductBrand>>()
               .Which.Should().HaveCount(mockBrands.Count)
               .And.BeEquivalentTo(mockBrands);
            // Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.InstanceOf<IEnumerable<ProductBrand>>());
            var returnedBrands = okResult.Value as IEnumerable<ProductBrand>;
            returnedBrands.Count().Should().Be(mockBrands.Count);
            //Assert.That(returnedBrands.Count(), Is.EqualTo(mockBrands.Count));
           // CollectionAssert.AreEquivalent(mockBrands, returnedBrands);

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

        [Test]
        public async Task GetProducts_ShouldReturnEmptyList_WhenNoProductsExist()
        {
            var mockProducts = new List<Product>();
            _mockProductRepo
                .Setup(repo => repo.GetAllWithSpectAsync(It.IsAny<ISpecifications<Product>>()))
                .ReturnsAsync(mockProducts);
            _mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(mockProducts))
                .Returns(new List<ProductToReturnDto>());
            var result = await _controller.GetProducts();
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var products = okResult.Value as List<ProductToReturnDto>;
            Assert.That(products, Is.Empty);
            okResult.StatusCode.Should().Be(200);

        }

        [Test]
        //the ProductsController.GetProducts method does not handle exceptions. Instead, it directly calls 
        [AllureLabel("Fail", "the RootCause is that the ProductsController.GetProducts method does not handle exceptions. Instead, it directly calls ")]
        public async Task GetProduct_ShouldReturnInternalServerError_WhenExceptionIsThrowInMethod()
        {
            _mockProductRepo
                .Setup(repo => repo.GetAllWithSpectAsync(It.IsAny<ISpecifications<Product>>()))
                .ThrowsAsync(new Exception("Internal Server Error"));
            var result = await _controller.GetProducts();
            var okResult = result.Result as OkObjectResult;
            Assert.That(okResult.StatusCode, Is.EqualTo(500));
        }

        [Test]
        public async Task GetProducts_ShouldReturnLargeDataset_WhenRepositoryReturnsLargeList()
        {
            // Arrange
            var mockProducts = Enumerable.Range(1, 1000).Select(i => new Product
            {
                Id = i,
                Name = $"Product{i}",
                Description = $"Description{i}",
                Price = i * 10
            }).ToList();

            var mockProductDtos = mockProducts.Select(p => new ProductToReturnDto
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList();

            _mockProductRepo
                .Setup(repo => repo.GetAllWithSpectAsync(It.IsAny<ISpecifications<Product>>()))
                .ReturnsAsync(mockProducts);

            _mockMapper
                .Setup(mapper => mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(mockProducts))
                .Returns(mockProductDtos);

            // Act
            var result = await _controller.GetProducts();
            var okResult = result.Result as OkObjectResult;

            // Assert
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(mockProductDtos);
        }
        



    }
}
