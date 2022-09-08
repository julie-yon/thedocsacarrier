using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using dkstlzu.Utility;

namespace Docsa
{
    public enum StageName
    {
        [StringValue("StartScene")]
        StartScene,
        [StringValue("Cave")]
        Cave,
        [StringValue("Stage1")]
        Stage1,
        [StringValue("Stage2")]
        Stage2,
        [StringValue("Stage3")]
        Stage3,
        [StringValue("Stage4")]
        Stage4,
        Test,
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
            CurrentStage = GameObject.FindObjectOfType<Stage>();
            
            // if (CurrentStage)
            //     ResourceLoader.GetLoader<DocsaSoundNaming>().Load(CurrentStage.StageNumber);
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