using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RocketLandingCoordinatorTests")]
namespace RocketLandingCoordinator
{
    internal class LandingPlatform
    {
        private Area _area;
        private Dictionary<Guid, Rocket> _rockets;

        public LandingPlatform(Area area)
        {
            _area = area;
            _rockets = new Dictionary<Guid, Rocket>();
        }
             
        public string AcceptRocketLandingRequest(Rocket rocket)
        {
            if (PlatformAreaEncloses(rocket.LandingPosition) == false)
            {
                return CoordinatorResponses.OUT;
            }

            if (ExistingRocketsClashWith(rocket))
            {
                return CoordinatorResponses.CLASH;
            }

            if (_rockets.ContainsKey(rocket.RocketId))
            {
                _rockets[rocket.RocketId].LandingPosition = rocket.LandingPosition;
            }
            else
            {
                _rockets.Add(rocket.RocketId, rocket);
            }

            return CoordinatorResponses.OK;
        }

        public bool PlatformAreaEncloses(Coordinates coordinates)
        {
            if (coordinates == null)
            {
                return false;
            }

            return coordinates.IsInArea(_area);
        }

        private bool ExistingRocketsClashWith(Rocket newRocket)
        {
            foreach (var rocket in _rockets)
            {
                if (newRocket.RocketId == rocket.Value.RocketId)
                {
                    continue;
                }

                if (rocket.Value.LandingSpaceClashesWith(newRocket.LandingPosition))
                {
                    return true;
                }
            }

            return false;
        }

        public bool DoesOverLap(Area area)
        {
            return area.DoesOverLap(_area);
        }
    }
}
