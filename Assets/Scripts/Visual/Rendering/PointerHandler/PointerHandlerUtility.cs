using UnityEngine;

namespace Visual.Rendering.PointerHandler
{
    public class PointerHandlerUtility : MonoBehaviour
    {
        [SerializeField] private LayerMask targetLayer;

        private IPointerHandler currentHandler;
        private bool isHolding;

        private void Update()
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            HandleTouchInput();
#else
            HandleMouseInput();
#endif
        }

#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
        private void HandleTouchInput()
        {
            if (Input.touchCount <= 0) return;
            
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                TryPointerDown(VisualUtility.MainCamera.ScreenToWorldPoint(touch.position));
            else if (touch.phase is TouchPhase.Moved or TouchPhase.Stationary)
            {
                if (isHolding)
                    TryPointerExit(VisualUtility.MainCamera.ScreenToWorldPoint(touch.position));
            }
            else if (touch.phase is TouchPhase.Ended or TouchPhase.Canceled)
                TryPointerUp();
        }
#else
        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
                TryPointerDown(VisualUtility.MainCamera.ScreenToWorldPoint(Input.mousePosition));
            else if (Input.GetMouseButton(0))
            {
                if (isHolding)
                    TryPointerExit(VisualUtility.MainCamera.ScreenToWorldPoint(Input.mousePosition));
            }
            else if (Input.GetMouseButtonUp(0))
                TryPointerUp();
        }
#endif

        private void TryPointerDown(Vector2 pointerPosition)
        {
            var hit = Physics2D.OverlapPoint(pointerPosition, targetLayer);
            if (hit == null || !hit.TryGetComponent<IPointerHandler>(out var handler)) return;
            
            currentHandler = handler;
            isHolding = true;
            currentHandler.OnPointerDown();
        }

        private void TryPointerUp()
        {
            if (currentHandler == null) return;
            
            currentHandler.OnPointerUp();
            currentHandler = null;
            isHolding = false;
        }

        private void TryPointerExit(Vector2 pointerPosition)
        {
            if (currentHandler == null) return;
            
            var hit = Physics2D.OverlapPoint(pointerPosition, targetLayer);
            if (hit != null && hit.TryGetComponent<IPointerHandler>(out _)) return;
            
            currentHandler.OnPointerExit();
            currentHandler = null;
            isHolding = false;
        }
    }
}