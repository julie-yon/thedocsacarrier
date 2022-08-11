using UnityEngine;
using UnityEngine.UI;

using dkstlzu.Utility;
using Michsky.UI.ModernUIPack; 

namespace Docsa
{
    public class ESCUIManager : Singleton<ESCUIManager>
    {
        [SerializeField] private Core _core;
        public WindowManager WM;
        public SliderManager SoundSlider;
        public Toggle DocsaAttendToggle;
        public ViewerAssignUIManager ViewerAssignUI;

        public bool isOn;

        void Awake()
        {
            _core.InputAsset.Player.ESC.performed += OnESCPerformed;
        }

        void OnESCPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (!isOn) OpenUI();
            ESCManager.instance.AddItem("ESCUI", () => CloseUI(), 10);
        }

        public void OpenUI()
        {
            isOn = true;
            WM.gameObject.SetActive(true);
            _core.AdjustInputAsset();
        }

        public void CloseUI()
        {
            isOn = false;
            WM.gameObject.SetActive(false);
            _core.AdjustInputAsset();
        }

        /// <summary>
        /// On button Reference
        /// </summary>
        public void OnSoundSliderValueChanged()
        {
            AudioListener.volume = SoundSlider.mainSlider.value;
        }

        /// <summary>
        /// On button Reference
        /// </summary>
        public void OnDocsaAttendToggle()
        {
            DocsaSakkiManager.instance.DocsaCanAttend = DocsaAttendToggle.isOn;
        }

        /// <summary>
        /// On button Reference
        /// </summary>
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