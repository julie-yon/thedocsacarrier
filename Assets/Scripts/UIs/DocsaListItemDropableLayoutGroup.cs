using UnityEngine;

using Docsa;

namespace Utility.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class DocsaListItemDropableLayoutGroup : DropableLayoutGroup
    {
        public DocsaData.DocsaState State;

        void Start()
        {
            OnDropCallBack += DocsaListItemDropCallBack;
        }

        void DocsaListItemDropCallBack(DragAndDropableUI TargetUI, UnityEngine.EventSystems.PointerEventData eventData)
        {
            string dragingAuthor = TargetUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;
            
            DocsaSakkiManager.instance.MoveDocsaDataTo(dragingAuthor, State);
        }
    }
}