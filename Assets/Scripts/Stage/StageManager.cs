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
        public List<GameObject> StageList = new List<GameObject>();
        public Stage CurrentStage;

        void Awake()
        {
            SceneManager.sceneLoaded += LoadStage;
        }

        void LoadStage(Scene scene, LoadSceneMode mode)
        {
            if (CurrentStage)
            {
                return;
            }
            Stage stage = null;
            switch (scene.name)
            {
                case "Cave":
                    stage = MakeStage(1);
                break;
                case "Stage1":
                    stage = MakeStage(2);
                break;
                case "Stage2":
                    stage = MakeStage(3);
                break;
                case "Stage3":
                    stage = MakeStage(4);
                break;
                case "Stage4":
                    stage = MakeStage(5);
                break;
            }

            if (stage == null)
            {
                LogWriter.DebugWrite("Wrong loading scene");
                print("Wrong loading scene");
            }

            CurrentStage = stage;
            // Because each stage prefab has camera respectively, after make new stage you should assign UzuHama to Procamera again.
            ProCamera2D.Instance.AddCameraTarget(UzuHama.Hama.transform);

            AfterLoadStage(CurrentStage.StageNumber);
        }

        void AfterLoadStage(int stageNumber)
        {
            
        }

        public Stage MakeStage(int stageNum)
        {
            GameObject stageObj = Instantiate(StageList[stageNum]);

            if (CurrentStage != null)
            {
                Destroy(CurrentStage.gameObject);
            }
            CurrentStage = stageObj.GetComponent<Stage>();

            GameObject MapHierarchy = GameObject.Find("====Map====");
            stageObj.transform.SetSiblingIndex(MapHierarchy.transform.GetSiblingIndex() + 1);

            return CurrentStage;
        }
    }
}