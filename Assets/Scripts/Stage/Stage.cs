using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa
{
    public class Stage : MonoBehaviour
    {
        public static Stage Current
        {
            get 
            {
                if (StageManager.instance == null) return null;
                
                return StageManager.instance.CurrentStage;
            }
        }

        public StageManager Manager
        {
            get {return StageManager.instance;}
        }
        public StageName StageName;
        public int StageNumber
        {
            get{return (int)StageName;}
            set{StageName = (StageName)value;}
        }

        public List<Chunk> ChunkList = new List<Chunk>();
        public Chunk CurrentChunk;

        public bool HasNextChunk {get{return CurrentChunk.ChunkNumber + 1 < ChunkList.Count ? true : false;}}
        public bool HasPreviousChunk {get{return CurrentChunk.ChunkNumber >= 1 && ChunkList.Count >= 2 ? true : false;}}

        public bool HasNextStage {get{return StageNumber + 1 < Manager.StageSceneNameList.Count ? true : false;}}
        public bool HasPreviousStage {get{return StageNumber >= 1 ? true : false;}}

        public bool Clear()
        {
            print(gameObject.name);
            if (CurrentChunk.Clear())
            {
                if (HasNextChunk)
                {
                    CurrentChunk.gameObject.SetActive(false);
                    CurrentChunk = ChunkList[CurrentChunk.ChunkNumber+1];
                    CurrentChunk.gameObject.SetActive(true);
                    Docsa.Character.UzuHama.Hama.transform.position = CurrentChunk.StartPosition.position;
                    return false;
                } else
                {
                    return true;
                }
            } else
            {
                return false;
            } 
        }
    }
}