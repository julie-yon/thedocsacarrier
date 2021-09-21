using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Docsa.Character;

namespace Docsa.Gimmick
{
    public class Rock : Gimmick
    {
        public int DamageValue = 10;
        protected override void GimmickInvoke()
        {
            base.GimmickInvoke();
            GiveDamage(DamageValue);
            Bounce(UzuHama.Hama);
        }

        void OnTriggerEnter2D(Collider2D otherCol)
        {
            int includeLayer = 1<<10;
            if((1<<otherCol.gameObject.layer & includeLayer) != 0)
            {
                GimmickInvoke();
            }
        }
        

        private void Bounce(UzuHama uzuHama) //부딪혔을 때 튕기기
        {
            uzuHama.PlayBounceAnimation();
        } //이건 에니메이션으로 구현하기! 
    }
}

