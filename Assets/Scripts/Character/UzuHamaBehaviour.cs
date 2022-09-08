using UnityEngine.InputSystem;
using UnityEngine;
using dkstlzu.Utility;

namespace  Docsa.Character
{
    public class UzuHamaBehaviour : CharacterBehaviour
    {
        [Header("Behaviour stats")]
        public float MaxSpeed;
        public float MoveAcceleration;
        public int MaxJumps;
        public int JumpCount;
        public Vector2 MoveDirection;

        public UzuHama Hama
        {
            get {return (UzuHama)Character;}
        }

        void Update()
        {
            if (JumpCount > 0 && _rigidbody.velocity.y < 0)
            {
                Animator.SetBool("Falling", true);
            } else
            {
                Animator.SetBool("Falling", false);
            }
        }

        void FixedUpdate()
        {
            Move(MoveDirection);
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

        void OnCollisionEnter2D(Collision2D other)
        {
            RaycastHit2D _hit;
            _hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, LayerMask.GetMask("Ground"));
            if (_hit && _hit.distance < 0.4)
            {
                JumpCount = 0;
            }
        }

        private float noMoveTime = 0;
        public float NoMoveTimeThreshold = 0.3f;
        public float NoMoveThreshold = 0.3f;

        public override void Move(Vector2 moveDirection)
        {
            if (_rigidbody.velocity.magnitude < NoMoveThreshold)
            {
                noMoveTime += Time.deltaTime;
            } else
            {
                noMoveTime = 0f;
            }

            _rigidbody.velocity = new Vector2(Mathf.Clamp(_rigidbody.velocity.x, -MaxSpeed, MaxSpeed), _rigidbody.velocity.y);

            if (Hama.PlayingActionMap.Move.IsPressed())
            {
                Animator.SetFloat(BlendValueName, Mathf.Abs(_rigidbody.velocity.x / MaxSpeed) < 0.1f ? 0.1f : _rigidbody.velocity.x / MaxSpeed);
            }

            _rigidbody.AddForce(MoveAcceleration * moveDirection, ForceMode2D.Impulse);
            if (moveDirection.magnitude == 0 && noMoveTime > NoMoveTimeThreshold)
            {
                Animator.SetBool(MoveBoolName, false);
            } else
            {
                Animator.SetBool(MoveBoolName, true);
            }
        }

        public Vector2 GrabDocsaBoxCastSize = new Vector2(2, 2);
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