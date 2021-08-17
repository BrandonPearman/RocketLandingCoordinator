using RocketLandingCoordinator;
using System;
using Xunit;

namespace RocketLandingCoordinatorTests
{
    public class LandingPlatformTests
    {
        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(5, 4, false)]
        [InlineData(4, 5, false)]
        [InlineData(5, 5, true)]
        [InlineData(7, 6, true)]
        [InlineData(10, 10, true)]
        [InlineData(11, 10, false)]
        [InlineData(10, 11, false)]
        [InlineData(15, 15, false)]
        public void LandingPlatform_Correctly_Checks_If_Incoming_Coordinates_Are_WithIn_Its_LandingArea(int x, int y, bool shouldBeInArea)
        {
            // Arrange
            var area = new Area(new Coordinates(5, 5), new Coordinates(10, 10));
            var landingPlatform = new LandingPlatform(area);
            var coordinates = new Coordinates(x, y);

            // Act
            var isInArea = landingPlatform.PlatformAreaEncloses(coordinates);

            // Assert
            Assert.Equal(shouldBeInArea, isInArea);
        }

        [Fact]
        public void When_Coordinates_Are_Null_LandingPlatform_Returns_False()
        {
            // Arrange
            var area = new Area(new Coordinates(5, 5), new Coordinates(10, 10));
            var landingPlatform = new LandingPlatform(area);

            // Act
            var isInArea = landingPlatform.PlatformAreaEncloses(null);

            // Assert
            Assert.False(isInArea);
        }

        [Fact]
        public void LandingPlatform_Returns_Ok_When_Accepts_A_Rocket()
        {
            // Arrange
            var area = new Area(new Coordinates(5, 5), new Coordinates(10, 10));
            var landingPlatform = new LandingPlatform(area);
            var rocket = new Rocket(Guid.NewGuid(), new Coordinates(5, 5));

            // Act
            var response = landingPlatform.AcceptRocketLandingRequest(rocket);

            // Assert
            Assert.Equal(CoordinatorResponses.OK, response);
        }

        [Fact]
        public void LandingPlatform_Returns_Ok_When_Accepts_New_Coordinates_For_Existing_Rocket()
        {
            // Arrange
            var area = new Area(new Coordinates(5, 5), new Coordinates(10, 10));
            var landingPlatform = new LandingPlatform(area);
            var rocketId = Guid.NewGuid();
            var rocket = new Rocket(rocketId, new Coordinates(5, 5));
            landingPlatform.AcceptRocketLandingRequest(rocket);

            // Act
            var response = landingPlatform.AcceptRocketLandingRequest(new Rocket(rocketId, new Coordinates(10, 10)));

            // Assert
            Assert.Equal(CoordinatorResponses.OK, response);
        }

        [Fact]
        public void LandingPlatform_Returns_Clash_When_Coordinates_Close_To_An_Existing_Rocket()
        {
            // Arrange
            var area = new Area(new Coordinates(5, 5), new Coordinates(10, 10));
            var landingPlatform = new LandingPlatform(area);
            var rocket = new Rocket(Guid.NewGuid(), new Coordinates(5, 5));
            landingPlatform.AcceptRocketLandingRequest(rocket);

            // Act
            var response = landingPlatform.AcceptRocketLandingRequest(new Rocket(Guid.NewGuid(), new Coordinates(5, 5)));

            // Assert
            Assert.Equal(CoordinatorResponses.CLASH, response);
        }

        [Fact]
        public void LandingPlatform_Returns_Out_When_Out_Of_Platforms_Area()
        {
            // Arrange
            var area = new Area(new Coordinates(5, 5), new Coordinates(10, 10));
            var landingPlatform = new LandingPlatform(area);

            // Act
            var response = landingPlatform.AcceptRocketLandingRequest(new Rocket(Guid.NewGuid(), new Coordinates(20, 20)));

            // Assert
            Assert.Equal(CoordinatorResponses.OUT, response);
        }

    }
}
