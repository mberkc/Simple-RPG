﻿using System;
using Data.ScriptableObjects;
using Visual.Rendering.DamageValue;
using Visual.Rendering.PointerHandler;

namespace Visual.Rendering
{
    public class BattleHeroRenderer : BattleEntityRenderer, IPointerHandler
    {
        internal event Action<int> OnHeroSelected;
        internal event Action<BattleHeroRenderer> OnHeroHold;

        internal HeroData HeroData;
        private int boardIndex;
        
        public override void Initialize(EntityData entityData, DamageValueSpawner damageValueSpawner, int boardIndex)
        {
            HeroData = entityData as HeroData;
            this.boardIndex = boardIndex;
            base.Initialize(entityData, damageValueSpawner);
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
            
            OnHeroSelected?.Invoke(boardIndex);
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