using UnityEngine;

namespace Tilerift.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Tilerift/Config/Combat", fileName = "CombatConfig")]
    public sealed class CombatConfig : ScriptableObject
    {
        [Min(0)] public int sideFacingBonus = 1;
        [Min(0)] public int backFacingBonus = 2;
        [Range(0f, 0.5f)] public float presentationDurationSeconds = 0.5f;

        private void OnValidate()
        {
            sideFacingBonus = Mathf.Max(0, sideFacingBonus);
            backFacingBonus = Mathf.Max(sideFacingBonus, backFacingBonus);
            presentationDurationSeconds = Mathf.Clamp(presentationDurationSeconds, 0f, 0.5f);
        }
    }
}
