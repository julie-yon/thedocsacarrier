using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Docsa.Gimmick.LinearGimmick))]
    public class LinearGimmickEditor : GimmickEditor
    {
        SerializedProperty Direction;
        SerializedProperty Speed;

        protected override void OnEnable()
        {
            base.OnEnable();
            Direction = serializedObject.FindProperty("Direction");
            Speed = serializedObject.FindProperty("Speed");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(Direction);
            EditorGUILayout.PropertyField(Speed);
        }
    }
}