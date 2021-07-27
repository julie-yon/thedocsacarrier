using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utility;

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

        public void ChunkTriggerEnter(Collider2D collider)
        {
            if (collider.gameObject == CurrentStage.CurrentChunk.RightChunkTriggerObject)
            {
                print("ChunkTriggerEnter Right " + collider.name);
                CurrentStage.GotoRightChunk();
            } else 
            {
                print("ChunkTriggerEnter Left " + collider.name);
                CurrentStage.GotoLeftChunk();
            }
        }

        public void ChunkTriggerExit(Collider2D collider)
        {

        } 

        public Stage MakeStage(int stageNum)
        {
            GameObject obj = Instantiate(StageList[stageNum]);

            Destroy(CurrentStage);
            CurrentStage = obj.GetComponent<Stage>();

            Camera.main.transform.position = obj.GetComponent<Stage>().ChunkList[1].DefaultCameraPosition;

            return obj.GetComponent<Stage>();
        }
        
        void LoadStage(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "SampleScene":
                    MakeStage(1);
                break;
                case "Stage1":
                    MakeStage(1);
                break;
                case "Stage2":
                    MakeStage(2);
                break;
                case "Stage3":
                    MakeStage(3);
                break;
                case "Stage4":
                    MakeStage(4);
                break;
            } 
        }
    }
}