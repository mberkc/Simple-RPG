using System.Collections.Generic;
using Core;
using UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Controllers
{
    /// <summary>
    /// Manages the hero grid and handles user interactions.
    /// </summary>
    public class HeroSelectionController : MonoBehaviour
    {
        [SerializeField] private Transform heroGrid;
        [SerializeField] private HeroStatsView heroStatsView;
        [SerializeField] private Button battleButton;

        private HeroCardView[] heroCards;
        private List<HeroCardView> selectedHeroes = new List<HeroCardView>();

        private void Awake()
        {
            InitializeHeroCards();
            UpdateBattleButton();
        }
        
        // Cache all hero cards under the hero grid
        private void InitializeHeroCards()
        {
            var childCount = heroGrid.childCount;
            heroCards = new HeroCardView[childCount];
            for (var i = 0; i < childCount; i++)
            {
                var heroCard = heroGrid.GetChild(i).GetComponent<HeroCardView>();
                if (heroCard == null) continue;
                
                heroCard.OnHeroSelected += OnHeroSelected;
                heroCard.OnHeroDeselected += OnHeroDeselected;
                heroCard.OnHeroHovered += OnHeroHovered;
                heroCard.Initialize($"Hero {i+1}", 100, 10);
                heroCards[i] = heroCard;
            }
        }

        // Handle when a hero is selected
        private void OnHeroSelected(HeroCardView hero)
        {
            if (selectedHeroes.Count < GameConstants.MaxSelectedHeroes)
            {
                if(selectedHeroes.Contains(hero)) return;
                
                selectedHeroes.Add(hero);
                hero.SetSelected(true);
                heroStatsView.Show(hero);
            }
            else
                Debug.Log("Cannot select more than " + GameConstants.MaxSelectedHeroes + " heroes.");
            UpdateBattleButton();
        }

        // Handle when a hero is deselected
        private void OnHeroDeselected(HeroCardView hero)
        {
            if (selectedHeroes.Contains(hero))
            {
                selectedHeroes.Remove(hero);
                hero.SetSelected(false);
                heroStatsView.Hide();
            }
            UpdateBattleButton();
        }

        // Handle when a hero is hovered to update stats panel
        private void OnHeroHovered(HeroCardView hero)
        {
            heroStatsView.Show(hero);
        }

        // Enable or disable the battle button based on selection count
        private void UpdateBattleButton()
        {
            battleButton.interactable = selectedHeroes.Count == GameConstants.MaxSelectedHeroes;
        }
    }
}
