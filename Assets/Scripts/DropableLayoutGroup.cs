using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Docsa;

namespace Utility.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class DropableLayoutGroup : MonoBehaviour, IDropHandler
    {
        public LayoutGroup LayoutGroup;
        RectTransform _rect;

        // Docsa Project Specifically
        public DocsaData.DocsaState State;
        // Docsa Project End
        void Awake()
        {
            if (LayoutGroup == null)
            {
                LayoutGroup = GetComponent<LayoutGroup>();
            }
            _rect = LayoutGroup.GetComponent<RectTransform>();
        }

        public void OnDrop(PointerEventData eventData)
        {
            print("OnDrop");
            if (DragAndDropUI.instance.DragingUI != null)
            {
                if (DragAndDropUI.instance.DragingUI.transform.parent == LayoutGroup.transform)
                {
                    print("Same rect");
                    return;
                }

                // If not DocsaProject you must use this line instead of Tag : DocsaMove
                // DragAndDropUI.instance.DragingUI.transform.SetParent(LayoutGroup.transform);
                DragAndDropUI.instance.DragingUI.SuccessfullyDroped = true;

                // Docsa Project Specifically
                string dragingAuthor = DragAndDropUI.instance.DragingUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;
                
                ESCUIManager.instance.MoveDocsaDataCardTo(dragingAuthor, State); // Tag : DocsaMove
                DocsaSakkiManager.instance.MoveDocsaDataTo(dragingAuthor, State);
                // Docsa Project End
            }
        }
    }
}