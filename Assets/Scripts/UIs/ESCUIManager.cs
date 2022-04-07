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
        public WindowManager WM;
        public SliderManager SoundSlider;
        public Toggle DocsaAttendToggle;
        public ViewerAssignUIManager ViewerAssignUI;

        public bool isOn;

        void Start()
        {
            Core.instance.InputAsset.Player.ESC.performed += OnESCPerformed;
            Core.instance.InputAsset.UI.Cancel.performed += OnESCPerformed;
        }

        void OnESCPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (isOn)
            {
                CloseUI();
            } else
            {
                OpenUI();
            }
        }

        public void OpenUI()
        {
            isOn = true;
            WM.gameObject.SetActive(true);
            Core.instance.AdjustInputAsset();
        }

        public void CloseUI()
        {
            isOn = false;
            WM.gameObject.SetActive(false);
            Core.instance.AdjustInputAsset();
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