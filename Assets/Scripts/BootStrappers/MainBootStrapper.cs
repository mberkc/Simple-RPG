using Core.BootStrapper;
using GameLogic;

namespace BootStrappers
{
    public class MainBootStrapper : GameBootStrapper
    {
        private StateManager stateManager;
        private GameFlowManager gameFlowManager;
        
        public override void Initialize()
        {
            stateManager = new StateManager();
            stateManager.Initialize();
            
            gameFlowManager = new GameFlowManager();
            gameFlowManager.Initialize();
        }
    }
}