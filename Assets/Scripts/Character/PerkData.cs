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

        void Reset()
        {
            Init();
        }

        public void Init()
        {
            FloatingTextManager floatingTextManager = FloatingTextManager.instance;
            Docsa.Character.UzuHama hama = Docsa.Character.UzuHama.Hama;
            Action<string> printAction = (str) =>
            {
                floatingTextManager.MakeNewText(hama.transform.position, str);
            };

            DocsaChimPerk.PerkType = ItemType.DocsaAttack;
            DocsaChimPerk.PrintMessageAction = printAction;
            DocsaChimPerk.CannotMessage = "공격 불가";
            DocsaJumpPerk.PerkType = ItemType.DocsaJump;
            DocsaJumpPerk.PrintMessageAction = printAction;
            DocsaJumpPerk.CannotMessage = "점프 불가";
            HunterNetPerk.PerkType = ItemType.HunterAttack;
            HunterNetPerk.PrintMessageAction = printAction;
            HunterNetPerk.CannotMessage = "공격 불가";
            HunterJumpPerk.PerkType = ItemType.HunterJump;
            HunterJumpPerk.PrintMessageAction = printAction;
            HunterJumpPerk.CannotMessage = "점프 불가";
            ViewerAttendPerk.PerkType = ItemType.Attend;
            ViewerAttendPerk.PrintMessageAction = printAction;
            ViewerAttendPerk.CannotMessage = "시청자 참여가 불가능한 상태입니다.";
            ViewerExitPerk.PerkType = ItemType.Exit;
            ViewerExitPerk.PrintMessageAction = printAction;
            ViewerExitPerk.CannotMessage = "시청자 퇴장이 불가능한 상태입니다.";
            StarLightPerk.PerkType = ItemType.StarLight;
            StarLightPerk.PrintMessageAction = printAction;
            StarLightPerk.CannotMessage = "별빛이 내릴 수 없음";

            UzuhamaAttackPerk.PerkType = ItemType.HamaAttack;
            UzuhamaAttackPerk.PrintMessageAction = printAction;
            UzuhamaAttackPerk.CannotMessage = "공격 불가";
            UzuhamaJumpPerk.PerkType = ItemType.HamaJump;
            UzuhamaJumpPerk.PrintMessageAction = printAction;
            UzuhamaJumpPerk.CannotMessage = "점프 불가";
            UzuhamaGrabDocsaPerk.PerkType = ItemType.GrabDocsa;
            UzuhamaGrabDocsaPerk.PrintMessageAction = printAction;
            UzuhamaGrabDocsaPerk.CannotMessage = "집을 수 없음";
            UzuhamaBaguniPerk.PerkType = ItemType.HamaBaguni;
            UzuhamaBaguniPerk.PrintMessageAction = printAction;
            UzuhamaBaguniPerk.CannotMessage = "바구니 능력 해금 필요";
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
            if (PrintMessageAction.GetInvocationList().Length > 0)
            {
                PrintMessageAction?.Invoke(CannotMessage);
            } else
            {
                Debug.Log(CannotMessage);
            }
        }
    }
}