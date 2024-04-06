using MockQueryable.Moq;
using Moq;
using Voam.Core.Services;
using Voam.Infrastucture.Data.Common;

namespace Voam.Tests
{
    [TestFixture]
    public class ProductImageTests
    {
        private Mock<IRepository> mockRepository;
        private ImageService imageService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository>();
            imageService = new ImageService(mockRepository.Object);
        }

        [Test]
        public async Task CreateImageAsync_ValidImages_CreatesImages()
        {
            // Arrange
            var images = new List<string>
            {
                "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg=="
            };
            int productId = 1;

            // Act
            var result = await imageService.CreateImageAsync(images, productId);

            // Assert
            Assert.IsTrue(result);
            mockRepository.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<Infrastructure.Data.Models.ProductImage>>()), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void CreateImageAsync_InvalidBase64_ThrowsArgumentException()
        {
            // Arrange
            var images = new List<string> { "data:image/png;base64,invalid_base64" };
            int productId = 1;

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => imageService.CreateImageAsync(images, productId));
            Assert.That(ex.Message, Is.EqualTo("Invalid Base64 string."));
        }

        [Test]
        public void CreateImageAsync_InvalidFormat_ThrowsArgumentException()
        {
            // Arrange
            var images = new List<string> { "data:image/jpeg;base64,invalid_base64" };
            int productId = 1;

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => imageService.CreateImageAsync(images, productId));
            Assert.That(ex.Message, Is.EqualTo("Invalid image format. Only base64 encoded PNG images are accepted."));
        }

        [Test]
        public async Task CreateImageAsync_NoImagesProvided_ReturnsFalse()
        {
            // Arrange
            var images = new List<string>();
            int productId = 1;

            // Act & Assert
            var result = await imageService.CreateImageAsync(images, productId);
            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateImageAsync_ValidScenario_UpdatesImages()
        {
            // Arrange
            var existingImages = new List<Infrastructure.Data.Models.ProductImage>
            {
                new Infrastructure.Data.Models.ProductImage { ProductId = 1, ImageData = new byte[] { 1, 2, 3 } }
            }.AsQueryable().BuildMockDbSet();

            var newImages = new List<string>
            {
                "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg=="
            };

            mockRepository.Setup(r => r.All<Infrastructure.Data.Models.ProductImage>()).Returns(existingImages.Object);
            mockRepository.Setup(r => r.RemoveRange(It.IsAny<IEnumerable<Infrastructure.Data.Models.ProductImage>>()));
            mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            await imageService.UpdateImageAsync(newImages, 1);

            // Assert
            mockRepository.Verify(r => r.RemoveRange(It.IsAny<IEnumerable<Infrastructure.Data.Models.ProductImage>>()), Times.Once);
            mockRepository.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<Infrastructure.Data.Models.ProductImage>>()), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Exactly(2));  // Once for remove, once for add
        }

        [Test]
        public async Task UpdateImageAsync_InvalidBase64_ThrowsArgumentException()
        {
            // Arrange
            var existingImages = new List<Infrastructure.Data.Models.ProductImage>
            {
                new Infrastructure.Data.Models.ProductImage { ProductId = 1, ImageData = new byte[] { 1, 2, 3 } }
            }.BuildMockDbSet();

            var invalidImages = new List<string> { "data:image/png;base64,not_really_base64" };

            mockRepository.Setup(r => r.All<Infrastructure.Data.Models.ProductImage>()).Returns(existingImages.Object);
            mockRepository.Setup(r => r.RemoveRange(It.IsAny<IEnumerable<Infrastructure.Data.Models.ProductImage>>()));  // Setup to remove regardless of the error in adding
            mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);  // Setup to succeed for the removal call

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(() => imageService.UpdateImageAsync(invalidImages, 1));
            Assert.That(ex.Message, Contains.Substring("Invalid Base64 string"));

            // Verify that changes were made for removal but no new changes were saved after the exception
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once());  // Should only be called once for the removal part
            mockRepository.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<Infrastructure.Data.Models.ProductImage>>()), Times.Never);  // Ensure no attempt to add new images
        }
    }
}
