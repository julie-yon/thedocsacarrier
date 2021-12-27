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

        void DocsaListItemDropCallBack()
        {
            string dragingAuthor = DragAndDropableUI.DragingUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text;
            
            DocsaSakkiManager.instance.MoveDocsaDataTo(dragingAuthor, State);
        }
    }
}