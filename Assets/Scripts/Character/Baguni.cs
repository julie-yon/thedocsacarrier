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
        public Transform TargetTransform;
        Queue<Vector3> _HamaPositionQueue = new Queue<Vector3>();
        Vector3 _lastHamaPosition;
        Vector3 _nextTargetPosition;
        public float HamaPositionStep = 0.01f;
        public float QueueFlushSize = 30;
        public float FollowSpeed = 1f;

        void Awake()
        {
            _HamaPositionQueue.Enqueue(TargetTransform.position);
            _lastHamaPosition = TargetTransform.position;
        }

        void FixedUpdate()
        {
            FollowHama();
        }

        void FollowHama()
        {
            if ((TargetTransform.position - _lastHamaPosition).magnitude >= HamaPositionStep)
            {
                _lastHamaPosition = TargetTransform.position;
                _HamaPositionQueue.Enqueue(TargetTransform.position);
            }

            if (_HamaPositionQueue.Count > QueueFlushSize)
            {
                _nextTargetPosition = _HamaPositionQueue.Dequeue();
            }

            transform.position = Vector2.MoveTowards(transform.position, _nextTargetPosition, FollowSpeed);
        }
    }
}  