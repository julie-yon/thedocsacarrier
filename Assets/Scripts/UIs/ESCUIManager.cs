using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Utility;
using Michsky.UI.ModernUIPack; 

namespace Docsa
{
    public class ESCUIManager : Singleton<ESCUIManager>
    {
        public GameObject ESCUIGameObject;
        public SliderManager SoundSlider;
        public Toggle DocsaAttendToggle;

        bool isOn;

        void Start()
        {
            Core.instance.InputAsset.Player.ESC.performed += OnESCPerformed;
            Core.instance.InputAsset.UI.Cancel.performed += OnESCPerformed;
        }

        public void OnESCPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            isOn = !isOn;
            if (isOn)
            {
                Time.timeScale = 0;
                Core.instance.InputAsset.Player.Disable();
                Core.instance.InputAsset.UI.Enable();
            } else
            {
                Time.timeScale = 1;
                Core.instance.InputAsset.Player.Enable();
                Core.instance.InputAsset.UI.Disable();
            }
            ESCUIGameObject.SetActive(isOn);
            ViewerAssignUI.instance.UpdateData();
        }

        public void OnSoundSliderValueChanged()
        {
            AudioListener.volume = SoundSlider.mainSlider.value;
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