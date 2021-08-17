using RocketLandingCoordinator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RocketLandingCoordinator
{
    public class RocketLandingCoordinatorMain
    {
        private Area _landingArea;
        private List<LandingPlatform> _landingPlatforms;

        /// <summary>
        /// Create a new Coordinator with its landing area
        /// </summary>
        /// <param name="landingAreaWidth">This is the max width of how far the X axis goes</param>
        /// <param name="landingAreaHeight">This is the max height of how far the Y axis goes</param>
        public RocketLandingCoordinatorMain(int landingAreaWidth, int landingAreaHeight)
        {
            var start = new Coordinates(0, 0);
            var end = new Coordinates(landingAreaWidth, landingAreaHeight);
            _landingArea = new Area(start, end);
            _landingPlatforms = new List<LandingPlatform>();
        }

        /// <summary>
        /// Add a platform for rockets to land on
        /// The Coordinates given are inclusive eg 1,1,1,1 will create a single block for the platform
        /// </summary>
        /// <param name="topLeft">top left Coordinates of the platform, center is at 0,0</param>
        /// <param name="bottomRight">bottom right Coordinates of the platform</param>
        public void AddPlatform(Coordinates topLeft, Coordinates bottomRight)
        {
            AddPlatform(topLeft.Y, topLeft.X, bottomRight.Y, bottomRight.X);
        }

        /// <summary>
        /// Add a platform for rockets to land on
        /// The Coordinates given are inclusive eg 1,1,1,1 will create a single block for the platform
        /// </summary>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="bottom"></param>
        /// <param name="right"></param>
        public void AddPlatform(int top, int left, int bottom, int right)
        {
            var platformArea = new Area(top, left, bottom, right);

            if (_landingArea.Surrounds(platformArea) == false)
            {
                throw new PlatformOutOfBoundsException();
            }

            foreach (var landingPlatform in _landingPlatforms)
            {
                if (landingPlatform.DoesOverLap(platformArea))
                {
                    throw new PlatformAreaOverlapException();
                }
            }

            _landingPlatforms.Add(new LandingPlatform(platformArea));
        }

        /// <summary>
        /// Check if a specific rocket can land at given Coordinates
        /// If it can it will reserve those Coordinates
        /// </summary>
        /// <param name="rocketId">The Id of the incoming rocket</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>CoordinatorResponses = "ok for landing" / "out of platform" / "clash"</returns>
        public string Check(Guid rocketId, int x, int y)
        {
            return Check(rocketId, new Coordinates(x, y));
        }

        /// <summary>
        /// Check if a specific rocket can land at given Coordinates
        /// If it can it will reserve those Coordinates
        /// </summary>
        /// <param name="rocketId">The Id of the incoming rocket</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>CoordinatorResponses = "ok for landing" / "out of platform" / "clash"</returns>
        public string Check(Guid rocketId, Coordinates coordinates)
        {
            if (coordinates == null)
            {
                return CoordinatorResponses.OUT;
            }

            var landingPlatform = _landingPlatforms.SingleOrDefault(lp => lp.PlatformAreaEncloses(coordinates));
            if (landingPlatform == null)
            {
                return CoordinatorResponses.OUT;
            }

            return landingPlatform.AcceptRocketLandingRequest(new Rocket(rocketId, coordinates));
        }
    }
}
