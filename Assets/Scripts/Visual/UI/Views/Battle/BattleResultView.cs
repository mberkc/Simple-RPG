using System.Collections.Generic;
using Core;
using Core.EventManager.UIEventManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Visual.UI.Views.Battle
{
    public class BattleResultView: MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private TextMeshProUGUI resultText;
        [SerializeField] private Button returnButton;
    
        public void ShowResult(bool victory, List<int> experienceGainedHeroes)
        {
            resultText.text = victory ? "Victory!" : "Defeat...";
            ShowExperienceGainedHeroes(experienceGainedHeroes);
            returnButton.onClick.RemoveAllListeners();
            returnButton.onClick.AddListener(ReturnToHeroSelection);
            rectTransform.DOScale(Vector3.one, Constants.FastAnimationDuration);
        }

        private void ReturnToHeroSelection()
        {
            Debug.Log("Confirm button clicked");
            rectTransform.DOKill();
            returnButton.onClick.RemoveAllListeners();
            UIEventManager.RaiseReturnToHeroSelectionRequested?.Invoke();
        }

        private void ShowExperienceGainedHeroes(List<int> experienceGainedHeroes)
        {
            // TODO: Show Hero Experience gain
        }
    }
}