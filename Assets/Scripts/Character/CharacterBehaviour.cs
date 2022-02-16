using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Utility;

namespace  Docsa.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animation))]
    public class CharacterBehaviour : MonoBehaviour
    {
        public Character Character;

        [Header("Behaviour stats")]
        public float MaxSpeed;
        public float MoveAcceleration;
        public float JumpPower;
        [Range(0, 90)] public float AimMaxAngle;

        [SerializeField] private Rigidbody2D rigid;
        public Vector2 CurrentVelocity
        {
            get {return rigid.velocity;}
        }
        
        float _directionThreashold = 0.01f;
        float _moveRightScaleX = -1;
        float _moveLeftScaleX = 1;

        [Header("GameObjects Refs")]
        public DocsaPoolType WeaponType;
        public Transform ProjectileEmitter = null;

        [SerializeField] List<string> animationList = new List<string>();
        [SerializeField] Animation _animation;

        void Reset()
        {
            rigid = GetComponent<Rigidbody2D>();
            _animation = GetComponent<Animation>();
            _animation.wrapMode = WrapMode.Once;
            _animation.playAutomatically = false;
            foreach(AnimationState docaniState in _animation)
            {
                animationList.Add(docaniState.name);
            }
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
            if (Character is UzuHama)
            {
                SpawnWeapon();
            }
            else if (Character is DocsaSakki)
            {
                if (!_animation.isPlaying)
                {
                    _animation.Play(animationList[0]);
                }
                // _animation.Play("DocsaChim", 0, 0.25f);
                // _animation.Play(animationList[0], PlayMode.StopSameLayer);
                // animator.Play("DocsaChim",-1, 0f);
                
            }
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
            rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
        }

        public void JumpHead()
        {
            rigid.AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
        }

        
        public void Move(float moveDirection)
        {
            if(rigid.velocity.x > MaxSpeed) //Right Max Speed
                rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
            else if(rigid.velocity.x < MaxSpeed * (-1)) //Left Max Speed
                rigid.velocity = new Vector2(MaxSpeed * (-1), rigid.velocity.y);

            if(rigid.velocity.x > _directionThreashold)
            {
                transform.localScale = new Vector2(_moveRightScaleX , transform.localScale.y);
            }
            else if(rigid.velocity.x < -_directionThreashold)
            {
                transform.localScale = new Vector2(_moveLeftScaleX , transform.localScale.y);
            }

            rigid.AddForce(Vector2.right * MoveAcceleration * moveDirection, ForceMode2D.Impulse);
        }

        public void GrabDocsa(DocsaSakki targetDocsa)
        {
            BezierCurve BGCurve = BezierCurve.ParabolaFromTo(targetDocsa.transform, false, Character.GrabDocsaPosition, true);
            BGCurve.AddCursorTranslate(targetDocsa.transform);
            BGCurve.gameObject.AddComponent<Docsa.Events.GrabDocsaCoroutine>().Cursor = BGCurve.Cursor;
            if (Character is UzuHama hama)
            {
                hama.Baguni.TemporaryOff(3);
            }
        }

        public void Die()
        {
            // 코루틴 정지
            Character.isDie = true;
        }
    }
}