using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Docsa.Character
{
    public class Baguni : MonoBehaviour
    {
        public GameObject Bucket;
        public GameObject BucketCap;
        public AnimationCurve BucketCurve;

        private float _hamaVelocityMinY = -2f;
        private float _hamaVelocityMaxY = 2f;

        void FixedUpdate()
        {
            AdjustBucketPosition();
        }

        public void OnOff()
        {
            foreach (var col in GetComponentsInChildren<Collider2D>())
            {
                col.enabled = !col.enabled;
            }
        }

        public async void TemporaryOff(float time)
        {
            foreach (var col in GetComponentsInChildren<Collider2D>())
            {
                col.enabled = false;
            }

            await System.Threading.Tasks.Task.Delay((int)time * 1000);
            
            foreach (var col in GetComponentsInChildren<Collider2D>())
            {
                col.enabled = true;
            }
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