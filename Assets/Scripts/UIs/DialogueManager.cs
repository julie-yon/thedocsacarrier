using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using Michsky.UI.ModernUIPack;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

using TMPro;

namespace dkstlzu.Utility
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        public DialogueScriptable Dialogue
        {
            get {return _dialogue;}
            set 
            {
                _dialogue = value;
                LeftSpeakerImage = value.LeftSpeakerSprite;
                RightSpeakerImage = value.RightSpeakerSprite;
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
        public Sprite SpriteImage {set {_itemImage.sprite = value;}}
        public int CurrentDialogueIndex => _currentDialogueIndex;
        public bool isOpened => DialogueOpener.isOpened;
        
        public InputAction NextDialogueInputAction;

        [Header("Dialogue UI Object")]
        public ObjectOpenClose DialogueOpener;
        [SerializeField] private TextMeshProUGUI DialogueNameUI;
        [SerializeField] private TextMeshProUGUI DialogueMessageUI;
        [SerializeField] private Image _leftSpeakerImage;
        [SerializeField] private Image _rightSpeakerImage;
        [SerializeField] private Image _itemImage;

        [Header("Dialogue ScriptableObject")]
        [SerializeField] private DialogueScriptable _dialogue;
        public DialogueItem CurrentDialogueItem;
        private DialogueItem _previousDialogueItem;
        [SerializeField] private int _currentDialogueIndex = -1;

        void Awake()
        {
            NextDialogueInputAction.performed += NextDialogue;
            DialogueOpener.AfterOpen += () => NextDialogueInputAction.Enable();
            DialogueOpener.OnClose += () => NextDialogueInputAction.Disable();
        }

        public void Open()
        {
            DialogueOpener.Open();
            _currentDialogueIndex = 0;
        }

        public void Close()
        {
            DialogueOpener.Close();
        }

        public void NextDialogue(Context context)
        {
            OnContinueButtonClicked();
        }

        public virtual void OnContinueButtonClicked()
        {
            _currentDialogueIndex++;

            if (Dialogue.ItmeList.Count <= _currentDialogueIndex)
            {
                if (Dialogue.NextDialogue == null)
                {
                    Close();
                    return;
                } else
                {
                    Dialogue = Dialogue.NextDialogue;
                    _previousDialogueItem = null;
                    _currentDialogueIndex = 0;
                }
            } else
            {
                _previousDialogueItem = CurrentDialogueItem;
            }

            CurrentDialogueItem = Dialogue.ItmeList[CurrentDialogueIndex];
            SetDatas();
        }

        void SetDatas()
        {
            _leftSpeakerImage.enabled = CurrentDialogueItem.LeftSpeakerImageOn;
            _rightSpeakerImage.enabled = CurrentDialogueItem.RightSpeakerImageOn;
            DialogueName = CurrentDialogueItem.Name;
            DialogueMessage = CurrentDialogueItem.Message;
            _itemImage.sprite = CurrentDialogueItem.Sprite;
        }
    }
}