using MockQueryable.Moq;
using Moq;
using Voam.Core.Services;
using Voam.Infrastucture.Data.Common;

namespace Voam.Tests
{
    [TestFixture]
    public class SizeTests
    {
        private Mock<IRepository> mockRepository;
        private SizeService sizeService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository>();
            sizeService = new SizeService(mockRepository.Object);
        }

        [Test]
        public async Task CreateSizeAsync_ValidSizes_CreatesSizes()
        {
            // Arrange
            var productId = 1;
            var sizeSAmount = 10;
            var sizeMAmount = 20;
            var sizeLAmount = 30;

            mockRepository.Setup(r => r.AddRangeAsync(It.IsAny<IEnumerable<Infrastructure.Data.Models.Size>>())).Returns(Task.CompletedTask);

            // Act
            var result = await sizeService.CreateSizeAsync(sizeSAmount, sizeMAmount, sizeLAmount, productId);

            // Assert
            Assert.IsTrue(result);
            mockRepository.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<Infrastructure.Data.Models.Size>>()), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task CreateSizeAsync_AllQuantitiesZero_DoesNotCreateSizes()
        {
            // Arrange
            var productId = 1;
            var sizeSAmount = 0;
            var sizeMAmount = 0;
            var sizeLAmount = 0;

            // Act
            var result = await sizeService.CreateSizeAsync(sizeSAmount, sizeMAmount, sizeLAmount, productId);

            // Assert
            Assert.IsFalse(result);
            mockRepository.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<Infrastructure.Data.Models.Size>>()), Times.Never);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Test]
        public async Task UpdateSizesAsync_SizesExist_UpdatesSizes()
        {
            // Arrange
            var productId = 1;
            var sizes = new List<Infrastructure.Data.Models.Size>
            {
                new Infrastructure.Data.Models.Size { SizeChar = 'S', Quantity = 5, ProductId = productId },
                new Infrastructure.Data.Models.Size { SizeChar = 'M', Quantity = 5, ProductId = productId },
                new Infrastructure.Data.Models.Size { SizeChar = 'L', Quantity = 5, ProductId = productId }
            };

            mockRepository.Setup(r => r.All<Infrastructure.Data.Models.Size>()).Returns(sizes.BuildMockDbSet().Object);

            // Act
            await sizeService.UpdateSizesAsync(10, 20, 30, productId);

            // Assert
            Assert.AreEqual(10, sizes[0].Quantity);
            Assert.AreEqual(20, sizes[1].Quantity);
            Assert.AreEqual(30, sizes[2].Quantity);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task UpdateSizesAsync_SizesDoNotExist_CreatesNewSizes()
        {
            // Arrange
            var productId = 2;
            var existingSizes = new List<Infrastructure.Data.Models.Size>();  // No sizes exist initially

            var mockSet = existingSizes.BuildMockDbSet();
            mockRepository.Setup(r => r.All<Infrastructure.Data.Models.Size>()).Returns(mockSet.Object);
            mockRepository.Setup(r => r.AddAsync(It.IsAny<Infrastructure.Data.Models.Size>()))
                          .Callback((Infrastructure.Data.Models.Size size) => existingSizes.Add(size))
                          .Returns(Task.CompletedTask);

            // Act
            await sizeService.UpdateSizesAsync(10, 20, 30, productId);

            // Assert
            Assert.AreEqual(3, existingSizes.Count); // Check that three sizes were added
            Assert.IsTrue(existingSizes.Any(s => s.SizeChar == 'S' && s.Quantity == 10 && s.ProductId == productId));
            Assert.IsTrue(existingSizes.Any(s => s.SizeChar == 'M' && s.Quantity == 20 && s.ProductId == productId));
            Assert.IsTrue(existingSizes.Any(s => s.SizeChar == 'L' && s.Quantity == 30 && s.ProductId == productId));

            mockRepository.Verify(r => r.AddAsync(It.IsAny<Infrastructure.Data.Models.Size>()), Times.Exactly(3));  // Expect three new sizes to be added
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
