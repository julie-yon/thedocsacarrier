using UnityEngine;
using System;

namespace Docsa
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "PerkData", menuName = "ScriptableObjects/PerkData", order = 1)]
    public class PerkData : ScriptableObject
    {
        public Perk DocsaChimPerk;
        public Perk DocsaJumpPerk;
        public Perk HunterNetPerk;
        public Perk HunterJumpPerk;
        public Perk ViewerAttendPerk;
        public Perk ViewerExitPerk;
        public Perk StarLightPerk;

        public Perk UzuhamaAttackPerk;
        public Perk UzuhamaJumpPerk;
        public Perk UzuhamaGrabDocsaPerk;
        public Perk UzuhamaBaguniPerk;

        public void Reset()
        {
            DocsaChimPerk.PerkType = ItemType.DocsaAttack;
            DocsaChimPerk.CannotMessage = "공격 불가";
            DocsaChimPerk.enabled = true;
            DocsaJumpPerk.PerkType = ItemType.DocsaJump;
            DocsaJumpPerk.CannotMessage = "점프 불가";
            DocsaJumpPerk.enabled = true;
            HunterNetPerk.PerkType = ItemType.HunterAttack;
            HunterNetPerk.CannotMessage = "공격 불가";
            HunterNetPerk.enabled = true;
            HunterJumpPerk.PerkType = ItemType.HunterJump;
            HunterJumpPerk.CannotMessage = "점프 불가";
            HunterJumpPerk.enabled = true;
            ViewerAttendPerk.PerkType = ItemType.Attend;
            ViewerAttendPerk.CannotMessage = "시청자 참여가 불가능한 상태입니다.";
            ViewerAttendPerk.enabled = true;;
            ViewerExitPerk.PerkType = ItemType.Exit;
            ViewerExitPerk.CannotMessage = "시청자 퇴장이 불가능한 상태입니다.";
            ViewerExitPerk.enabled = true;;
            StarLightPerk.PerkType = ItemType.StarLight;
            StarLightPerk.CannotMessage = "별빛이 내릴 수 없음";
            StarLightPerk.enabled = true;;

            UzuhamaAttackPerk.PerkType = ItemType.HamaAttack;
            UzuhamaAttackPerk.CannotMessage = "공격 불가";
            UzuhamaAttackPerk.enabled = true;
            UzuhamaJumpPerk.PerkType = ItemType.HamaJump;
            UzuhamaJumpPerk.CannotMessage = "점프 불가";
            UzuhamaJumpPerk.enabled = true;
            UzuhamaGrabDocsaPerk.PerkType = ItemType.GrabDocsa;
            UzuhamaGrabDocsaPerk.CannotMessage = "집을 수 없음";
            UzuhamaGrabDocsaPerk.enabled = true;
            UzuhamaBaguniPerk.PerkType = ItemType.HamaBaguni;
            UzuhamaBaguniPerk.CannotMessage = "바구니 능력 해금 필요";
            UzuhamaBaguniPerk.enabled = true;
        }
    }

    [System.Serializable]
    public struct Perk
    {
        public ItemType PerkType;
        public bool enabled;
        public string CannotMessage;

        public void Enable()
        {
            enabled = true;
        }

        public void Diable()
        {
            enabled = false;
        }

        public Action<string> PrintMessageAction;
        public void PrintCannotMessage(Vector3 position)
        {
            PrintMessageAction?.Invoke(CannotMessage);
            Debug.Log(CannotMessage);
        }
    }
}