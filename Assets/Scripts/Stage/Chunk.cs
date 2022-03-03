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
        public static List<DocsaSakki> ActiveDocsaList = new List<DocsaSakki>();
        public static List<Hunter> ActiveHunterList = new List<Hunter>();

        public int ChunkNumber;

        public int DocsaCount {get {return ActiveDocsaList.Count;}}
        public int HunterCount {get {return ActiveHunterList.Count;}}


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
    }
}