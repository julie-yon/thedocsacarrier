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
        [SerializeField] private Transform _rightStartPosition;
        [SerializeField] private Transform _leftStartPosition;
        public Vector3 RightStartPosition {get {return _rightStartPosition.position;}}
        public Vector3 LeftStartPosition {get {return _leftStartPosition.position;}}
        [SerializeField] Transform _defaultCameraPosition;
        public GameObject DocsaPositionObject;
        public GameObject HunterPositionObject;
        public bool DrawGizmos = true;

        public int ChunkNumber;
        public Vector3 DefaultCameraPosition
        {
            get {return _defaultCameraPosition.position;}
        }

        public int DocsaCount
        {
            get {return _docsaPosList.Count;}
        }

        public int HunterCount
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
            if (_docsaPosList.Count == 0 && DocsaPositionObject != null)
            {
                _docsaPosList.AddRange(DocsaPositionObject.GetComponentsInChildren<Transform>());
                // Removing "DocsaPos" itself
                _docsaPosList.RemoveAt(0);
            }

            foreach (Transform docsa in _docsaPosList)
            {
                objTemp = ObjectPool.GetOrCreate(DocsaPoolType.Docsa).Instantiate(docsa.position, docsa.rotation);
                ActiveDocsaList.Add(objTemp.GetComponent<DocsaSakki>());
            }

            if (_hunterPosList.Count == 0 && HunterPositionObject != null)
            {
                _hunterPosList.AddRange(HunterPositionObject.GetComponentsInChildren<Transform>());
                // Removing "HunterPos" itself
                _hunterPosList.RemoveAt(0);
            }

            foreach (Transform docsa in _docsaPosList)
            {
                objTemp = ObjectPool.GetOrCreate(DocsaPoolType.Hunter).Instantiate(docsa.position, docsa.rotation);
                ActiveHunterList.Add(objTemp.GetComponent<Hunter>());
            }

            UzuHama.Hama.transform.position = LeftStartPosition;
        }

        void OnDisable()
        {
            ObjectPool.GetOrCreate(DocsaPoolType.Docsa).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.Hunter).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.Weapon).ReturnAll();
            ObjectPool.GetOrCreate(DocsaPoolType.StarRain).ReturnAll();
            
            ActiveDocsaList.Clear();
            ActiveHunterList.Clear();
        }

        void OnDrawGizmos()
        {
            if (DrawGizmos)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(transform.position + new Vector3(-8, 6, 0), transform.position + new Vector3(8, 6, 0));
                Gizmos.DrawLine(transform.position + new Vector3(-8, -6, 0), transform.position + new Vector3(-8, 6, 0));
                Gizmos.DrawLine(transform.position + new Vector3(8, -6, 0), transform.position + new Vector3(-8, -6, 0));
                Gizmos.DrawLine(transform.position + new Vector3(8, 6, 0), transform.position + new Vector3(8, -6, 0));
            }
        }
    }
}