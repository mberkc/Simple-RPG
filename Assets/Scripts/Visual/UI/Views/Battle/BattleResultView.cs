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
    
        public void ShowResult(bool victory)
        {
            resultText.text = victory ? "Victory!" : "Defeat...";
            returnButton.onClick.RemoveAllListeners();
            returnButton.onClick.AddListener(ReturnToHeroSelection);
            rectTransform.DOScale(Vector3.one, Constants.FastAnimationSpeed);
        }

        private void ReturnToHeroSelection()
        {
            Debug.Log("Confirm button clicked");
            returnButton.onClick.RemoveAllListeners();
            UIEventManager.RaiseReturnToHeroSelectionRequested?.Invoke();
        }
    }
}