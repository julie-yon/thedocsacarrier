using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Docsa.Character;

using Utility;

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

        public Chunk MakeChunk(int chunkNumber)
        {
            Chunk chunk = null;

            ObjectPool.GetOrCreate(DocsaPoolType.Docsa).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.Chim).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.Hunter).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.Net).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.Weapon).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.VolcanicAsh).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.StarRain).ReturnAll();

            if (CurrentChunk != null)
            {
                CurrentChunk.gameObject.SetActive(false);
            }

            ChunkList[chunkNumber].gameObject.SetActive(true);
            _currentChunkNum = chunkNumber;

            return chunk;
        }
    }
}