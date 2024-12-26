using System;
using Data;
using Data.ScriptableObjects;
using UnityEngine;
using Visual.Rendering.DamageValue;
using Visual.Rendering.PointerHandler;

namespace Visual.Rendering
{
    public class BattleHeroRenderer : BattleEntityRenderer, IPointerHandler
    {
        internal event Action<int> OnHeroSelected;
        internal event Action<BattleHeroRenderer> OnHeroHold;

        internal HeroData HeroData;
        
        public void Initialize(HeroData heroData, DamageValueSpawner damageValueSpawner, int boardIndex)
        {
            HeroData = heroData;
            BoardIndex = boardIndex;
            DamageValueSpawner = damageValueSpawner;
            materialPropertyBlock = new MaterialPropertyBlock();
            SetColor(heroData.Color);
            SetAlive(true);
            healthView.Initialize(heroData.ModifiedHealth);
            animationDirection = Vector2.left / 2f;
        }
        
        #region Click & Hold Actions

        public void OnPointerDown()
        {
            OnHeroClicked();
            Invoke(nameof(HoldActionComplete), 3f);
        }

        public void OnPointerUp()
        {
            HoldActionCancel();
        }

        public void OnPointerExit()
        {
            HoldActionCancel();
        }
        
        private void OnHeroClicked()
        {
            if(!IsAlive) return;
            
            OnHeroSelected?.Invoke(BoardIndex);
        }

        private void HoldActionCancel()
        {
            CancelInvoke(nameof(HoldActionComplete));
            OnHeroHold?.Invoke(null);
        }

        private void HoldActionComplete()
        {
            OnHeroHold?.Invoke(this);
        }

        #endregion

    }
}