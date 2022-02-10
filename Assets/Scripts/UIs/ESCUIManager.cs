using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utility;

using Michsky.UI.ModernUIPack; 
using TMPro;

namespace Docsa
{
    public class ESCUI : Singleton<ESCUIManager>{}
    public class ESCUIManager : ListUI
    {
        public GameObject ESCUIGameObject;
        public SliderManager SoundSlider;
        public GameObject DocsaListObject;
        public GameObject TwitchCommandsObject;
        public Toggle DocsaAttendToggle;

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
                    Core.instance.InputAsset.Player.Disable();
                    Core.instance.InputAsset.UI.Enable();
                } else
                {
                    Time.timeScale = 1;
                    Core.instance.InputAsset.Player.Enable();
                    Core.instance.InputAsset.UI.Disable();
                    Reset();
                }
            }
        }

        void Start()
        {
            Core.instance.InputAsset.Player.ESC.performed += OnESCPerformed;
            Core.instance.InputAsset.UI.Cancel.performed += OnESCPerformed;
        }

        void OnESCPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            isOn = !isOn;
            ESCUIGameObject.SetActive(isOn);
        }

        protected override void Reset()
        {
            base.Reset();
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
    }
}