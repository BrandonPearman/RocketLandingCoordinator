using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLandingCoordinator.Exceptions
{
    public class InvalidAreaException : Exception
    {
        public InvalidAreaException() : base("The area is invalid. Please make sure the top is smaller than the bottom and that the left is smaller than the right.")
        {
        }
    }
}
