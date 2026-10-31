using System;
using System.Collections.Generic;
using Tilerift.Gameplay.Tiles;

namespace Tilerift.Gameplay.Inventory
{
    public sealed class TileInventory
    {
        private readonly int maxHandSize;
        private readonly int previewCount;
        private readonly List<ActionTileState> hand = new List<ActionTileState>();
        private readonly Queue<ActionTileState> queue = new Queue<ActionTileState>();

        public TileInventory(int startingHandSize, int maxHandSize, int previewCount, IEnumerable<ActionTileState> startingTiles = null)
        {
            if (maxHandSize < 1) throw new ArgumentOutOfRangeException(nameof(maxHandSize));
            this.maxHandSize = maxHandSize;
            this.previewCount = Math.Max(0, previewCount);
            var initialHandLimit = Math.Min(maxHandSize, Math.Max(0, startingHandSize));
            if (startingTiles != null)
            {
                foreach (var tile in startingTiles)
                {
                    if (tile == null) continue;
                    if (hand.Count < initialHandLimit) hand.Add(tile);
                    else queue.Enqueue(tile);
                }
            }
        }

        public IReadOnlyList<ActionTileState> Hand => hand;
        public int QueuedCount => queue.Count;
        public int HiddenQueuedCount => Math.Max(0, queue.Count - previewCount);

        public void Enqueue(ActionTileState tile)
        {
            if (tile == null) throw new ArgumentNullException(nameof(tile));
            queue.Enqueue(tile);
        }

        public bool RemoveFromHand(ActionTileState tile)
        {
            if (!hand.Remove(tile)) return false;
            if (queue.Count > 0)
                hand.Add(queue.Dequeue());
            return true;
        }

        public IReadOnlyList<ActionTileState> Preview()
        {
            var result = new List<ActionTileState>();
            foreach (var tile in queue)
            {
                if (result.Count >= previewCount) break;
                result.Add(tile);
            }
            return result;
        }

    }
}
