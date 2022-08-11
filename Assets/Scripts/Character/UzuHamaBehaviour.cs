using UnityEngine.InputSystem;
using UnityEngine;
using dkstlzu.Utility;

using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace  Docsa.Character
{
    public class UzuHamaBehaviour : CharacterBehaviour
    {
        public UzuHama Hama
        {
            get {return (UzuHama)Character;}
        }

        void Update()
        {
            if (JumpCount > 0 && CurrentVelocity.y < 0)
            {
                Animator.SetBool("Falling", true);
            } else
            {
                Animator.SetBool("Falling", false);
            }
        }

        public override void Attack(Vector2 direction)
        {
            if (!PerkManager.instance.Data.UzuhamaAttackPerk.enabled)
            {
                PerkManager.instance.Data.UzuhamaAttackPerk.PrintCannotMessage(Character.transform.position);
                return;
            }
            base.Attack(direction);
        }

        /// <summary>
        /// this function will be used at animation component with animation event
        /// </summary>
        protected override void SpawnWeapon()
        {
            ObjectPool.Initiater preInitiater = null;
            // ObjectPool.Initiater postInitiater = null;
            preInitiater = (weaponGameObject) => 
            {
                Projectile weapon = weaponGameObject.GetComponent<Projectile>();
                weapon.ShooterCharacter = Hama;
                weapon.TargetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            };

            ObjectPool.GetOrCreate(WeaponType).Instantiate(ProjectileEmitter.position, Quaternion.identity, preInitiater);
        }

        public override void Jump()
        {
            if (!PerkManager.instance.Data.UzuhamaJumpPerk.enabled)
            {
                PerkManager.instance.Data.UzuhamaJumpPerk.PrintCannotMessage(Character.transform.position);
                return;
            }
            base.Jump();
        }

        private float noMoveTime = 0;
        public float NoMoveTimeThreshold = 0.3f;
        public float NoMoveThreshold = 0.3f;
        public GameObject SpriteObject;
        public Vector2 HamaMoveBoxCastSize = new Vector2(0.1f, 0.8f);
        public float HamaMoveBoxCastDistance = 1f;
        public LayerMask UzuhamaCollisionLayerMask;

        public override void Move(float moveDirection)
        {
            if (_rigidbody.velocity.magnitude < NoMoveThreshold)
            {
                noMoveTime += Time.deltaTime;
            } else
            {
                noMoveTime = 0f;
            }

            if(_rigidbody.velocity.x > MaxSpeed) //Right Max Speed
                _rigidbody.velocity = new Vector2(MaxSpeed, _rigidbody.velocity.y);
            else if(_rigidbody.velocity.x < MaxSpeed * (-1)) //Left Max Speed
                _rigidbody.velocity = new Vector2(MaxSpeed * (-1), _rigidbody.velocity.y);



            if (Core.instance.InputAsset.Player.Move.IsPressed())
                Animator.SetFloat(BlendValueName, Mathf.Clamp(_rigidbody.velocity.x, -3, 3) == 0 ? 0.2f : _rigidbody.velocity.x);

            _rigidbody.AddForce(Vector2.right * MoveAcceleration * moveDirection, ForceMode2D.Impulse);
            if (moveDirection == 0)
            {
                if (noMoveTime > NoMoveTimeThreshold)
                    Animator.SetBool(MoveBoolName, false);
            } else
            {
                Animator.SetBool(MoveBoolName, true);
            }

        }

        public Vector2 GrabDocsaBoxCastSize = new Vector2(2, 2);
        public GameObject GrabDocsaRagDoll;
        public void GrabDocsa(Context context)
        {
            RaycastHit2D hit2D = Physics2D.BoxCast(Hama.transform.position, GrabDocsaBoxCastSize, 0, Vector2.right, 0, GrabDocsaLayerMask);
            if (hit2D.collider != null) GrabDocsa(hit2D.transform.GetComponent<DocsaSakki>());
        }

        public override void GrabDocsa(DocsaSakki targetDocsa)
        {
            if (!PerkManager.instance.Data.UzuhamaGrabDocsaPerk.enabled) 
            {
                PerkManager.instance.Data.UzuhamaGrabDocsaPerk.PrintCannotMessage(Character.transform.position);
                return;
            }
            
            Hama.RescuedDocsaNumger++;
            // ObjectPool.GetOrCreate(DocsaPoolType.Docsa).Return(targetDocsa.gameObject);
            // var temp = Instantiate(GrabDocsaRagDoll, targetDocsa.position, Quaternion.identity);
            // temp.transform.SetParent(Hama.GrabDocsaPosition);

            // base.GrabDocsa(temp.transform);

            targetDocsa.transform.SetParent(Hama.GrabDocsaPosition);

            base.GrabDocsa(targetDocsa);
        }
    }
}