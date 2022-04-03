using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Uchievements
{
    public class UchievementsWindow : EditorWindow
    {
        private bool isNeedShowPopup;

        private Vector2 scrollPos;

        [MenuItem("Window/Uchievements")]
        public static void Init()
        {
            var window = (UchievementsWindow)EditorWindow.GetWindow(typeof(UchievementsWindow));
            window.titleContent = new GUIContent("Uchievements");
            window.Show();
        }

        private void OnGUI()
        {
            var uchievements = LoadUchievements();
            
            if(uchievements == default || uchievements.Count == 0) return;

            DrawScrollArea(uchievements.Count);

            for(var i = 0; i < uchievements.Count; i++)
            {
                DrawUchievement(uchievements[i], i);
                if(i < uchievements.Count - 1)
                {
                    GUILayout.Space(UchievementStyle.UchievementPadding);
                }
            }

            GUILayout.EndScrollView();

        }

        private void DrawScrollArea(int uchievements)
        {
            var height = uchievements * (UchievementStyle.UchievementHeight + UchievementStyle.UchievementPadding) - UchievementStyle.UchievementPadding;
            var showVertical = height > this.position.height;
            scrollPos = GUILayout.BeginScrollView(scrollPos, false, showVertical, GUILayout.Width(this.position.width), GUILayout.Height(position.height));
        }

        private void DrawUchievement(in UchievementData u, int index)
        {
            GUILayout.BeginVertical();


            GUILayout.BeginHorizontal();
            var iconStyle = new GUIStyle();
            iconStyle.fixedWidth = UchievementStyle.UchievementIconWidth;
            iconStyle.fixedHeight = UchievementStyle.UchievementHeight;
            GUILayout.Label(UchievementUtils.GetIconForUchievement(u), iconStyle);

            GUILayout.BeginVertical();

            DrawUchievementLabel(u, index);
            DrawUchievementProgressbar(u, index);

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        private void DrawUchievementLabel(UchievementData u, int index)
        {
            var w = position.width - UchievementStyle.UchievementHeight - UchievementStyle.OffsetForScrollbar;
            var labelStyle = new GUIStyle();
            labelStyle.fixedHeight = UchievementStyle.LabelHeight;
            labelStyle.fixedWidth = w;
            labelStyle.fontSize = UchievementStyle.TextSize;
            labelStyle.alignment = UchievementStyle.TextAligment;
            labelStyle.normal.textColor = UchievementStyle.TextColor;
            GUILayout.Label(u.Name, labelStyle);
        }

        private void DrawUchievementProgressbar(UchievementData u, int index)
        {
            var w = position.width - UchievementStyle.UchievementHeight - UchievementStyle.OffsetForScrollbar;
            var y = (UchievementStyle.UchievementHeight + UchievementStyle.UchievementPadding) * index - UchievementStyle.UchievementPadding + UchievementStyle.LabelHeight + UchievementStyle.ProgressOffset;
            
            var pos = new Rect(UchievementStyle.UchievementHeight, y, w, UchievementStyle.ProgressHeight);
            
            var maxProgress = UchievementUtils.GetPointsForReward(u);
            var progressbarLabel = UchievementUtils.GetLabelForProgressbar(u);
            EditorGUI.ProgressBar(pos, u.Progress / maxProgress, progressbarLabel);
        }

        private List<UchievementData> LoadUchievements()
        {
            return Uchievements.Manager.All;
        }
    }

    [System.Serializable]
    public class UchievementData
    {
        public float Progress;
        public string Name;
        public UchievementDifficult Difficult;
    }
}
