using UnityEngine;
using Utility;

namespace Docsa.Gimmick
{
    [RequireComponent(typeof(EventTrigger))]
    public class LinearGimmick : Gimmick
    {
        public Vector2 Direction;
        public float Speed = 1;
        [SerializeField][HideInInspector] protected EventTrigger ET;
        protected virtual void Reset()
        {
            ET = GetComponent<EventTrigger>();
            ET.ClearEnterEvent();
            ET.AddEnterEvent(Invoke);
        }

        protected virtual void FixedUpdate()
        {
            if (Started)
                transform.Translate(Direction * Speed * Time.deltaTime);
        }
    }
}