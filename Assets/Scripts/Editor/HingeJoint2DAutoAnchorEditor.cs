using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace dkstlzu.Utility
{
    [CustomEditor(typeof(Joint2DAutoAnchor))]
    public class HingeJoint2DAutoAnchor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Do"))
            {
                (target as Joint2DAutoAnchor).AutoAnchor();
            }
        }
    }
}