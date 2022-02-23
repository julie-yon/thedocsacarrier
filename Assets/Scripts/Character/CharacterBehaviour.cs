using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Utility;

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
        [Range(0, 90)] public float AimMaxAngle;

        [SerializeField] private Rigidbody2D _rigidbody;
        public Vector2 CurrentVelocity
        {
            get {return _rigidbody.velocity;}
        }
        
        float _directionThreashold = 0.01f;
        float _moveRightScaleX = -1;
        float _moveLeftScaleX = 1;

        [Header("GameObjects Refs")]
        public DocsaPoolType WeaponType;
        public Transform ProjectileEmitter = null;

        // Animator relatives
        [SerializeField] Animator _animator;

        private const string AttackTriggerName = "AttackTrigger";
        private const string JumpTriggerName = "JumpTrigger";
        private const string HitTriggerName = "HitTrigger";
        private const string DieTriggerName = "DieTrigger";
        private const string DieBoolName = "Die";

        void Reset()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();

            MaxSpeed = 3;
            MoveAcceleration = 1;
            JumpPower = 3;
        }

        public void AimToMouse(Transform targetTransform)
        {
            // Behaviour에 있어야 할까?
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 aimDirection = new Vector2(mousePos.x - targetTransform.position.x , mousePos.y - targetTransform.position.y);
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x);

            float realAngle = Mathf.Clamp(angle, -AimMaxAngle * Mathf.Deg2Rad, AimMaxAngle * Mathf.Deg2Rad);
            Vector3 realDirection = new Vector2(1, Mathf.Tan(realAngle));
            Matrix4x4 matrix = new Matrix4x4(new Vector4(0, Character.transform.localScale.x, 0, 0), new Vector4(-Character.transform.localScale.x, 0, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
            Vector3 realRealDirection = matrix.MultiplyPoint3x4(realDirection);
            targetTransform.right = realRealDirection;
            // print ($"aimDirection : {aimDirection}, angle : {angle * Mathf.Rad2Deg}, realAngle : {realAngle * Mathf.Rad2Deg}, realDirection : {realDirection}");
        }

        public void Attack(Vector2 direction)
        {   
            _animator.SetTrigger(AttackTriggerName);
            // if (Character is UzuHama)
            // {
            //     SpawnWeapon();
            // }
            // else if (Character is DocsaSakki)
            // {
            // }
        }

        /// <summary>
        /// this function will be used at animation component with animation event
        /// </summary>
        void SpawnWeapon()
        {
            ObjectPool.Initiater initiater = null;
            if (Character is UzuHama)
            {
                initiater = (weaponGameObject) => 
                {
                    Projectile weapon = weaponGameObject.GetComponent<Projectile>();
                    weapon.ShooterGameObject = Character.gameObject;
                };
            } else if(Character is DocsaSakki)
            {
                initiater = (chimGameObject) =>
                {
                    Projectile chim = chimGameObject.GetComponent<Projectile>();
                    chim.ShooterGameObject = Character.gameObject;
                    chim.Direction = -transform.right * transform.localScale.x;
                    print(chim.ShooterGameObject);
                    print(chim.Direction);
                };
            } else if (Character is Hunter hunter)
            {
                initiater = (netGameObject) => {
                    ProjectileNet net = netGameObject.GetComponent<ProjectileNet>();
                    net.ShooterGameObject = Character.gameObject;
                    net.TargetGameObject = hunter.FocusingDocsa.gameObject;
                };
            }

            if (initiater == null)
            {
                return;
            }

            ObjectPool.GetOrCreate(WeaponType).InstantiateAfterInit(ProjectileEmitter.position, ProjectileEmitter.rotation, initiater);
        }


        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            _animator.SetTrigger(JumpTriggerName);
        }

        public void JumpHead()
        {
            _rigidbody.AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
            _animator.SetTrigger(JumpTriggerName);
        }

        
        public void Move(float moveDirection)
        {
            if(_rigidbody.velocity.x > MaxSpeed) //Right Max Speed
                _rigidbody.velocity = new Vector2(MaxSpeed, _rigidbody.velocity.y);
            else if(_rigidbody.velocity.x < MaxSpeed * (-1)) //Left Max Speed
                _rigidbody.velocity = new Vector2(MaxSpeed * (-1), _rigidbody.velocity.y);

            if(_rigidbody.velocity.x > _directionThreashold)
            {
                transform.localScale = new Vector2(_moveRightScaleX , transform.localScale.y);
            }
            else if(_rigidbody.velocity.x < -_directionThreashold)
            {
                transform.localScale = new Vector2(_moveLeftScaleX , transform.localScale.y);
            }

            _rigidbody.AddForce(Vector2.right * MoveAcceleration * moveDirection, ForceMode2D.Impulse);
        }

        public void GrabDocsa(DocsaSakki targetDocsa)
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
            if (Character is UzuHama hama)
            {
                hama.Baguni.TemporaryOff(2);
            }
        }

        public void Die()
        {
            // 코루틴 정지
            Character.isDie = true;
            _animator.SetTrigger(DieTriggerName);
            _animator.SetBool(DieBoolName, true);
        }
    }
}