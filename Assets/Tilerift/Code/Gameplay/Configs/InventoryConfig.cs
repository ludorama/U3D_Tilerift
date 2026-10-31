using UnityEngine;

namespace Tilerift.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Tilerift/Config/Inventory", fileName = "InventoryConfig")]
    public sealed class InventoryConfig : ScriptableObject
    {
        [Min(0)] public int startingHandSize = 3;
        [Min(1)] public int maxHandSize = 5;
        [Min(0)] public int queuePreviewCount = 3;

        private void OnValidate()
        {
            startingHandSize = Mathf.Clamp(startingHandSize, 0, maxHandSize);
            maxHandSize = Mathf.Max(1, maxHandSize);
            queuePreviewCount = Mathf.Max(0, queuePreviewCount);
        }
    }
}
