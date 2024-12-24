using System.Collections.Generic;
using System.Linq;
using Core;
using Core.EventManager.UIEventManager;
using Data;
using TMPro;
using UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Controllers
{
    /// <summary>
    /// Manages hero selection scene and handles user interactions.
    /// </summary>
    public class HeroSelectionSceneController : SceneController
    {
        [SerializeField] private Transform heroGrid;
        [SerializeField] private HeroStatsView heroStatsView;
        [SerializeField] private Button battleButton;
        [SerializeField] private TextMeshProUGUI levelText;

        private List<HeroCardView> selectedHeroes = new ();

        public override void Initialize(UserData userData, EntityService entityService)
        {
            UpdateLevelText(userData.CurrentLevel);
            battleButton.onClick.AddListener(BattleButtonClicked);
            InitializeHeroCards(userData.SelectedHeroIndexes, entityService);
            UpdateBattleButton();
        }

        private void InitializeHeroCards(List<int> selectedHeroIndexes, EntityService entityService)
        {
            var childCount = heroGrid.childCount;
            if (childCount != Constants.TotalHeroes)
            {
                Debug.LogError($"Hero grid count mismatch! Please have {Constants.TotalHeroes} HeroCardView on {heroGrid.name}");
                return;
            }
            
            for (var i = 0; i < childCount; i++)
            {
                var heroCard = heroGrid.GetChild(i).GetComponent<HeroCardView>();
                if (heroCard == null)
                {
                    Debug.LogError($"Hero card view could not be found on {heroGrid.name} hero grid with child index: {i}");
                    return;
                }
                
                heroCard.OnHeroSelected += OnHeroSelected;
                heroCard.OnHeroDeselected += OnHeroDeselected;
                heroCard.OnHeroHold += OnHeroHold;
    
                var heroData = entityService.GetHeroByIndex(i);
                var isSelected = selectedHeroIndexes.Contains(i);
                heroCard.Initialize(heroData, isSelected);
                if(isSelected)
                    selectedHeroes.Add(heroCard);
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
                heroStatsView.Show(hero);
            else
                heroStatsView.Hide();
        }

        #endregion

        private void UpdateHeroSelection()
        {
            var heroIndexes = selectedHeroes.Select(hero => hero.HeroData.Index).ToList();
            UIEventManager.RaiseHeroesUpdateRequested(heroIndexes)?.Invoke();
        }

        private void UpdateLevelText(int level)
        {
            levelText.text = $"Level: {level}";
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
            UIEventManager.RaiseBattleStartRequested?.Invoke();
        }
    }
}
