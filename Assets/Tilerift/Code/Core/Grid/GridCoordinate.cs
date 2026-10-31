using System;

namespace Tilerift.Core.Grid
{
    [Serializable]
    public readonly struct GridCoordinate : IEquatable<GridCoordinate>
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Level;

        public GridCoordinate(int x, int y, int level)
        {
            X = x;
            Y = y;
            Level = level;
        }

        public GridCoordinate Add(GridCoordinate offset)
        {
            return new GridCoordinate(X + offset.X, Y + offset.Y, Level + offset.Level);
        }

        public int ManhattanDistanceTo(GridCoordinate other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Level - other.Level);
        }

        public bool Equals(GridCoordinate other)
        {
            return X == other.X && Y == other.Y && Level == other.Level;
        }

        public override bool Equals(object obj)
        {
            return obj is GridCoordinate other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Level);
        }

        public override string ToString()
        {
            return $"({X},{Y},{Level})";
        }

        public static bool operator ==(GridCoordinate left, GridCoordinate right) => left.Equals(right);
        public static bool operator !=(GridCoordinate left, GridCoordinate right) => !left.Equals(right);
    }
}
