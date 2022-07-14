using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Docsa.Gimmick.SinkingSwamp))]
    public class SinkingSwampEditor : GimmickEditor
    {
        SerializedProperty SwallowingForce;

        protected override void OnEnable()
        {
            base.OnEnable();
            SwallowingForce = serializedObject.FindProperty("SwallowingForce");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(SwallowingForce);
        }
    }
}