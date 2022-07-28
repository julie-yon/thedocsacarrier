using UnityEngine;

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