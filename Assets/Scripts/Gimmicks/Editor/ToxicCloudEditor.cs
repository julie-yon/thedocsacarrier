using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Docsa.Gimmick.ToxicCloud))]
    public class ToxicCloudEditor : LinearGimmickEditor
    {
        SerializedProperty Damage;
        SerializedProperty DamageCoolTime;

        protected override void OnEnable()
        {
            base.OnEnable();
            Damage = serializedObject.FindProperty("Damage");
            DamageCoolTime = serializedObject.FindProperty("DamageCoolTime");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(Damage);
            EditorGUILayout.PropertyField(DamageCoolTime);
        }
    }
}