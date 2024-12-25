using System;
using System.Collections.Generic;

namespace Core.Progression
{
    [Serializable]
    public class ProgressionData
    {
        public int BattlePlayAmount = 0; // Increases after every battle (win or lose)
        public int CurrentLevel = 1; // Default starting level
        public List<int> SelectedHeroIndexes = new (){0, 1, 2}; // Store hero IDs
    }
}