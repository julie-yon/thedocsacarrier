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

        protected virtual void Awake()
        {
            if (ActivateOnAwake) Activate();
            if (StartOnAwake) {Activate(); StartGimmick();}
        }
        
        public virtual bool Activate()
        {
            if (Activated)
            {
                return false;
            }

            Activated = true;
            return true;
        }

        public virtual bool Deactivate()
        {
            End();

            if (!Activated)
            {
                return false;
            }

            Activated = false;
            return true;
        }

        public virtual bool StartGimmick()
        {
            if (!Activated || Started) return false;

            Started = true;
            return true;
        }
        
        public virtual bool End()
        {
            if (!Activated || !Started) return false;

            ResetGimmick();
            Started = false;
            return true;
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

