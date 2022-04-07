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
            get 
            {
                if (Stage.Current == null) return null;

                return Stage.Current.CurrentChunk;
            }
        }
        public Stage Stage;
        public int ChunkNumber;
        public Transform StartPosition;
        public bool ReadyToPlay = false;


        public static List<DocsaSakki> ActiveDocsaList = new List<DocsaSakki>();
        public static List<Hunter> ActiveHunterList = new List<Hunter>();

        public PerkData PerkData;


        public ClearCondition ClearCondition;

        void Awake()
        {
            int chunkID = Stage.StageNumber * 10 + ChunkNumber;

            ClearCondition.CheckIfTrue CheckMethod = null;

            switch(chunkID)
            {
                // ForestChunk1
                case 10 :
                    CheckMethod = (chunk) => 
                    {
                        return true;
                    };
                break;
                // ForestChunk2
                case 11 :
                    CheckMethod = (chunk) => 
                    {
                        return UzuHama.Hama.RescuedDocsaNumger > 0;
                    };
                break;
                // ForestChunk3
                case 12 :
                    CheckMethod = (chunk) => 
                    {
                        return UzuHama.Hama.RescuedDocsaNumger > 0;
                    };
                break;
                // ForestChunk4
                case 13 :
                    CheckMethod = (chunk) => 
                    {
                        return UzuHama.Hama.RescuedDocsaNumger > 1;
                    };
                break;
                // VolcanoChunk1
                case 20 :
                    CheckMethod = (chunk) => 
                    {
                        return true;
                    };
                break;
                // VolcanoChunk2
                case 21 :
                    CheckMethod = (chunk) => 
                    {
                        return true;
                    };
                break;
                // VolcanoChunk3
                case 22 :
                    CheckMethod = (chunk) => 
                    {
                        return true;
                    };
                break;
                // VolcanoChunk4
                case 23 :
                    CheckMethod = (chunk) => 
                    {
                        return true;
                    };
                break;
            }

            ClearCondition = new ClearCondition(CheckMethod);
        }

        void OnEnable()
        {
            PerkManager.instance.Data = PerkData;
        }

        void OnDisable()
        {
            ObjectPool.GetOrCreate(DocsaPoolType.Docsa).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.Hunter).ReturnAll();
            ActiveDocsaList.Clear();
            ActiveHunterList.Clear();
        }

        public void InitCharacters()
        {
            var positionSetters = GetComponentsInChildren<CharacterPositionSetter>();

            GameObject goTemp;
            ViewerCharacter characterTemp;

            foreach (var setter in positionSetters)
            {
                if (setter.CharacterType == DocsaPoolType.Docsa)
                {
                    print("isDocsa");
                    goTemp = ObjectPool.GetOrCreate(DocsaPoolType.Docsa).Instantiate(setter.transform.position, setter.transform.rotation);
                    characterTemp = goTemp.GetComponent<DocsaSakki>();
                    characterTemp.Flip = setter.Flip;
                    ActiveDocsaList.Add((DocsaSakki)characterTemp);
                } else if (setter.CharacterType == DocsaPoolType.Hunter)
                {
                    print("isHunter");
                    goTemp = ObjectPool.GetOrCreate(DocsaPoolType.Hunter).Instantiate(setter.transform.position, setter.transform.rotation);
                    characterTemp = goTemp.GetComponent<Hunter>();
                    characterTemp.Flip = setter.Flip;
                    ActiveHunterList.Add((Hunter)characterTemp);
                }
            }
            ReadyToPlay = true;
        }

        public bool Clear()
        {
            if (ClearCondition.Fulfilled(this))
            {
                return true;
            } else return false;
        }
    }
}