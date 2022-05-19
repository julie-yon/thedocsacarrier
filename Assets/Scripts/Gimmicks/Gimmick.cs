using UnityEngine;
using Docsa.Character;

namespace Docsa.Gimmick
{
    public class Gimmick : MonoBehaviour
    {
        public bool ActivateOnAwake;
        public bool StartOnAwake;
        public bool Activated;
        public bool Started;

        void Awake()
        {
            if (ActivateOnAwake) Activate();
            if (StartOnAwake) StartGimmick();
        }
        
        public virtual void Activate()
        {
            Activated = true;
        }

        public virtual void Deactivate()
        {
            End();
            Activated = false;
        }

        public virtual void StartGimmick()
        {
            if (!Activated) return;
            Started = true;
        }
        
        public virtual void End()
        {
            if (!Activated) return;
            ResetGimmick();
            Started = false;
        }

        public virtual void Invoke()
        {
            if (!Started) return;
        }

        public virtual void ResetGimmick()
        {
            if (!Started) return;
        }
    }
}

