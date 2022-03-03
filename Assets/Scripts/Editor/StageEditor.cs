using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Utility;

namespace Docsa
{
    [CustomEditor(typeof(Stage))]
    public class StageEditor : Editor
    {
        SerializedProperty StageNumber;
        SerializedProperty ChunkList;
        SerializedProperty CurrentChunk;

        bool ShowChunkList;
        void OnEnable()
        {
            StageNumber = serializedObject.FindProperty("StageNumber");
            ChunkList = serializedObject.FindProperty("ChunkList");
            CurrentChunk = serializedObject.FindProperty("CurrentChunk");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Stage targetStage = (Stage)target;

            EditorGUILayout.PropertyField(StageNumber, new GUIContent("Stage Number"));
            EditorGUILayout.PropertyField(CurrentChunk);

            EditorGUILayout.BeginHorizontal();
            ShowChunkList = EditorGUILayout.BeginFoldoutHeaderGroup(ShowChunkList, "Chunks"); EditorGUILayout.EndFoldoutHeaderGroup();
            if (GUILayout.Button("Search", GUILayout.Width(EditorGUIUtility.currentViewWidth/5)))
            {
                Chunk[] chunks = targetStage.GetComponentsInChildren<Chunk>(true);
                SerializedObject chunksSerializedObject = new SerializedObject(chunks);
                chunksSerializedObject.Update();
                ChunkList.ClearArray();
                CurrentChunk.objectReferenceValue = (Chunk)chunksSerializedObject.targetObject;
                for (int i = 0; i < chunksSerializedObject.targetObjects.Length; i++)
                {
                    ((Chunk)chunksSerializedObject.targetObjects[i]).Stage = targetStage;
                    ((Chunk)chunksSerializedObject.targetObjects[i]).ChunkNumber = i;
                    ChunkList.InsertArrayElementAtIndex(i);
                    ChunkList.GetArrayElementAtIndex(i).objectReferenceValue = (Chunk)chunksSerializedObject.targetObjects[i];
                }
                chunksSerializedObject.ApplyModifiedProperties();
                // int index = 0;
                // foreach(Chunk chunk in chunks)
                // {
                //     chunk.ChunkNumber = index;
                //     index++;
                // }
                // SerializedPropertyDebug.SetSerializedProperty(target, "ChunkList", chunks);
                ShowChunkList = true;
            }
            EditorGUILayout.EndHorizontal();

            if (ShowChunkList)
            {
                for (int i = 0; i < ChunkList.arraySize; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(ChunkList.GetArrayElementAtIndex(i));
                    if (GUILayout.Button("x", GUILayout.Width(30)))
                    {
                        ChunkList.MoveArrayElement(i, ChunkList.arraySize);
                        ChunkList.arraySize--;
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}