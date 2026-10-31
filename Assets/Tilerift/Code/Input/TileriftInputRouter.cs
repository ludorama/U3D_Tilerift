using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tilerift.Input
{
    public sealed class TileriftInputRouter : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;

        private InputAction pointAction;
        private InputAction pressAction;
        private InputAction navigateAction;
        private InputAction submitAction;
        private InputAction cancelAction;

        public event Action<Vector2> Pointed;
        public event Action Pressed;
        public event Action Released;
        public event Action<Vector2> Navigated;
        public event Action Submitted;
        public event Action Cancelled;

        public Vector2 Point => pointAction?.ReadValue<Vector2>() ?? Vector2.zero;
        public Vector2 Navigate => navigateAction?.ReadValue<Vector2>() ?? Vector2.zero;

        private void Awake()
        {
            if (actions == null)
            {
                Debug.LogError("TileriftInputRouter requires a New Input System action asset.", this);
                enabled = false;
                return;
            }

            pointAction = actions.FindAction("Point", true);
            pressAction = actions.FindAction("Press", true);
            navigateAction = actions.FindAction("Navigate", true);
            submitAction = actions.FindAction("Submit", true);
            cancelAction = actions.FindAction("Cancel", true);

            pointAction.performed += OnPoint;
            pressAction.started += OnPressStarted;
            pressAction.canceled += OnPressCanceled;
            navigateAction.performed += OnNavigate;
            submitAction.performed += OnSubmit;
            cancelAction.performed += OnCancel;
        }

        private void OnEnable() => actions?.Enable();
        private void OnDisable() => actions?.Disable();

        private void OnDestroy()
        {
            if (pointAction == null) return;
            pointAction.performed -= OnPoint;
            pressAction.started -= OnPressStarted;
            pressAction.canceled -= OnPressCanceled;
            navigateAction.performed -= OnNavigate;
            submitAction.performed -= OnSubmit;
            cancelAction.performed -= OnCancel;
        }

        private void OnPoint(InputAction.CallbackContext context) => Pointed?.Invoke(context.ReadValue<Vector2>());
        private void OnPressStarted(InputAction.CallbackContext context) => Pressed?.Invoke();
        private void OnPressCanceled(InputAction.CallbackContext context) => Released?.Invoke();
        private void OnNavigate(InputAction.CallbackContext context) => Navigated?.Invoke(context.ReadValue<Vector2>());
        private void OnSubmit(InputAction.CallbackContext context) => Submitted?.Invoke();
        private void OnCancel(InputAction.CallbackContext context) => Cancelled?.Invoke();
    }
}
