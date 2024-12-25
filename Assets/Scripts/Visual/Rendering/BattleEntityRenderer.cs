using Core;
using Data.ScriptableObjects;
using DG.Tweening;
using UnityEngine;
using Visual.UI.Views.Battle;

namespace Visual.Rendering
{
    public class BattleEntityRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private HealthView healthView;

        protected bool IsAlive = false;
        private MaterialPropertyBlock materialPropertyBlock;
        
        public virtual void Initialize(EntityData entityData, int boardIndex = 0)
        {
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
            PlayDamageAnimation();
            healthView.UpdateHealth(targetHealth);
        }
        internal void Die()
        {
            PlayDieAnimation();
        }
        
        private void PlayAttackAnimation()
        {
            transform.DOShakePosition(Constants.NormalAnimationSpeed);
        }

        private void PlayDamageAnimation()
        {
            transform.DOPunchScale(Vector3.one * 0.1f, Constants.NormalAnimationSpeed);
        }
        
        private void PlayDieAnimation()
        {
            SetAlive(false);
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
            // Disable
            //entityImage.DOKill(true);
            //entityImage.DOFade(alive ? 1f : 0.3f, Constants.FastAnimationSpeed);
        }

    }
}