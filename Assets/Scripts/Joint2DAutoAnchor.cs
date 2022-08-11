using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dkstlzu.Utility
{
    public class Joint2DAutoAnchor : MonoBehaviour
    {
        public Joint2D Joint;
        void Reset()
        {
            Joint = GetComponent<Joint2D>();
            AutoAnchor();
        }

        public void AutoAnchor()
        {
            if (Joint is RelativeJoint2D || Joint is TargetJoint2D)
            {
                Debug.LogWarning(name + "'s Join2D does not have anchor");
                return;
            }

            // dynamic jt = Joint;
            // jt.anchor = transform.InverseTransformPoint(Joint.connectedBody.transform.position);

            // switch (Joint)
            // {
            //     case DistanceJoint2D DJ2D :
            //     Do(DJ2D);
            //     break;
            //     case FixedJoint2D FxJ2D :
            //     Do(FxJ2D);
            //     break;
            //     case FrictionJoint2D FrJ2D :
            //     Do(FrJ2D);
            //     break;
            //     case HingeJoint2D HJ2D :
            //     Do(HJ2D);
            //     break;
            //     case SliderJoint2D SlJ2D :
            //     Do(SlJ2D);
            //     break;
            //     case SpringJoint2D SpJ2D :
            //     Do(SpJ2D);
            //     break;
            //     case WheelJoint2D WJ2D :
            //     Do(WJ2D);
            //     break;
                
            // }

            // void Do(dynamic jt)
            // {
            //     jt.anchor = transform.InverseTransformPoint(Joint.connectedBody.transform.position);
            // }
        }

    }
}