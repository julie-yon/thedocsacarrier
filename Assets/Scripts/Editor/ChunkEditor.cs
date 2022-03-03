using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Docsa
{
    [CustomEditor(typeof(Chunk))]
    public class ChunkEditor : Editor
    {
        SerializedObject stageSerializedObject;

        Stage parentStage;

        void OnEnable()
        {
            parentStage = ((Component)target).GetComponentInParent<Stage>();
            if (parentStage != null)
            {
                stageSerializedObject = new SerializedObject(parentStage);
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            if (stageSerializedObject != null)
                stageSerializedObject.Update();

            Chunk targetChunk = (Chunk)target;
            if (parentStage == null) return;
            if (!parentStage.ChunkList.Contains(targetChunk))
            {
                if (GUILayout.Button("Add in stage on parent"))
                {
                    serializedObject.FindProperty("Stage").objectReferenceValue = stageSerializedObject.targetObject;
                    var listProperty = stageSerializedObject.FindProperty("ChunkList");
                    serializedObject.FindProperty("ChunkNumber").intValue = listProperty.arraySize;
                    listProperty.InsertArrayElementAtIndex(listProperty.arraySize);
                    listProperty.GetArrayElementAtIndex(listProperty.arraySize-1).objectReferenceValue = targetChunk;
                    // targetChunk.ChunkNumber = parentStage.ChunkList.Count;
                    // parentStage.ChunkList.Add(targetChunk);
                }
            }

            if (stageSerializedObject != null)
                stageSerializedObject.ApplyModifiedProperties();
                
            serializedObject.ApplyModifiedProperties();
        }
    }
}