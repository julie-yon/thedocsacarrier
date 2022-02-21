using UnityEngine;
using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;

namespace Docsa
{
    /// <summary>
    /// 밤낮 변화를 관리받고 싶은 경우 상속받으십시오. 이 클래스는 유니티의 MonoBehaviour 클래스를 상속 받습니다.
    /// 1. 밤낮에 따라 어떤 변화를 주고 싶은지 ChangeState 메소드를 오버라이드 하면 됩니다.
    /// 2. eventName에 적절한 이름을 부여하십시오. NightDaySwitch는 이 이름을 통해 밤낮전환 대상을 관리합니다.
    /// </summary>
    public abstract class NightDaySwitchEvent : MonoBehaviour
    {
        public string EventName;
        
        private protected void Awake()
        {
            NightDaySwitch.instance.RegisterToSwitch(this);
        }

        private protected void OnDestroy()
        {
            NightDaySwitch.instance.RemoveFromSwitch(this);
        }

        public abstract void ChangeState(bool isNight);
    }
}
