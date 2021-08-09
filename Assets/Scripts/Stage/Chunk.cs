using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Docsa.Character;
using Utility;

namespace Docsa
{
    public class Chunk : MonoBehaviour
    {
        public static List<DocsaSakki> ActiveDocsaList = new List<DocsaSakki>();
        public static List<Hunter> ActiveHunterList = new List<Hunter>();
        public GameObject RightChunkTriggerObject;
        public GameObject LeftChunkTriggerObject;
        public Transform RightStartPosition;
        public Transform LeftStartPosition;
        [SerializeField] Transform _defaultCameraPosition;
        public Vector3 DefaultCameraPosition
        {
            get {return _defaultCameraPosition.position;}
        }

        public int DocsaNumber
        {
            get {return _docsaPosList.Count;}
        }

        public int HunterNumber
        {
            get {return _hunterPosList.Count;}
        }

        private List<Transform> _docsaPosList;
        private List<Transform> _hunterPosList;
        void Awake()
        {
            _docsaPosList = new List<Transform>();
            _hunterPosList = new List<Transform>();
        }

        void OnEnable()
        {
            GameObject objTemp;
            if (_docsaPosList.Count == 0)
            {
                _docsaPosList.AddRange(transform.Find("DocsaPos").GetComponentsInChildren<Transform>());
                _docsaPosList.RemoveAt(0);
            }

            foreach (Transform docsa in _docsaPosList)
            {
                objTemp = ObjectPool.SPoolDict[PoolType.Docsa].Instantiate(docsa.position, docsa.rotation);
                ActiveDocsaList.Add(objTemp.GetComponent<DocsaSakki>());
            }

            if (_hunterPosList.Count == 0)
            {
                _hunterPosList.AddRange(transform.Find("HunterPos").GetComponentsInChildren<Transform>());
                _hunterPosList.RemoveAt(0);
            }

            foreach (Transform docsa in _docsaPosList)
            {
                objTemp = ObjectPool.SPoolDict[PoolType.Hunter].Instantiate(docsa.position, docsa.rotation);
                ActiveHunterList.Add(objTemp.GetComponent<Hunter>());
            }
        }

        void OnDisable()
        {
            ObjectPool.SPoolDict[PoolType.Docsa].ReturnAll();
            ObjectPool.SPoolDict[PoolType.Hunter].ReturnAll();
            ObjectPool.SPoolDict[PoolType.Weapon].ReturnAll();
            ObjectPool.SPoolDict[PoolType.StarRain].ReturnAll();
            
            ActiveDocsaList.Clear();
            ActiveHunterList.Clear();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(new Vector3(-8, 6, 0), new Vector3(8, 6, 0));
            Gizmos.DrawLine(new Vector3(-8, -6, 0), new Vector3(-8, 6, 0));
            Gizmos.DrawLine(new Vector3(8, -6, 0), new Vector3(-8, -6, 0));
            Gizmos.DrawLine(new Vector3(8, 6, 0), new Vector3(8, -6, 0));
        }
    }
}