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
        public GameObject UIObject;
        public ButtonManagerBasic DetermineButton;
        public TextMeshProUGUI DocsaCountText;
        public TextMeshProUGUI HunterCountText;
        public TextMeshProUGUI WaitingCountText;
        public Color ValidCountColor;
        public Color FitCountColor;
        public Color InvalidCountColor;
        public bool isOn;

        protected override void Awake()
        {
            base.Awake();
            AttendingDocsaList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().AfterDropCallBack += CheckEnableDetermineButton;
            AttendingHunterList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().AfterDropCallBack += CheckEnableDetermineButton;
            WaitingViewerList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().AfterDropCallBack += CheckEnableDetermineButton;

            AttendingDocsaList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().AfterDropCallBack += UpdateCountTexts;
            AttendingHunterList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().AfterDropCallBack += UpdateCountTexts;
            WaitingViewerList.transform.parent.GetComponent<DocsaListItemDropableLayoutGroup>().AfterDropCallBack += UpdateCountTexts;
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

        public void OpenUI()
        {
            UIObject.SetActive(true);
            isOn = true;
            Time.timeScale = 0;
            UpdateCountTexts();
            CheckEnableDetermineButton();
        }

        public void CloseUI()
        {
            UIObject.SetActive(false);
            isOn = false;
            Time.timeScale = 1;
        }

        public override void Listene(DocsaData data)
        {
            base.Listene(data);
            UpdateCountTexts();
            CheckEnableDetermineButton();
        }

        public void OnDetermineButtonClicked()
        {
            CloseUI();
        }

        public override void RandomDistribute()
        {
            base.RandomDistribute();
            CheckEnableDetermineButton();
        }

        /// <summary>
        /// Call when listItem move
        /// </summary>
        void CheckEnableDetermineButton(DragAndDropableUI ui = null, UnityEngine.EventSystems.PointerEventData eventData = null)
        {
            try
            {
            if (DocsaSakkiManager.instance.AttendingDocsaDict.Count <= DocsaSakkiManager.instance.AttendingDocsaLimit
                    && DocsaSakkiManager.instance.AttendingHunterDict.Count <= DocsaSakkiManager.instance.AttendingHunterLimit
                    && DocsaSakkiManager.instance.WaitingViewerDict.Count <= DocsaSakkiManager.instance.WaitingViewerLimit)
            {
                DetermineButton.buttonVar.interactable = true;
            } else
            {
                DetermineButton.buttonVar.interactable = false;
            }
            } catch {}
        }
    }
}