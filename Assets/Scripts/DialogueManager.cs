using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Michsky.UI.ModernUIPack;

using TMPro;

namespace Utility
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public GameObject DialogueObject;
        public DialogueScriptable Dialogue
        {
            get {return _dialogue;}
            set 
            {
                _currentDialogueIndex = 0;
                DialogueName = value.Name;
                DialogueMessage = value.Message[_currentDialogueIndex];
                LeftSpeakerImage = value.LeftSpeakerSprite;
                RightSpeakerImage = value.RightSpeakerSprite;
                _dialogue = value;
            }
        }

        public string DialogueName
        {
            get {return DialogueNameUI.text;}
            set
            {
                DialogueNameUI.text = value;
            }
        }

        public string DialogueMessage
        {
            get {return DialogueMessageUI.text;}
            set
            {
                DialogueMessageUI.text = value;
            }
        }

        public Sprite LeftSpeakerImage
        {
            set 
            {
                _leftSpeakerImage.sprite = value;
            }
        }

        public Sprite RightSpeakerImage
        {
            set 
            {
                _rightSpeakerImage.sprite = value;
            }
        }

        public int CurrentDialogueIndex
        {
            get {return _currentDialogueIndex;}
        }
        public ButtonManagerBasic ContinueButton;

        [SerializeField] private DialogueScriptable _dialogue;
        [SerializeField] private TextMeshProUGUI DialogueNameUI;
        [SerializeField] private TextMeshProUGUI DialogueMessageUI;
        [SerializeField] private int _currentDialogueIndex = -1;
        [SerializeField] private Image _leftSpeakerImage;
        [SerializeField] private Image _rightSpeakerImage;

        void Awake()
        {
            Dialogue = _dialogue;
        }

        public void OpenDialogue()
        {
            DialogueObject.SetActive(true);
        }

        public void OnContinueButtonClicked()
        {
            _currentDialogueIndex++;

            if (Dialogue.Message.Count <= _currentDialogueIndex)
            {
                if (Dialogue.NextDialogue != null)
                {
                    Dialogue = Dialogue.NextDialogue;
                }
                else
                {
                    DialogueObject.SetActive(false);
                }
                return;
            } else
            {
                DialogueName = Dialogue.Name;
                DialogueMessage = Dialogue.Message[_currentDialogueIndex];
            }
            
        }
    }
}