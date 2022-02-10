using UnityEngine;
using UnityEngine.EventSystems;

using Docsa;

namespace Utility.UI
{
    public class DragAndDropTrashCan : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            GameObject go;
            if (go = DragAndDropableUI.DragingUI.gameObject)
            {
                DocsaListItem listItem;
                if (go.TryGetComponent<DocsaListItem>(out listItem))
                {
                    ViewerAssignUI.instance.RemoveListItem(listItem);
                }
            }
        }
    }
}