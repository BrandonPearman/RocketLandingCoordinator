using RocketLandingCoordinator;
using RocketLandingCoordinator.Exceptions;
using System;
using Xunit;

namespace RocketLandingCoordinatorTests
{
    public class GeometeryTests
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
        public void Coordinates_Correctly_Checks_If_It_Is_WithIn_An_Area(int x, int y, bool shouldBeInArea)
        {
            // Arrange
            var area = new Area(new Coordinates(5, 5), new Coordinates(10, 10));
            var coordinates = new Coordinates(x, y);

            // Act
            var isInArea = coordinates.IsInArea(area);

            // Assert
            Assert.Equal(shouldBeInArea, isInArea);
        }

        [Theory]
        [InlineData(10, 5, 5, 10)]
        [InlineData(5, 10, 10, 5)]
        public void When_Area_Constructed_Incorrectly_Throw_InvalidAreaException(int top, int left, int bottom, int right)
        {
            Assert.Throws<InvalidAreaException>(() => new Area(top, left, bottom, right));
        }

        [Theory]
        [InlineData(4, 4, 11, 11, true)]
        [InlineData(6, 6, 9, 9, true)]
        [InlineData(5, 5, 10, 10, true)]
        [InlineData(9, 9, 12, 12, true)]
        [InlineData(11, 11, 12, 12, false)]
        [InlineData(6, 11, 9, 12, false)]
        [InlineData(11, 6, 12, 9, false)]
        [InlineData(6, 3, 9, 4, false)]
        [InlineData(3, 6, 4, 9, false)]
        public void Area_Correctly_Checks_If_It_Overlaps_Another_Area(int top, int left, int bottom, int right, bool shouldOverlap)
        {
            // Arrange
            var area1 = new Area(5,5, 10, 10);
            var area2 = new Area(top, left, bottom, right);

            // Act
            var overlaps = area1.DoesOverLap(area2);

            // Assert
            Assert.Equal(shouldOverlap, overlaps);
        }

        [Theory]
        [InlineData(5, 5, 10, 10, true)]
        [InlineData(6, 6, 9, 9, true)]
        [InlineData(4, 4, 11, 11, false)]
        [InlineData(4, 4, 9, 9, false)]
        [InlineData(6, 6, 11, 11, false)]
        public void Area_Correctly_Checks_If_It_Surrounds_Another_Area(int top, int left, int bottom, int right, bool shouldOverlap)
        {
            // Arrange
            var area1 = new Area(5, 5, 10, 10);
            var area2 = new Area(top, left, bottom, right);

            // Act
            var overlaps = area1.Surrounds(area2);

            // Assert
            Assert.Equal(shouldOverlap, overlaps);
        }

    }
}
