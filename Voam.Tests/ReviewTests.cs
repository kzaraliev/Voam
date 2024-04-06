using MockQueryable.Moq;
using Moq;
using Voam.Core.Services;
using Voam.Infrastructure.Data.Models;
using Voam.Infrastucture.Data.Common;

namespace Voam.Tests
{
    [TestFixture]
    public class ReviewTests
    {
        private Mock<IRepository> mockRepository;
        private ReviewService reviewService;

        [SetUp]
        public void Setup()
        {
            mockRepository = new Mock<IRepository>();
            reviewService = new ReviewService(mockRepository.Object);
        }

        [Test]
        public async Task AddRatingAsync_ReviewExists_UpdatesReview()
        {
            // Arrange
            var reviews = new List<Review>
            {
                new Review { Id = 1, CustomerId = "user1", ProductId = 1, Rating = 3 }
            };

            var mockSet = reviews.BuildMock();
            mockRepository.Setup(r => r.All<Review>()).Returns(mockSet);

            // Act
            var result = await reviewService.AddRatingAsync(1, "user1", 5);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(5, reviews.First().Rating); // Check if the rating was updated in the list
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public async Task AddRatingAsync_NoReviewExists_AddsNewReview()
        {
            // Arrange
            var reviews = new List<Review>().BuildMock();

            mockRepository.Setup(r => r.All<Review>()).Returns(reviews);
            mockRepository.Setup(r => r.AddAsync(It.IsAny<Review>())).Returns(Task.CompletedTask);

            // Act
            var result = await reviewService.AddRatingAsync(1, "user2", 4);

            // Assert
            Assert.IsTrue(result);
            mockRepository.Verify(r => r.AddAsync(It.IsAny<Review>()), Times.Once());
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once());
        }

        [Test]
        public async Task GetAvgRatingForProductAsync_CalculatesRatingsCorrectly()
        {
            // Arrange
            var reviews = new List<Review>
            {
                new Review { Id= 1, CustomerId = "user1", ProductId = 1, Rating = 5 },
                new Review { Id = 2, CustomerId = "user2", ProductId = 1, Rating = 3 }
            }.BuildMock();

            mockRepository.Setup(r => r.AllReadOnly<Review>()).Returns(reviews);

            // Act
            var result = await reviewService.GetAvgRatingForProductAsync(1, "user1");

            // Assert
            Assert.AreEqual(4.0, result.AverageRating); // Average of 5 and 3
            Assert.AreEqual(5.0, result.UserRating);    // User1's rating
        }

        [Test]
        public async Task GetAvgRatingForProductAsync_NoReviews_ReturnsDefaultValues()
        {
            // Arrange
            var reviews = new List<Review>().BuildMock();

            mockRepository.Setup(r => r.AllReadOnly<Review>()).Returns(reviews);

            // Act
            var result = await reviewService.GetAvgRatingForProductAsync(999, "userX"); // Assuming 999 is an unreviewed product ID

            // Assert
            Assert.AreEqual(0.0, result.AverageRating, "Average rating should be 0.0 for products with no reviews.");
            Assert.IsNull(result.UserRating, "User rating should be null for products with no reviews.");
        }

        [Test]
        public void AddRatingAsync_RatingOutOfRange_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var reviews = new List<Review>().BuildMock();

            mockRepository.Setup(r => r.All<Review>()).Returns(reviews);

            // Act & Assert
            var exDown = Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => reviewService.AddRatingAsync(1, "user1", 0),
                "Service should throw ArgumentOutOfRangeException for ratings outside 1-5 range.");
            Assert.That(exDown.ParamName, Is.EqualTo("rating"), "Exception should be for the 'rating' parameter.");

            var exUp = Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => reviewService.AddRatingAsync(1, "user1", 6),
                "Service should throw ArgumentOutOfRangeException for ratings outside 1-5 range.");
            Assert.That(exUp.ParamName, Is.EqualTo("rating"), "Exception should be for the 'rating' parameter.");
        }
    }
}