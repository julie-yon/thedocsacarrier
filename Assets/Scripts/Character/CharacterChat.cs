using System.Collections;
using UnityEngine;

using TMPro;

namespace Docsa.Character
{
    public class CharacterChat : MonoBehaviour
    {
        public Character Character;
        public TextMeshProUGUI ChatText;
        void Reset()
        {
            ChatText = GetComponent<TextMeshProUGUI>();
            if (!transform.parent.TryGetComponent<Character>(out Character))
            {
                Debug.LogWarning("HPBar could not find Character at parent. Make ref in inspector yourself");
            } else
            {
                transform.position = Character.HeaderPosition;
                Character.Chat = this;
            }
        }

        void OnValidate()
        {
            if (Character)
                transform.position = Character.HeaderPosition;
        }

        void Update()
        {
            transform.position = Character.HeaderPosition;
            transform.rotation = Quaternion.identity;
        }

        private Coroutine _chatCoroutine;
        private bool _coroutineIsPlaying = false;
        private string _delayedChat = string.Empty;
        public void Chat(string chat, float time = 2f)
        {
            // if (_chatCoroutine != null)
            // {
            //     StopCoroutine(_chatCoroutine);
            // }

            print("Character SetChat : " + chat);
            // ChatText.text = chat;
            _chatCoroutine = StartCoroutine(SetChatDataCoroutine(chat, time));
        }

        IEnumerator SetChatDataCoroutine(string chat, float time=2f)
        {
            Info("AA");
            if (_coroutineIsPlaying) 
            {
                Info("BB");
                if (_delayedChat == string.Empty)
                {
                    Info("CC");
                    _delayedChat = chat;
                }
                yield break;
            }
            
            ChatText.text = chat;
            _coroutineIsPlaying = true;
            Info("DD");
            yield return new WaitForSeconds(time);
            Info("EE");

            if (_delayedChat != string.Empty)
            {
                Info("FF");
                _coroutineIsPlaying = false;
                _chatCoroutine = StartCoroutine(SetChatDataCoroutine(_delayedChat, time));
                _delayedChat = string.Empty;
            } else
            {
                Info("GG");
                _coroutineIsPlaying = false;
                ChatText.text = string.Empty;
            }

            void Info(string Tag)
            {
                print($"{Tag} hasCorountine : {_chatCoroutine != null}, isplaying : {_coroutineIsPlaying}, _delayedChat : {_delayedChat}");
            }
        }
    }
}