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

        public bool isOn;

        void Start()
        {
            Core.instance.InputAsset.Player.ESC.performed += OnESCPerformed;
            Core.instance.InputAsset.UI.Cancel.performed += OnESCPerformed;
        }

        public void OnESCPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (isOn)
            {
                CloseUI();
                Core.instance.InputAsset.Player.Enable();
            } else
            {
                OpenUI();
                Core.instance.InputAsset.Player.Disable();
            }
        }

        public void OpenUI()
        {
            isOn = true;
            ESCUIGameObject.SetActive(true);
            ViewerAssignUI.instance.UpdateData();
            Core.instance.InputAsset.UI.Enable();
        }

        public void CloseUI()
        {
            isOn = false;
            ESCUIGameObject.SetActive(false);
            ViewerAssignUI.instance.UpdateData();
            if (Core.instance.ReadyToPlay)
                Core.instance.InputAsset.UI.Disable();
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