using UnityEngine;
using Utility;

namespace  Docsa.Character
{
    public class DocsaBehaviour : CharacterBehaviour
    {
        public DocsaSakki DocsaSakki
        {
            get {return (DocsaSakki)Character;}
        }

        /// <summary>
        /// this function will be used at animation component with animation event
        /// </summary>
        protected override void SpawnWeapon()
        {
            ObjectPool.Initiater preInitiater = null; 
            preInitiater = (chimGameObject) =>
            {
                Projectile chim = chimGameObject.GetComponent<Projectile>();
                chim.ShooterCharacter = DocsaSakki;
                chim.Direction = -transform.right * transform.localScale.x;
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