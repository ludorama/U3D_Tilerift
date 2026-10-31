using NUnit.Framework;
using Tilerift.Core.Grid;

namespace Tilerift.Tests.EditMode
{
    public sealed class GridCoordinateTests
    {
        [Test]
        public void CardinalOffsetsUseFourWayGrid()
        {
            var origin = new GridCoordinate(2, 3, 0);

            Assert.That(origin.Add(CardinalDirection.North.Offset()), Is.EqualTo(new GridCoordinate(2, 4, 0)));
            Assert.That(origin.Add(CardinalDirection.East.Offset()), Is.EqualTo(new GridCoordinate(3, 3, 0)));
            Assert.That(origin.Add(CardinalDirection.South.Offset()), Is.EqualTo(new GridCoordinate(2, 2, 0)));
            Assert.That(origin.Add(CardinalDirection.West.Offset()), Is.EqualTo(new GridCoordinate(1, 3, 0)));
        }

        [Test]
        public void ManhattanDistanceIncludesElevationForFutureCompatibility()
        {
            var origin = new GridCoordinate(2, 2, 0);
            Assert.That(origin.ManhattanDistanceTo(new GridCoordinate(2, 4, 0)), Is.EqualTo(2));
            Assert.That(origin.ManhattanDistanceTo(new GridCoordinate(3, 3, 1)), Is.EqualTo(3));
        }

        [Test]
        public void DirectionTurnsRemainCardinal()
        {
            Assert.That(CardinalDirection.North.TurnLeft(), Is.EqualTo(CardinalDirection.West));
            Assert.That(CardinalDirection.North.TurnRight(), Is.EqualTo(CardinalDirection.East));
            Assert.That(CardinalDirection.North.TurnAround(), Is.EqualTo(CardinalDirection.South));
        }
    }
}
