using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework.Legacy;
using Talabaat.APIs.Controllers;
using Talabaat.Core.Entity;
using Talabaat.Core.Reposatory.Interfaces;

namespace Talabaat.Tests.Test
{
    [TestFixture]
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
           Assert.That(result, Is.Empty);
           var okResult = result.Result as OkObjectResult;
           Assert.That(okResult.StatusCode, Is.EqualTo(200));

        }
        //it will fail becouse End Point not handle Throw Exception
        // i will handle it 
        [Test]
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

    }
}
