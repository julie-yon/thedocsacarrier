using UnityEngine;

namespace Docsa.Events
{
    public class ViewerAssignBox : MonoBehaviour
    {
        /// <summary>
        /// On EventTrigger Reference
        /// </summary>
        public void Open()
        {
            Chunk.Current.InitCharacters();
            ESCUIManager.instance.OpenUI();
            ESCUIManager.instance.ViewerAssignUI.UpdateData();
            ESCUIManager.instance.WM.currentWindowIndex = 1;
        }
    }
}