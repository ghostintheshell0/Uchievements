using UnityEditor;
using UnityEngine;

namespace Uchievements
{
    [CustomEditor(typeof(UchievementsManager))]
    public class UchievementsManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var manager = (UchievementsManager) target;

            EditorGUI.BeginChangeCheck();
            manager.Mute = GUILayout.Toggle(manager.Mute, "Mute");
            manager.Console = GUILayout.Toggle(manager.Console, "Console");
            if(!manager.Console)
            {
                EditorGUILayout.HelpBox("Can cause error messages.", MessageType.Warning);
            }

            if(GUILayout.Button("Clear"))
            {
                if(EditorUtility.DisplayDialog("Uchievements", "Do you really want to clear your Uchievements progress?", "Yes", "No"))
                {
                    manager.Clear();
                }
            }

            if(EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(manager);
            }
        }
    }
}