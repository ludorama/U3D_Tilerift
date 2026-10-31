using UnityEngine;

namespace Tilerift.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Tilerift/Config/Camera", fileName = "CameraConfig")]
    public sealed class CameraConfig : ScriptableObject
    {
        [Header("Provisional Phase 1 defaults")]
        [Min(0f)] public float dragDeadzonePixels = 12f;
        [Min(0f)] public float doubleTapIntervalSeconds = 0.3f;
        [Min(0f)] public float panSpeed = 1f;
        [Min(0f)] public float recenterEasing = 8f;

        private void OnValidate()
        {
            dragDeadzonePixels = Mathf.Max(0f, dragDeadzonePixels);
            doubleTapIntervalSeconds = Mathf.Max(0f, doubleTapIntervalSeconds);
            panSpeed = Mathf.Max(0f, panSpeed);
            recenterEasing = Mathf.Max(0f, recenterEasing);
        }
    }
}
