using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

using BansheeGz.BGSpline.Curve;
using BansheeGz.BGSpline.Components;

namespace Docsa.Character
{
    public class Baguni : MonoBehaviour
    {
        public GameObject Bucket;
        public GameObject BucketCap;
        public AnimationCurve BucketCurve;

        [SerializeField] private BGCurve BaguniBGCurve;

        private float _hamaVelocityMinY = -2f;
        private float _hamaVelocityMaxY = 2f;

        void Awake()
        {
            BGmath = BaguniBGCurve.GetComponent<BGCcMath>();
            BGcursor = BaguniBGCurve.GetComponent<BGCcCursor>();
            Core.instance.InputAsset.Player.Baguni.performed += SetBaguniTargetPosition;
        }

        void FixedUpdate()
        {
            AdjustBucketPosition();
            AdjustBaguniPosition();
        }

        public void OnOff()
        {
            foreach (var col in GetComponentsInChildren<Collider2D>())
            {
                col.enabled = !col.enabled;
            }
        }

        private void AdjustBucketPosition()
        {
            Vector2 hamaVelocity = UzuHama.Hama.Behaviour.CurrentVelocity;

            float velocityYRatio = Mathf.Clamp01((hamaVelocity.y - _hamaVelocityMinY) / (_hamaVelocityMaxY - _hamaVelocityMinY));
            Vector3 targetPosition =  new Vector3(Bucket.transform.localPosition.x, Mathf.SmoothStep(Bucket.transform.localPosition.y, BucketCurve.Evaluate(velocityYRatio), 0.5f), Bucket.transform.localPosition.z);
            Bucket.transform.localPosition = targetPosition;
        }

        BGCcCursor BGcursor;
        BGCcMath BGmath;
        public AnimationCurve BaguniAnimationCurve;
        public float BaguniAdjustTime;
        private float BaguniAdjustingTime = 0;
        private void AdjustBaguniPosition()
        {
            if (BaguniAdjustingTime > BaguniAdjustTime) return;

            BaguniAdjustingTime += Time.deltaTime;
            BGcursor.Distance = BaguniAnimationCurve.Evaluate(BaguniAdjustingTime);
        }

        private void SetBaguniTargetPosition(Context context)
        {
            float distance;
            BGmath.CalcPositionByClosestPoint(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), out distance);
            int coefficient;
            if (BGcursor.Distance < distance)
            {
                coefficient = 1;
            } else
            {
                coefficient = -1;
            }
            BaguniAnimationCurve.MoveKey(0, new Keyframe(0, BGcursor.Distance, 0, 1/BaguniAdjustTime * coefficient));
            BaguniAnimationCurve.MoveKey(1, new Keyframe(BaguniAdjustTime, distance, 0, 0));
            BaguniAdjustingTime = 0;
        }
    }
}  