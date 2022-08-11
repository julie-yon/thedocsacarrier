using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Michsky.UI.ModernUIPack;

using dkstlzu.Utility;
using dkstlzu.Utility.UI;

namespace Docsa
{
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

            AttendingDocsaList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateDropCallBack += CheckEnableDetermineButton;
            AttendingHunterList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateDropCallBack += CheckEnableDetermineButton;
            WaitingViewerList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateDropCallBack += CheckEnableDetermineButton;

            AttendingDocsaList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateDropCallBack += CheckEnableRandomDistributionButton;
            AttendingHunterList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateDropCallBack += CheckEnableRandomDistributionButton;
            WaitingViewerList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().OnLateDropCallBack += CheckEnableRandomDistributionButton;

        }

        // Use on button
        public void OnDetermineButtonClicked()
        {
            DocsaSakkiManager.instance.AssignViewers();
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
        void CheckEnableDetermineButton(DragAndDropableUI ui = null, UnityEngine.EventSystems.PointerEventData eventData = null)
        {
            try
            {
                if (DocsaSakkiManager.instance.AttendingDocsaDict.Count == DocsaSakkiManager.instance.AttendingDocsaLimit
                        && DocsaSakkiManager.instance.AttendingHunterDict.Count == DocsaSakkiManager.instance.AttendingHunterLimit)
                {
                    DetermineButton.buttonVar.interactable = true;
                } else
                {
                    DetermineButton.buttonVar.interactable = false;
                }
            } catch (System.NullReferenceException) {}
        }

        void CheckEnableRandomDistributionButton(DragAndDropableUI ui = null, UnityEngine.EventSystems.PointerEventData eventData = null)
        {
            try
            {
                if (DocsaSakkiManager.instance.WaitingViewerDict.Count > 0)
                {
                    RandomDistributionButton.buttonVar.interactable = true;
                } else
                {
                    RandomDistributionButton.buttonVar.interactable = false;
                }
            } catch {}
        }

        void UpdateCountTexts(DragAndDropableUI ui = null, UnityEngine.EventSystems.PointerEventData eventData = null)
        {
            if (!DocsaSakkiManager.instance) return;

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