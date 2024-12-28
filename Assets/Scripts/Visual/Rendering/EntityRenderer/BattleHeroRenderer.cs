using System;
using Data;
using Visual.Rendering.DamageValue;
using Visual.Rendering.PointerHandler;

namespace Visual.Rendering.EntityRenderer
{
    public class BattleHeroRenderer : BattleEntityRenderer, IPointerHandler
    {
        private event Action<int> OnHeroSelected;
        private event Action<BattleHeroRenderer> OnHeroHold;

        internal HeroData HeroData { get; private set; }
        
        public void Initialize(HeroData heroData, int boardIndex, DamageValueSpawner damageValueSpawner, Action<int> onHeroSelected, Action<BattleHeroRenderer> onHeroHold)
        {
            HeroData = heroData;
            OnHeroSelected = onHeroSelected;
            OnHeroHold = onHeroHold;
            base.Initialize(heroData.EntityName, heroData.Color, heroData.ModifiedHealth, boardIndex, damageValueSpawner);
        }

        internal override void Die()
        {
            OnHeroSelected = null;
            OnHeroHold = null;
            base.Die();
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