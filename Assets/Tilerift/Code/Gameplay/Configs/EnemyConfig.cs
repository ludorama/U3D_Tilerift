using UnityEngine;

namespace Tilerift.Gameplay.Configs
{
    public enum EnemyMovementType
    {
        Stationary
    }

    [CreateAssetMenu(menuName = "Tilerift/Config/Enemy", fileName = "EnemyConfig")]
    public sealed class EnemyConfig : ScriptableObject
    {
        [Min(0)] public int combatBonus = 0;
        [Min(1)] public int baseHitPoints = 1;
        public EnemyMovementType movementType = EnemyMovementType.Stationary;

        private void OnValidate()
        {
            combatBonus = Mathf.Max(0, combatBonus);
            baseHitPoints = Mathf.Max(1, baseHitPoints);
            movementType = EnemyMovementType.Stationary;
        }
    }
}
