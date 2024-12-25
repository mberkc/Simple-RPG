using UnityEngine;

namespace Visual
{
    public static class VisualUtility
    {
        private static Camera _mainCamera;
        
        internal static Camera MainCamera {
            get
            {
                if(_mainCamera == null) _mainCamera = Camera.main;
                return _mainCamera;
            }
        }

        internal static Vector3 WorldToScreenPosition(Vector3 worldPosition)
        {
           return MainCamera.WorldToScreenPoint(worldPosition); 
        }
        
        internal static Vector3 ScreenToWorldPosition(Vector3 screenPosition)
        {
            return MainCamera.ScreenToWorldPoint(screenPosition);
        }
    }
}