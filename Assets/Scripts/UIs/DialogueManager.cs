using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using Michsky.UI.ModernUIPack;

using TMPro;

namespace Utility
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public DialogueScriptable Dialogue
        {
            get {return _dialogue;}
            set 
            {
                _currentDialogueIndex = 0;
                DialogueName = value.Name;
                DialogueMessage = value.Message[_currentDialogueIndex];
                if (value.LeftSpeakerSprite != null)
                    LeftSpeakerImage = value.LeftSpeakerSprite;
                if (value.RightSpeakerSprite != null)
                    RightSpeakerImage = value.RightSpeakerSprite;
                _dialogue = value;
                _previousDialogue = value;
            }
        }

        public string DialogueName
        {
            get {return DialogueNameUI.text;}
            set {DialogueNameUI.text = value;}
        }

        public string DialogueMessage
        {
            get {return DialogueMessageUI.text;}
            set {DialogueMessageUI.text = value;}
        }

        public Sprite LeftSpeakerImage {set {_leftSpeakerImage.sprite = value;}}
        public Sprite RightSpeakerImage {set {_rightSpeakerImage.sprite = value;}}
        public int CurrentDialogueIndex {get {return _currentDialogueIndex;}}
        public bool isOpened {get {return DialogueObject.activeSelf;}}

        [Header("Dialogue UI Object")]
        public GameObject DialogueObject;
        public ButtonManagerBasic ContinueButton;
        [SerializeField] private TextMeshProUGUI DialogueNameUI;
        [SerializeField] private TextMeshProUGUI DialogueMessageUI;

        [Header("Dialogue ScriptableObject")]
        [SerializeField] private DialogueScriptable _dialogue;
        private DialogueScriptable _previousDialogue;
        [SerializeField] private int _currentDialogueIndex = -1;
        [SerializeField] private Image _leftSpeakerImage;
        [SerializeField] private Image _rightSpeakerImage;

        void Awake()
        {
            Dialogue = _dialogue;
        }

        void Update()
        {
            if (isOpened)
            {
                if (Keyboard.current.enterKey.wasPressedThisFrame)
                {

                }
            }
        }

        void OnValidate()
        {
            if (DialogueObject != null)
            {
                ContinueButton = DialogueObject.GetComponentInChildren<ButtonManagerBasic>();
                var texts = DialogueObject.GetComponentsInChildren<TextMeshProUGUI>();
                DialogueNameUI = texts[0];
                DialogueMessageUI = texts[1];
            }
            
            if (_dialogue != null && !_dialogue.Equals(_previousDialogue))
            {
                Dialogue = _dialogue;
            }
        }

        public void OpenDialogue()
        {
            Dialogue = Dialogue;
            DialogueObject.SetActive(true);
        }

        public void CloseDialogue()
        {
            DialogueObject.SetActive(false);
        }

        public virtual void OnContinueButtonClicked()
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