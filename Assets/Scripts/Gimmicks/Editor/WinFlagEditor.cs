using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Docsa.Gimmick.WinFlag))]
    public class WinFlagEditor : GimmickEditor
    {
        SerializedProperty ClearAudioClip;
        SerializedProperty ClearSoundArg;

        protected override void OnEnable()
        {
            base.OnEnable();
            ClearAudioClip = serializedObject.FindProperty("ClearAudioClip");
            ClearSoundArg = serializedObject.FindProperty("ClearSoundArg");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(ClearAudioClip);
            EditorGUILayout.PropertyField(ClearSoundArg);

            serializedObject.ApplyModifiedProperties();
        }
    }
}