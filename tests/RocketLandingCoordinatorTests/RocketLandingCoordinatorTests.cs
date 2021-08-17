using RocketLandingCoordinator;
using RocketLandingCoordinator.Exceptions;
using System;
using Xunit;

namespace RocketLandingCoordinatorTests
{
    public class RocketLandingCoordinatorTests
    {

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Ok_When_Accepts_Rocket()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5,14, 14);

            // Act
            var response = rocketLandingCoordinatorMain.Check(Guid.NewGuid(), 12, 7);

            // Assert
            Assert.Equal(CoordinatorResponses.OK, response);
        }

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Ok_When_Accepts_Rockets_On_Each_Platform()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5, 14, 14);
            rocketLandingCoordinatorMain.AddPlatform(20, 20, 21, 21);

            // Act
            var response1 = rocketLandingCoordinatorMain.Check(Guid.NewGuid(), 12, 7);
            var response2 = rocketLandingCoordinatorMain.Check(Guid.NewGuid(), 20, 20);

            // Assert
            Assert.Equal(CoordinatorResponses.OK, response1);
            Assert.Equal(CoordinatorResponses.OK, response2);
        }

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Out_When_Out_Of_Range_Of_Area()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5, 14, 14);

            // Act
            var response = rocketLandingCoordinatorMain.Check(Guid.NewGuid(), new Coordinates(200, 200));

            // Assert
            Assert.Equal(CoordinatorResponses.OUT, response);
        }

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Out_When_Out_Of_Range_Of_Platform()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5, 14, 14);

            // Act
            var response = rocketLandingCoordinatorMain.Check(Guid.NewGuid(), new Coordinates(90, 90));

            // Assert
            Assert.Equal(CoordinatorResponses.OUT, response);
        }

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Out_When_Coordinates_Are_null()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5, 14, 14);

            // Act
            var response = rocketLandingCoordinatorMain.Check(Guid.NewGuid(), null);

            // Assert
            Assert.Equal(CoordinatorResponses.OUT, response);
        }

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Clash_When_Another_Rocket_Is_There()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5, 14, 14);
            rocketLandingCoordinatorMain.Check(Guid.NewGuid(), new Coordinates(6, 6));

            // Act
            var response = rocketLandingCoordinatorMain.Check(Guid.NewGuid(), new Coordinates(6, 6));

            // Assert
            Assert.Equal(CoordinatorResponses.CLASH, response);
        }

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Clash_When_Another_Rocket_Is_Directly_Next_To_Existing()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5, 14, 14);
            rocketLandingCoordinatorMain.Check(Guid.NewGuid(), new Coordinates(6, 6));

            // Act
            var response = rocketLandingCoordinatorMain.Check(Guid.NewGuid(), new Coordinates(7, 7));

            // Assert
            Assert.Equal(CoordinatorResponses.CLASH, response);
        }

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Ok_When_A_Rocket_Moves_And_Another_Takes_Its_Place()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5, 14, 14);
            var rocketId = Guid.NewGuid();
            var intitalResponse = rocketLandingCoordinatorMain.Check(rocketId, new Coordinates(6, 6));
            var moveResponse = rocketLandingCoordinatorMain.Check(rocketId, new Coordinates(10, 10));

            // Act
            var newRocketId = Guid.NewGuid();
            var newRocketMovesInResponse = rocketLandingCoordinatorMain.Check(newRocketId, new Coordinates(6, 6));

            // Assert
            Assert.Equal(CoordinatorResponses.OK, intitalResponse);
            Assert.Equal(CoordinatorResponses.OK, moveResponse);
            Assert.Equal(CoordinatorResponses.OK, newRocketMovesInResponse);
        }

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Ok_When_A_Rocket_Moves_And_comes_Back()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5, 14, 14);
            var rocketId = Guid.NewGuid();
            var intitalResponse = rocketLandingCoordinatorMain.Check(rocketId, new Coordinates(6, 6));
            var moveResponse = rocketLandingCoordinatorMain.Check(rocketId, new Coordinates(10, 10));

            // Act
            var moveBackResponse = rocketLandingCoordinatorMain.Check(rocketId, new Coordinates(6, 6));

            // Assert
            Assert.Equal(CoordinatorResponses.OK, intitalResponse);
            Assert.Equal(CoordinatorResponses.OK, moveResponse);
            Assert.Equal(CoordinatorResponses.OK, moveBackResponse);
        }

        [Fact]
        public void RocketLandingCoordinatorMain_Returns_Ok_When_A_Rocket_Checks_The_Same_Location()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(5, 5, 14, 14);
            var rocketId = Guid.NewGuid();
            rocketLandingCoordinatorMain.Check(rocketId, new Coordinates(6, 6));

            // Act
            var response = rocketLandingCoordinatorMain.Check(rocketId, new Coordinates(6, 6));

            // Assert
            Assert.Equal(CoordinatorResponses.OK, response);
        }

        [Fact]
        public void When_Platform_Not_In_LandingArea_RocketLandingCoordinatorMain_Throws_PlatformOutOfBoundsException()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);

            // Act & Assert
            Assert.Throws<PlatformOutOfBoundsException>(() => rocketLandingCoordinatorMain.AddPlatform(new Coordinates(200, 200), new Coordinates(200, 200)));
        }

        [Fact]
        public void When_Platform_Overlaps_Another_RocketLandingCoordinatorMain_Throws_PlatformAreaOverlapException()
        {
            // Arrange
            var rocketLandingCoordinatorMain = new RocketLandingCoordinatorMain(100, 100);
            rocketLandingCoordinatorMain.AddPlatform(new Coordinates(50, 50), new Coordinates(50, 50));

            // Act & Assert
            Assert.Throws<PlatformAreaOverlapException>(() => rocketLandingCoordinatorMain.AddPlatform(new Coordinates(50, 50), new Coordinates(50, 50)));
        }
    }
}
