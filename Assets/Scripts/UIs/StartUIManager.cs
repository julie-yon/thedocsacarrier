using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

using dkstlzu.Utility;
using TwitchIRC;

using Michsky.UI.ModernUIPack; 
using TMPro;


namespace Docsa
{
    public class StartUIManager : Singleton<StartUIManager>
    {
        public ButtonManagerBasic StartButton;
        public TMP_InputField ChannelNameInputField;
        public TMP_InputField UserNameInputField;
        public TMP_InputField OAuthInputField;
        public ModalWindowManager OAuthModalWindow;
        public NotificationManager WrongInputNotification;
        public NotificationManager LoadingNotification;
        public NotificationManager PleaseWaitNotification;
        public UIManagerProgressBarLoop LoadingProgressBar;


    #if UNITY_EDITOR
        public const string DefaultChannelName = "dev_test_dkstlzu";
        public const string DefaultUserName = "dev_test_dkstlzu2";
        public const string DefaultOAuth = "oauth:o4ng9no20ev8jsx1ji0iur0vjxziep";
    #else
        public const string DefaultChannelName = "uzuhama";
        public const string DefaultUserName = "uzuhama";
        public const string DefaultOAuth = "oauth:o4ng9no20ev8jsx1ji0iur0vjxziep";
    #endif
        private bool _chnnelNameInputSelectedFirstTime = true;
        private bool _userNameInputSelectedFirstTime = true;
        private bool _oauthInputSelectedFirstTime = true;
        private bool _oauthAuthenticationConfirmed = false;

        private Task ConnectTask;

        // SoundEventArgs
        public AudioClip OnInputSelectedAudioClip;
        public SoundArgs OnInputSelectedSoundArg;
        public AudioClip OnButtonClickedAudioClip;
        public SoundArgs OnButtonClickedSoundArg;

        void Awake()
        {
            LoadingNotification.timer = TwitchChat.instance.CheckingConnectivityTimeLimit;
        }

        void Start()
        {
        }

        public void OnChnnelNameInputSelected()
        {
            SoundManager.instance.Play(OnInputSelectedAudioClip, OnInputSelectedSoundArg);
            if (_chnnelNameInputSelectedFirstTime)
            {
                _chnnelNameInputSelectedFirstTime = false;
                ChannelNameInputField.text = DefaultChannelName;
            }
        }

        public void OnUserNameInputSelected()
        {
            SoundManager.instance.Play(OnInputSelectedAudioClip, OnInputSelectedSoundArg);
            if (_userNameInputSelectedFirstTime)
            {
                _userNameInputSelectedFirstTime = false;
                UserNameInputField.text = DefaultUserName;
            }
        }

        public void OnOAuthInputSelected()
        {
            SoundManager.instance.Play(OnInputSelectedAudioClip, OnInputSelectedSoundArg);
            if (_oauthInputSelectedFirstTime)
            {
                _oauthInputSelectedFirstTime = false;
                OAuthInputField.text = DefaultOAuth;
            }

            if (!_oauthAuthenticationConfirmed)
            {
                OAuthModalWindow.OpenWindow();
            }
        }

        public void OnOAuthModalWindowConfirm()
        {
            SoundManager.instance.Play(OnButtonClickedAudioClip, OnButtonClickedSoundArg);
            _oauthAuthenticationConfirmed = true;
            Application.OpenURL("https://twitchapps.com/tmi/");
        }

        public void OnOAuthModalWindowIgnore()
        {
            SoundManager.instance.Play(OnButtonClickedAudioClip, OnButtonClickedSoundArg);
            _oauthAuthenticationConfirmed = true;
        }

        public void OnStartButtonHovered()
        {
            if (ChannelNameInputField.text.Length <= 0 || UserNameInputField.text.Length <= 0 || OAuthInputField.text.Length <= 0)
            {
                WrongInputNotification.OpenNotification();
            }
        }

        public void OnStartButtonClicked()
        {
            SoundManager.instance.Play(OnButtonClickedAudioClip, OnButtonClickedSoundArg);
            if (ConnectTask != null && !ConnectTask.IsCompleted)
            {
                PleaseWaitNotification.OpenNotification();
                return;
            }

            TwitchChat.instance.ChannelName = ChannelNameInputField.text;
            TwitchChat.instance.IRCHostName = UserNameInputField.text;
            TwitchChat.instance.OAuthAuthorization = OAuthInputField.text;

            LoadingNotification.OpenNotification();
            StartCoroutine(LoadingText());


            TwitchChat.instance.ConnectAsync();
        }

        public void OnQuitButtonClicked()
        {
            SoundManager.instance.Play(OnButtonClickedAudioClip, OnButtonClickedSoundArg);
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
            Application.Quit();
    #endif
        }

        // Call when input values are changed
        public void CheckEnableStartButton()
        {
            if (ChannelNameInputField.text.Length > 0 && UserNameInputField.text.Length > 0 && OAuthInputField.text.Length > 0)
            {
                StartButton.buttonVar.interactable = true;
            }
        }

        IEnumerator LoadingText()
        {
            LoadingProgressBar.gameObject.SetActive(true);
            string[] loadingTexts = new string[]{"Waiting.", "Waiting..", "Waiting...", "Waiting...."};
            int index = 0;

            float oneTickTime = 0.8f;
            float timer = oneTickTime;
            LoadingNotification.description = loadingTexts[0];

            while (timer < LoadingNotification.timer)
            {
                LoadingNotification.description = loadingTexts[index];
                index++;
                index = index % loadingTexts.Length;

                timer += oneTickTime;
                yield return new WaitForSeconds(oneTickTime);
            }

            LoadingProgressBar.gameObject.SetActive(false);
        }
    }
}