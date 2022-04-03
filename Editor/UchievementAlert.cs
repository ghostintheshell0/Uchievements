using UnityEditor;
using UnityEngine;

namespace Uchievements
{
    public class UchievementAlert : EditorWindow
    {
        public float LifeTime = 1.5f;
        private long creationTime = System.DateTime.Now.Ticks;

        private UchievementData uchievementData;

        public static void Show(UchievementData u)
        {
            var window = (UchievementAlert) EditorWindow.CreateInstance(typeof(UchievementAlert));
            window.titleContent = new GUIContent("Uchievements unlocked");
            window.minSize = new Vector2(1, 1);
            window.maxSize = new Vector2(UchievementStyle.AlertWidth, UchievementStyle.IconHeight);
            window.position = new Rect(9999, 9999, UchievementStyle.AlertWidth, UchievementStyle.IconHeight);

            window.ShowPopup();
            window.uchievementData = u;
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

            GUILayout.BeginHorizontal();
            var iconStyle = new GUIStyle();
            iconStyle.fixedWidth = UchievementStyle.IconWidth;
            iconStyle.fixedHeight = UchievementStyle.IconHeight;
            GUILayout.Label(UchievementUtils.GetIconForUchievement(uchievementData), iconStyle);
            
            var labelStyle = new GUIStyle();
            labelStyle.fixedWidth = position.width - UchievementStyle.IconWidth;
            labelStyle.fixedHeight = UchievementStyle.IconHeight;
            labelStyle.fontSize = UchievementStyle.TextSize;
            labelStyle.alignment = UchievementStyle.TextAligment;
            labelStyle.normal.textColor = UchievementStyle.TextColor;

            GUILayout.Label(uchievementData.Name, labelStyle);
            GUILayout.EndHorizontal();
        }
    }
}