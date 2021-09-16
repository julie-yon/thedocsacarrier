using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Docsa.Gimmick
{
    public class Rock : Gimmick
    {
        protected override void GimmickInvoke()
        {
            base.GimmickInvoke();
        }

        private bool HamaCrush() //하마가 부딪혔는지
        {
            return false;
        }

        protected override void GiveDamage() //데미지 얼마를 줄지
        {
            base.GiveDamage();
        }

        private void Bounce() //부딪혔을 때 튕기기
        {

        } //이건 에니메이션으로 구현하기! 
    }
}

