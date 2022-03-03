using UnityEngine;
using Utility;

namespace  Docsa.Character
{
    public class HunterBehaviour : CharacterBehaviour
    {
        public Hunter Hunter
        {
            get {return (Hunter)Character;}
        }

        /// <summary>
        /// this function will be used at animation component with animation event
        /// </summary>
        protected override void SpawnWeapon()
        {
            ObjectPool.Initiater preInitiater = null;
            preInitiater = (netGameObject) => {
                Net net = netGameObject.GetComponent<Net>();
                net.ShooterCharacter = Hunter;
                net.TargetPosition = Hunter.FocusingDocsa.transform.position;
            };

            ObjectPool.GetOrCreate(WeaponType).Instantiate(ProjectileEmitter.position, Quaternion.identity, preInitiater);
        }

        public override void Jump()
        {
            if (JumpCount < MaxJumps)
            {
                _rigidbody.AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
                Animator.SetTrigger(JumpTriggerName);
            }
        }
    }
}