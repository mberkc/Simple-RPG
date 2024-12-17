using System.Collections.Generic;
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
        [SerializeField] private GameObject heroStatsPanel;
        [SerializeField] private Button battleButton;
        [SerializeField] private GameObject heroCardPrefab;

        private List<HeroCard> selectedHeroes = new List<HeroCard>();
        private const int MaxSelectedHeroes = 3;

        private void Start()
        {
            InitializeHeroGrid();
            UpdateBattleButton();
        }

        // Initialize hero grid with heroes (example hardcoded data for now)
        private void InitializeHeroGrid()
        {
            for (var i = 0; i < 3; i++)
            {
                var heroCardObject = Instantiate(heroCardPrefab, heroGrid);
                var heroCard = heroCardObject.GetComponent<HeroCard>();
                heroCard.Initialize($"Hero {i + 1}", 100 + i * 10, 20 + i * 5); // Example data

                // Subscribe to hero card events
                heroCard.OnHeroSelected += OnHeroSelected;
                heroCard.OnHeroDeselected += OnHeroDeselected;
            }
        }

        // Handle when a hero is selected
        private void OnHeroSelected(HeroCard hero)
        {
            if (selectedHeroes.Count < MaxSelectedHeroes)
            {
                selectedHeroes.Add(hero);
                hero.SetSelected(true);
            }
            else
            {
                Debug.Log("Cannot select more than " + MaxSelectedHeroes + " heroes.");
            }
            UpdateBattleButton();
        }

        // Handle when a hero is deselected
        private void OnHeroDeselected(HeroCard hero)
        {
            if (selectedHeroes.Contains(hero))
            {
                selectedHeroes.Remove(hero);
                hero.SetSelected(false);
            }
            UpdateBattleButton();
        }

        // Enable or disable the battle button based on selection count
        private void UpdateBattleButton()
        {
            battleButton.interactable = selectedHeroes.Count == MaxSelectedHeroes;
        }
    }
}
