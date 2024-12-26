using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Core.Progression
{
    [Serializable]
    public class ProgressionData
    {
        public int BattlePlayAmount = 0; // Increases after every battle (win or lose)
        public int CurrentLevel = 1; // Default starting level
        public List<int> SelectedHeroIndexes = new (){0, 1, 2};
        public SerializableUserHeroCollection SerializableUserHeroCollection = new();
    }

    [Serializable]
    public class SerializableUserHeroCollection
    {
        public SerializableUserHero[] SerializableUserHeroes = new SerializableUserHero[Constants.TotalHeroes];

        public SerializableUserHeroCollection()
        {
            for (var i = 0; i < Constants.TotalHeroes; i++)
            {
                var hero = new SerializableUserHero
                {
                    Index = i,
                    UserHeroData = new UserHeroData()
                };
                if (i < Constants.MaxSelectedHeroes)
                    hero.UserHeroData.Unlocked = true;
                
                SerializableUserHeroes[i] = hero;
            }
        }
    }
    
    [Serializable]
    public class SerializableUserHero
    {
        public int Index;
        public UserHeroData UserHeroData = new();
    }

    [Serializable]
    public class UserHeroData
    {
        public int Experience = 0;
        public int Level = 1;
        public bool Unlocked = false;
    }
}