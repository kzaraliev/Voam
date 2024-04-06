using MailKit.Search;
using MockQueryable.Moq;
using Moq;
using Voam.Core.Contracts;
using Voam.Core.Models.Product;
using Voam.Core.Services;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;
using static Voam.Core.Utils.Constants;

namespace Voam.Tests
{
    [TestFixture]
    public class ShoppingCartTests
    {
        private Mock<IRepository> _repositoryMock;
        private Mock<ICartItemService> _cartItemServiceMock;
        private Mock<IProductService> _productServiceMock;
        private Mock<ISizeService> _sizeServiceMock;
        private IShoppingCartService _shoppingCartService;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRepository>();
            _cartItemServiceMock = new Mock<ICartItemService>();
            _productServiceMock = new Mock<IProductService>();
            _sizeServiceMock = new Mock<ISizeService>();

            _shoppingCartService = new ShoppingCartService(
                _repositoryMock.Object,
                _cartItemServiceMock.Object,
                _productServiceMock.Object,
                _sizeServiceMock.Object
            );
        }


        [Test]
        public async Task AddCartItemAsync_ShoppingCartNotFound_ReturnsFalse()
        {
            // Arrange
            var userId = "testUserId";
            _repositoryMock.Setup(r => r.All<ShoppingCart>())
                .Returns(new List<ShoppingCart>().AsQueryable()); // Mock empty shopping cart

            // Act
            var result = await _shoppingCartService.AddCartItemAsync(userId, 1, 1, 1);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddCartItemAsync_ProductNotFound_ReturnsFalse()
        {
            // Arrange
            var userId = "testUserId";
            var existingShoppingCart = new ShoppingCart { CustomerId = userId, CartItems = new List<CartItem>()};
            var carts = new List<ShoppingCart>() { existingShoppingCart };

            var query = carts.AsQueryable().BuildMockDbSet();

            _repositoryMock.Setup(r => r.All<ShoppingCart>()).Returns(query.Object);

            // Act
            var result = await _shoppingCartService.AddCartItemAsync(userId, 1, 1, 1);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddCartItemAsync_SizeNotFound_ReturnsFalse()
        {
            // Arrange
            var userId = "testUserId";
            var existingShoppingCart = new ShoppingCart { CustomerId = userId, CartItems = new List<CartItem>() };
            var carts = new List<ShoppingCart>() { existingShoppingCart };

            var existingProduct = new Product { Id = 1, Name = "Test", Description = "Lorem ipsum" };

            var queryCart = carts.AsQueryable().BuildMockDbSet();

            _repositoryMock.Setup(r => r.All<ShoppingCart>()).Returns(queryCart.Object);
            _productServiceMock.Setup(p => p.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DetailsProductModel() { Description = "Lorem ipsum", Name = "Lorem" });


            // Act
            var result = await _shoppingCartService.AddCartItemAsync(userId, 1, 1, 1);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddCartItemAsync_NotEnoughQuantityInStock_ReturnsFalse()
        {
            // Arrange
            var userId = "testUserId";
            var existingShoppingCart = new ShoppingCart { CustomerId = userId, CartItems = new List<CartItem>() };
            var carts = new List<ShoppingCart>() { existingShoppingCart };

            var existingProduct = new Product { Id = 1, Name = "Test", Description = "Lorem ipsum" };

            var queryCart = carts.AsQueryable().BuildMockDbSet();

            _repositoryMock.Setup(r => r.All<ShoppingCart>()).Returns(queryCart.Object);
            _productServiceMock.Setup(p => p.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DetailsProductModel() { Description = "Lorem ipsum", Name = "Lorem" });
            _sizeServiceMock.Setup(s => s.GetSizeByIdAsync(It.IsAny<int>()))
                            .ReturnsAsync(new Size { Quantity = 10 });

            // Act
            var result = await _shoppingCartService.AddCartItemAsync(userId, 1, 1, 100);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddCartItemAsync_CartItemExists_UpdatesQuantity_ReturnsTrue()
        {
            // Arrange
            var userId = "testUserId";
            var existingCartItem = new CartItem { ProductId = 1, SizeId = 1, Quantity = 5 };
            var existingShoppingCart = new ShoppingCart { CustomerId = userId, CartItems = new List<CartItem> { existingCartItem } };
            _repositoryMock.Setup(r => r.All<ShoppingCart>())
                .Returns(new List<ShoppingCart> { existingShoppingCart }.AsQueryable()); // Mock existing shopping cart with cart item

            _sizeServiceMock.Setup(s => s.GetSizeByIdAsync(It.IsAny<int>()))
                            .ReturnsAsync(new Size { Quantity = 10 }); // Mock available quantity

            _productServiceMock.Setup(p => p.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DetailsProductModel() { Description = "Lorem ipsum", Name = "Lorem" });

            // Act
            var result = await _shoppingCartService.AddCartItemAsync(userId, 1, 1, 3); // Updating quantity from 5 to 8 (existing quantity + new quantity)

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AddCartItemAsync_CartItemExists_ExceedsStockQuantity_ReturnsFalse()
        {
            // Arrange
            var userId = "testUserId";
            var existingCartItem = new CartItem { ProductId = 1, SizeId = 1, Quantity = 5 };
            var existingShoppingCart = new ShoppingCart { CustomerId = userId, CartItems = new List<CartItem> { existingCartItem } };
            _repositoryMock.Setup(r => r.All<ShoppingCart>())
                .Returns(new List<ShoppingCart> { existingShoppingCart }.AsQueryable()); // Mock existing shopping cart with cart item

            _sizeServiceMock.Setup(s => s.GetSizeByIdAsync(It.IsAny<int>()))
                            .ReturnsAsync(new Size { Quantity = 10 }); // Mock available quantity

            _productServiceMock.Setup(p => p.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DetailsProductModel() { Description = "Lorem ipsum", Name = "Lorem" });

            // Act
            var result = await _shoppingCartService.AddCartItemAsync(userId, 1, 1, 8); // Updating quantity from 5 to 8 (existing quantity + new quantity)

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddCartItemAsync_ExceedsStockQuantity_ReturnsFalse()
        {
            // Arrange
            _sizeServiceMock.Setup(s => s.GetSizeByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Size { Quantity = 10 }); // Mock available quantity

            // Act
            var result = await _shoppingCartService.AddCartItemAsync("testUserId", 1, 1, 10);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task AddCartItemAsync_NewCartItem_AddedToShoppingCart_ReturnsTrue()
        {
            // Arrange
            var mockShoppingCart = new ShoppingCart { Id = 1, CustomerId = "testUserId", CartItems = new List<CartItem>() };
            _repositoryMock.Setup(r => r.All<ShoppingCart>())
                           .Returns(new List<ShoppingCart> { mockShoppingCart }.AsQueryable());

            _sizeServiceMock.Setup(s => s.GetSizeByIdAsync(It.IsAny<int>()))
                            .ReturnsAsync(new Size { Quantity = 5 }); // Mock available quantity

            _productServiceMock.Setup(p => p.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DetailsProductModel() { Description = "Lorem ipsum", Name = "Lorem" });

            _cartItemServiceMock.Setup(c => c.CreateCartItemAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                                .ReturnsAsync(new CartItem()); // Mock creating cart item

            // Act
            var result = await _shoppingCartService.AddCartItemAsync("testUserId", 1, 1, 1);

            // Assert
            Assert.IsTrue(result);
        }

        //_________________________________________

        [Test]
        public async Task AddCartItemAsync_NewCartItemAdded_ReturnsTrue()
        {
            // Arrange
            var shoppingCart = new ShoppingCart { CustomerId = "testUser", CartItems = new List<CartItem>() };
            _repositoryMock.Setup(r => r.All<ShoppingCart>()).Returns(new List<ShoppingCart> { shoppingCart }.AsQueryable());
            _productServiceMock.Setup(p => p.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DetailsProductModel() { Description = "Lorem ipsum", Name = "Lorem"});
            _sizeServiceMock.Setup(s => s.GetSizeByIdAsync(It.IsAny<int>())).ReturnsAsync(new Size { Quantity = 10 });
            _cartItemServiceMock.Setup(c => c.CreateCartItemAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new CartItem());

            // Act
            var result = await _shoppingCartService.AddCartItemAsync("testUser", 1, 1, 1);

            // Assert
            Assert.IsTrue(result);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task AddCartItemAsync_ExistingCartItemQuantityUpdated_ReturnsTrue()
        {
            // Arrange
            var existingCartItem = new CartItem { ProductId = 1, SizeId = 1, Quantity = 5 };
            var shoppingCart = new ShoppingCart { CustomerId = "testUser", CartItems = new List<CartItem> { existingCartItem } };
            _repositoryMock.Setup(r => r.All<ShoppingCart>()).Returns(new List<ShoppingCart> { shoppingCart }.AsQueryable());
            _productServiceMock.Setup(p => p.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new DetailsProductModel() { Description = "Lorem ipsum", Name = "Lorem" });
            _sizeServiceMock.Setup(s => s.GetSizeByIdAsync(It.IsAny<int>())).ReturnsAsync(new Size { Quantity = 10 });

            // Act
            var result = await _shoppingCartService.AddCartItemAsync("testUser", 1, 1, 5);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(10, existingCartItem.Quantity); // Check if quantity is updated
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task CreateShoppingCartAsync_AddsNewShoppingCartWithCorrectUserId()
        {
            // Arrange
            string userId = "testUser";

            // Act
            await _shoppingCartService.CreateShoppingCartAsync(userId);

            // Assert
            _repositoryMock.Verify(x => x.AddAsync(It.Is<ShoppingCart>(sc => sc.CustomerId == userId)), Times.Once);
        }

        [Test]
        public async Task CreateShoppingCartAsync_SavesChanges()
        {
            // Act
            await _shoppingCartService.CreateShoppingCartAsync("testUser");

            // Assert
            _repositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteCartItemAsyncByIdAsync_WhenCartItemNotFound_ReturnsNotFound()
        {
            // Arrange
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync((CartItem)null);

            // Act
            var result = await _shoppingCartService.DeleteCartItemAsyncByIdAsync(1);

            // Assert
            Assert.AreEqual(DeleteResult.NotFound, result);
        }

        [Test]
        public async Task DeleteCartItemAsyncByIdAsync_WhenShoppingCartNotFound_ReturnsNotFound()
        {
            // Arrange
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync(new CartItem());
            _repositoryMock.Setup(x => x.FindAsync<ShoppingCart>(It.IsAny<int>())).ReturnsAsync((ShoppingCart)null);

            // Act
            var result = await _shoppingCartService.DeleteCartItemAsyncByIdAsync(1);

            // Assert
            Assert.AreEqual(DeleteResult.NotFound, result);
        }

        [Test]
        public async Task DeleteCartItemAsyncByIdAsync_WhenProductNotFound_ReturnsNotFound()
        {
            // Arrange
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync(new CartItem());
            _repositoryMock.Setup(x => x.FindAsync<ShoppingCart>(It.IsAny<int>())).ReturnsAsync(new ShoppingCart() { CustomerId = "Lorem"});
            _repositoryMock.Setup(x => x.FindAsync<Product>(It.IsAny<int>())).ReturnsAsync((Product)null);

            // Act
            var result = await _shoppingCartService.DeleteCartItemAsyncByIdAsync(1);

            // Assert
            Assert.AreEqual(DeleteResult.NotFound, result);
        }

        [Test]
        public async Task DeleteCartItemAsyncByIdAsync_WhenDeleteFails_ReturnsError()
        {
            // Arrange
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync(new CartItem());
            _repositoryMock.Setup(x => x.FindAsync<ShoppingCart>(It.IsAny<int>())).ReturnsAsync(new ShoppingCart() { CustomerId = "Lorem" });
            _repositoryMock.Setup(x => x.FindAsync<Product>(It.IsAny<int>())).ReturnsAsync(new Product() { Description ="Lorem ipsum", Name = "Test product" });
            _repositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(0);

            // Act
            var result = await _shoppingCartService.DeleteCartItemAsyncByIdAsync(1);

            // Assert
            Assert.AreEqual(DeleteResult.Error, result);
        }

        [Test]
        public async Task DeleteCartItemAsyncByIdAsync_WhenDeleteSucceeds_ReturnsSuccess()
        {
            // Arrange
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync(new CartItem());
            _repositoryMock.Setup(x => x.FindAsync<ShoppingCart>(It.IsAny<int>())).ReturnsAsync(new ShoppingCart() { CustomerId = "Lorem" });
            _repositoryMock.Setup(x => x.FindAsync<Product>(It.IsAny<int>())).ReturnsAsync(new Product() { Description = "Lorem ipsum", Name = "Test product" });
            _repositoryMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _shoppingCartService.DeleteCartItemAsyncByIdAsync(1);

            // Assert
            Assert.AreEqual(DeleteResult.Success, result);
        }

        [Test]
        public async Task UpdateCartItemQuantity_WhenCartItemNotFound_ReturnsFalse()
        {
            // Arrange
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync((CartItem)null);

            // Act
            var result = await _shoppingCartService.UpdateCartItemQuantity(1, 10);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateCartItemQuantity_WhenSizeNotFound_ReturnsFalse()
        {
            // Arrange
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync(new CartItem());
            _sizeServiceMock.Setup(x => x.GetSizeByIdAsync(It.IsAny<int>())).ReturnsAsync((Size)null);

            // Act
            var result = await _shoppingCartService.UpdateCartItemQuantity(1, 10);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateCartItemQuantity_WhenQuantityIsZero_ReturnsFalse()
        {
            // Arrange
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync(new CartItem());
            _sizeServiceMock.Setup(x => x.GetSizeByIdAsync(It.IsAny<int>())).ReturnsAsync(new Size());

            // Act
            var result = await _shoppingCartService.UpdateCartItemQuantity(1, 0);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateCartItemQuantity_WhenQuantityIsGreaterThanSizeQuantity_ReturnsFalse()
        {
            // Arrange
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync(new CartItem());
            _sizeServiceMock.Setup(x => x.GetSizeByIdAsync(It.IsAny<int>())).ReturnsAsync(new Size() { Quantity = 5 });

            // Act
            var result = await _shoppingCartService.UpdateCartItemQuantity(1, 10);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateCartItemQuantity_WhenValidQuantity_ReturnsTrueAndUpdatesQuantity()
        {
            // Arrange
            var cartItem = new CartItem() { Quantity = 5 };
            var size = new Size() { Quantity = 10 };
            _repositoryMock.Setup(x => x.FindAsync<CartItem>(It.IsAny<int>())).ReturnsAsync(cartItem);
            _sizeServiceMock.Setup(x => x.GetSizeByIdAsync(It.IsAny<int>())).ReturnsAsync(size);

            // Act
            var result = await _shoppingCartService.UpdateCartItemQuantity(1, 8);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(8, cartItem.Quantity);
        }
    }
}
