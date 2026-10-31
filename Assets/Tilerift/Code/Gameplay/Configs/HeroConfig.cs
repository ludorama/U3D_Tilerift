using UnityEngine;
using Tilerift.Core.Grid;

namespace Tilerift.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Tilerift/Config/Hero", fileName = "HeroConfig")]
    public sealed class HeroConfig : ScriptableObject
    {
        [Header("Provisional Phase 0 defaults")]
        [Min(1)] public int baseHitPoints = 1;
        [Min(0)] public int combatBonus = 0;
        [Min(0.01f)] public float cellsPerSecond = 1f;
        public CardinalDirection entryFacing = CardinalDirection.North;
        [Min(0.01f)] public float collisionRadius = 0.35f;
        [Min(0f)] public float startingDelaySeconds = 3f;

        private void OnValidate()
        {
            baseHitPoints = Mathf.Max(1, baseHitPoints);
            combatBonus = Mathf.Max(0, combatBonus);
            cellsPerSecond = Mathf.Max(0.01f, cellsPerSecond);
            collisionRadius = Mathf.Max(0.01f, collisionRadius);
            startingDelaySeconds = Mathf.Max(0f, startingDelaySeconds);
        }
    }
}
