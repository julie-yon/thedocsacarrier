using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Michsky.UI.ModernUIPack;

namespace Docsa.Events
{
    public class ViewerAssignBox : MonoBehaviour
    {
        public void Open()
        {
            Chunk.Current.InitCharacters();
            ESCUIManager.instance.OpenUI();
            ESCUIManager.instance.ViewerAssignUI.UpdateData();
            ESCUIManager.instance.WM.currentWindowIndex = 1;
        }
    }
}