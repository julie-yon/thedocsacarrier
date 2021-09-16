using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Docsa.Gimmick
{
    public class VolcanicAsh : Gimmick
    {
        protected override void GimmickInvoke()
        {
            base.GimmickInvoke();
        }

        protected override void GiveDamage() //화산재가 우주하마에게 닿으면, 데미지
        {
            base.GiveDamage();
        }


        private void Fly() //공격 이후에도 계속 움직임. 우에서 좌로 
        {

        }


    }
}

