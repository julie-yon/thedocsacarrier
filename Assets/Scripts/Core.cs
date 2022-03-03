using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utility;

namespace Docsa
{
    public class Core : Singleton<Core>
    {
        public const string CaveSceneName = "Cave";
        public const string Stage1GameSceneName = "Stage1";
        public const string Stage2GameSceneName = "Stage2";
        public const string Stage3GameSceneName = "Stage3";
        public const string Stage4GameSceneName = "Stage4";
        public string UzuhamaTwitchNickName = "우주하마";
        public HamaInput InputAsset;

        void Awake()
        {
            InputAsset = new HamaInput();
            InputAsset.Player.Enable();
            InputAsset.UI.Disable();
        }

        public void GotoCave()
        {
            SceneManager.LoadScene(CaveSceneName);
        }

        public void GameStart()
        {
            SceneManager.LoadScene(Stage1GameSceneName);
        }

    }
}