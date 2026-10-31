using UnityEngine;
using Tilerift.Gameplay.Configs;
using Tilerift.Input;
using UnityCamera = UnityEngine.Camera;

namespace Tilerift.Presentation.Camera
{
    public sealed class TileriftCameraController : MonoBehaviour
    {
        [SerializeField] private CameraConfig config;
        [SerializeField] private TileriftInputRouter inputRouter;
        [SerializeField] private Transform heroTarget;
        [SerializeField] private UnityCamera targetCamera;

        private bool pointerHeld;
        private bool dragging;
        private Vector2 pressPosition;
        private Vector2 lastPointerPosition;
        private float lastTapTime = float.NegativeInfinity;
        private Vector3 targetOffset;

        private void Awake()
        {
            if (targetCamera == null) targetCamera = GetComponent<UnityCamera>();
            if (inputRouter == null) inputRouter = FindAnyObjectByType<TileriftInputRouter>();
            if (targetCamera != null && heroTarget != null) targetOffset = targetCamera.transform.position - heroTarget.position;
        }

        private void OnEnable()
        {
            if (inputRouter == null) return;
            inputRouter.Pressed += HandlePressed;
            inputRouter.Released += HandleReleased;
            inputRouter.Pointed += HandlePointed;
        }

        private void OnDisable()
        {
            if (inputRouter == null) return;
            inputRouter.Pressed -= HandlePressed;
            inputRouter.Released -= HandleReleased;
            inputRouter.Pointed -= HandlePointed;
        }

        private void LateUpdate()
        {
            if (targetCamera == null || heroTarget == null || dragging || config == null) return;
            var desired = heroTarget.position + targetOffset;
            targetCamera.transform.position = Vector3.Lerp(targetCamera.transform.position, desired, config.recenterEasing * Time.deltaTime);
        }

        private void HandlePressed()
        {
            pointerHeld = true;
            dragging = false;
            pressPosition = inputRouter.Point;
            lastPointerPosition = pressPosition;
        }

        private void HandlePointed(Vector2 pointerPosition)
        {
            if (!pointerHeld || config == null) return;
            var totalDelta = pointerPosition - pressPosition;
            if (!dragging && totalDelta.magnitude < config.dragDeadzonePixels) return;
            dragging = true;
            var delta = pointerPosition - lastPointerPosition;
            lastPointerPosition = pointerPosition;
            Pan(delta);
        }

        private void HandleReleased()
        {
            if (!pointerHeld) return;
            pointerHeld = false;
            if (!dragging && Time.unscaledTime - lastTapTime <= config.doubleTapIntervalSeconds)
                Recenter();
            if (!dragging) lastTapTime = Time.unscaledTime;
            dragging = false;
        }

        private void Pan(Vector2 screenDelta)
        {
            if (targetCamera == null || config == null) return;
            var right = targetCamera.transform.right;
            var up = Vector3.ProjectOnPlane(targetCamera.transform.up, Vector3.up).normalized;
            targetCamera.transform.position -= (right * screenDelta.x + up * screenDelta.y) * config.panSpeed * Time.unscaledDeltaTime;
        }

        public void Recenter()
        {
            if (targetCamera != null && heroTarget != null)
                targetCamera.transform.position = heroTarget.position + targetOffset;
        }
    }
}
