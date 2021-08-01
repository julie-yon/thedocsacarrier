using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Utility;
using TwitchIRC;
using Docsa.Character;

using Michsky.UI.ModernUIPack; 
using TMPro;


namespace Docsa
{
    public class ESCUIManager : Singleton<ESCUIManager>
    {
        public GameObject ESCUIGameObject;
        public SliderManager SoundSlider;
        public ButtonManagerBasic DocsaListButton;
        public GameObject DocsaListObject;
        public GameObject DocsaListCancelButton;
        public VerticalLayoutGroup AttendingDocsaList;
        public VerticalLayoutGroup AttendingHunterList;
        public VerticalLayoutGroup WaitingViewerList;
        public GameObject ListItemPrefab;
        public Toggle DocsaAttendToggle;
        Dictionary<string, Button> _listItemsRemoveActionDict = new Dictionary<string, Button>();

        bool _isOn;
        bool isOn
        {
            get {return _isOn;}
            set 
            {
                _isOn = value;
                if (value)
                {
                    Time.timeScale = 0;
                    Core.instance.UserInputEnable = false;
                } else
                {
                    Time.timeScale = 1;
                    Core.instance.UserInputEnable = true;
                    Reset();
                }
            }
        }

        void Awake()
        {
            DontDestroyObjects.Add(this);
            DontDestroyObjects.Add(ESCUIGameObject);
            DontDestroyObjects.Add(FindObjectOfType<UnityEngine.EventSystems.StandaloneInputModule>());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isOn = !isOn;
                ESCUIGameObject.SetActive(isOn);
            }
        }

        public void Reset()
        {
            DocsaListObject.SetActive(false);
            DocsaListCancelButton.SetActive(false);
        }

        public void OnSoundSliderValueChanged()
        {
            AudioListener.volume = SoundSlider.mainSlider.value;
        }

        public void OnDocsaListClicked()
        {
            DocsaListObject.SetActive(true);
            DocsaListCancelButton.SetActive(true);
        }

        public void OnDocsaListCancel()
        {
            DocsaListObject.SetActive(false);
            DocsaListCancelButton.SetActive(false);
        }

        public void OnDocsaAttendToggle()
        {
            DocsaSakkiManager.instance.DocsaCanAttend = DocsaAttendToggle.isOn;
        }

        public void AddAttendingDocsa(DocsaSakki docsa)
        {
            GameObject item = Instantiate(ListItemPrefab, AttendingDocsaList.transform);
            item.GetComponentInChildren<Text>().text = docsa.Author;
            UnityEngine.Events.UnityAction action = () => 
            {
                DocsaSakkiManager.instance.Kick(docsa.Author);
                _listItemsRemoveActionDict.Remove(docsa.Author);
                Destroy(item);
            };
            item.GetComponentInChildren<Button>().onClick.AddListener(action);
            _listItemsRemoveActionDict.Add(docsa.Author, item.GetComponentInChildren<Button>());
        }

        public void AddAttendingHunter(Hunter hunter)
        {
            GameObject item = Instantiate(ListItemPrefab, AttendingHunterList.transform);
            item.GetComponentInChildren<Text>().text = hunter.Author;
            UnityEngine.Events.UnityAction action = () => 
            {
                DocsaSakkiManager.instance.Kick(hunter.Author);
                _listItemsRemoveActionDict.Remove(hunter.Author);
                Destroy(item);
            };
            item.GetComponentInChildren<Button>().onClick.AddListener(action);
            _listItemsRemoveActionDict.Add(hunter.Author, item.GetComponentInChildren<Button>());
        }

        public void AddWaitingViewer(WaitingData viewer)
        {
            GameObject item = Instantiate(ListItemPrefab, WaitingViewerList.transform);
            item.GetComponentInChildren<TextMeshProUGUI>().text = viewer.Author;
            UnityEngine.Events.UnityAction action = () => 
            {
                DocsaSakkiManager.instance.Kick(viewer.Author);
                _listItemsRemoveActionDict.Remove(viewer.Author);
                Destroy(item);
            };
            item.GetComponentInChildren<Button>().onClick.AddListener(action);
            _listItemsRemoveActionDict.Add(viewer.Author, item.GetComponentInChildren<Button>());

        }

        public void RemoveAttendingDocsa(DocsaSakki docsa)
        {
            Button buttonTemp;
            if (_listItemsRemoveActionDict.TryGetValue(docsa.Author, out buttonTemp))
            {
                buttonTemp.onClick.Invoke();
                _listItemsRemoveActionDict.Remove(docsa.Author);
            } else
            {
                return;
            }
        }

        public void RemoveAttendingHunter(Hunter hunter)
        {
            Button buttonTemp;
            if (_listItemsRemoveActionDict.TryGetValue(hunter.Author, out buttonTemp))
            {
                buttonTemp.onClick.Invoke();
                _listItemsRemoveActionDict.Remove(hunter.Author);
            } else
            {
                return;
            }
        }

        public void RemoveWaitingViewer(WaitingData viewer)
        {
            Button buttonTemp;
            if (_listItemsRemoveActionDict.TryGetValue(viewer.Author, out buttonTemp))
            {
                buttonTemp.onClick.Invoke();
                _listItemsRemoveActionDict.Remove(viewer.Author);
            } else
            {
                return;
            }
        }
    }
}