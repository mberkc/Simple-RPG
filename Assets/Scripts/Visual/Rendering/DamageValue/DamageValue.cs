using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Visual.Rendering.DamageValue
{
    public class DamageValue : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        private float verticalMoveVector = Screen.height / 3f;
        private Sequence sequence;
        private Color startColor;

        internal void Show(float damage, Vector3 scenePosition, Action<DamageValue> recycle)
        {
            rectTransform.position = scenePosition;
            textMeshPro.text = damage.ToString("F0");
            startColor = textMeshPro.color;
            
            sequence = DOTween.Sequence();
            sequence.Append(textMeshPro.DOFade(0, 1.5f))
                .Join(rectTransform.DOMoveY(rectTransform.position.y + verticalMoveVector, 1.5f))
                .SetEase(Ease.OutQuad).OnComplete(() => Recycle(recycle));
        }

        private void Recycle(Action<DamageValue> recycle)
        {
            textMeshPro.color = startColor;
            recycle?.Invoke(this);
        }
        
        private void OnDestroy()
        {
            sequence?.Kill();
        }
    }
}