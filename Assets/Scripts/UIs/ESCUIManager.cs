using UnityEngine;
using UnityEngine.UI;

using Docsa.Character;
using dkstlzu.Utility;
using Michsky.UI.ModernUIPack; 

namespace Docsa
{
    public class ESCUIManager : Singleton<ESCUIManager>
    {
        [SerializeField] private Core _core;
        public ObjectOpenClose UIOpener;
        public WindowManager WM;
        public SliderManager SoundSlider;
        public Toggle DocsaAttendToggle;
        public ViewerAssignUIManager ViewerAssignUI;

        public bool isOn => UIOpener.isOpened;

        void Awake()
        {
            UIOpener.OnOpen += () => UzuHama.AdjustInputAsset();
            UIOpener.OnClose += () => UzuHama.AdjustInputAsset();
        }

        public void OpenUI()
        {
            UIOpener.Open();
        }

        public void CloseUI()
        {
            UIOpener.Close();
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