using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using TwitchIRC;

namespace Docsa
{
    public class PerkItem : Item
    {
        public List<Sprite> PerkSprites;
        public ItemType TargetPerk;

        [SerializeField] private SpriteRenderer _renderer;

        protected override void Awake()
        {
            base.Awake();
            OnValidate();
        }

        protected override void Reset()
        {
            base.Reset();
            _renderer = GetComponent<SpriteRenderer>();
        }

        void OnValidate()
        {
            switch (TargetPerk)
            {
                case ItemType.DocsaAttack :
                break;
                case ItemType.DocsaJump :
                break;
                case ItemType.HunterAttack :
                break;
                case ItemType.HunterJump :
                break;
                case ItemType.Attend :
                break;
                case ItemType.Exit :
                break;
                case ItemType.StarLight :
                break;
                case ItemType.HamaAttack :
                    _renderer.sprite = PerkSprites[0];
                break;
                case ItemType.HamaJump :
                    _renderer.sprite = PerkSprites[1];
                break;
                case ItemType.GrabDocsa :
                    _renderer.sprite = PerkSprites[2];
                break;
            }
        }


        public override void Effect()
        {
            switch(TargetPerk)
            {
                case ItemType.DocsaAttack :
                PerkManager.instance.Data.DocsaChimPerk.Enable();
                break;
                case ItemType.DocsaJump :
                PerkManager.instance.Data.DocsaJumpPerk.Enable();
                break;
                case ItemType.HunterAttack :
                PerkManager.instance.Data.HunterNetPerk.Enable();
                break;
                case ItemType.HunterJump :
                PerkManager.instance.Data.HunterJumpPerk.Enable();
                break;
                case ItemType.Attend :
                PerkManager.instance.Data.ViewerAttendPerk.Enable();
                break;
                case ItemType.Exit :
                PerkManager.instance.Data.ViewerExitPerk.Enable();
                break;
                case ItemType.StarLight :
                PerkManager.instance.Data.StarLightPerk.Enable();
                break;
                case ItemType.HamaAttack :
                PerkManager.instance.Data.UzuhamaAttackPerk.Enable();
                break;
                case ItemType.HamaJump :
                PerkManager.instance.Data.UzuhamaJumpPerk.Enable();
                break;
                case ItemType.GrabDocsa :
                PerkManager.instance.Data.UzuhamaGrabDocsaPerk.Enable();
                break;
            }

            base.Effect();
        }
    }
}