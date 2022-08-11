using UnityEditor;
using dkstlzu.Utility;

namespace Docsa.Events
{
    [CustomEditor(typeof(AnimatorTrigger))]
    public class AnimatorTriggerEditor : EventTriggerEditor
    {
        SerializedProperty TargetAnimator;
        SerializedProperty TriggerNames;
        SerializedProperty TrueBoolNames;
        SerializedProperty FalseBoolNames;

        protected override void OnEnable()
        {
            base.OnEnable();
            TargetAnimator = serializedObject.FindProperty("TargetAnimator");
            TriggerNames = serializedObject.FindProperty("TriggerNames");
            TrueBoolNames = serializedObject.FindProperty("TrueBoolNames");
            FalseBoolNames = serializedObject.FindProperty("FalseBoolNames");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            if (isReady.boolValue)
            {
                EditorGUILayout.PropertyField(TargetAnimator);
                EditorGUILayout.PropertyField(TriggerNames);
                EditorGUILayout.PropertyField(TrueBoolNames);
                EditorGUILayout.PropertyField(FalseBoolNames);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}