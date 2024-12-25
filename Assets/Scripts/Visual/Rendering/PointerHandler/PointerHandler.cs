using System;
using UnityEngine;

namespace Visual.Rendering.PointerHandler
{
    public class PointerHandler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private string targetTag = "Interactable"; // Set a tag for valid objects

        private bool isHolding = false;
        private float holdTime = 0f;
        private const float HoldThreshold = 3f;

        public event Action OnClick;
        public event Action OnHoldComplete;

        private Camera mainCamera;

        private Camera MainCamera
        {
            get
            { 
                if (mainCamera == null) 
                    mainCamera = Camera.main; 
                return mainCamera;
            }
        }

        private void Update()
        {
    #if UNITY_ANDROID || UNITY_IOS
            HandleTouchInput();
    #else
            HandleMouseInput();
    #endif
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 clickPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);

                if (IsTargetValid(clickPosition))
                {
                    OnClick?.Invoke();
                    isHolding = true;
                    holdTime = 0f;
                }
            }

            if (Input.GetMouseButton(0) && isHolding)
            {
                holdTime += Time.deltaTime;

                if (holdTime >= HoldThreshold)
                {
                    isHolding = false;
                    OnHoldComplete?.Invoke();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isHolding = false;
            }
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPosition = MainCamera.ScreenToWorldPoint(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (IsTargetValid(touchPosition))
                        {
                            OnClick?.Invoke();
                            isHolding = true;
                            holdTime = 0f;
                        }
                        break;
                    case TouchPhase.Stationary:
                    case TouchPhase.Moved:
                        if (isHolding)
                        {
                            holdTime += Time.deltaTime;

                            if (holdTime >= HoldThreshold)
                            {
                                isHolding = false;
                                OnHoldComplete?.Invoke();
                            }
                        }
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        isHolding = false;
                        break;
                }
            }
        }

        private bool IsTargetValid(Vector2 position)
        {
            if (spriteRenderer.bounds.Contains(position))
            {
                if (!string.IsNullOrEmpty(targetTag))
                {
                    Collider2D hitCollider = Physics2D.OverlapPoint(position);
                    return hitCollider != null && hitCollider.CompareTag(targetTag);
                }

                return true;
            }

            return false;
        }
    }
}