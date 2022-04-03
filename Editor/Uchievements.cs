using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.Callbacks;

namespace Uchievements
{
    [InitializeOnLoad]
    public class Uchievements
    {
        private readonly static long alertShowColdownSeconds = 3;
        public static UchievementsManager Manager;
        private static bool Inited;
        private static long timeInCompiling;
        private static bool compiling;
        private static long lastCompilingTime;
        private static long lastAlertShowTime = long.MinValue;
        private static long alertShowColdown;

        private static List<UchievementData> forShow = new List<UchievementData>();

        static Uchievements()
        {
            if(!Inited)
            {
                
                Manager = UchievementsResourcesLoader.GetManager();
                
                if(Manager != default)
                {
                    alertShowColdown = System.TimeSpan.TicksPerSecond * alertShowColdownSeconds;
                    EditorApplication.update += OnUpdate;
                    EditorSceneManager.sceneSaved += OnSceneSave;
                    EditorApplication.pauseStateChanged += OnPause;
                    SceneManager.sceneLoaded += OnSceneLoaded;
                    AssetDatabase.importPackageCompleted += OnImportPackageCompleted;

                    Inited = true;
                }
            }
        }

        private static void OnImportPackageCompleted(string packageName)
        {
            AddPoints(Manager.ImportPackages, 1f);
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SetPoints(Manager.MultiScenes, SceneManager.sceneCount);
        }

        private static void OnPause(PauseState state)
        {
            if(state == PauseState.Paused)
            {
                if(EditorApplication.isPlaying)
                {
                    AddPoints(Manager.PauseInPlayMode, 1f);
                }
                else
                {
                    AddPoints(Manager.PauseInEditMode, 1f);
                }
            }
        }

        private static void OnSceneSave(Scene scene)
        {
            AddPoints(Manager.SaveScene, 1f);
        }

        private static void OnUpdate()
        {
            OnCompiling();

            var now = System.DateTime.Now.Ticks;
            if(forShow.Count > 0 && lastAlertShowTime + alertShowColdown < now)
            {
                lastAlertShowTime = now;
                UchievementAlert.Show(forShow[0]);
                forShow.RemoveAt(0);
                if(!Manager.Mute)
                {
                    AudioUtils.PlayClip(UchievementsResourcesLoader.GetAudio());
                }
                
            }
        }

        private static void OnCompiling()
        {
            if(EditorApplication.isCompiling)
            {
                if(compiling)
                {
                    var delta = System.DateTime.Now.Ticks - lastCompilingTime;
                    var seconds = (float) delta / (float) TimeSpan.TicksPerMinute;
                    AddPoints(Manager.CompilingTime, seconds);
                    lastCompilingTime = System.DateTime.Now.Ticks;
                }
                else
                {
                    compiling = true;
                    lastCompilingTime = System.DateTime.Now.Ticks;
                }
            }
            else
            {
                if(compiling)
                {
                    compiling = false;
                }
            }
        }
        
        [PostProcessBuildAttribute(100)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            switch(target)
            {
                case BuildTarget.StandaloneLinux64:
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.StandaloneOSX:
                    AddPoints(Manager.BuildForPc, 1);
                break;

                case BuildTarget.Android:
                case BuildTarget.iOS:
                    AddPoints(Manager.BuildForAndroidOrIos, 1);
                break;

                case BuildTarget.XboxOne:
                case BuildTarget.PS4:
                case BuildTarget.PS5:
                case BuildTarget.Switch:
                    AddPoints(Manager.BuildForConsole, 1);
                break;

                case BuildTarget.WebGL:
                    AddPoints(Manager.BuildForWeb, 1);
                break;

                default:
                break;
            }
            
            AddPoints(Manager.Builds, 1f);

            SetPoints(Manager.BuildWithALotOfScenes, EditorBuildSettings.scenes.Length);
            
        }

        public static void AddPoints(UchievementData u, float points)
        {
            if(UchievementUtils.AddPoints(u, points))
            {
                Show(u);
            }

            EditorUtility.SetDirty(Manager);
        }


        public static void SetPoints(UchievementData u, float points)
        {
            if(UchievementUtils.SetPoints(u, points))
            {
                Show(u);
            }

            EditorUtility.SetDirty(Manager);
        }

        public static void Show(UchievementData u)
        {
            if(Manager.Console)
            {
                if(!Manager.Mute)
                {
                    AudioUtils.PlayClip(UchievementsResourcesLoader.GetAudio());
                }
                Debug.Log($"<Color=#{ColorUtility.ToHtmlStringRGB(UchievementStyle.TextColor)}>{u.Name} (Level {UchievementUtils.GetLevel(u)})</Color>");
            }
            else
            {
                forShow.Add(u);
            }
        }
    }
}