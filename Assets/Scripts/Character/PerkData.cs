using UnityEngine;

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

        // [ContextMenuItem("Init", "Init")]
        public void Init()
        {
            DocsaChimPerk.PerkType = ItemType.DocsaAttack;
            DocsaJumpPerk.PerkType = ItemType.DocsaJump;
            HunterNetPerk.PerkType = ItemType.HunterAttack;
            HunterJumpPerk.PerkType = ItemType.HunterJump;
            ViewerAttendPerk.PerkType = ItemType.Attend;
            ViewerExitPerk.PerkType = ItemType.Exit;
            StarLightPerk.PerkType = ItemType.StarLight;

            UzuhamaAttackPerk.PerkType = ItemType.HamaAttack;
            UzuhamaJumpPerk.PerkType = ItemType.HamaJump;
            UzuhamaGrabDocsaPerk.PerkType = ItemType.GrabDocsa;
            UzuhamaBaguniPerk.PerkType = ItemType.HamaBaguni;
        }
    }

    [System.Serializable]
    public struct Perk
    {
        public ItemType PerkType;
        public bool enabled;

        public Perk(Perk perk)
        {
            PerkType = perk.PerkType;
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

        public void PrintCannotMessage(Vector3 position)
        {
            switch (PerkType)
            {
                case ItemType.Attend :
                FloatingTextManager.instance.MakeNewText(position, "????????? ????????? ???????????? ???????????????.");
                break;
                case ItemType.DocsaAttack :
                FloatingTextManager.instance.MakeNewText(position, "?????? ??????");
                break;
                case ItemType.DocsaJump :
                FloatingTextManager.instance.MakeNewText(position, "?????? ??????");
                break;
                case ItemType.Exit :
                FloatingTextManager.instance.MakeNewText(position, "????????? ????????? ???????????? ???????????????.");
                break;
                case ItemType.GrabDocsa :
                FloatingTextManager.instance.MakeNewText(position, "?????? ??? ??????");
                break;
                case ItemType.HamaAttack :
                FloatingTextManager.instance.MakeNewText(position, "?????? ??????");
                break;
                case ItemType.HamaBaguni :
                FloatingTextManager.instance.MakeNewText(position, "????????? ?????? ?????? ??????");
                break;
                case ItemType.HamaJump :
                FloatingTextManager.instance.MakeNewText(position, "?????? ??????");
                break;
                case ItemType.HunterAttack :
                FloatingTextManager.instance.MakeNewText(position, "?????? ??????");
                break;
                case ItemType.HunterJump :
                FloatingTextManager.instance.MakeNewText(position, "?????? ??????");
                break;
                case ItemType.StarLight :
                FloatingTextManager.instance.MakeNewText(position, "????????? ?????? ??? ??????");
                break;
            }
        }
    }
}