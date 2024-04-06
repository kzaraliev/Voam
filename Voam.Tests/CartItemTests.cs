using MockQueryable.Moq;
using Moq;
using Voam.Core.Services;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Tests
{
    [TestFixture]
    public class CartItemTests
    {
        private Mock<IRepository> _repositoryMock;
        private CartItemService _cartItemService;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRepository>();
            _cartItemService = new CartItemService(_repositoryMock.Object);
        }

        [Test]
        public async Task CreateCartItemAsync_CreatesAndReturnsCartItem()
        {
            // Arrange
            var productId = 1;
            var shoppingCartId = 1;
            var sizeId = 1;
            var quantity = 2;

            var cartItem = new CartItem
            {
                ProductId = productId,
                ShoppingCartId = shoppingCartId,
                SizeId = sizeId,
                Quantity = quantity
            };

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<CartItem>()))
                .Callback((CartItem ci) => Assert.AreEqual(quantity, ci.Quantity)) // Asserting during setup for demonstration purposes.
                .Returns(Task.CompletedTask);

            _repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _cartItemService.CreateCartItemAsync(productId, shoppingCartId, sizeId, quantity);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(productId, result.ProductId);
            Assert.AreEqual(shoppingCartId, result.ShoppingCartId);
            Assert.AreEqual(sizeId, result.SizeId);
            Assert.AreEqual(quantity, result.Quantity);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void CreateCartItemAsync_WithInvalidData_ThrowsException()
        {
            // Arrange
            int invalidProductId = -1;  // Assuming -1 is an invalid ID for demonstration.
            int shoppingCartId = 1;
            int sizeId = 1;
            int invalidQuantity = 0;  // Assuming quantity must be greater than zero.

            var cartItem = new CartItem
            {
                ProductId = invalidProductId,
                ShoppingCartId = shoppingCartId,
                SizeId = sizeId,
                Quantity = invalidQuantity
            };

            // Setup the AddAsync to throw an exception when an invalid cart item is added.
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<CartItem>()))
                .ThrowsAsync(new InvalidOperationException("Invalid data provided."));

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => _cartItemService.CreateCartItemAsync(invalidProductId, shoppingCartId, sizeId, invalidQuantity));
            Assert.That(ex.Message, Is.EqualTo("Invalid data provided."));
        }

        [Test]
        public async Task RemoveAllCartItemsFromShoppingCartAsync_RemovesAllCartItems()
        {
            // Arrange
            int shoppingCartId = 1;
            var cartItems = new List<CartItem>
            {
                new CartItem { ShoppingCartId = shoppingCartId, ProductId = 1, SizeId = 1, Quantity = 2 },
                new CartItem { ShoppingCartId = shoppingCartId, ProductId = 2, SizeId = 2, Quantity = 3 }
            };

            var cartItemsQueryable = cartItems.AsQueryable().BuildMock();

            _repositoryMock.Setup(r => r.AllReadOnly<CartItem>())
                .Returns(cartItemsQueryable);

            _repositoryMock.Setup(r => r.RemoveRange(It.IsAny<IEnumerable<CartItem>>()))
                .Callback((IEnumerable<CartItem> items) => Assert.AreEqual(2, items.Count()));

            _repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            await _cartItemService.RemoveAllCartItemsFromShoppingCartAsync(shoppingCartId);

            // Assert
            _repositoryMock.Verify(r => r.RemoveRange(It.IsAny<IEnumerable<CartItem>>()), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task RemoveAllCartItemsFromShoppingCartAsync_InvalidShoppingCartId_NoItemsRemoved()
        {
            // Arrange
            int invalidShoppingCartId = 999;  // Assuming 999 is an invalid/non-existent shopping cart ID.
            var cartItems = new List<CartItem>
            {
                new CartItem { ShoppingCartId = 1, ProductId = 1, SizeId = 1, Quantity = 2 },
                new CartItem { ShoppingCartId = 2, ProductId = 2, SizeId = 2, Quantity = 3 }
            };

            var mockSet = cartItems.AsQueryable().Where(ci => ci.ShoppingCartId == invalidShoppingCartId).BuildMock();

            _repositoryMock.Setup(r => r.AllReadOnly<CartItem>()).Returns(mockSet);

            // Act
            await _cartItemService.RemoveAllCartItemsFromShoppingCartAsync(invalidShoppingCartId);

            // Assert
            _repositoryMock.Verify(r => r.RemoveRange(It.IsAny<IEnumerable<CartItem>>()), Times.Never, "RemoveRange should not be called since no cart items match the invalid shopping cart ID.");
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never, "SaveChangesAsync should not be called since no operations should occur.");
        }
    }
}
