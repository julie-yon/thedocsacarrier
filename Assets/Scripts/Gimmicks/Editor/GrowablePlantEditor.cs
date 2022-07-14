using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Docsa.Gimmick.GrowablePlant))]
    public class GrowablePlantEditor : GimmickEditor
    {
        SerializedProperty Animator;

        protected override void OnEnable()
        {
            base.OnEnable();
            Animator = serializedObject.FindProperty("Animator");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(Animator);

            serializedObject.ApplyModifiedProperties();
        }
    }
}