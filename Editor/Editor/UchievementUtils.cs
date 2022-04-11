using System.Collections.Generic;
using UnityEngine;

namespace Uchievements
{
    public static class UchievementUtils
    {
        private static Dictionary<UchievementDifficult, Reward> rewards = new Dictionary<UchievementDifficult, Reward>()
        {
            {UchievementDifficult.Easy, new Reward(1, 5, 10)}, 
            {UchievementDifficult.NotSoEasy, new Reward(2, 5, 8)}, 
            {UchievementDifficult.Medium, new Reward(10, 50, 100)}, 
            {UchievementDifficult.Hard, new Reward(100, 500, 1000)}, 
        };

        public static Texture2D GetIconForUchievement(this UchievementData u)
        {
            var level = GetLevel(u);
            return UchievementsResourcesLoader.GetIcon(level);
        }


        public static int GetLevel(this UchievementData data)
        {
            var reward = rewards[data.Difficult];

            for(var i = 0; i < reward.Values.Length; i++)
            {

                if(data.Progress < reward.Values[i])
                {
                    return i;
                }
            }

            return reward.Values.Length;
        }

        public static float GetPointsForReward(this UchievementData u)
        {
            if(IsMaxLevel(u))
            {
                return u.Progress;
            }

            var level = GetLevel(u);
            return rewards[u.Difficult].Values[level];
        }

        public static bool IsMaxLevel(this UchievementData u)
        {
            var level = GetLevel(u);

            return level >= rewards[u.Difficult].Values.Length;
        }


        public static bool SetPoints(UchievementData u, float points)
        {
            if(u.Progress < points)
            {
                var oldLevel = UchievementUtils.GetLevel(u);
                u.Progress = points;
                return UchievementUtils.GetLevel(u) > oldLevel;
            }

            return false;
        }

        public static string GetLabelForProgressbar(this UchievementData u)
        {
            if(IsMaxLevel(u))
            {
                return Mathf.Floor(u.Progress).ToString();
            }

            return $"{Mathf.Floor(u.Progress)}/{GetPointsForReward(u):F0}";
        }

        public static bool AddPoints(UchievementData data, float points)
        {
            var oldLevel = UchievementUtils.GetLevel(data);
            data.Progress += points;
            return UchievementUtils.GetLevel(data) > oldLevel;
        }

    }
}