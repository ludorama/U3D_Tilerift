using System;
using Tilerift.Core.Grid;

namespace Tilerift.Core.Simulation
{
    public sealed class HeroMovementSimulation
    {
        private readonly float startingDelaySeconds;
        private readonly float cellTravelSeconds;
        private readonly float pauseDurationSeconds;
        private readonly Func<GridCoordinate, bool> canEnter;
        private float countdownRemaining;
        private float pauseRemaining;
        private float travelRemaining;

        public GridCoordinate Position { get; private set; }
        public CardinalDirection Facing { get; private set; }
        public MovementState State { get; private set; }
        public float CountdownRemaining => countdownRemaining;
        public float TravelProgress => cellTravelSeconds <= 0f ? 1f : 1f - travelRemaining / cellTravelSeconds;

        public event Action<GridCoordinate> EnteredCell;
        public event Action Blocked;

        public HeroMovementSimulation(
            GridCoordinate start,
            CardinalDirection facing,
            float startingDelaySeconds,
            float cellTravelSeconds,
            float pauseDurationSeconds,
            Func<GridCoordinate, bool> canEnter)
        {
            Position = start;
            Facing = facing;
            this.startingDelaySeconds = Math.Max(0f, startingDelaySeconds);
            this.cellTravelSeconds = Math.Max(float.Epsilon, cellTravelSeconds);
            this.pauseDurationSeconds = Math.Max(0f, pauseDurationSeconds);
            this.canEnter = canEnter ?? throw new ArgumentNullException(nameof(canEnter));
            countdownRemaining = this.startingDelaySeconds;
            State = countdownRemaining > 0f ? MovementState.Countdown : MovementState.Moving;
        }

        public void Tick(float deltaSeconds)
        {
            if (deltaSeconds <= 0f || State is MovementState.Blocked or MovementState.Finished)
                return;

            var remaining = deltaSeconds;
            while (remaining > 0f && State is not MovementState.Blocked and not MovementState.Finished)
            {
                switch (State)
                {
                    case MovementState.Countdown:
                        AdvanceCountdown(ref remaining);
                        break;
                    case MovementState.Paused:
                        AdvancePause(ref remaining);
                        break;
                    case MovementState.Moving:
                        AdvanceMovement(ref remaining);
                        break;
                }
            }
        }

        public void Pause()
        {
            if (State == MovementState.Moving)
            {
                pauseRemaining = pauseDurationSeconds;
                State = pauseRemaining > 0f ? MovementState.Paused : MovementState.Moving;
            }
        }

        public void TurnLeft() => Facing = Facing.TurnLeft();
        public void TurnRight() => Facing = Facing.TurnRight();
        public void TurnAround() => Facing = Facing.TurnAround();
        public void Finish() => State = MovementState.Finished;

        private void AdvanceCountdown(ref float remaining)
        {
            var elapsed = Math.Min(remaining, countdownRemaining);
            countdownRemaining -= elapsed;
            remaining -= elapsed;
            if (countdownRemaining <= 0f)
                State = MovementState.Moving;
        }

        private void AdvancePause(ref float remaining)
        {
            var elapsed = Math.Min(remaining, pauseRemaining);
            pauseRemaining -= elapsed;
            remaining -= elapsed;
            if (pauseRemaining <= 0f)
                State = MovementState.Moving;
        }

        private void AdvanceMovement(ref float remaining)
        {
            if (travelRemaining <= 0f)
            {
                var destination = Position.Add(Facing.Offset());
                if (!canEnter(destination))
                {
                    State = MovementState.Blocked;
                    Blocked?.Invoke();
                    return;
                }

                travelRemaining = cellTravelSeconds;
            }

            var elapsed = Math.Min(remaining, travelRemaining);
            travelRemaining -= elapsed;
            remaining -= elapsed;
            if (travelRemaining <= 0f)
            {
                Position = Position.Add(Facing.Offset());
                EnteredCell?.Invoke(Position);
            }
        }
    }
}
