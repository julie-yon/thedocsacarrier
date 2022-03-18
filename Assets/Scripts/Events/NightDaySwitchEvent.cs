using System;
using System.Collections;
using UnityEngine;

using Utility.EventSystem;
using Utility;

using UnityEngine.Experimental.Rendering.Universal;

namespace Docsa.Events
{
    public class NightDaySwitchEvent : MonoBehaviour, IEvent, IEventListener
    {
        public bool isDay;
        public Light2D Light;
        private CoroutineSwaper coroutineSwaper;
        
        public Enum eventCode
        {
            get {return DocsaEvents.NightDaySwitchEvent;}
        }

        void Awake()
        {
            coroutineSwaper = new CoroutineSwaper(new Func<IEnumerator>[]{LightFadeIn, LightFadeOut});
            coroutineSwaper.hasExitTime = true;
        }

        public void OnEvent(IEvent Event)
        {
            isDay = !isDay;

            if (isDay)
            {
                coroutineSwaper.Play(0);
            }
            else
            {
                coroutineSwaper.Play(1);
            }
        }

        IEnumerator LightFadeIn()
        {
            while (Light.intensity < 1)
            {
                Light.intensity += 0.01f;
                yield return new WaitForFixedUpdate();
            }
        }

        IEnumerator LightFadeOut()
        {
            while (Light.intensity > 0)
            {
                Light.intensity -= 0.01f;
                yield return new WaitForFixedUpdate();
            }
        }
    }
}