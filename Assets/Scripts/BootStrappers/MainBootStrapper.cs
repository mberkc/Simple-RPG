using Core.BootStrapper;
using GameLogic;

namespace BootStrappers
{
    public class MainBootStrapper : GameBootStrapper
    {
        public override void Initialize()
        {
            var gameFlowManager = new GameFlowManager();
            gameFlowManager.Initialize();
        }
    }
}