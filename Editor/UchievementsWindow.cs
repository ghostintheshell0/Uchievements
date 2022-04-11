using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Uchievements
{
    public class UchievementsWindow : EditorWindow
    {
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
            root.styleSheets.Add(UchievementsResourcesLoader.StyleSheet);

            for(var i = 0; i < count; i++)
            {
                VisualElement uxml = UchievementsResourcesLoader.VisualTree.CloneTree();
                scroll.Add(uxml);

                var view = new UchievementView(uxml);
                views.Add(view);
            }
        }

        private List<UchievementData> LoadUchievements()
        {
            return Uchievements.Manager.All;
        }

    }
}