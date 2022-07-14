using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Docsa.Gimmick.Gimmick))]
    public class GimmickEditor : Editor
    {
        SerializedProperty ActivateOnAwake;
        SerializedProperty StartOnAwake;
        SerializedProperty isActivated;
        SerializedProperty isStarted;

        SerializedProperty script;

        protected virtual void OnEnable()
        {
            ActivateOnAwake = serializedObject.FindProperty("ActivateOnAwake");
            StartOnAwake = serializedObject.FindProperty("StartOnAwake");
            isActivated = serializedObject.FindProperty("Activated");
            isStarted = serializedObject.FindProperty("Started");

            script = serializedObject.FindProperty("m_Script");
        }
        
        public override void OnInspectorGUI()
        {
            // GUI.enabled = false;
            // EditorGUILayout.PropertyField(script, true, new GUILayoutOption[0]);
            // GUI.enabled = true;

            EditorGUILayout.PropertyField(ActivateOnAwake);
            EditorGUILayout.PropertyField(StartOnAwake);

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(isActivated);
            if (EditorGUI.EndChangeCheck())
            {
                if (isActivated.boolValue) ((Docsa.Gimmick.Gimmick)target).Activate();
                else ((Docsa.Gimmick.Gimmick)target).Deactivate();
            }
            
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(isStarted);
            if (EditorGUI.EndChangeCheck())
            {
                if (isStarted.boolValue) ((Docsa.Gimmick.Gimmick)target).StartGimmick();
                else ((Docsa.Gimmick.Gimmick)target).End();
            }
        }
    }
}