using UnityEngine;

namespace Core
{
    public static class CoreUtility
    {
        private static int _normalAnimationDurationAsMS;

        public static int GetNormalAnimationDurationAsMS
        {
            get
            {
                if(_normalAnimationDurationAsMS == 0)
                    _normalAnimationDurationAsMS = (int) (Constants.FastAnimationDuration * 1000f);
                return _normalAnimationDurationAsMS;
            }
        }
        
        /// <summary>
        /// Default Level is 1, Don't calculate for the first level!
        /// </summary>
        /// <param name="baseAttribute"></param>
        /// <param name="attributeLevelUpModifier"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static float CalculateModifiedAttribute(float baseAttribute, float attributeLevelUpModifier, int level = 1)
        {
            return Mathf.Ceil(baseAttribute * Mathf.Pow(attributeLevelUpModifier, level - 1));
        }
    }
}