using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Character
{
    public class Baguni : MonoBehaviour
    {
        public GameObject Bucket;
        public GameObject BucketCap;
        public AnimationCurve BucketCurve;

        private float _bucketMinY = -0.1f;
        private float _bucketMaxY = 0.1f;
        private float _hamaVelocityMinY = -2f;
        private float _hamaVelocityMaxY = 2f;

        void Awake()
        {
            BucketCurve = new AnimationCurve(new Keyframe(0, _bucketMaxY), new Keyframe(1, _bucketMinY));
        }


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                OnOffBucketCap();
            }

        }

        void FixedUpdate()
        {
            AdjustBucketPosition();
        }

        public void OnOffBucketCap()
        {
            BucketCap.SetActive(!BucketCap.activeSelf);
        }

        private void AdjustBucketPosition()
        {
            Vector2 hamaVelocity = UzuHama.Hama.Behaviour.CurrentVelocity;

            float velocityYRatio = Mathf.Clamp01((hamaVelocity.y - _hamaVelocityMinY) / (_hamaVelocityMaxY - _hamaVelocityMinY));
            Vector3 targetPosition =  new Vector3(Bucket.transform.localPosition.x, Mathf.SmoothStep(Bucket.transform.localPosition.y, BucketCurve.Evaluate(velocityYRatio), 0.5f), Bucket.transform.localPosition.z);
            Bucket.transform.localPosition = targetPosition;
        }

    }
}  