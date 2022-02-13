using System.Collections.Generic;
using Utility;


namespace Docsa
{
    /// <summary>
    /// 밤낮을 관리하는 싱글턴 클래스입니다. 이 클래스를 통해 밤낮이 관리되고 싶다면 해당 오브젝트에 NightDaySwithEvent 클래스를 상속받으면 됩니다.
    /// </summary>
    public class NightDaySwitch : Singleton<NightDaySwitch>
    {
        private delegate void EventDelegate(bool isNight);
        private EventDelegate _nightDayChangeEventListener;
        private bool _isNight;
        private List<string> _targets;
        private Dictionary<string, List<NightDaySwitchEvent>> _allElements = new Dictionary<string, List<NightDaySwitchEvent>>();
        
        /// <summary>
        /// 밤낮전환의 대상을 등록합니다. 기존 대상은 초기화됩니다.
        /// </summary>
        /// <param name="targets">NightDaySwithEvent 클래스 eventName에 설정한 스트링 값을 리스트 형식으로 넣으십시오. 해당되는 모든 오브젝트가 밤낮 전환의 대상이 됩니다.</param>
        public void ChangeTargets(List<string> targets)
        {
            _targets = targets;
            _nightDayChangeEventListener = null;
            foreach (var target in _targets)
            {
                foreach (var targetObject in _allElements[target])
                {
                    _nightDayChangeEventListener  += targetObject.ChangeState;
                }
            }
        }
    
        /// <summary>
        /// 밤낮을 전환합니다. 밤낮전환의 대상으로 등록된 오브젝트를 변화시킵니다.
        /// </summary>
        public void Switching()
        {
            _isNight = !_isNight;

            if (_nightDayChangeEventListener != null)
            {
                _nightDayChangeEventListener(_isNight);
            }
        }
        
        /// <summary>
        /// NightDaySwitch에 등록합니다. 이는 밤낮전환 대상에 등록하는 것과는 다릅니다.
        /// NightDaySwitchEvent를 상속받는 클래스는 Unity Awake시 이 함수가 자동 실행됩니다.
        /// </summary>
        /// <param name="element"></param>
        public void RegisterToSwitch(NightDaySwitchEvent element)
        {

            List<NightDaySwitchEvent> elementList;
            if (_allElements.TryGetValue(element.eventName, out elementList))
            {
                elementList.Add(element);
            }
            else
            {
                List<NightDaySwitchEvent> eventLi = new List<NightDaySwitchEvent>();
                eventLi.Add(element);
                _allElements.Add(element.eventName, eventLi);
            }
        }

        /// <summary>
        /// NightDaySwitch에서 제거합니다.
        /// NightDaySwitchEvent를 상속받는 클래스는 Unity Destroy시 이 함수가 자동 실행됩니다.
        /// </summary>
        /// <param name="element"></param>
        public void RemoveFromSwitch(NightDaySwitchEvent element)
        {
            element.ChangeState(_isNight);
            List<NightDaySwitchEvent> elementList;
            if (_allElements.TryGetValue(element.eventName, out elementList))
            {
                elementList.Remove(element);
            }
            try
            {
                _nightDayChangeEventListener -= element.ChangeState;
            } catch {}
        }
    }
}
