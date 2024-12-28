using Core;
using Data.ScriptableObjects;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Visual.Rendering.DamageValue;
using Visual.UI.Views.Battle;

namespace Visual.Rendering
{
    public class BattleEntityRenderer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] protected HealthView healthView;
        [SerializeField] protected TextMeshProUGUI NameText;
        
        protected DamageValueSpawner DamageValueSpawner;
        protected MaterialPropertyBlock materialPropertyBlock;
        protected bool IsAlive = false;
        protected int BoardIndex;

        protected Vector2 animationDirection;
        private Vector2 targetScale = Vector2.one/5f;
        
        public void Initialize(EntitySO entitySo, DamageValueSpawner damageValueSpawner, int boardIndex)
        {
            BoardIndex = boardIndex;
            DamageValueSpawner = damageValueSpawner;
            materialPropertyBlock = new MaterialPropertyBlock();
            SetName(entitySo.EntityName);
            SetColor(entitySo.Color);
            SetAlive(true);
            healthView.Initialize(entitySo.BaseHealth);
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
            transform.DOPunchScale(targetScale, Constants.NormalAnimationSpeed, elasticity: 0.1f);
        }
        
        private void PlayDieAnimation()
        {
            transform.DOScale(Vector2.zero, Constants.NormalAnimationSpeed);
        }

        protected void SetColor(Color color)
        {
            spriteRenderer.GetPropertyBlock(materialPropertyBlock, 0);
            materialPropertyBlock.SetColor(Constants.ShaderColorPropertyId, color);
            spriteRenderer.SetPropertyBlock(materialPropertyBlock, 0);
        }

        protected void SetAlive(bool alive)
        {
            IsAlive = alive;
        }

        protected void SetName(string name)
        {
            NameText.text = name;
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}