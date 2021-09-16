using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Docsa.Gimmick
{
    public class Basalt : Gimmick
    {
        protected override void GimmickInvoke()
        {
            base.GimmickInvoke();
        }

        protected override void GiveDamage()
        {
            base.GiveDamage();
        }

        // private void HitHama() //하마 공격
        // {

        // }

        //rigidbody 이용해서 움직임 구현

        private void Disappear() //공격 후 사라지기 or 공격을 피했다면, 쭉 가서 카메라 밖으로 사라짐.
        {

        }
    }
}


