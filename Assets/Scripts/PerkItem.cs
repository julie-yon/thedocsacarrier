using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TwitchIRC;

namespace Docsa
{
    public class PerkItem : Item
    {
        public DocsaTwitchCommand TargetPerk;

        public override void Effect()
        {
            switch(TargetPerk)
            {
                case DocsaTwitchCommand.DOCSA_ATTACK :
                PerkManager.instance.Data.DocsaChimPerk.Enable();
                break;
                case DocsaTwitchCommand.DOCSA_JUMP :
                PerkManager.instance.Data.DocsaJumpPerk.Enable();
                break;
                case DocsaTwitchCommand.HUNTER_NET :
                PerkManager.instance.Data.HunterNetPerk.Enable();
                break;
                case DocsaTwitchCommand.HUNTER_Jump :
                PerkManager.instance.Data.HunterJumpPerk.Enable();
                break;
                case DocsaTwitchCommand.ATTEND :
                PerkManager.instance.Data.ViewerAttendPerk.Enable();
                break;
                case DocsaTwitchCommand.EXIT :
                PerkManager.instance.Data.ViewerExitPerk.Enable();
                break;
                case DocsaTwitchCommand.STARLIGHT :
                PerkManager.instance.Data.StarLightPerk.Enable();
                break;
            }

            base.Effect();
        }
    }
}