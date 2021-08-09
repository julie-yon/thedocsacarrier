using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Docsa.Character
{

    [RequireComponent(typeof(CharacterBehaviour))]
    public class Character : MonoBehaviour
    {
        public HPBar HPBar;
        public TextMeshProUGUI ChatText;
        public CharacterBehaviour Behaviour;
        public Transform GrabDocsaPosition;
        public string Author;
        private Coroutine _chatCoroutine;
        private const int maxHP = 100;
        private int currentHP = 100;
        public int MaxHP{
            get {return maxHP;}
        }

        public int CurrentHP{
            get {return currentHP;}
            private set {
                currentHP = value;
                if (HPBar != null)
                {
                    HPBar.Value = value;
                }
                if (currentHP <= 0)
                {
                    Behaviour.Die();
                }
            }
        }

        void Awake()
        {
            CurrentHP = currentHP;
        }

        public void GetDamage(int damageValue)
        {   
            CurrentHP -= damageValue ; 
        }

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
            ChatText.text = chat;
            yield return new WaitForSeconds(time);
            ChatText.text = "";
        }
    }

}