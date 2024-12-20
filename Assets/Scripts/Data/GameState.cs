using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// Can only be manipulated by GameState Service. Don't access from other scripts.
    /// </summary>
    public class GameState
    {
        public List<int> SelectedHeroIndexes { get; set; } = new ();
        public int CurrentLevel{ get; set; }
    }
    
    /// <summary>
    /// GameState Data for UI
    /// </summary>
    public class GameStateUI
    {
        public List<int> SelectedHeroIndexes { get; set; } = new ();
        public int CurrentLevel { get; set; }
    }
}