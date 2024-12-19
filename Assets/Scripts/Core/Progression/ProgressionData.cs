using System;
using System.Collections.Generic;

namespace Core.Progression
{
    [Serializable]
    public class ProgressionData
    {
        public int CurrentLevel = 1; // Default starting level
        public List<int> SelectedHeroIndexes = new (); // Store hero IDs
    }
}