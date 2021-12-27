using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utility;

namespace Docsa
{
    public class Core : Singleton<Core>
    {
        public string Stage1GameSceneName = "Stage1";
        public string UzuhamaTwitchNickName = "우주하마";
        public HamaInput InputAsset;
        public PerksManager PerksManager;

        void Awake()
        {
            InputAsset = new HamaInput();
            InputAsset.Player.Enable();
            InputAsset.UI.Disable();
        }

        public void GameStart()
        {
            // SceneManager.LoadSceneAsync(Core.instance.Stage1GameSceneName);
            SceneManager.LoadScene(Core.instance.Stage1GameSceneName);
        }
    }
}