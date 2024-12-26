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
        private bool unlocked = false;

        internal void Initialize(HeroData heroData, bool selected)
        {
            HeroData = heroData;

            backgroundImage.color = heroData.Color;
            nameText.text = heroData.EntityName;
            SetSelected(selected);
            SetUnlocked(heroData.UserHeroData.Unlocked);
        }

        internal void SetSelected(bool selected)
        {
            isSelected = selected;
            selectionBorder.DOKill(true);
            selectionBorder.DOFade(selected ? 1f : 0f, Constants.FastAnimationSpeed);
        }
        
        private void SetUnlocked(bool unlocked)
        {
            this.unlocked = unlocked;
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
            if(!unlocked) return;
            
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
        }
    }
}