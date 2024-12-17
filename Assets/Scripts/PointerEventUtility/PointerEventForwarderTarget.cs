using UnityEngine;
using UnityEngine.EventSystems;

namespace PointerEventUtility
{
    public abstract class PointerEventForwarderTarget: MonoBehaviour
    {
        public abstract void OnPointerDown(PointerEventData eventData);
        public abstract void OnPointerUp(PointerEventData eventData);
        public abstract void OnPointerExit(PointerEventData eventData);

        public virtual void Awake()
        {
            if (transform.GetComponentInChildren<PointerEventForwarder>() == null)
                Debug.LogWarning("PointerEventUtility: Don't have a PointerEventForwarder component in children.");
        }
    }
}