// using System.Text.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utility;
using TwitchIRC;

namespace Docsa
{
    public class PerkManager : Singleton<PerkManager>
    {
        public PerkData Data;

        void Reset()
        {
            Data.Init();
        }

        void OnApplicationQuit()
        {
            
        }

    }

    [System.Serializable]
    public struct PerkData
    {
        public Perk DocsaChimPerk;
        public Perk DocsaJumpPerk;
        public Perk HunterNetPerk;
        public Perk HunterAttackPerk;
        public Perk ViewerAttendPerk;
        public Perk ViewerExitPerk;
        public Perk StarLightPerk;
        
        public void Init()
        {
            DocsaChimPerk.PerkCommand = DocsaTwitchCommand.DOCSA_ATTACK;
            DocsaJumpPerk.PerkCommand = DocsaTwitchCommand.DOCSA_JUMP;
            HunterNetPerk.PerkCommand = DocsaTwitchCommand.HUNTER_NET;
            HunterAttackPerk.PerkCommand = DocsaTwitchCommand.HUNTER_ATTACK;
            ViewerAttendPerk.PerkCommand = DocsaTwitchCommand.ATTEND;
            ViewerExitPerk.PerkCommand = DocsaTwitchCommand.EXIT;
            StarLightPerk.PerkCommand = DocsaTwitchCommand.STARLIGHT;

            ViewerAttendPerk.Enable();
            ViewerExitPerk.Enable();
            StarLightPerk.Enable();
        }
    }

    [System.Serializable]
    public struct Perk
    {
        public DocsaTwitchCommand PerkCommand;
        public bool enabled;

        public Perk(Perk perk)
        {
            PerkCommand = perk.PerkCommand;
            enabled = perk.enabled;
        }

        public void Enable()
        {
            enabled = true;
        }

        public void Diable()
        {
            enabled = false;
        }
    }
}