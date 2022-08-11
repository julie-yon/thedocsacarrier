using UnityEngine;
using UnityEngine.EventSystems;

using Docsa;

namespace dkstlzu.Utility.UI
{
    public class DragAndDropTrashCan : MonoBehaviour, IDropHandler
    {
        public ViewerAssignUIManager ViewerAssignUIManager;
        public void OnDrop(PointerEventData eventData)
        {
            GameObject go;
            if (go = DragAndDropableUI.DragingUI.gameObject)
            {
                DocsaListItem listItem;
                if (go.TryGetComponent<DocsaListItem>(out listItem))
                {
                    ViewerAssignUIManager.RemoveListItem(listItem);
                }
            }
        }
    }
}