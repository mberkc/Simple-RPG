using Core.Initializable;
using Data.ScriptableObjects;

namespace GameLogic
{
    public class StateManager : Initializable
    {
        protected override void SubscribeEvents()
        {
            //GameLogicEventManager.OnHeroesUpdateRequested += UpdateSelectedHeroes;
        }

        protected override void UnSubscribeEvents()
        {
            //GameLogicEventManager.OnHeroesUpdateRequested -= UpdateSelectedHeroes;
        }

        private static void UpdateSelectedHeroes(HeroData[] heroes)
        {
            //if (heroes is not { Length: Constants.MaxSelectedHeroes }) return;

            GameState.SelectedHeroes = heroes;
        }
    }
}