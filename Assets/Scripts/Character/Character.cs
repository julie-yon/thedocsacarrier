using System.Collections;
using UnityEngine;

using TMPro;

namespace Docsa.Character
{

    [RequireComponent(typeof(CharacterBehaviour))]
    public class Character : MonoBehaviour
    {
        public CharacterBehaviour Behaviour;
        public Transform GrabDocsaPosition;
        public bool isDie = false;
        
        [SerializeField] Transform RootBoneTransform;
        public Vector3 HeaderRelativePosition = Vector2.up;
        public Vector3 HeaderPosition
        {
            get {return RootBoneTransform.position + HeaderRelativePosition;}
        }
        public CharacterChat Chat;

        [Header("HP Stats")]
        [Space(10)]
        public HPBar HPBar;
        [SerializeField] private int _maxHP;
        [SerializeField] private int _currentHP;
        public int MaxHP{
            get {return _maxHP;}
            set {_maxHP = value;}
        }

        public int CurrentHP{
            get {return _currentHP;}
            private set {
                _currentHP = value;
                if (HPBar != null)
                {
                    HPBar.Value = value;
                }
                if (_currentHP <= 0)
                {
                    Behaviour.Die();
                }
            }
        }

        protected virtual void Reset()
        {
            Behaviour = GetComponent<CharacterBehaviour>();
            Behaviour.Character = this;
            _maxHP = 100;
            _currentHP = 100;
        }

        protected virtual void Awake()
        {
            CurrentHP = _currentHP;
        }

        public void GetDamage(int damageValue)
        {   
            CurrentHP -= damageValue;
        }

        public void GetDamage(float damageValue)
        {   
            CurrentHP -= (int)damageValue;
        }

        public void SetChatData(string chat, float time = 2f)
        {
            Chat.Chat(chat, time);
        }
    }
}