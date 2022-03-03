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

        public StageManager Manager
        {
            get {return StageManager.instance;}
        }
        public int StageNumber;

        public List<Chunk> ChunkList = new List<Chunk>();
        public Chunk CurrentChunk;
        public bool HasNextStage {get{return StageNumber + 1 < Manager.StageSceneNameList.Count ? true : false;}}
        public bool HasPreviousStage {get{return StageNumber >= 1 ? true : false;}}

        public void AddChunk(Chunk chunk)
        {
            chunk.Stage = this;
            ChunkList.Add(chunk);
        }

        public bool Clear()
        {
            if (CurrentChunk.Clear()) return true;
            else 
            {
                if (HasNextStage)
                {
                    Manager.GotoStage(Manager.CurrentStage.StageNumber+1);
                    return true;
                } else
                {
                    return false;
                }
            }
        }
    }
}