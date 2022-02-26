using UnityEditor;
using Utility;

namespace Docsa.Events
{
    [CustomEditor(typeof(AnimationTrigger))]
    public class AnimationTriggerEditor : EventTriggerEditor
    {
        SerializedProperty TargetAnimation;
        SerializedProperty AnimClipName;

        protected override void OnEnable()
        {
            base.OnEnable();
            TargetAnimation = serializedObject.FindProperty("TargetAnimation");
            AnimClipName = serializedObject.FindProperty("AnimClipName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();

            if (isReady.boolValue)
            {
                EditorGUILayout.PropertyField(TargetAnimation);
                EditorGUILayout.PropertyField(AnimClipName);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}