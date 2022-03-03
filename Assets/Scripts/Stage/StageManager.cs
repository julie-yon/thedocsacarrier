using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Docsa.Character;
using Utility;

using Com.LuisPedroFonseca.ProCamera2D;

namespace Docsa
{
    public class StageManager : Singleton<StageManager>
    {
        public List<string> StageSceneNameList = new List<string>();
        public Stage CurrentStage;

        void Awake()
        {
            SceneManager.sceneLoaded += LoadScene;
        }

        void LoadScene(Scene scene, LoadSceneMode mode)
        {
            CurrentStage = GameObject.FindObjectOfType<Stage>();
            SoundManager.instance.LoadSounds(CurrentStage.StageNumber);
        }

        public void GotoStage(int stageNum)
        {
            SceneManager.LoadScene(StageSceneNameList[stageNum]);
        }

        public bool Clear()
        {
            if (CurrentStage.Clear()) return false;
            else
            {
                Core.instance.GameClear();
                return true;
            }
        }
    }
}