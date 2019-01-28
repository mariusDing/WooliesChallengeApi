using AutoMapper;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WooliesChallengeApi.Application.Infrastructure;
using WooliesChallengeApi.Application.Products.Model;
using WooliesChallengeApi.Application.Products.Queries;
using WooliesChallengeApi.Application.ShopHistories.Model;
using WooliesChallengeApi.Application.Users.Models;
using WooliesChallengeApi.Application.Users.Queries;
using WooliesChallengeApi.Options;
using WooliesChallengeApi.ViewModels;
using Xunit;

namespace WooliesChallengeApi.Test
{
    public class TestProductHandler
    {
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<IWooliesClient> _client = new Mock<IWooliesClient>();
        private readonly CancellationToken _cancellationToken;

        public TestProductHandler()
        {
            _cancellationToken = new CancellationToken();

            _mockMapper.Setup(x => x.Map<List<ProductVM>>(It.IsAny<List<Product>>()))
                       .Returns((List<Product> source) => source.Select(s => new ProductVM() { Name = s.Name, Price = s.Price, Quantity = s.Quantity }).ToList());

            var products = new List<Product> {
                new Product() { Name = "Product A", Price = 1, Quantity = 0 },
                new Product() { Name = "Product B", Price = 2, Quantity = 0 },
                new Product() { Name = "Product C", Price = 3, Quantity = 0 },
            };

            var shopperHistories = new List<ShopperHistory>{
                new ShopperHistory()
                {
                    CustomerId = 1,
                    Products = new List<Product>
                    {
                        new Product() { Name = "Product A", Price = 1, Quantity = 1 },
                        new Product() { Name = "Product B", Price = 2, Quantity = 3 },
                    }
                },
                new ShopperHistory()
                {
                    CustomerId = 2,
                    Products = new List<Product>
                    {
                        new Product() { Name = "Product B", Price = 2, Quantity = 5 },
                        new Product() { Name = "Product C", Price = 3, Quantity = 2 },
                    }
                }
            };

            _client.Setup(x => x.GetProducts()).Returns(Task.FromResult(products));
            _client.Setup(x => x.GetShopHistories()).Returns(Task.FromResult(shopperHistories));
        }

        [Fact]
        public async void Should_GetProductsQueryHandler_ReturnProductsSortedByHighPrices_WhenSortOptionsIsHigh()
        {
            // Arrange
            var query = new GetProductsQuery() { SortOption = Application.Enum.SortOptions.High};

            var handler = new GetProductsQueryHandler(_mockMapper.Object, _client.Object);

            // Action
            var result = await handler.Handle(query, _cancellationToken);

            // Assert
            Assert.Equal("Product C", result[0].Name);
            Assert.Equal("Product B", result[1].Name);
            Assert.Equal("Product A", result[2].Name);
        }

        [Fact]
        public async void Should_GetProductsQueryHandler_ReturnProductsSortedByLowPrices_WhenSortOptionsIsLow()
        {
            // Arrange
            var query = new GetProductsQuery() { SortOption = Application.Enum.SortOptions.Low };

            var handler = new GetProductsQueryHandler(_mockMapper.Object, _client.Object);

            // Action
            var result = await handler.Handle(query, _cancellationToken);

            // Assert
            Assert.Equal("Product A", result[0].Name);
            Assert.Equal("Product B", result[1].Name);
            Assert.Equal("Product C", result[2].Name);
        }

        [Fact]
        public async void Should_GetProductsQueryHandler_ReturnProductsSortedByNameAscending_WhenSortOptionsIsAscending()
        {
            // Arrange
            var query = new GetProductsQuery() { SortOption = Application.Enum.SortOptions.Ascending };

            var handler = new GetProductsQueryHandler(_mockMapper.Object, _client.Object);

            // Action
            var result = await handler.Handle(query, _cancellationToken);

            // Assert
            Assert.Equal("Product A", result[0].Name);
            Assert.Equal("Product B", result[1].Name);
            Assert.Equal("Product C", result[2].Name);
        }

        [Fact]
        public async void Should_GetProductsQueryHandler_ReturnProductsSortedByHighPrices_WhenSortOptionsIsDescending()
        {
            // Arrange
            var query = new GetProductsQuery() { SortOption = Application.Enum.SortOptions.Descending };

            var handler = new GetProductsQueryHandler(_mockMapper.Object, _client.Object);

            // Action
            var result = await handler.Handle(query, _cancellationToken);

            // Assert
            Assert.Equal("Product C", result[0].Name);
            Assert.Equal("Product B", result[1].Name);
            Assert.Equal("Product A", result[2].Name);
        }

        [Fact]
        public async void Should_GetProductsQueryHandler_ReturnProductsSortBypopularity_WhenSortOptionsIsRecommended()
        {
            // Arrange
            var query = new GetProductsQuery() { SortOption = Application.Enum.SortOptions.Recommended };

            var handler = new GetProductsQueryHandler(_mockMapper.Object, _client.Object);

            // Action
            var result = await handler.Handle(query, _cancellationToken);

            // Assert
            Assert.Equal("Product B", result[0].Name);
            Assert.Equal("Product C", result[1].Name);
            Assert.Equal("Product A", result[2].Name);
        }
    }
}
