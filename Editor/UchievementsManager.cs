using System.Collections.Generic;
using UnityEngine;

namespace Uchievements
{
    public class UchievementsManager : ScriptableObject
    {
        public bool Mute = false;
        public bool Console = false;

        [Space(1000)]
        public UchievementData MultiScenes;
        public UchievementData BuildWithALotOfScenes;
        public UchievementData BuildForAndroidOrIos;
        public UchievementData BuildForPc;
        public UchievementData BuildForConsole;
        public UchievementData Builds;
        public UchievementData PauseInPlayMode;
        public UchievementData PauseInEditMode;
        public UchievementData SaveScene;
        public UchievementData ImportPackages;
        public UchievementData CompilingTime;
        public UchievementData BuildForWeb;

        private List<UchievementData> all;


        public void InitAchievements()
        {
            MultiScenes = new UchievementData()
            {
                Name = "More is better",
                Difficult = UchievementDifficult.NotSoEasy,
            };

            BuildWithALotOfScenes = new UchievementData()
            {
                Name = "Multiverse",
                Difficult = UchievementDifficult.Medium,
            };
            
            BuildForAndroidOrIos = new UchievementData()
            {
                Name = "Mobility",
                Difficult = UchievementDifficult.Easy,
            };

            BuildForPc = new UchievementData()
            {
                Name = "PC Forever!",
                Difficult = UchievementDifficult.Easy,
            };

            BuildForConsole = new UchievementData()
            {
                Name = "Exclusive build",
                Difficult = UchievementDifficult.Easy,
            };

            BuildForWeb = new UchievementData()
            {
                Name = "Browser warrior",
                Difficult = UchievementDifficult.Easy,
            };

            Builds = new UchievementData()
            {
                Name = "Great builder",
                Difficult = UchievementDifficult.Medium,
            };

            PauseInPlayMode = new UchievementData()
            {
                Name = "Debug like a pro",
                Difficult = UchievementDifficult.Hard,
            };

            PauseInEditMode = new UchievementData()
            {
                Name = "Pause in pause",
                Difficult = UchievementDifficult.Hard,
            };

            SaveScene = new UchievementData()
            {
                Name = "Keep calm and save scene",
                Difficult = UchievementDifficult.Hard,
            };

            ImportPackages = new UchievementData()
            {
                Name = "Project from projects",
                Difficult = UchievementDifficult.Easy,
            };

            CompilingTime = new UchievementData()
            {
                Name = "Compiling...",
                Difficult = UchievementDifficult.Easy,
            };
        }

        private List<UchievementData> CreateAll()
        {
            if(all == default)
            {
                all = new List<UchievementData>();
            }

            all.Clear();

            all.Add(MultiScenes);
            all.Add(BuildWithALotOfScenes);
            all.Add(BuildForAndroidOrIos);
            all.Add(BuildForPc);
            all.Add(BuildForConsole);
            all.Add(Builds);
            all.Add(PauseInPlayMode);
            all.Add(PauseInEditMode);
            all.Add(SaveScene);
            all.Add(CompilingTime);
            all.Add(BuildForWeb);
            all.Add(ImportPackages);

            return all;
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            InitAchievements();
        }

        public List<UchievementData> All
        {
            get => CreateAll();
        }
    }
}