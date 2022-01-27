using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Docsa.Character
{

    [RequireComponent(typeof(CharacterBehaviour))]
    public class Character : MonoBehaviour
    {
        public string ViewerName;
        public CharacterBehaviour Behaviour;
        public Transform GrabDocsaPosition;
        public bool isDie = false;
        
        public TextMeshProUGUI ChatText;

        [Header("HP Stats")]
        [Space(10)]
        [SerializeField] private HPBar HPBar;
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


        private Coroutine _chatCoroutine;
        private bool _coroutineIsPlaying;
        private string _delayedChat;
        public void SetChatData(string chat, float time = 2f)
        {
            if (_chatCoroutine != null)
            {
                StopCoroutine(_chatCoroutine);
            }

            _chatCoroutine = StartCoroutine(SetChatDataCoroutine(chat, time));
        }

        IEnumerator SetChatDataCoroutine(string chat, float time=2f)
        {
            if (_coroutineIsPlaying) 
            {
                if (_delayedChat == string.Empty)
                    _delayedChat = chat;
                yield break;
            }
            
            ChatText.text = chat;
            _coroutineIsPlaying = true;
            yield return new WaitForSeconds(time);

            if (_delayedChat != string.Empty)
            {
                _chatCoroutine = StartCoroutine(SetChatDataCoroutine(_delayedChat, time));
                _delayedChat = string.Empty;
            }
        }
    }

}