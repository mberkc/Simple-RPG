using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Visual.Rendering.DamageValue
{
    public class DamageValue : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        private float verticalMove = Screen.height / 3f;

        internal void Show(float damage, Vector3 scenePosition, DamageValueSpawner spawner)
        {
            rectTransform.position = scenePosition;
            textMeshPro.text = damage.ToString("F0");
            
            var startColor = textMeshPro.color;
            textMeshPro.DOFade(0, 1.5f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                textMeshPro.color = startColor;
                spawner.Recycle(this);
            });

            rectTransform.DOMoveY(rectTransform.position.y + verticalMove, 1.5f).SetEase(Ease.OutQuad);
        }
    }
}