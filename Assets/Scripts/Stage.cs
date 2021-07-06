using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Docsa.Character;

namespace Docsa
{
    public class Stage : MonoBehaviour
    {
        public List<Chunk> ChunkList = new List<Chunk>();
        public Vector3 StartCameraPosition;
        public float CameraMoveSpeed = 1;
        public GameObject ChunkBoundary;
        private int _maxChunkNum;
        private int _currentChunkNum = 1;
        private int _targetChunkNum;
        private int _previousChunkNum;
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

        void Awake()
        {
            ChunkList.Insert(0, null);
            
            _maxChunkNum = ChunkList.Count;

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

            MoveCameraToTargetChunk();

            return true;
        }

        public bool GotoLeftChunk()
        {
            if (LeftChunk == null)
            {
                return false;
            }
            _targetChunkNum = _currentChunkNum-1;

            MoveCameraToTargetChunk();

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

        private void MoveCameraToTargetChunk()
        {
            StartCoroutine(CameraMove(_targetChunkNum > _currentChunkNum ? true : false));
        }

        IEnumerator CameraMove(bool toRight)
        {
            print(ChunkList[_targetChunkNum]);
            ChunkList[_targetChunkNum].gameObject.SetActive(true);
            StopFieldObjects();

            bool moveFinished = false;
            bool camPosisRightSide = false;

            while(!moveFinished)
            {
                yield return new WaitForSecondsRealtime(0.02f);

                Camera.main.transform.Translate(CameraMoveSpeed * (toRight ? Vector3.right : Vector3.left));
                camPosisRightSide = Camera.main.transform.position.x > ChunkList[_targetChunkNum].DefaultCameraPosition.x;

                if (toRight ? camPosisRightSide : !camPosisRightSide)
                {
                    moveFinished = true;
                    Camera.main.transform.position = ChunkList[_targetChunkNum].DefaultCameraPosition;
                    UzuHama.Hama.transform.position = toRight ? ChunkList[_targetChunkNum].LeftStartPosition.position : ChunkList[_targetChunkNum].RightStartPosition.position;
                }
            }

            _previousChunkNum = _currentChunkNum++;
            CurrentChunk.RightChunkTriggerObject = CurrentChunk.RightChunkTriggerObject;
            CurrentChunk.LeftChunkTriggerObject = CurrentChunk.LeftChunkTriggerObject;
            UnStopFieldObjects();
            ChunkList[_previousChunkNum].gameObject.SetActive(false);
        }

        public void Dispose()
        {

        }
    }
}