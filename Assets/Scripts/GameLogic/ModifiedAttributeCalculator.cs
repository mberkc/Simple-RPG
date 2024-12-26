using UnityEngine;

namespace GameLogic
{
    public static class ModifiedAttributeCalculator
    {
        /// <summary>
        /// Default Level is 1, Don't calculate for first level!
        /// </summary>
        /// <param name="baseAttribute"></param>
        /// <param name="attributeLevelUpModifier"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static float CalculateLevelEffect(float baseAttribute, float attributeLevelUpModifier, int level = 1)
        {
            return Mathf.Ceil(baseAttribute * Mathf.Pow(attributeLevelUpModifier, level - 1));
        }
    }
}