using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Docsa.Gimmick.Rock))]
    public class RockEditor : GimmickEditor
    {
        SerializedProperty RockSprite;
        SerializedProperty RockSprite_Transparent;
        SerializedProperty Renderer;

        protected override void OnEnable()
        {
            base.OnEnable();
            RockSprite = serializedObject.FindProperty("RockSprite");
            RockSprite_Transparent = serializedObject.FindProperty("RockSprite_Transparent");
            Renderer = serializedObject.FindProperty("Renderer");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(RockSprite);
            EditorGUILayout.PropertyField(RockSprite_Transparent);
            EditorGUILayout.PropertyField(Renderer);

            serializedObject.ApplyModifiedProperties();
        }
    }
}