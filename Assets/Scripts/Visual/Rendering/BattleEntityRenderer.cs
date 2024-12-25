using Core;
using Data.ScriptableObjects;
using DG.Tweening;
using UnityEngine;
using Visual.Rendering.DamageValue;
using Visual.UI.Views.Battle;

namespace Visual.Rendering
{
    public class BattleEntityRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private HealthView healthView;
        
        private DamageValueSpawner DamageValueSpawner;
        protected bool IsAlive = false;
        private MaterialPropertyBlock materialPropertyBlock;
        protected int BoardIndex;

        private Vector3 animationDirection;
        
        public virtual void Initialize(EntityData entityData, DamageValueSpawner damageValueSpawner, int boardIndex)
        {
            BoardIndex = boardIndex;
            DamageValueSpawner = damageValueSpawner;
            materialPropertyBlock = new MaterialPropertyBlock();
            SetColor(entityData.Color);
            SetAlive(true);
            healthView.Initialize(entityData.Health);
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
            DamageValueSpawner.Spawn(damage, screenPosition);
        }
        
        private void PlayAttackAnimation()
        {
            transform.DOPunchPosition(animationDirection, Constants.NormalAnimationSpeed, elasticity: 0.1f);
        }

        private void PlayDamageAnimation()
        {
            transform.DOPunchScale(Vector2.one/5f, Constants.NormalAnimationSpeed, elasticity: 0.1f);
        }
        
        private void PlayDieAnimation()
        {
            transform.DOScale(Vector2.zero, Constants.NormalAnimationSpeed);
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

    }
}