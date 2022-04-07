using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utility;

namespace Docsa
{
    public enum StageName
    {
        StartScene,
        Cave,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
    }
    public class StageManager : Singleton<StageManager>
    {
        public List<StageName> StageSceneNameList = new List<StageName>();
        public Stage CurrentStage;

        void Awake()
        {
            SceneManager.sceneLoaded += LoadScene;
        }

        void LoadScene(Scene scene, LoadSceneMode mode)
        {
            if (CurrentStage)
                ResourceLoader.GetLoader<DocsaSoundNaming>().Load(CurrentStage.StageNumber);
        }

        public void GotoStage(int stageNum)
        {
            GotoStage((StageName)stageNum);
        }

        public void GotoStage(StageName stageName)
        {
            if ((int)stageName >= StageSceneNameList.Count)
            {
                Core.instance.GameClear();
            } else
            {
                SceneManager.LoadScene(stageName.ToString());
            }
        }

        public void Clear()
        {
            if (CurrentStage.Clear()) GotoStage(CurrentStage.StageNumber+1);
        }
    }
}