using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Docsa.Character;

namespace Docsa
{
    public class Stage : MonoBehaviour
    {
        public int StageNumber;
        public List<Chunk> ChunkList = new List<Chunk>();
        public float CameraMoveSpeed = 1;
        private int _currentChunkNum = 1;
        private int _targetChunkNum = 0;
        public Chunk CurrentChunk
        {
            get{return ChunkList[_currentChunkNum];}
        }
        
        public Chunk RightChunk
        {
            get{return _currentChunkNum < ChunkList.Count-1 ? ChunkList[_currentChunkNum+1] : null;}
        }
        
        public Chunk LeftChunk
        {
            get{return _currentChunkNum > 1 ? ChunkList[_currentChunkNum-1] : null;}
        }

        public Chunk TargetChunk
        {
            get {return ChunkList[_targetChunkNum];}
        }

        void Awake()
        {
            ChunkList.Insert(0, null);
            
            Chunk[] chunks = GetComponentsInChildren<Chunk>(true);
            foreach(Chunk chunk in chunks)
            {
                ChunkList.Add(chunk);
            }

            if (ChunkList.Count > 0)
            {
                CurrentChunk.RightChunkTriggerObject = ChunkList[1].RightChunkTriggerObject;
                CurrentChunk.LeftChunkTriggerObject = ChunkList[1].LeftChunkTriggerObject;
            }
        }
        
        public bool GotoRightChunk()
        {
            if (RightChunk == null)
            {
                return false;
            }
            _targetChunkNum = _currentChunkNum+1;

            StartCoroutine(CameraMove(true));

            return true;
        }

        public bool GotoLeftChunk()
        {
            if (LeftChunk == null)
            {
                return false;
            }
            _targetChunkNum = _currentChunkNum-1;

            StartCoroutine(CameraMove(false));

            return true;
        }

        private void StopFieldObjects()
        {
            Time.timeScale = 0;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        private void UnStopFieldObjects()
        {
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        IEnumerator CameraMove(bool toRight)
        {
            BeforeCameraMove(toRight);
            // print("Camera Move Start");
            bool moveFinished = false;
            bool camPosisRightSide = false;

            while(!moveFinished)
            {
                yield return new WaitForSecondsRealtime(0.02f);

                Camera.main.transform.Translate(CameraMoveSpeed * (toRight ? Vector3.right : Vector3.left));
                camPosisRightSide = Camera.main.transform.position.x > TargetChunk.DefaultCameraPosition.x;

                if (toRight ? camPosisRightSide : !camPosisRightSide)
                {
                    moveFinished = true;
                    Camera.main.transform.position = TargetChunk.DefaultCameraPosition;
                    // UzuHama.Hama.transform.position = toRight ? TargetChunk.LeftStartPosition.position : TargetChunk.RightStartPosition.position;
                }
            }
            // print("Camera Move Finish");
            AfterCameraMove(toRight);
        }

        void BeforeCameraMove(bool toRight)
        {
            // print(TargetChunk);
            StopFieldObjects();
        }

        void AfterCameraMove(bool toRight)
        {
            CurrentChunk.gameObject.SetActive(false);
            if (toRight)
            {
                UzuHama.Hama.transform.position = RightChunk.LeftStartPosition;
            } else
            {
                UzuHama.Hama.transform.position = LeftChunk.RightStartPosition;
            }

            _currentChunkNum = _targetChunkNum;
            CurrentChunk.gameObject.SetActive(true);
            UnStopFieldObjects();
        }

        public void Dispose()
        {

        }
    }
}