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
            StageList.Insert(0, null);
            SceneManager.sceneLoaded += LoadStage;
        }

        void Start()
        {
            // SceneManager.sceneLoaded += LoadStage;
        }

        void LoadStage(Scene scene, LoadSceneMode mode)
        {
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

            stage.GetComponentInChildren<ProCamera2D>().AddCameraTarget(UzuHama.Hama.transform);
        }

        public void ChunkTriggerEnter(Collider2D collider)
        {
            if (collider.gameObject == CurrentStage.CurrentChunk.RightChunkTriggerObject)
            {
                // print("ChunkTriggerEnter Right " + collider.name);
                CurrentStage.GotoRightChunk();
            } else if (collider.gameObject == CurrentStage.CurrentChunk.LeftChunkTriggerObject)
            {
                // print("ChunkTriggerEnter Left " + collider.name);
                CurrentStage.GotoLeftChunk();
            }
        }

        public void ChunkTriggerExit(Collider2D collider)
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