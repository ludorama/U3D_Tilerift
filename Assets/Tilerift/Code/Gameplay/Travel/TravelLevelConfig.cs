using System;
using System.Collections.Generic;
using Tilerift.Core.Grid;
using UnityEngine;

namespace Tilerift.Gameplay.Travel
{
    [Serializable]
    public sealed class TravelGridCell
    {
        public int x;
        public int y;
        public int level;

        public GridCoordinate Coordinate => new GridCoordinate(x, y, level);
    }

    [CreateAssetMenu(menuName = "Tilerift/Travel/Level", fileName = "TravelLevel")]
    public sealed class TravelLevelConfig : ScriptableObject
    {
        [Header("Identity and timing")]
        public string levelId = "travel_001";
        [Min(0.1f)] public float timeLimitSeconds = 30f;
        [Min(0f)] public float startingDelaySeconds = 3f;
        [Min(0.001f)] public float cellTravelSeconds = 1f;

        [Header("Route")]
        public TravelGridCell start = new TravelGridCell();
        public TravelGridCell exit = new TravelGridCell { x = 0, y = 2 };
        public CardinalDirection startingFacing = CardinalDirection.North;
        public List<TravelGridCell> walkableCells = new List<TravelGridCell>();
        public List<TravelGridCell> hazardCells = new List<TravelGridCell>();

        [Header("Objectives and collectibles")]
        [Min(0)] public int targetCoins;
        [Min(0)] public int targetJewels = 1;
        public List<TravelGridCell> coinCells = new List<TravelGridCell>();
        public List<TravelGridCell> jewelCells = new List<TravelGridCell>();

        private void OnValidate()
        {
            levelId = string.IsNullOrWhiteSpace(levelId) ? name : levelId.Trim();
            timeLimitSeconds = Mathf.Max(0.1f, timeLimitSeconds);
            startingDelaySeconds = Mathf.Max(0f, startingDelaySeconds);
            cellTravelSeconds = Mathf.Max(0.001f, cellTravelSeconds);
            targetCoins = Mathf.Max(0, targetCoins);
            targetJewels = Mathf.Max(0, targetJewels);
        }

        public bool IsWalkable(GridCoordinate coordinate)
        {
            return walkableCells.Count == 0 || Contains(walkableCells, coordinate);
        }

        public bool IsHazard(GridCoordinate coordinate) => Contains(hazardCells, coordinate);

        public bool HasCoin(GridCoordinate coordinate) => Contains(coinCells, coordinate);

        public bool HasJewel(GridCoordinate coordinate) => Contains(jewelCells, coordinate);

        public GridCoordinate StartCoordinate => start.Coordinate;
        public GridCoordinate ExitCoordinate => exit.Coordinate;

        private static bool Contains(IEnumerable<TravelGridCell> cells, GridCoordinate coordinate)
        {
            foreach (var cell in cells)
                if (cell != null && cell.Coordinate == coordinate) return true;
            return false;
        }

    }
}
