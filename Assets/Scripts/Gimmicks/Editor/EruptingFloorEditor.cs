using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Docsa.Gimmick.EruptingFloor))]
    public class EruptingFloorEditor : GimmickEditor
    {
        SerializedProperty Height;

        protected override void OnEnable()
        {
            base.OnEnable();
            Height = serializedObject.FindProperty("Height");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(Height);

            serializedObject.ApplyModifiedProperties();
        }
    }
}