using System;
using Tilerift.Core.Grid;

namespace Tilerift.Gameplay.Tiles
{
    public sealed class ActionTileState
    {
        public string InstanceId { get; }
        public GridCoordinate Coordinate { get; private set; }
        public bool IsPlaced { get; private set; }
        public bool IsActivated { get; private set; }
        public bool IsLocked => IsActivated && !IsConsumable;
        public bool IsConsumable { get; }

        public ActionTileState(string instanceId, bool isConsumable)
        {
            if (string.IsNullOrWhiteSpace(instanceId)) throw new ArgumentException("Tile instance ID is required.", nameof(instanceId));
            InstanceId = instanceId;
            IsConsumable = isConsumable;
        }

        public void Place(GridCoordinate coordinate)
        {
            if (IsLocked) throw new InvalidOperationException("Activated reusable tiles cannot be moved.");
            Coordinate = coordinate;
            IsPlaced = true;
        }

        public void Retrieve()
        {
            if (IsLocked) throw new InvalidOperationException("Activated reusable tiles cannot be retrieved.");
            IsPlaced = false;
        }

        public bool Activate()
        {
            if (!IsPlaced || IsActivated) return false;
            IsActivated = true;
            if (IsConsumable) IsPlaced = false;
            return true;
        }
    }
}
