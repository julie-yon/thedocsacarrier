using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Docsa.Character;

using Utility;

namespace Docsa
{
    public class Stage : MonoBehaviour
    {
        public static Stage Current
        {
            get {return StageManager.instance.CurrentStage;}
        }

        public int StageNumber;
        public List<Chunk> ChunkList = new List<Chunk>();
        private int _currentChunkNum = -1;

        public Chunk CurrentChunk {get{return _currentChunkNum >= 0 && ChunkList.Count > 0 ? ChunkList[_currentChunkNum] : null;}}
        public Chunk RightChunk {get{return _currentChunkNum + 1 < ChunkList.Count ? ChunkList[_currentChunkNum + 1] : null;}}
        public Chunk LeftChunk {get{return _currentChunkNum >= 1 && ChunkList.Count >= 2 ? ChunkList[_currentChunkNum-1] : null;}}

        public Chunk MakeChunk(int chunkNumber)
        {
            ObjectPool.ReturnAllPools();

            if (ChunkList[chunkNumber] != null)
            {
                CurrentChunk.gameObject.SetActive(false);
                ChunkList[chunkNumber].gameObject.SetActive(true);
                _currentChunkNum = chunkNumber;
                return ChunkList[chunkNumber];
            }

            return null;
        }

        public void AddChunk(Chunk chunk)
        {
            ChunkList.Add(chunk);
        }
    }
}