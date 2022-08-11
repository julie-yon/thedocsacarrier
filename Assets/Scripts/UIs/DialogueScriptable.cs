using System.Collections.Generic;
using UnityEngine;

namespace dkstlzu.Utility
{
    [CreateAssetMenu(fileName ="Dialogue", menuName = "ScriptableObjects/Dialogue")]
    [System.Serializable]
    public class DialogueScriptable : ScriptableObject
    {
        public string Name;
        [TextArea(3, 10)]
        public List<string> Message;
        public DialogueScriptable NextDialogue;
        public Sprite LeftSpeakerSprite;
        public Sprite RightSpeakerSprite;

    }
}