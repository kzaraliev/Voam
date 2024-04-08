using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using Voam.Core.Contracts;
using Voam.Core.Models.Order;
using Voam.Core.Services;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Tests
{
    [TestFixture]
    public class OrderTests
    {
        private Mock<IRepository> _repositoryMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<IOrderItemService> _orderItemServiceMock;
        private Mock<IShoppingCartService> _shoppingCartServiceMock;
        private Mock<IEmailService> _emailServiceMock;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRepository>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                new Mock<IUserStore<ApplicationUser>>().Object,
                null, null, null, null, null, null, null, null);
            _orderItemServiceMock = new Mock<IOrderItemService>();
            _shoppingCartServiceMock = new Mock<IShoppingCartService>();
            _emailServiceMock = new Mock<IEmailService>();

            _orderService = new OrderService(
                _repositoryMock.Object,
                _userManagerMock.Object,
                _orderItemServiceMock.Object,
                _shoppingCartServiceMock.Object,
                _emailServiceMock.Object);
        }

        [Test]
        public async Task PlaceOrderAsync_UserDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            string userId = "nonexistent";
            PlaceOrderModel model = new PlaceOrderModel();

            _userManagerMock.Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync((ApplicationUser)null);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(async () => await _orderService.PlaceOrderAsync(userId, model), "No such user");
        }

        [Test]
        public async Task PlaceOrderAsync_ValidOrder_CreatesOrderAndSendsConfirmationEmail()
        {
            // Arrange
            string userId = "existinguser";
            var model = new PlaceOrderModel
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123456789",
                EcontOffice = "123 Econt Office Street",
                City = "SomeCity",
                PaymentMethod = "Cash",
                Products = new List<OrderItemsModel>
                {
                    new OrderItemsModel
                    {
                        ProductId = 1,
                        SizeId = 1,
                        Quantity = 2
                    }
                },
                TotalPrice = 40.00m
            };

            var identityUser = new ApplicationUser { Id = userId, FirstName = "John", LastName = "Doe" };
            var product = new Product { Id = 1, Name = "T-Shirt", Price = 20.00m, Description = "Lorem ipsum" };
            var size = new Size { Id = 1, Quantity = 3, SizeChar = 'M' };

            _userManagerMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(identityUser);
            _repositoryMock.SetupSequence(x => x.AllReadOnly<Product>())
                .Returns(new List<Product> { product }.AsQueryable().BuildMockDbSet().Object);
            _repositoryMock.SetupSequence(x => x.All<Size>())
                .Returns(new List<Size> { size }.AsQueryable().BuildMockDbSet().Object);
            _repositoryMock.Setup(x => x.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);
            _repositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1); // Assume order save success
            _orderItemServiceMock.Setup(x => x.CreateOrderItemsAsync(It.IsAny<int>(), It.IsAny<List<OrderItemCreateModel>>()))
                .ReturnsAsync(new List<OrderItem> { new OrderItem { Name = "T-Shirt", SizeChar = 'M', Quantity = 2 } });
            _shoppingCartServiceMock.Setup(x => x.EmptyShoppingCartAsync(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _orderService.PlaceOrderAsync(userId, model);

            // Assert
            Assert.AreEqual("Success", result);
            _repositoryMock.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
            _repositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
            _orderItemServiceMock.Verify(x => x.CreateOrderItemsAsync(It.IsAny<int>(), It.IsAny<List<OrderItemCreateModel>>()), Times.Once);
            _shoppingCartServiceMock.Verify(x => x.EmptyShoppingCartAsync(userId), Times.Once);
        }

        [Test]
        public async Task PlaceOrderAsync_ProductQuantityExceedsSize_ThrowsException()
        {
            // Arrange
            var userId = "existinguser";
            var model = new PlaceOrderModel
            {
                FullName = "John Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123456789",
                EcontOffice = "123 Econt Office Street",
                City = "SomeCity",
                PaymentMethod = "Cash",

                Products = new List<OrderItemsModel>
                {
                    new OrderItemsModel { ProductId = 1, SizeId = 1, Quantity = 5 }  // Assuming size.Quantity is less than 5
                },
            };

            var user = new ApplicationUser { Id = userId, FirstName = "John", LastName = "Doe" };
            var product = new Product { Id = 1, Name = "T-Shirt", Price = 20.00m, Description = "Lorem ipsum" };
            var size = new Size { Id = 1, Quantity = 3, SizeChar = 'M' };

            _userManagerMock.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(user);
            _repositoryMock.SetupSequence(x => x.AllReadOnly<Product>())
                .Returns(new List<Product> { product }.AsQueryable().BuildMockDbSet().Object);
            _repositoryMock.SetupSequence(x => x.All<Size>())
                .Returns(new List<Size> { size }.AsQueryable().BuildMockDbSet().Object);

            // Act
            var result = await _orderService.PlaceOrderAsync(userId, model);

            // Assert
            Assert.AreEqual("Not enough units", result);
        }

        [Test]
        public async Task GetAllOrdersForUserAsync_UserNotFound_ThrowsException()
        {
            // Arrange
            var userId = "nonExistingUserId";
            _userManagerMock.Setup(um => um.FindByIdAsync(userId))
                            .ReturnsAsync((ApplicationUser)null); // Mock user not found

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _orderService.GetAllOrdersForUserAsync(userId, 5, 1));
        }

        [Test]
        public async Task GetAllOrdersFromUserAsync_UserFound_ReturnsOrders() 
        {
            // Arrange
            var userId = "existingUserId";
            var user = new ApplicationUser { Id = userId, FirstName = "Pesho", LastName ="Peshov" };

            _userManagerMock.Setup(um => um.FindByIdAsync(userId))
                    .ReturnsAsync(user);

            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    OrderDate = new DateTime(2024, 4, 1),
                    Econt = "Econt Address 1",
                    PaymentMethod = "Payment Method 1",
                    City = "City 1",
                    TotalPrice = 100,
                    Products = new List<OrderItem>
                    {
                        new OrderItem { Id = 1, Name = "Product 1", Quantity = 1, SizeChar = 'S' }
                    },
                    PhoneNumber = "0876033889",
                    CustomerId = "existingUserId",
                    Email = "roma@gmail.com",
                    FullName = "Pesho Peshov"
                },
                new Order
                {
                    Id = 2,
                    OrderDate = new DateTime(2024, 3, 15),
                    Econt = "Econt Address 2",
                    PaymentMethod = "Payment Method 2",
                    City = "City 2",
                    TotalPrice = 200,
                    Products = new List<OrderItem>
                    {
                        new OrderItem { Id = 2, Name = "Product 2", Quantity = 2, SizeChar = 'M' }
                    }
                    ,
                    PhoneNumber = "0876033889",
                    CustomerId = "existingUserId",
                    Email = "roma@gmail.com",
                    FullName = "Pesho Peshov"
                }
            };

            var query = orders.AsQueryable().BuildMockDbSet();

            _repositoryMock.Setup(r => r.AllReadOnly<Order>()).Returns(query.Object);

            // Act
            var result = await _orderService.GetAllOrdersForUserAsync(userId, 6, 1);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Items.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllOrders_ReturnsOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    OrderDate = new DateTime(2024, 4, 1),
                    Econt = "Econt Address 1",
                    PaymentMethod = "Payment Method 1",
                    City = "City 1",
                    TotalPrice = 100,
                    Products = new List<OrderItem>
                    {
                        new OrderItem { Id = 1, Name = "Product 1", Quantity = 1, SizeChar = 'S' }
                    },
                    PhoneNumber = "0876033889",
                    CustomerId = "existingUser1",
                    Email = "roma@gmail.com",
                    FullName = "Pesho Peshov"
                },
                new Order
                {
                    Id = 2,
                    OrderDate = new DateTime(2024, 3, 15),
                    Econt = "Econt Address 2",
                    PaymentMethod = "Payment Method 2",
                    City = "City 2",
                    TotalPrice = 200,
                    Products = new List<OrderItem>
                    {
                        new OrderItem { Id = 2, Name = "Product 2", Quantity = 2, SizeChar = 'M' }
                    }
                    ,
                    PhoneNumber = "0876033889",
                    CustomerId = "existingUser2",
                    Email = "roma@gmail.com",
                    FullName = "Petur Petrov"
                }
            };

            var query = orders.AsQueryable().BuildMockDbSet();

            _repositoryMock.Setup(r => r.AllReadOnly<Order>()).Returns(query.Object);

            // Act
            var result = await _orderService.GetAllOrdersAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}
