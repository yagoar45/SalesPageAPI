
using Microsoft.EntityFrameworkCore;
using Moq;
using SalesPageAPI.Data;
using SalesPageAPI.Interfaces;
using SalesPageAPI.Models;
using SalesPageAPI.Services.SalesListServices;

namespace SalesPageAPITests.SalesPageControllerTests
{
    public class ReturnSalesListServiceTest
    {
        private readonly DbContextOptions<SalesListDbContext> _options;
        private readonly SalesListDbContext _context;
        public ReturnSalesListServiceTest()
        {
            _options = new DbContextOptionsBuilder<SalesListDbContext>()
           .UseInMemoryDatabase(databaseName: "dbsaleslist")
           .Options;
            _context = new SalesListDbContext(_options);
        }

  
        [Fact]
        public async Task SearchSalesListsShouldThrowException()
        {
            // Arrange
            var service = new SalesListServices(_context);

            // Act and Assert
            await Assert.ThrowsAsync<ApplicationException>(() => service.SearchSalesLists());
        }
    }
}

