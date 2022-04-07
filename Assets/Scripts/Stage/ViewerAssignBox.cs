using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Michsky.UI.ModernUIPack;

namespace Docsa.Events
{
    public class ViewerAssignBox : MonoBehaviour
    {
        ESCUIManager ESCUI;

        void Awake()
        {
            ESCUI = FindObjectOfType<ESCUIManager>();
            if (!ESCUI)
            {
                Debug.LogWarning(gameObject.name + " : can not find ESCUIManager.");
                Destroy(gameObject);
            }
        }

        public void Open()
        {
            Chunk.Current.InitCharacters();
            ESCUI.OpenUI();
            ESCUI.GetComponentInChildren<WindowManager>().currentWindowIndex = 1;
        }
    }
}