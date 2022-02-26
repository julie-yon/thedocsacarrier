using UnityEditor;
using Utility;

namespace Docsa.Events
{
    [CustomEditor(typeof(AnimatorTrigger))]
    public class AnimatorTriggerEditor : EventTriggerEditor
    {
        SerializedProperty TargetAnimator;
        SerializedProperty TriggerNames;

        protected override void OnEnable()
        {
            base.OnEnable();
            TargetAnimator = serializedObject.FindProperty("TargetAnimator");
            TriggerNames = serializedObject.FindProperty("TriggerNames");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            if (isReady.boolValue)
            {
                EditorGUILayout.PropertyField(TargetAnimator);
                EditorGUILayout.PropertyField(TriggerNames);
            }

            serializedObject.ApplyModifiedProperties();
        }    }
}