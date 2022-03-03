using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Docsa.Character;
using Utility;

namespace Docsa
{
    public class Chunk : MonoBehaviour
    {
        public static Chunk Current
        {
            get {return Stage.Current.CurrentChunk;}
        }
        public Stage Stage;
        public int ChunkNumber;

        public static List<DocsaSakki> ActiveDocsaList = new List<DocsaSakki>();
        public static List<Hunter> ActiveHunterList = new List<Hunter>();

        public bool HasNextChunk {get{return ChunkNumber + 1 < Stage.ChunkList.Count ? true : false;}}
        public bool HasPreviousChunk {get{return ChunkNumber >= 1 && Stage.ChunkList.Count >= 2 ? true : false;}}

        void OnEnable()
        {
            var positionSetters = GetComponentsInChildren<CharacterPositionSetter>();

            foreach (var setter in positionSetters)
            {
                if (setter.CharacterType == DocsaPoolType.Docsa)
                {
                    ActiveDocsaList.Add(ObjectPool.GetOrCreate(DocsaPoolType.Docsa).Instantiate(setter.transform.position, setter.transform.rotation).GetComponent<DocsaSakki>());
                } else if (setter.CharacterType == DocsaPoolType.Hunter)
                {
                    ActiveHunterList.Add(ObjectPool.GetOrCreate(DocsaPoolType.Hunter).Instantiate(setter.transform.position, setter.transform.rotation).GetComponent<Hunter>());
                }
            }
        }

        void OnDisable()
        {
            ObjectPool.GetOrCreate(DocsaPoolType.Docsa).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.Hunter).ReturnAll();
            ActiveDocsaList.Clear();
            ActiveHunterList.Clear();
        }

        public bool Clear()
        {
            gameObject.SetActive(false);
            if (HasNextChunk)
            {
                Stage.Current.ChunkList[ChunkNumber+1].gameObject.SetActive(true);
                return true;
            }
            return false;
        }
    }
}