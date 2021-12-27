using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utility;
using TwitchIRC;

namespace Docsa
{
    public class PerksManager : Singleton<PerksManager>
    {
        void Awake()
        {
        
        }
    }

    public struct Perks
    {
        public DocsaTwitchCommand PerkCommand;
        public bool enabled;
    }
}