using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dkstlzu.Utility;

namespace Docsa.Gimmick
{
    public class VolcanicAshEmitter : MonoBehaviour
    {   
        public float Min;
        public float Max;
        public bool NotFinished = true;
        public float Interval = 1.0f;
        
        /// <summary>
        /// ToxicCloud를 만듭니다.
        /// </summary>
        /// <returns></returns>
        public ToxicCloud Emit ()
        {
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(2.0f, Random.Range(Min, Max) , Camera.main.nearClipPlane)); // ToxicCloud가 생성될 좌표
             
            return ObjectPool.GetOrCreate(DocsaPoolType.ToxicCloud).Instantiate(pos, Quaternion.identity).GetComponent<ToxicCloud>();  //첫번째로 생성되는 ToxicCloud
        }

        
        IEnumerator RunEmit ()
        {   
            while (NotFinished)
            {
                yield return new WaitForSeconds(Interval);
                Emit();
            }
            
        }
        
        public void Start()
        {
           StartCoroutine (RunEmit()); 
        }
    }

}
