using UnityEngine;

namespace Tilerift.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Tilerift/Config/Movement", fileName = "MovementConfig")]
    public sealed class MovementConfig : ScriptableObject
    {
        [Header("Provisional Phase 0 defaults")]
        [Min(0.001f)] public float simulationTickSeconds = 0.05f;
        [Min(0.001f)] public float cellTravelSeconds = 1f;
        [Min(0f)] public float pauseTileSeconds = 2f;
        [Min(1)] public int jumpRangeCells = 1;
        [Min(1)] public int fatalFallHeightLevels = 2;

        private void OnValidate()
        {
            simulationTickSeconds = Mathf.Max(0.001f, simulationTickSeconds);
            cellTravelSeconds = Mathf.Max(0.001f, cellTravelSeconds);
            pauseTileSeconds = Mathf.Max(0f, pauseTileSeconds);
            jumpRangeCells = Mathf.Max(1, jumpRangeCells);
            fatalFallHeightLevels = Mathf.Max(1, fatalFallHeightLevels);
        }
    }
}
