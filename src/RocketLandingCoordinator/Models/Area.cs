using RocketLandingCoordinator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLandingCoordinator
{
    public class Area
    {
        public int Top { get; }
        public int Left { get; }
        public int Bottom { get; }
        public int Right { get; }

        public Area(int top, int left, int bottom, int right)
        {
            if (top > bottom || left > right)
            {
                throw new InvalidAreaException();
            }
            
            Top = top;
            Left = left;
            Bottom = bottom;
            Right = right;
        }

        public Area(Coordinates topLeft, Coordinates bottomRight)
        {
            Top = topLeft.Y;
            Left = topLeft.X;
            Bottom = bottomRight.Y;
            Right = bottomRight.X;
        }

        public bool Surrounds(Area area)
        {
            return Top <= area.Top && Bottom >= area.Bottom && Left <= area.Left && Right >= area.Right;
        }

        public bool DoesOverLap(Area area)
        {
            return !(area.Top > Bottom || area.Bottom < Top || area.Left > Right || area.Right < Left);
        }
    }
}
