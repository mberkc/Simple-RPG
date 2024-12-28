using System;
using Core;
using Data;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Visual.UI.PointerEventUtility;

namespace Visual.UI.Views.HeroSelection
{
    public class HeroCardView : PointerEventForwarderTarget
    {
        internal event Action<HeroCardView> OnHeroSelected, OnHeroDeselected, OnHeroHold;

        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image selectionBorder;
        [SerializeField] private GameObject lockedVisual;
        
        internal HeroData HeroData { get; private set; }

        private bool isSelected = false;
        private bool isUnlocked = false;

        internal void Initialize(HeroData heroData, bool selected, Action<HeroCardView> onHeroSelected, Action<HeroCardView> onHeroDeselected, Action<HeroCardView> onHeroHold)
        {
            HeroData = heroData;
            backgroundImage.color = heroData.Color;
            nameText.text = heroData.EntityName;
            SetSelected(selected);
            SetUnlocked(heroData.UserHeroData.Unlocked);
            OnHeroSelected = onHeroSelected;
            OnHeroDeselected = onHeroDeselected;
            OnHeroHold = onHeroHold;
        }

        internal void SetSelected(bool selected)
        {
            isSelected = selected;
            selectionBorder.DOKill(true);
            selectionBorder.DOFade(selected ? 1f : 0f, Constants.FastAnimationDuration);
        }
        
        private void SetUnlocked(bool unlocked)
        {
            isUnlocked = unlocked;
            lockedVisual.SetActive(!unlocked);
        }

        #region Click & Hold Actions

        public override void OnPointerDown(PointerEventData eventData)
        {
            OnCardClicked();
            Invoke(nameof(HoldActionComplete), 3f);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            HoldActionCancel();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            HoldActionCancel();
        }
        
        private void OnCardClicked()
        {
            if(!isUnlocked) return;
            
            if (isSelected)
                OnHeroDeselected?.Invoke(this);
            else
                OnHeroSelected?.Invoke(this);
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

        private void OnDestroy()
        {
            selectionBorder.DOKill(true);
            OnHeroSelected = null;
            OnHeroDeselected = null;
            OnHeroHold = null;
        }
    }
}