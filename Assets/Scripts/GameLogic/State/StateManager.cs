﻿using System.Collections.Generic;
using Core.EventManager.GameLogicEventManager;
using Core.Initializable;
using Data;

namespace GameLogic.State
{
    public class StateManager : Initializable
    {
        protected override void SubscribeEvents()
        {
            GameLogicEventManager.OnHeroesUpdateRequested += UpdateSelectedHeroes;
        }

        protected override void UnSubscribeEvents()
        {
            GameLogicEventManager.OnHeroesUpdateRequested -= UpdateSelectedHeroes;
        }

        private static void UpdateSelectedHeroes(List<int> selectedHeroIndexes)
        {
            //if (heroes is not { Length: Constants.MaxSelectedHeroes }) return;

            var amount = selectedHeroIndexes.Count;
            for (var i = 0; i < amount; i++)
            {
                var heroData = EntityDatabase.GetHeroByIndex(selectedHeroIndexes[i]);
                if(heroData != null)
                    GameState.SelectedHeroes[i] = heroData;
            }
        }
    }
}