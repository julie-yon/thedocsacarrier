using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utility;

using Michsky.UI.ModernUIPack; 
using TMPro;

namespace Docsa
{
    public class ESCUIManager : Singleton<ESCUIManager>
    {
        public GameObject ESCUIGameObject;
        public SliderManager SoundSlider;
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

        void Reset()
        {
            ViewerAssignUI.instance.CloseUI();
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
            ViewerAssignUI.instance.OpenUI();
        }

        public void OnDocsaListExit()
        {
            ViewerAssignUI.instance.CloseUI();
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