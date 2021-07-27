using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

using Utility;
using TwitchIRC;

using Michsky.UI.ModernUIPack; 
using TMPro;


public class UIManager : Singleton<UIManager>
{
    public ButtonManagerBasic StartButton;
    public ButtonManagerBasic QuitButton;
    public TMP_InputField ChannelNameInputField;
    public TMP_InputField UserNameInputField;
    public TMP_InputField OAuthInputField;
    public ModalWindowManager OAuthModalWindow;
    public NotificationManager WrongInputNotification;
    public NotificationManager LoadingNotification;
    public NotificationManager PleaseWaitNotification;
    public UIManagerProgressBarLoop LoadingProgressBar;

    public bool Checker = false;


#if UNITY_EDITOR
    public const string DefaultChannelName = "dev_test_dkstlzu";
    public const string DefaultUserName = "dev_test_dkstlzu2";
    public const string DefaultOAuth = "oauth:h2wt7ex2z11qnb1xkape0oych7fiae";
#else
    public const string DefaultChannelName = "uzuhama";
    public const string DefaultUserName = "uzuhama";
#endif
    private bool _chnnelNameInputSelectedFirstTime = true;
    private bool _userNameInputSelectedFirstTime = true;
    private bool _oauthInputSelectedFirstTime = true;
    private bool _oauthAuthenticationConfirmed = false;

    private Task ConnectTask;


    void Awake()
    {
        LoadingNotification.timer = TwitchChat.instance.CheckingConnectivityTimeLimit;
    }

    public void OnChnnelNameInputSelected()
    {
        if (_chnnelNameInputSelectedFirstTime)
        {
            _chnnelNameInputSelectedFirstTime = false;
            ChannelNameInputField.text = DefaultChannelName;
        }
    }

    public void OnUserNameInputSelected()
    {
        if (_userNameInputSelectedFirstTime)
        {
            _userNameInputSelectedFirstTime = false;
            UserNameInputField.text = DefaultUserName;
        }
    }

    public void OnOAuthInputSelected()
    {
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
        _oauthAuthenticationConfirmed = true;
        Application.OpenURL("https://twitchapps.com/tmi/");
    }

    public void OnOAuthModalWindowIgnore()
    {
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
        if (ConnectTask != null && !ConnectTask.IsCompleted)
        {
            PleaseWaitNotification.OpenNotification();
            return;
        }

        TwitchChat.instance.ChannelName = ChannelNameInputField.text;
        TwitchChat.instance.IRCHostName = UserNameInputField.text;
        TwitchChat.instance.OAuthAuthorization = OAuthInputField.text;

        LoadingNotification.OpenNotification();
        LoadingProgressBar.gameObject.SetActive(true);
        StartCoroutine(LoadingText());

        TwitchChat.instance.ConnectCoroutine();
        // ConnectAsync();
        // ConnectTask = TwitchChat.instance.ConnectAsync();       
    }

    private async Task ConnectAsync()
    {
        await TwitchChat.instance.ConnectAsync();
    }

    public void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void CheckEnableStartButton()
    {
        if (ChannelNameInputField.text.Length > 0 && UserNameInputField.text.Length > 0 && OAuthInputField.text.Length > 0)
        {
            StartButton.buttonVar.interactable = true;
        }
    }

    IEnumerator LoadingText()
    {
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
