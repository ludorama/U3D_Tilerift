namespace Tilerift.Core.Grid
{
    public enum CardinalDirection
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public static class CardinalDirectionExtensions
    {
        public static GridCoordinate Offset(this CardinalDirection direction)
        {
            return direction switch
            {
                CardinalDirection.North => new GridCoordinate(0, 1, 0),
                CardinalDirection.East => new GridCoordinate(1, 0, 0),
                CardinalDirection.South => new GridCoordinate(0, -1, 0),
                CardinalDirection.West => new GridCoordinate(-1, 0, 0),
                _ => new GridCoordinate(0, 0, 0)
            };
        }

        public static CardinalDirection TurnLeft(this CardinalDirection direction)
        {
            return (CardinalDirection)(((int)direction + 3) % 4);
        }

        public static CardinalDirection TurnRight(this CardinalDirection direction)
        {
            return (CardinalDirection)(((int)direction + 1) % 4);
        }

        public static CardinalDirection TurnAround(this CardinalDirection direction)
        {
            return (CardinalDirection)(((int)direction + 2) % 4);
        }
    }
}
