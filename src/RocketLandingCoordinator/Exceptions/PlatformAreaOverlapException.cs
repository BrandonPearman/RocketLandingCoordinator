using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLandingCoordinator.Exceptions
{
    public class PlatformAreaOverlapException : Exception
    {
        public PlatformAreaOverlapException() : base("The platform area you have selected overlaps with an existing platform.")
        {
        }
    }
}
