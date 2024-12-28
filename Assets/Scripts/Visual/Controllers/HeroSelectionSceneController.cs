using System.Collections.Generic;
using System.Linq;
using Core;
using Core.EventManager.UIEventManager;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Visual.UI.Views;
using Visual.UI.Views.HeroSelection;

namespace Visual.Controllers
{
    /// <summary>
    /// Manages hero selection scene and handles user interactions.
    /// </summary>
    public class HeroSelectionSceneController : MonoBehaviour
    {
        [SerializeField] private Transform heroGrid;
        [SerializeField] private HeroStatsView heroStatsView;
        [SerializeField] private Button battleButton;
        [SerializeField] private TextMeshProUGUI levelText;

        private List<HeroCardView> selectedHeroes = new ();

        public void Initialize(UserData userData)
        {
            levelText.text = $"Level: {userData.CurrentLevel}";
            battleButton.onClick.RemoveAllListeners();
            battleButton.onClick.AddListener(BattleButtonClicked);
            InitializeHeroCards(userData);
            UpdateBattleButton();
        }

        private void InitializeHeroCards(UserData userData)
        {
            var childCount = heroGrid.childCount;
            if (childCount != Constants.TotalHeroes)
            {
                Debug.LogError($"Hero grid count mismatch! Please have {Constants.TotalHeroes} HeroCardView on {heroGrid.name}");
                return;
            }

            var selectedHeroIndexes = userData.SelectedHeroIndexes;
            
            for (var i = 0; i < childCount; i++)
            {
                var heroCard = heroGrid.GetChild(i).GetComponent<HeroCardView>();
                if (heroCard == null)
                {
                    Debug.LogError($"Hero card view could not be found on {heroGrid.name} hero grid with child index: {i}");
                    return;
                }
    
                var heroData = userData.GetHeroData(i);
                var isSelected = selectedHeroIndexes.Contains(i);
                heroCard.Initialize(heroData, isSelected, OnHeroSelected, OnHeroDeselected, OnHeroHold);
                if(isSelected) selectedHeroes.Add(heroCard);
            }
        }

        #region HeroCardView Callbacks

        // Handle when a hero is selected
        private void OnHeroSelected(HeroCardView hero)
        {
            if (selectedHeroes.Count < Constants.MaxSelectedHeroes)
            {
                if(selectedHeroes.Contains(hero)) return;
                
                selectedHeroes.Add(hero);
                hero.SetSelected(true);
                UpdateHeroSelection();
            }
            else
                Debug.Log("Cannot select more than " + Constants.MaxSelectedHeroes + " heroes.");
            UpdateBattleButton();
        }

        // Handle when a hero is deselected
        private void OnHeroDeselected(HeroCardView hero)
        {
            if (selectedHeroes.Contains(hero))
            {
                selectedHeroes.Remove(hero);
                hero.SetSelected(false);
                UpdateHeroSelection();
            }
            UpdateBattleButton();
        }

        // Handle when a hero is hold
        private void OnHeroHold(HeroCardView hero)
        {
            if(hero != null)
                heroStatsView.Show(hero.HeroData, hero.transform.position);
            else
                heroStatsView.Hide();
        }

        #endregion

        private void UpdateHeroSelection()
        {
            var heroIndexes = selectedHeroes.Select(hero => hero.HeroData.Index).ToList();
            UIEventManager.RaiseHeroesUpdateRequested(heroIndexes)?.Invoke();
        }

        private void UpdateBattleButton()
        {
            battleButton.interactable = selectedHeroes.Count == Constants.MaxSelectedHeroes;
        }

        private void BattleButtonClicked()
        {
            // Extra check
            if(selectedHeroes.Count != Constants.MaxSelectedHeroes)
            {
                UpdateBattleButton();
                return;
            }
            
            Debug.Log("Battle button clicked");
            battleButton.onClick.RemoveAllListeners();
            UIEventManager.RaiseBattleStartRequested?.Invoke();
        }
    }
}
