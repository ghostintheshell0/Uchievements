using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Uchievements
{
    public class UchievementsWindow : EditorWindow
    {
        private static string uxmlPath = "Assets/Uchievements/Editor/Res/UchievementWindow.uxml";
        private static string ussPath = "Assets/Uchievements/Editor/Res/UchievementWindow.uss";

        private static VisualTreeAsset visualTree;
        private static StyleSheet styleSheet;

        private List<UchievementView> views;

        [MenuItem("Window/Uchievements")]

        public static void Init()
        {
            var window = (UchievementsWindow)EditorWindow.GetWindow(typeof(UchievementsWindow));
            window.titleContent = new GUIContent("Uchievements");

            var uchievements = window.LoadUchievements();

            window.Show();
        }

        private void OnGUI()
        {
            var uchievements = LoadUchievements();
            
            if(uchievements == default || uchievements.Count == 0) return;

            if(views == default)
            {
                CreateViews(uchievements.Count);
            }

            for(var i = 0; i < uchievements.Count; i++)
            {
                views[i].Show(uchievements[i]);
            }
        }

        private void CreateViews(int count)
        {
            VisualElement root = rootVisualElement;

            views = new List<UchievementView>(count);
            ScrollView scroll = new ScrollView();
            scroll.showVertical = true;
            root.Add(scroll);
            root.styleSheets.Add(StyleSheet);

            for(var i = 0; i < count; i++)
            {
                VisualElement uxml = VisualTree.CloneTree();
                uxml.styleSheets.Add(StyleSheet);
                scroll.Add(uxml);

                var view = new UchievementView(uxml);
                views.Add(view);
            }
        }

        private List<UchievementData> LoadUchievements()
        {
            return Uchievements.Manager.All;
        }

        public static VisualTreeAsset VisualTree
        {
            get
            {
                if(visualTree == default)
                {
                    visualTree  = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
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
                    styleSheet  = AssetDatabase.LoadAssetAtPath<StyleSheet>(ussPath);
                }
                
                return styleSheet;
            }
        }
    }
}