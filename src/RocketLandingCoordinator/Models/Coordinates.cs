namespace RocketLandingCoordinator
{
    public class Coordinates
    {
        public int X { get; }
        public int Y { get; }

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsInArea(Area area)
        {
            return X >= area.Left && X <= area.Right &&
                Y >= area.Top && Y <= area.Bottom;
        }
    }
}
