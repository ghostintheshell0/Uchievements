using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.UIElements;

namespace Uchievements
{
    public static class UchievementsResourcesLoader
    {
        private static string[] iconPaths = new string[]
        {
            "Editor/Res/none-medal.png",
            "Editor/Res/bronze-medal.png",
            "Editor/Res/silver-medal.png",
            "Editor/Res/gold-medal.png"
        };

        private static AudioClip clip;
        private static string clipPath = "Editor/Res/unlock.mp3";

        private static string uxmlPath = "Editor/Res/UchievementWindow.uxml";
        private static string ussPath = "Editor/Res/UchievementWindow.uss";
        private static VisualTreeAsset visualTree;
        private static StyleSheet styleSheet;
        
        private static string ManagerPath = "Assets/UchievementsData";
        private static string ManagerName = "Assets/UchievementsData/UchievementsData.asset";

        

        private static bool IsInited;
        private static bool IsPackage;

        private static string Assets = "Assets/Uchievements";
        private static string Packages = "Packages/com.ghostintheshell0.uchievements";


        public static Texture2D GetIcon(int level)
        {
            Init();
            var path = GetPath(iconPaths[level]);
            return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }

        public static AudioClip GetAudio()
        {
            if(clip == default)
            {
                Init();
                var path = GetPath(clipPath);
                clip = AssetDatabase.LoadAssetAtPath<AudioClip>(path);
            }

            return clip;
        }

        public static UchievementsManager GetManager()
        {
            var manager = AssetDatabase.LoadAssetAtPath<UchievementsManager>(ManagerName);

            if(manager == default)
            {
                if(!System.IO.Directory.Exists(ManagerPath))
                {
                    System.IO.Directory.CreateDirectory(ManagerPath);
                }

                manager = UchievementsManager.CreateInstance<UchievementsManager>();
                manager.InitAchievements();
                AssetDatabase.CreateAsset(manager, ManagerName);
                AssetDatabase.SaveAssets();
            }

            return manager;
        }
        
        public static VisualTreeAsset VisualTree
        {
            get
            {
                if(visualTree == default)
                {
                    var path = GetPath(uxmlPath);
                    visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
                }
                
                return visualTree;
            }
        }


        public static StyleSheet StyleSheet
        {
            get
            {
                if(styleSheet == default)
                {
                    var path = GetPath(ussPath);
                    styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
                }
                
                return styleSheet;
            }
        }

        private static void Init()
        {
            if(!IsInited)
            {
                IsInited = true;
                var path = Path.Combine(Packages);
                IsPackage = Directory.Exists(path);
            }
        }

        private static string GetPath(string name)
        {
            var start = IsPackage ? Packages : Assets;
            var path = Path.Combine(start, name);
            return path;
        }
    }
}
