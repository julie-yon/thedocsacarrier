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
        public GameObject DocsaPositionObject;
        public GameObject HunterPositionObject;

        public int DocsaCount {get {return ActiveDocsaList.Count;}}
        public int HunterCount {get {return ActiveHunterList.Count;}}


        void OnEnable()
        {
            List<Transform> _docsaPosList = new List<Transform>();
            List<Transform> _hunterPosList = new List<Transform>();
            GameObject objTemp;
            if (DocsaPositionObject != null)
            {
                _docsaPosList.AddRange(DocsaPositionObject.GetComponentsInChildren<Transform>());
                // Removing "DocsaPos" itself
                _docsaPosList.RemoveAt(0);
                foreach (Transform docsa in _docsaPosList)
                {
                    objTemp = ObjectPool.GetOrCreate(DocsaPoolType.Docsa).Instantiate(docsa.position, docsa.rotation);
                    ActiveDocsaList.Add(objTemp.GetComponent<DocsaSakki>());
                }
            }

            if (HunterPositionObject != null)
            {
                _hunterPosList.AddRange(HunterPositionObject.GetComponentsInChildren<Transform>());
                // Removing "HunterPos" itself
                _hunterPosList.RemoveAt(0);
                foreach (Transform docsa in _docsaPosList)
                {
                    objTemp = ObjectPool.GetOrCreate(DocsaPoolType.Hunter).Instantiate(docsa.position, docsa.rotation);
                    ActiveHunterList.Add(objTemp.GetComponent<Hunter>());
                }
            }
        }

        void OnDisable()
        {
            ActiveDocsaList.Clear();
            ActiveHunterList.Clear();
        }
    }
}