using System.Collections.Generic;
using NUnit.Framework;
using Tilerift.Core.Grid;
using Tilerift.Core.Simulation;
using Tilerift.Gameplay.Tiles;

namespace Tilerift.Tests.EditMode
{
    public sealed class Phase1SimulationTests
    {
        [Test]
        public void CountdownDelaysMovementAndEntryIsDeterministic()
        {
            var simulation = new HeroMovementSimulation(
                new GridCoordinate(0, 0, 0), CardinalDirection.East,
                startingDelaySeconds: 3f, cellTravelSeconds: 1f, pauseDurationSeconds: 2f,
                canEnter: _ => true);

            simulation.Tick(2.9f);
            Assert.That(simulation.State, Is.EqualTo(MovementState.Countdown));
            Assert.That(simulation.Position, Is.EqualTo(new GridCoordinate(0, 0, 0)));

            simulation.Tick(1.1f);
            Assert.That(simulation.State, Is.EqualTo(MovementState.Moving));
            Assert.That(simulation.Position, Is.EqualTo(new GridCoordinate(1, 0, 0)));
        }

        [Test]
        public void PauseOccupancyConsumesConfiguredDurationThenResumes()
        {
            var simulation = new HeroMovementSimulation(
                new GridCoordinate(0, 0, 0), CardinalDirection.East,
                startingDelaySeconds: 0f, cellTravelSeconds: 1f, pauseDurationSeconds: 2f,
                canEnter: _ => true);

            simulation.Pause();
            simulation.Tick(1.5f);
            Assert.That(simulation.State, Is.EqualTo(MovementState.Paused));
            Assert.That(simulation.Position, Is.EqualTo(new GridCoordinate(0, 0, 0)));

            simulation.Tick(0.5f);
            Assert.That(simulation.State, Is.EqualTo(MovementState.Moving));
            simulation.Tick(1f);
            Assert.That(simulation.Position, Is.EqualTo(new GridCoordinate(1, 0, 0)));
        }

        [Test]
        public void UnexpectedBlockedEntryIsTerminal()
        {
            var simulation = new HeroMovementSimulation(
                new GridCoordinate(0, 0, 0), CardinalDirection.North,
                startingDelaySeconds: 0f, cellTravelSeconds: 1f, pauseDurationSeconds: 2f,
                canEnter: _ => false);

            simulation.Tick(1f);
            Assert.That(simulation.State, Is.EqualTo(MovementState.Blocked));
        }

        [Test]
        public void PlacementRevalidatesDistanceAndOccupancy()
        {
            var tile = new ActionTileState("turn-left-1", isConsumable: false);
            var occupied = new HashSet<GridCoordinate> { new GridCoordinate(2, 0, 0) };
            var validator = new PlacementValidator(2, _ => true);

            Assert.That(validator.Validate(new GridCoordinate(0, 0, 0), new GridCoordinate(1, 0, 0), tile, occupied).Failure,
                Is.EqualTo(PlacementFailure.TooCloseToHero));
            Assert.That(validator.Validate(new GridCoordinate(0, 0, 0), new GridCoordinate(2, 0, 0), tile, occupied).Failure,
                Is.EqualTo(PlacementFailure.Occupied));
            Assert.That(validator.Validate(new GridCoordinate(0, 0, 0), new GridCoordinate(0, 2, 0), tile, occupied).IsValid,
                Is.True);
        }

        [Test]
        public void ReusableTileLocksAfterActivationAndConsumableDisappears()
        {
            var reusable = new ActionTileState("pause-1", isConsumable: false);
            reusable.Place(new GridCoordinate(2, 0, 0));
            Assert.That(reusable.Activate(), Is.True);
            Assert.That(reusable.IsLocked, Is.True);

            var consumable = new ActionTileState("pause-2", isConsumable: true);
            consumable.Place(new GridCoordinate(2, 1, 0));
            Assert.That(consumable.Activate(), Is.True);
            Assert.That(consumable.IsPlaced, Is.False);
        }
    }
}
