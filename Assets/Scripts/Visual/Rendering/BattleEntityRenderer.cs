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
        
        protected DamageValueSpawner DamageValueSpawner;
        protected bool IsAlive = false;
        private MaterialPropertyBlock materialPropertyBlock;
        
        public virtual void Initialize(EntityData entityData, DamageValueSpawner damageValueSpawner, int boardIndex = 0)
        {
            DamageValueSpawner = damageValueSpawner;
            materialPropertyBlock = new MaterialPropertyBlock();
            SetColor(entityData.Color);
            SetAlive(true);
            healthView.Initialize(entityData.Health);
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
            transform.DOShakePosition(Constants.NormalAnimationSpeed);
        }

        private void PlayDamageAnimation()
        {
            transform.DOPunchScale(Vector3.one * 0.5f, Constants.NormalAnimationSpeed);
        }
        
        private void PlayDieAnimation()
        {
            transform.DOScale(Vector3.zero, Constants.NormalAnimationSpeed);
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