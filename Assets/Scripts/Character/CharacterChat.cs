using System.Collections;
using UnityEngine;

using TMPro;
using dkstlzu.Utility;

namespace Docsa.Character
{
    public class CharacterChat : MonoBehaviour
    {
        public Character Character;
        public TextMeshPro ChatText;

        void Update()
        {
            transform.position = Character.HeaderPosition;
            transform.rotation = Quaternion.identity;
        }

        private string _delayedChat = string.Empty;
        TaskManagerTask chatTask;

        public void Chat(string chat, float time = 2f)
        {
            Printer.Print($"Character SetChat : {chat}, time : {time}");

            if (chatTask == null)
            {
                chatTask = new TaskManagerTask(SetChatDataCoroutine(chat, time));
                chatTask.Finished += (stop) => 
                {
                    if (_delayedChat != string.Empty)
                    {
                        chatTask = new TaskManagerTask(SetChatDataCoroutine(_delayedChat, time));
                        _delayedChat = string.Empty;
                    } else
                    {
                        chatTask = null;
                    }
                };
            } else
            {
                _delayedChat = chat;
            }
        }

        IEnumerator SetChatDataCoroutine(string chat, float time=2f)
        {
            ChatText.text = chat;
            yield return new WaitForSeconds(time);
            ChatText.text = string.Empty;
        }
    }
}