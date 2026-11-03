using System;
using Tilerift.Core.Grid;

namespace Tilerift.Gameplay.Travel
{
    public enum TravelRunStatus
    {
        Countdown,
        Active,
        Succeeded,
        Failed
    }

    public enum TravelFailureReason
    {
        None,
        TimeExpired,
        Hazard,
        Blocked
    }

    public sealed class TravelRunState
    {
        private readonly float timeLimitSeconds;
        private readonly float startingDelaySeconds;
        private readonly GridCoordinate exitCoordinate;
        private readonly int targetCoins;
        private readonly int targetJewels;

        public TravelRunState(float timeLimitSeconds, float startingDelaySeconds, GridCoordinate exitCoordinate,
            int targetCoins, int targetJewels)
        {
            this.timeLimitSeconds = Math.Max(0.1f, timeLimitSeconds);
            this.startingDelaySeconds = Math.Max(0f, startingDelaySeconds);
            this.exitCoordinate = exitCoordinate;
            this.targetCoins = Math.Max(0, targetCoins);
            this.targetJewels = Math.Max(0, targetJewels);
            Retry();
        }

        public TravelRunStatus Status { get; private set; }
        public TravelFailureReason FailureReason { get; private set; }
        public float CountdownRemaining { get; private set; }
        public float RemainingSeconds { get; private set; }
        public int Coins { get; private set; }
        public int Jewels { get; private set; }
        public bool ObjectivesComplete => Coins >= targetCoins && Jewels >= targetJewels;

        public event Action<TravelRunStatus> StatusChanged;

        public void Tick(float deltaSeconds)
        {
            if (deltaSeconds <= 0f || Status is TravelRunStatus.Succeeded or TravelRunStatus.Failed) return;

            if (Status == TravelRunStatus.Countdown)
            {
                CountdownRemaining = Math.Max(0f, CountdownRemaining - deltaSeconds);
                if (CountdownRemaining <= 0f)
                {
                    Status = TravelRunStatus.Active;
                    StatusChanged?.Invoke(Status);
                }
                return;
            }

            RemainingSeconds = Math.Max(0f, RemainingSeconds - deltaSeconds);
            if (RemainingSeconds <= 0f)
                Fail(TravelFailureReason.TimeExpired);
        }

        public void CollectCoin() => Coins++;
        public void CollectJewel() => Jewels++;

        public bool TryReachExit(GridCoordinate coordinate)
        {
            if (Status != TravelRunStatus.Active || coordinate != exitCoordinate || !ObjectivesComplete)
                return false;
            Status = TravelRunStatus.Succeeded;
            StatusChanged?.Invoke(Status);
            return true;
        }

        public void Fail(TravelFailureReason reason)
        {
            if (Status is TravelRunStatus.Succeeded or TravelRunStatus.Failed) return;
            FailureReason = reason;
            Status = TravelRunStatus.Failed;
            StatusChanged?.Invoke(Status);
        }

        public void Retry()
        {
            CountdownRemaining = startingDelaySeconds;
            RemainingSeconds = timeLimitSeconds;
            Coins = 0;
            Jewels = 0;
            FailureReason = TravelFailureReason.None;
            Status = CountdownRemaining > 0f ? TravelRunStatus.Countdown : TravelRunStatus.Active;
        }
    }
}
