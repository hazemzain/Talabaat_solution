using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
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
    }
}
