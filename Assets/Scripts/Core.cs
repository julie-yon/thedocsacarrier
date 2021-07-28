using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utility;

namespace Docsa
{
    public class Core : Singleton<Core>
    {
        public string Stage1GameSceneName = "SampleScene";

        void Awake()
        {
            DontDestroyObjects.Add(this);
        }

        public void GameStart()
        {
            // SceneManager.LoadSceneAsync(Core.instance.Stage1GameSceneName);
            SceneManager.LoadScene(Core.instance.Stage1GameSceneName);
        }


    }
}