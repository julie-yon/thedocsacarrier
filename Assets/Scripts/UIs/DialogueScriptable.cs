using System;
using System.Collections.Generic;
using UnityEngine;

namespace dkstlzu.Utility
{
    [CreateAssetMenu(fileName ="Dialogue", menuName = "ScriptableObjects/Dialogue")]
    [System.Serializable]
    public class DialogueScriptable : ScriptableObject
    {
        public List<DialogueItem> ItmeList;
        public DialogueScriptable NextDialogue;
        public Sprite LeftSpeakerSprite;
        public Sprite RightSpeakerSprite;

    }

    [System.Serializable]
    public class DialogueItem
    {
        public string Name;
        [TextArea(3, 10)]
        public string Message;
        public bool LeftSpeakerImageOn;
        public bool RightSpeakerImageOn;
        public Sprite Sprite;
        public Action OnItemShow;
    }
}