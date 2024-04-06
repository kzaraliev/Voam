using Moq;
using Voam.Core.Models.Order;
using Voam.Core.Services;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Tests
{
    [TestFixture]
    public class OrderItemTests
    {
        private Mock<IRepository> _repositoryMock;
        private OrderItemService _orderItemService;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRepository>();
            _orderItemService = new OrderItemService(_repositoryMock.Object);
        }

        [Test]
        public async Task CreateOrderItemsAsync_WithValidData_CreatesAndSavesOrderItems()
        {
            // Arrange
            int orderId = 1;
            var products = new List<OrderItemCreateModel>
            {
                new OrderItemCreateModel { Name = "Product A", Price = 10.00m, Quantity = 1, SizeChar = 'S' },
                new OrderItemCreateModel { Name = "Product B", Price = 20.00m, Quantity = 2, SizeChar = 'M' }
            };
            List<OrderItem> createdItems = new List<OrderItem>();

            _repositoryMock.Setup(r => r.AddRangeAsync(It.IsAny<IEnumerable<OrderItem>>()))
                .Callback((IEnumerable<OrderItem> items) => createdItems.AddRange(items))
                .Returns(Task.CompletedTask);
            _repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _orderItemService.CreateOrderItemsAsync(orderId, products);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Product A", result.First().Name);
            Assert.AreEqual(10.00m, result.First().Price);
            Assert.AreEqual(1, result.First().Quantity);
            Assert.AreEqual('S', result.First().SizeChar);
            Assert.AreEqual(orderId, result.First().OrderId);

            _repositoryMock.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<OrderItem>>()), Times.Once());
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once());
        }
    }
}
