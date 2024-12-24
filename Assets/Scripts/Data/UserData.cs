using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// Can only be manipulated by UserData Manager. Don't access from other scripts.
    /// </summary>
    public class UserData
    {
        public List<int> SelectedHeroIndexes { get; set; } = new ();
        public int CurrentLevel{ get; set; }
    }
    
    /// <summary>
    /// UserData for UI
    /// </summary>
    public class UserDataUI
    {
        public List<int> SelectedHeroIndexes { get; set; } = new ();
        public int CurrentLevel { get; set; }
    }
}