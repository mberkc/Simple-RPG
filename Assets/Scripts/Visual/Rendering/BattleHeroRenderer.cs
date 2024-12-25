using System;
using Data.ScriptableObjects;

namespace Visual.Rendering
{
    public class BattleHeroRenderer : BattleEntityRenderer
    {
        internal event Action<int> OnHeroSelected;
        internal event Action<BattleHeroRenderer> OnHeroHold;

        internal HeroData HeroData;
        private int boardIndex;
        
        public override void Initialize(EntityData entityData, int boardIndex)
        {
            HeroData = entityData as HeroData;
            this.boardIndex = boardIndex;
            base.Initialize(entityData);
        }
        
        #region Click & Hold Actions

        /*
        public void OnPointerDown(PointerEventData eventData)
        {
            OnHeroClicked();
            Invoke(nameof(HoldActionComplete), 3f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            HoldActionCancel();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HoldActionCancel();
        }
        */
        
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