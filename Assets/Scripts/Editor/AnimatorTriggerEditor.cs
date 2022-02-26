using UnityEditor;
using Utility;

namespace Docsa.Events
{
    [CustomEditor(typeof(AnimatorTrigger))]
    public class AnimatorTriggerEditor : EventTriggerEditor
    {
        SerializedProperty TargetAnimator;
        SerializedProperty TriggerName;

        protected override void OnEnable()
        {
            base.OnEnable();
            TargetAnimator = serializedObject.FindProperty("TargetAnimator");
            TriggerName = serializedObject.FindProperty("TriggerName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            if (isReady.boolValue)
            {
                EditorGUILayout.PropertyField(TargetAnimator);
                EditorGUILayout.PropertyField(TriggerName);
            }

            serializedObject.ApplyModifiedProperties();
        }    }
}