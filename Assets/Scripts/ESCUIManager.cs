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
        public GameObject DocsaListObject;
        public VerticalLayoutGroup AttendingDocsaList;
        public VerticalLayoutGroup AttendingHunterList;
        public VerticalLayoutGroup WaitingViewerList;
        public GameObject TwitchCommandsObject;
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
            TwitchCommandsObject.SetActive(false);
        }

        public void OnSoundSliderValueChanged()
        {
            AudioListener.volume = SoundSlider.mainSlider.value;
        }

        public void OnTwitchCommandsButtonClicked()
        {
            TwitchCommandsObject.SetActive(true);
        }
        public void OnTwitchCommandsExit()
        {
            TwitchCommandsObject.SetActive(false);
        }

        public void OnDocsaListClicked()
        {
            DocsaListObject.SetActive(true);
        }

        public void OnDocsaListExit()
        {
            DocsaListObject.SetActive(false);
        }

        public void OnDocsaAttendToggle()
        {
            DocsaSakkiManager.instance.DocsaCanAttend = DocsaAttendToggle.isOn;
        }

        public void OnExitButtonClicked()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void RandomDistribute()
        {
            DocsaData[] datas = DocsaSakkiManager.instance.GetRandomWaitingDocsaDatas(StageManager.instance.CurrentStage.CurrentChunk.DocsaNumber);
            foreach (DocsaData data in datas)
            {
                MoveDocsaDataCardTo(data, DocsaData.DocsaState.Docsa);
                DocsaSakkiManager.instance.MoveDocsaDataTo(data, DocsaData.DocsaState.Docsa);
            }

            datas = DocsaSakkiManager.instance.GetRandomWaitingDocsaDatas(StageManager.instance.CurrentStage.CurrentChunk.HunterNumber);
            foreach (DocsaData data in datas)
            {
                MoveDocsaDataCardTo(data, DocsaData.DocsaState.Hunter);
                DocsaSakkiManager.instance.MoveDocsaDataTo(data, DocsaData.DocsaState.Hunter);
            }
            
        }

        public void AddAttendingDocsa(DocsaData docsa)
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

        public void AddAttendingHunter(DocsaData hunter)
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

        public void AddWaitingViewer(DocsaData viewer)
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

        public void RemoveAttendingDocsa(DocsaData docsa)
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

        public void RemoveAttendingHunter(DocsaData hunter)
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

        public void RemoveWaitingViewer(DocsaData viewer)
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

        public void MoveDocsaDataCardTo(string author, DocsaData.DocsaState to)
        {
            switch (to)
            {
                case DocsaData.DocsaState.Docsa :
                _listItemsRemoveActionDict[author].transform.parent.SetParent(AttendingDocsaList.transform);
                break;
                case DocsaData.DocsaState.Hunter :
                _listItemsRemoveActionDict[author].transform.parent.SetParent(AttendingHunterList.transform);
                break;
                case DocsaData.DocsaState.Waiting :
                _listItemsRemoveActionDict[author].transform.parent.SetParent(WaitingViewerList.transform);
                break;
            }        }

        public void MoveDocsaDataCardTo(DocsaData docsaData, DocsaData.DocsaState to)
        {
            MoveDocsaDataCardTo(docsaData.Author, to);
        }
    }
}