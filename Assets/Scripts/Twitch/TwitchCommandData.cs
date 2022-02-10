using System.Collections;
using System.Collections.Generic;
using Utility;

namespace TwitchIRC
{
    [System.Serializable]
    public struct TwitchCommandData
    {
        public static string Prefix = "!";
        public string Author;
        public DocsaTwitchCommand Command;
        public System.DateTime Time;
        public string Chat;
    }

    public enum DocsaTwitchCommand
    {
        [StringValue("기본값")]
        NONE,
        
        // Docsa Commands
        [StringValue("퉤")]
        DOCSA_ATTACK,
        [StringValue("점프")]
        DOCSA_JUMP,

        // Hunter Commands
        [StringValue("놀아줘")]
        HUNTER_NET,
        [StringValue("형형")]
        HUNTER_ATTACK,

        // General Commands
        [StringValue("참가")]
        ATTEND,
        [StringValue("퇴장")]
        EXIT,
        [StringValue("별빛이내린다")]
        STARLIGHT,

    }
}
