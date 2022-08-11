using UnityEngine;
using dkstlzu.Utility;

using BansheeGz.BGSpline.Components;

namespace Docsa.Events
{
    public class GrabDocsaCoroutine : MonoBehaviour
    {
        public BezierCurve BGCurve;
        private BGCcCursor cursor;
        private Docsa.Character.DocsaSakki targetDocsa;
        public AnimationCurve MoveCurve = AnimationCurve.EaseInOut(0, 0, 2, 1);
        public float UpdateTime = 0;

        void Awake()
        {
            dkstlzu.Utility.AsyncAwait.Delay(()=>
            {
                cursor = BGCurve.Cursor;
                targetDocsa = ((BGCcTrs)BGCurve.GetBGCc<BGCcTrs>()).ObjectToManipulate.GetComponent<Docsa.Character.DocsaSakki>();
                targetDocsa.GetComponentInChildren<Collider2D>().enabled = false;
            }, 0.1f);
        }

        void Update()
        {
            if (!cursor)
            {
                return;
            }

            if (MoveCurve[MoveCurve.length-1].time >= UpdateTime)
            {
                UpdateTime += Time.deltaTime;
            }
            cursor.DistanceRatio = MoveCurve.Evaluate(UpdateTime);
        }

        void OnDestroy()
        {
            targetDocsa.GetComponent<Collider2D>().enabled = true;
        }
    }
}