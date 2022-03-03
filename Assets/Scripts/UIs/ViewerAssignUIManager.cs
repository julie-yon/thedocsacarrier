using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Michsky.UI.ModernUIPack;

using Utility;
using Utility.UI;

namespace Docsa
{
    public class ViewerAssignUI : Singleton<ViewerAssignUIManager> {}
    public class ViewerAssignUIManager : ListUI
    {
        public ButtonManagerBasic RandomDistributionButton;
        public ButtonManagerBasic DetermineButton;
        public TextMeshProUGUI DocsaCountText;
        public TextMeshProUGUI HunterCountText;
        public TextMeshProUGUI WaitingCountText;
        public Color ValidCountColor;
        public Color FitCountColor;
        public Color InvalidCountColor;

        protected void Awake()
        {
            AttendingDocsaList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateDropCallBack += UpdateCountTexts;
            AttendingHunterList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateDropCallBack += UpdateCountTexts;
            WaitingViewerList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateDropCallBack += UpdateCountTexts;

            AttendingDocsaList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateBoolDropCallBack += CheckEnableDetermineButton;
            AttendingHunterList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateBoolDropCallBack += CheckEnableDetermineButton;
            WaitingViewerList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateBoolDropCallBack += CheckEnableDetermineButton;

            AttendingDocsaList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateBoolDropCallBack += CheckEnableRandomDistributionButton;
            AttendingHunterList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateBoolDropCallBack += CheckEnableRandomDistributionButton;
            WaitingViewerList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateBoolDropCallBack += CheckEnableRandomDistributionButton;
        }

        public void OpenUI()
        {
            UpdateData();
        }

        public void CloseUI()
        {
        }

        public void OnDetermineButtonClicked()
        {
            Core.instance.ReadyToPlay = true;
            print("Viewer Determined");
        }

        public override void RandomDistribute()
        {
            base.RandomDistribute();
            UpdateData();
        }

        public override void UpdateData()
        {
            base.UpdateData();
            UpdateCountTexts();
            CheckEnableDetermineButton();
            CheckEnableRandomDistributionButton();
        }

        /// <summary>
        /// Call when listItem move
        /// </summary>
        bool CheckEnableDetermineButton(DragAndDropableUI ui = null, UnityEngine.EventSystems.PointerEventData eventData = null)
        {
            try
            {
                if (DocsaSakkiManager.instance.AttendingDocsaDict.Count <= DocsaSakkiManager.instance.AttendingDocsaLimit
                        && DocsaSakkiManager.instance.AttendingHunterDict.Count <= DocsaSakkiManager.instance.AttendingHunterLimit
                        && DocsaSakkiManager.instance.WaitingViewerDict.Count <= DocsaSakkiManager.instance.WaitingViewerLimit)
                {
                    DetermineButton.buttonVar.interactable = true;
                    return true;
                } else
                {
                    DetermineButton.buttonVar.interactable = false;
                    return false;
                }
            } catch {return false;}
        }

        bool CheckEnableRandomDistributionButton(DragAndDropableUI ui = null, UnityEngine.EventSystems.PointerEventData eventData = null)
        {
            try
            {
                if (DocsaSakkiManager.instance.WaitingViewerDict.Count > 0)
                {
                    RandomDistributionButton.buttonVar.interactable = true;
                    return true;
                } else
                {
                    RandomDistributionButton.buttonVar.interactable = false;
                    return false;
                }
            } catch {return false;}
        }

        void UpdateCountTexts(DragAndDropableUI ui = null, UnityEngine.EventSystems.PointerEventData eventData = null)
        {
            DocsaCountText.text = DocsaSakkiManager.instance.AttendingDocsaDict.Count.ToString() + " / " + DocsaSakkiManager.instance.AttendingDocsaLimit.ToString();
            HunterCountText.text = DocsaSakkiManager.instance.AttendingHunterDict.Count.ToString() + " / " + DocsaSakkiManager.instance.AttendingHunterLimit.ToString();
            WaitingCountText.text = DocsaSakkiManager.instance.WaitingViewerDict.Count.ToString() + " / " + DocsaSakkiManager.instance.WaitingViewerLimit.ToString();

            if (DocsaSakkiManager.instance.AttendingDocsaDict.Count < DocsaSakkiManager.instance.AttendingDocsaLimit)
            {
                DocsaCountText.color = ValidCountColor;
            } else if (DocsaSakkiManager.instance.AttendingDocsaDict.Count == DocsaSakkiManager.instance.AttendingDocsaLimit)
            {
                DocsaCountText.color = FitCountColor;
            } else
            {
                DocsaCountText.color = InvalidCountColor;
            }

            if (DocsaSakkiManager.instance.AttendingHunterDict.Count < DocsaSakkiManager.instance.AttendingHunterLimit)
            {
                HunterCountText.color = ValidCountColor;
            } else if (DocsaSakkiManager.instance.AttendingHunterDict.Count == DocsaSakkiManager.instance.AttendingHunterLimit)
            {
                HunterCountText.color = FitCountColor;
            } else
            {
                HunterCountText.color = InvalidCountColor;
            }

            if (DocsaSakkiManager.instance.WaitingViewerDict.Count < DocsaSakkiManager.instance.WaitingViewerLimit)
            {
                WaitingCountText.color = ValidCountColor;
            } else if (DocsaSakkiManager.instance.WaitingViewerDict.Count == DocsaSakkiManager.instance.WaitingViewerLimit)
            {
                WaitingCountText.color = FitCountColor;
            } else
            {
                WaitingCountText.color = InvalidCountColor;
            }
        }

    }
}