using UnityEngine;
using UnityEngine.EventSystems;

namespace Visual.UI.PointerEventUtility
{
    public class PointerEventForwarder : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private PointerEventForwarderTarget pointerEventForwarderTarget;

        private void Awake()
        {
            if (transform.parent == null)
            {
                Debug.LogWarning("PointerEventUtility: No Parent found!");
                return;
            }

            pointerEventForwarderTarget = transform.parent.GetComponent<PointerEventForwarderTarget>();
            
            if (pointerEventForwarderTarget == null)
                Debug.LogWarning("PointerEventUtility: Parent doesn't have PointerEventForwarderTarget component!");
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            pointerEventForwarderTarget?.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            pointerEventForwarderTarget?.OnPointerUp(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            pointerEventForwarderTarget?.OnPointerExit(eventData);
        }
    }
}