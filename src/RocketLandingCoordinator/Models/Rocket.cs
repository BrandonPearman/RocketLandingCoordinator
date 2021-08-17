using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RocketLandingCoordinatorTests")]
namespace RocketLandingCoordinator
{
    internal class Rocket
    {
        private Coordinates _landingPosition;
        private Area _landingArea;

        public Guid RocketId { get; }
        public Coordinates LandingPosition 
        {
            get
            {
                return _landingPosition;
            }
            set
            {
                _landingPosition = value;
                if (_landingPosition != null)
                {
                    var start = new Coordinates(_landingPosition.X - 1, _landingPosition.Y - 1);
                    var end = new Coordinates(_landingPosition.X + 1, _landingPosition.Y + 1);
                    _landingArea = new Area(start, end);                 
                }
                else
                {
                    _landingArea = null;
                }
            }
        }

        public Rocket(Guid rocketId, Coordinates landingPosition)
        {
            RocketId = rocketId;
            LandingPosition = landingPosition;
        }

        public bool LandingSpaceClashesWith(Coordinates coordinates)
        {
            if (_landingArea == null || coordinates == null)
            {
                return false;
            }

            return coordinates.IsInArea(_landingArea);
        }
    }
}
