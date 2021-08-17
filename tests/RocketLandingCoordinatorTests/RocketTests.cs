using RocketLandingCoordinator;
using System;
using Xunit;

namespace RocketLandingCoordinatorTests
{
    public class RocketTests
    {
        [Theory]
        [InlineData(3, 3, false)]
        [InlineData(4, 4, true)]
        [InlineData(4, 5, true)]
        [InlineData(5, 4, true)]
        [InlineData(5, 5, true)]
        [InlineData(6, 5, true)]
        [InlineData(5, 6, true)]
        [InlineData(6, 6, true)]
        [InlineData(7, 7, false)]
        public void Rocket_Correctly_Checks_If_Incoming_Coordinates_Clash_With_Its_LandingArea(int x, int y, bool shouldClash)
        {
            // Arrange
            var rocket = new Rocket(Guid.NewGuid(), new Coordinates(5, 5));
            var coordinates = new Coordinates(x, y);

            // Act
            var clashes = rocket.LandingSpaceClashesWith(coordinates);

            // Assert
            Assert.Equal(shouldClash, clashes);
        }

        [Fact]
        public void When_RocketLandingPosition_Is_Null_Return_False()
        {
            // Arrange
            var rocket = new Rocket(Guid.NewGuid(), null);
            var coordinates = new Coordinates(5, 5);

            // Act
            var clashes = rocket.LandingSpaceClashesWith(coordinates);

            // Assert
            Assert.False(clashes);
        }

        [Fact]
        public void When_RequestingCoordinates_Is_Null_Return_False()
        {
            // Arrange
            var rocket = new Rocket(Guid.NewGuid(), new Coordinates(5, 5));

            // Act
            var clashes = rocket.LandingSpaceClashesWith(null);

            // Assert
            Assert.False(clashes);
        }
    }
}
