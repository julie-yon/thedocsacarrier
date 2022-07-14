using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Docsa.Gimmick.Basalt))]
    public class BasaltEditor : LinearGimmickEditor
    {
        SerializedProperty Damage;

        protected override void OnEnable()
        {
            base.OnEnable();
            Damage = serializedObject.FindProperty("Damage");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(Damage);

            serializedObject.ApplyModifiedProperties();
        }
    }
}