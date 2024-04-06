using MockQueryable.Moq;
using Moq;
using Voam.Core.Contracts;
using Voam.Core.Services;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastructure.Data;
using Voam.Infrastucture.Data.Common;
using Voam.Core.Models.Product;
using static Voam.Core.Utils.Constants;

namespace Voam.Tests
{
    [TestFixture]
    public class ProductTests
    {
        private Mock<IRepository> _repositoryMock;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            // Mock the repository
            _repositoryMock = new Mock<IRepository>();

            // Create ProductService instance
            _productService = new ProductService(_repositoryMock.Object, null, null, null);
        }

        [Test]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 100 },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 200 }
            };

            var productsQueryable = products.AsQueryable().BuildMockDbSet();

            _repositoryMock.Setup(r => r.AllReadOnly<Product>()).Returns(productsQueryable.Object);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetRecentlyAddedProductsAsync_ReturnsTopThreeProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 3, Name = "Product C", Description = "Description C", Price = 300,  },
                new Product { Id = 1, Name = "Product A", Description = "Description A", Price = 100, },
                new Product { Id = 2, Name = "Product B", Description = "Description B", Price = 200,  },
                new Product { Id = 4, Name = "Product D", Description = "Description D", Price = 400,  }
            };

            // Using MockQueryable to simulate EF Core's behavior
            var mockSet = products.AsQueryable().BuildMockDbSet();

            _repositoryMock.Setup(r => r.AllReadOnly<Product>()).Returns(mockSet.Object);

            // Act
            var result = await _productService.GetRecentlyAddedProductsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count()); // Checks if only three products are returned
            Assert.AreEqual("Product D", result.First().Name); // Checks if the first product is the one with the highest ID (recently added)
            Assert.AreEqual("Product C", result.Skip(1).First().Name); // Further checks the order of products
            Assert.AreEqual("Product B", result.Skip(2).First().Name);
        }

        [Test]
        public async Task GetProductByIdAsync_ReturnsProduct()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 100 }
            };

            var productsQueryable = products.AsQueryable().BuildMockDbSet();

            _repositoryMock.Setup(r => r.AllReadOnly<Product>()).Returns(productsQueryable.Object);

            // Act
            var result = await _productService.GetProductByIdAsync(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Product 1", result.Name);
        }

        [Test]
        public async Task GetProductByIdAsync_InvalidId_ReturnsNull()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Description = "Description 1", Price = 100 },
                new Product { Id = 2, Name = "Product 2", Description = "Description 2", Price = 200 }
            };

            var productsQueryable = products.AsQueryable().BuildMockDbSet();
            _repositoryMock.Setup(r => r.AllReadOnly<Product>()).Returns(productsQueryable.Object);

            // Act
            var result = await _productService.GetProductByIdAsync(3); // ID 3 does not exist

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task CreateProductAsync_CreatesProduct()
        {
            // Arrange
            var newProduct = new CreateProductModel { Name = "New Product", Description = "New Description", Price = 300 };
            var createdProduct = new Product { Id = 3, Name = "New Product", Description = "New Description", Price = 300 };

            // Setup AddAsync to mimic setting an ID on add and return immediately
            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .Callback<Product>(p =>
                {
                    p.Id = 3; // Simulate the ID assignment that EF Core would handle
                })
                .Returns(Task.CompletedTask);

            // Setup SaveChangesAsync to simulate a successful save
            _repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // This is crucial: We need to mock AllReadOnly<Product>() to handle async operations
            var products = new List<Product> { createdProduct };
            var productsQueryable = products.AsQueryable().BuildMockDbSet();
            _repositoryMock.Setup(r => r.AllReadOnly<Product>()).Returns(productsQueryable.Object);

            // Act
            var result = await _productService.CreateProductAsync(newProduct);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("New Product", result.Name);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateProductAsync_ProductExists_UpdatesAndReturnsProduct()
        {
            // Arrange
            var existingProduct = new Product { Id = 1, Name = "Old Name", Description = "Old Description", Price = 100 };
            var editModel = new EditProductModel { Name = "New Name", Description = "New Description", Price = 200 };

            _repositoryMock.Setup(r => r.FindAsync<Product>(1)).ReturnsAsync(existingProduct);
            _repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _productService.UpdateProductAsync(1, editModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(editModel.Name, result.Name);
            Assert.AreEqual(editModel.Description, result.Description);
            Assert.AreEqual(editModel.Price, result.Price);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void UpdateProductAsync_ProductDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            var editModel = new EditProductModel
            {
                Name = "Valid Name",
                Description = "Valid Description",
                Price = 150,
                SizeS = 15,
                SizeM = 25,
                SizeL = 35,
                Images = new List<string> { "image1.jpg" }
            };
            _repositoryMock.Setup(r => r.FindAsync<Product>(1)).ReturnsAsync((Product)null);

            // Act & Assert
            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => _productService.UpdateProductAsync(1, editModel));
            Assert.That(ex.Message, Is.EqualTo("An error occurred while updating the product with ID 1."));
        }

        [Test]
        public async Task UpdateProductAsync_PartialUpdate_UpdatesNonNullFields()
        {
            // Arrange
            var existingProduct = new Product { Id = 1, Name = "Old Name", Description = "Old Description", Price = 100 };
            var editModel = new EditProductModel
            {
                Name = "New Name",
                Description = "Old Description",
                Price = 100,
                SizeS = 10,
                SizeM = 20,
                SizeL = 30,
                Images = new List<string> { "image1.jpg" }
            }; // Partial update: Not changing description or price

            _repositoryMock.Setup(r => r.FindAsync<Product>(1)).ReturnsAsync(existingProduct);
            _repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _productService.UpdateProductAsync(1, editModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(editModel.Name, result.Name);
            Assert.AreEqual(existingProduct.Description, result.Description); // Description should not change
            Assert.AreEqual(existingProduct.Price, result.Price); // Price should not change
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateProductAsync_PriceIsZero_UpdatesAndReturnsProduct()
        {
            // Arrange
            var existingProduct = new Product { Id = 1, Name = "Old Name", Description = "Old Description", Price = 100 };
            var editModel = new EditProductModel { Name = "New Name", Description = "New Description", Price = 0 };

            _repositoryMock.Setup(r => r.FindAsync<Product>(1)).ReturnsAsync(existingProduct);
            _repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _productService.UpdateProductAsync(1, editModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(editModel.Name, result.Name);
            Assert.AreEqual(editModel.Description, result.Description);
            Assert.AreEqual(existingProduct.Price, result.Price);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task DeleteProductByIdAsync_ProductExists_DeletesProductAndReturnsSuccess()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Existing Product", Description = "Lorem ipsum" };
            _repositoryMock.Setup(r => r.FindAsync<Product>(1)).ReturnsAsync(product);
            _repositoryMock.Setup(r => r.Remove(product));
            _repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _productService.DeleteProductByIdAsync(1);

            // Assert
            Assert.AreEqual(DeleteResult.Success, result);
            _repositoryMock.Verify(r => r.Remove(product), Times.Once());
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public async Task DeleteProductByIdAsync_ProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _repositoryMock.Setup(r => r.FindAsync<Product>(1)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.DeleteProductByIdAsync(1);

            // Assert
            Assert.AreEqual(DeleteResult.NotFound, result);
            _repositoryMock.Verify(r => r.Remove(It.IsAny<Product>()), Times.Never());
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Never());
        }

        [Test]
        public async Task DeleteProductByIdAsync_FailureInSaveChanges_ReturnsError()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Existing Product", Description = "Lorem ipsum" };
            _repositoryMock.Setup(r => r.FindAsync<Product>(1)).ReturnsAsync(product);
            _repositoryMock.Setup(r => r.Remove(product));
            _repositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0); // Simulate failure in saving changes

            // Act
            var result = await _productService.DeleteProductByIdAsync(1);

            // Assert
            Assert.AreEqual(DeleteResult.Error, result);
            _repositoryMock.Verify(r => r.Remove(product), Times.Once());
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once());
        }
    }
}
