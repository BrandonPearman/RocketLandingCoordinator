using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLandingCoordinator.Exceptions
{
    public class PlatformOutOfBoundsException : Exception
    {
        public PlatformOutOfBoundsException() : base("The platform area you have selected does not fit in the landing area.")
        {
        }
    }
}
