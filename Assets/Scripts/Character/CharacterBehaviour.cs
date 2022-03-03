using UnityEngine.InputSystem;
using UnityEngine;
using Utility;

using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace  Docsa.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterBehaviour : MonoBehaviour
    {
        public Character Character;

        [Header("Behaviour stats")]
        public float MaxSpeed;
        public float MoveAcceleration;
        public float JumpPower;
        public int MaxJumps;
        public int JumpCount;
        [Range(0, 90)] public float AimMaxAngle;

        [SerializeField] protected Rigidbody2D _rigidbody;
        public Vector2 CurrentVelocity
        {
            get {return _rigidbody.velocity;}
        }
        
        [Header("GameObjects Refs")]
        public DocsaPoolType WeaponType;
        public Transform ProjectileEmitter = null;

        // Animator relatives
        public Animator Animator;

        protected const string AttackTriggerName = "AttackTrigger";
        protected const string JumpTriggerName = "JumpTrigger";
        protected const string HitTriggerName = "HitTrigger";
        protected const string DieTriggerName = "DieTrigger";
        protected const string DieBoolName = "Die";
        protected const string MoveBoolName = "Move";
        protected const string BlendValueName = "ScaleBlend";

        protected virtual void Reset()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponentInChildren<Animator>();

            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            MaxSpeed = 3;
            MoveAcceleration = 1;
            JumpPower = 3;
            MaxJumps = 1;
            JumpCount = 0;
        }

        public virtual void AimToMouse(Transform targetTransform)
        {
        }

        public virtual void Attack(Vector2 direction)
        {   
            Animator.SetTrigger(AttackTriggerName);
            SpawnWeapon();
        }

        /// <summary>
        /// this function will be used at animation component with animation event
        /// </summary>
        protected virtual void SpawnWeapon()
        {
            ObjectPool.Initiater preInitiater = null;
            if (Character is UzuHama)
            {
                preInitiater = (weaponGameObject) => 
                {
                    Projectile weapon = weaponGameObject.GetComponent<Projectile>();
                    weapon.ShooterCharacter = Character;
                    weapon.TargetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                };
            } else if(Character is DocsaSakki)
            {
                preInitiater = (chimGameObject) =>
                {
                    Projectile chim = chimGameObject.GetComponent<Projectile>();
                    chim.ShooterCharacter = Character;
                    chim.Direction = -transform.right * transform.localScale.x;
                    print(chim.ShooterCharacter);
                    print(chim.Direction);
                };
            } else if (Character is Hunter hunter)
            {
                preInitiater = (netGameObject) => {
                    Net net = netGameObject.GetComponent<Net>();
                    net.ShooterCharacter = Character;
                    net.TargetPosition = hunter.FocusingDocsa.transform.position;
                };
            }

            if (preInitiater == null)
            {
                return;
            }

            ObjectPool.GetOrCreate(WeaponType).Instantiate(ProjectileEmitter.position, Quaternion.identity, preInitiater);
        }


        public virtual void Jump()
        {
            JumpCount++;
            if (JumpCount <= MaxJumps)
            {
                _rigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                Animator.SetTrigger(JumpTriggerName);
            }
        }

        public virtual void Move(float moveDirection)
        {
            transform.Translate(new Vector3(moveDirection, 0, 0));
        }

        public LayerMask GrabDocsaLayerMask;

        public virtual void GrabDocsa(DocsaSakki targetDocsa)
        {
            BezierCurve BGCurve = BezierCurve.ParabolaFromTo(targetDocsa.transform, false, Character.GrabDocsaPosition, true);
            var trs = BGCurve.AddTRS(targetDocsa.transform);
            BGCurve.Curve.AddField("Scale", BansheeGz.BGSpline.Curve.BGCurvePointField.TypeEnum.Vector3);
            BGCurve.Curve[0].SetField("Scale", new Vector3(1, 1, 1), typeof(Vector3));
            BGCurve.Curve[1].SetField("Scale", new Vector3(0.75f, 0.75f, 0.75f), typeof(Vector3));
            BGCurve.Curve[2].SetField("Scale", new Vector3(0.5f, 0.5f, 0.5f), typeof(Vector3));
            trs.OverflowControl = BansheeGz.BGSpline.Components.BGCcTrs.OverflowControlEnum.Stop;
            trs.ScaleObject = true;
            trs.ScaleField = BGCurve.Curve.GetField("Scale");
            BGCurve.gameObject.AddComponent<Docsa.Events.GrabDocsaCoroutine>().Cursor = BGCurve.Cursor;
            Destroy(BGCurve.gameObject, 2);
        }

        public virtual void Die()
        {
            Character.isDie = true;
            Animator.SetTrigger(DieTriggerName);
            Animator.SetBool(DieBoolName, true);
        }
    }
}