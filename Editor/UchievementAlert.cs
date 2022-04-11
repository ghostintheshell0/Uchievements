using UnityEditor;
using UnityEngine;

namespace Uchievements
{
    public class UchievementAlert : EditorWindow
    {
        public float LifeTime = 1.5f;
        private long creationTime = System.DateTime.Now.Ticks;

        private UchievementData uchievementData;
        private UchievementView view = default;

        public static void Show(UchievementData u)
        {
            var window = (UchievementAlert) EditorWindow.CreateInstance(typeof(UchievementAlert));
            window.uchievementData = u;
            window.titleContent = new GUIContent("Uchievements unlocked");
            window.minSize = new Vector2(1, 1);
            window.maxSize = new Vector2(500, 100);
            window.position = new Rect(9999, 9999, 500, 100);
            window.View.Show(u);
            window.ShowPopup();
        }

        private void OnGUI()
        {

            var now = System.DateTime.Now.Ticks;
            var lifeTime = LifeTime * System.TimeSpan.TicksPerSecond;
            if(now - creationTime > lifeTime)
            {
                Close();
                DestroyImmediate(this);
                return;
            }
        }

        private UchievementView View
        {
            get
            {
                if(view == default)
                {
                    var root = rootVisualElement;

                    var tree = UchievementsResourcesLoader.VisualTree.CloneTree();
                    tree.styleSheets.Add(UchievementsResourcesLoader.StyleSheet);

                    view = new UchievementView(tree);
                    
                    view.HideProgressBar();
                    root.Add(tree);
                }

                return view;
            }
        }
    }
}