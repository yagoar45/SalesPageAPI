using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;
using SalesPageAPI.Data;
using SalesPageAPI.Models;
using SalesPageAPI.Services.SalesListServices;

namespace SalesPageAPITests.SalesPageControllerTests
{
    public class AddSalesListServiceTest
    {
        private readonly DbContextOptions<SalesListDbContext> _options;
        private readonly SalesListDbContext _context;
        private readonly SalesListServices _service;
        public AddSalesListServiceTest()
        {
            // Configure the database em memory
            _options = new DbContextOptionsBuilder<SalesListDbContext>()
                .UseInMemoryDatabase(databaseName: "dbsaleslist")
                .Options;

            // Inicialize the dbContext
            _context = new SalesListDbContext(_options);

            // Inicialize the service
            _service = new SalesListServices(_context);
        }

        [Fact]
        public async Task AddSalesListWhenCalledWithValidDataShouldReturnListOfSalesListModels()
        {
            // Arrange
            var salesLists = new SalesListsModel
            {
                SalesLists = new List<SalesListModel>
            {
                new SalesListModel
                {
                    Seller = "Seller 1",
                    Product = "Product 1",
                    Type = 1
                },
                new SalesListModel
                {
                    Seller = "Seller 2",
                    Product = "Product 2",
                    Type = 1
                }
            }
            };

            // Act
            var result = await _service.AddSalesList(salesLists);

            // Assert
            Assert.IsType<List<SalesListModel>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddSalesList_WhenCalledWithInvalidData_ShouldThrowApplicationException()
        {
            // Arrange
            var salesLists = new SalesListsModel
            {
                SalesLists = new List<SalesListModel>
            {
                new SalesListModel
                {
                    Seller = "Seller 1",
                    Product = "Product 1",
                    Type = 1
                },
                new SalesListModel
                {
                    Seller = "Seller 2",
                    Product = "Product 1",
                    Type = 2,
                }
            }
            };

            // Act and Assert
            await Assert.ThrowsAsync<ApplicationException>(() => _service.AddSalesList(salesLists));
        }
    }
}