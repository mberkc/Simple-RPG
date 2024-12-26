using System.Collections.Generic;
using Core;
using Core.Progression;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Can only be manipulated by UserData Manager. Don't access from other scripts.
    /// </summary>
    public class UserData
    {
        public List<int> SelectedHeroIndexes = new ();
        public int CurrentLevel = 1;
        public int BattlePlayAmount = 0;
        public HeroCollection HeroCollection;

        public UserData(HeroCollection heroCollection)
        {
            HeroCollection = heroCollection;
        }
        
        public HeroData GetHeroData(int heroIndex) => HeroCollection.Heroes[heroIndex];
        public UserHeroData GetUserHeroData(int heroIndex) => HeroCollection.Heroes[heroIndex].UserHeroData;
    }

    public class HeroCollection
    {
        public HeroData[] Heroes = new HeroData[Constants.TotalHeroes];

        public HeroCollection()
        {
            for(var i = 0; i < Constants.TotalHeroes; i++)
                Heroes[i] = new HeroData();
        }
    }

    public class HeroData
    {
        public int Index;
        public string EntityName;
        public Color Color;
        public float BaseHealth;
        public float BaseAttackPower;
        public float ModifiedHealth;
        public float ModifiedAttackPower;
        public UserHeroData UserHeroData = new();
    }
}