using UnityEngine;
using dkstlzu.Utility;

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
                net.TargetPosition = Hunter.AttackTarget.position;
            };

            ObjectPool.GetOrCreate(WeaponType).Instantiate(ProjectileEmitter.position, Quaternion.identity, preInitiater);
        }
    }
}