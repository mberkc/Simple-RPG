using Core;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Visual.Rendering.DamageValue;
using Visual.UI.Views.Battle;

namespace Visual.Rendering.EntityRenderer
{
    public abstract class BattleEntityRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private HealthView healthView;
        [SerializeField] private TextMeshProUGUI NameText;
        
        protected int BoardIndex;
        protected bool IsAlive = false;
        
        private DamageValueSpawner damageValueSpawner;
        private MaterialPropertyBlock materialPropertyBlock;

        private Vector2 animationDirection;
        private Vector2 targetScale = Vector2.one/5f;
        
        protected void Initialize(string name, Color color, float health, int boardIndex, DamageValueSpawner damageValueSpawner)
        {
            BoardIndex = boardIndex;
            this.damageValueSpawner = damageValueSpawner;
            materialPropertyBlock = new MaterialPropertyBlock();
            SetName(name);
            SetColor(color);
            SetAlive(true);
            healthView.Initialize(health);
            animationDirection = (BoardIndex >= Constants.EnemyBoardIndex ? Vector2.left : Vector2.right) / 2f;
        }
        
        internal void Attack()
        {
            PlayAttackAnimation();
        }

        internal void TakeDamage(float damage, float targetHealth)
        {
            ShowDamageValue(damage);
            PlayDamageAnimation();
            healthView.UpdateHealth(targetHealth);
        }
        
        internal void Die()
        {
            SetAlive(false);
            PlayDieAnimation();
        }
        
        private void ShowDamageValue(float damage)
        {
            var centerPosition = spriteRenderer.bounds.center;
            var screenPosition = VisualUtility.WorldToScreenPosition(centerPosition);
            damageValueSpawner.Spawn(damage, screenPosition);
        }
        
        private void PlayAttackAnimation()
        {
            transform.DOPunchPosition(animationDirection, Constants.FastAnimationDuration, elasticity: 0f, vibrato: 0);
        }

        private void PlayDamageAnimation()
        {
            transform.DOPunchScale(targetScale, Constants.FastAnimationDuration, elasticity: 0.1f);
        }
        
        private void PlayDieAnimation()
        {
            transform.DOScale(Vector2.zero, Constants.FastAnimationDuration);
        }

        private void SetColor(Color color)
        {
            spriteRenderer.GetPropertyBlock(materialPropertyBlock, 0);
            materialPropertyBlock.SetColor(Constants.ShaderColorPropertyId, color);
            spriteRenderer.SetPropertyBlock(materialPropertyBlock, 0);
        }

        private void SetAlive(bool alive)
        {
            IsAlive = alive;
        }

        private void SetName(string name)
        {
            NameText.text = name;
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}