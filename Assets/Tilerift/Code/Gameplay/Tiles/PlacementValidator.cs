using System;
using System.Collections.Generic;
using Tilerift.Core.Grid;

namespace Tilerift.Gameplay.Tiles
{
    public enum PlacementFailure
    {
        None,
        TooCloseToHero,
        Occupied,
        Incompatible,
        TileLocked
    }

    public readonly struct PlacementResult
    {
        public readonly bool IsValid;
        public readonly PlacementFailure Failure;

        public PlacementResult(bool isValid, PlacementFailure failure)
        {
            IsValid = isValid;
            Failure = failure;
        }
    }

    public sealed class PlacementValidator
    {
        private readonly int minimumDistance;
        private readonly Func<GridCoordinate, bool> isCompatible;

        public PlacementValidator(int minimumDistance, Func<GridCoordinate, bool> isCompatible)
        {
            this.minimumDistance = Math.Max(0, minimumDistance);
            this.isCompatible = isCompatible ?? throw new ArgumentNullException(nameof(isCompatible));
        }

        public PlacementResult Validate(GridCoordinate hero, GridCoordinate destination, ActionTileState tile, ISet<GridCoordinate> occupied)
        {
            if (tile == null) throw new ArgumentNullException(nameof(tile));
            if (occupied == null) throw new ArgumentNullException(nameof(occupied));
            if (tile.IsLocked) return new PlacementResult(false, PlacementFailure.TileLocked);
            if (hero.ManhattanDistanceTo(destination) < minimumDistance) return new PlacementResult(false, PlacementFailure.TooCloseToHero);
            if (occupied.Contains(destination)) return new PlacementResult(false, PlacementFailure.Occupied);
            if (!isCompatible(destination)) return new PlacementResult(false, PlacementFailure.Incompatible);
            return new PlacementResult(true, PlacementFailure.None);
        }

        public IEnumerable<GridCoordinate> GetValidCells(GridCoordinate hero, IEnumerable<GridCoordinate> candidates, ActionTileState tile, ISet<GridCoordinate> occupied)
        {
            foreach (var candidate in candidates)
                if (Validate(hero, candidate, tile, occupied).IsValid)
                    yield return candidate;
        }
    }
}
