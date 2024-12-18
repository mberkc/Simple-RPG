using System.Collections.Generic;
using System.Linq;
using Core;
using Core.EventManager.UIEventManager;
using Data.ScriptableObjects;
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
            battleButton.onClick.AddListener(BattleButtonClicked);
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
                heroCard.OnHeroHold += OnHeroHold;
                
                // TODO: get from so pool
                var heroData = ScriptableObject.CreateInstance<HeroData>();
                //heroCard.Initialize($"Hero {i+1}", Constants.EntityDefaultHealth, Constants.EntityDefaultAttackPower);
                heroCard.Initialize(heroData);
                heroCards[i] = heroCard;
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
            var selectedHeroesData = selectedHeroes.Select(hero => hero.HeroData).ToList();
            //UIEventManager.RaiseHeroesUpdateRequested(selectedHeroesData);
        }

        // Enable or disable the battle button based on selection count
        private void UpdateBattleButton()
        {
            battleButton.interactable = selectedHeroes.Count == Constants.MaxSelectedHeroes;
        }

        private static void BattleButtonClicked()
        {
            Debug.Log("Battle button clicked");
            UIEventManager.RaiseBattleStartRequested?.Invoke();
        }
    }
}
