using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BansheeGz.BGSpline.Components;

namespace Docsa.Events
{
    public class GrabDocsaCoroutine : MonoBehaviour
    {
        public BGCcCursor Cursor;
        public AnimationCurve MoveCurve = AnimationCurve.EaseInOut(0, 0, 2, 1);
        public float UpdateTime = 0;

        void Update()
        {
            if (!Cursor)
            {
                return;
            }

            if (MoveCurve[MoveCurve.length-1].time >= UpdateTime)
            {
                UpdateTime += Time.deltaTime;
            }
            Cursor.DistanceRatio = MoveCurve.Evaluate(UpdateTime);
        }
    }
}