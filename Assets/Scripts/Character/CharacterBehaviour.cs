using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

using UnityEngine.InputSystem;

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

        [SerializeField] private Rigidbody2D rigid;
        public Vector2 CurrentVelocity
        {
            get {return rigid.velocity;}
        }
        
        float _directionThreashold = 0.01f;
        float _moveRightScaleX = 1;
        float _moveLeftScaleX = -1;

        [Header("GameObjects Refs")]
        public DocsaPoolType WeaponType;
        [SerializeField] Transform _projectileEmitter = null;

        [SerializeField] List<string> animationList = new List<string>();
        [SerializeField] Animation _animation;

        void Reset()
        {
            rigid = GetComponent<Rigidbody2D>();
            _animation = GetComponent<Animation>();
            _animation.wrapMode = WrapMode.Once;
            foreach(AnimationState docaniState in _animation)
            {
                animationList.Add(docaniState.name);
            }
            MaxSpeed = 3;
            MoveAcceleration = 1;
            JumpPower = 3;
        }

        public void AimToMouse()
        {
            // Behaviour에 있어야 할까?
            Vector2 t_mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 t_direction = new Vector2(t_mousePos.x - _projectileEmitter.position.x ,
                                        t_mousePos.y - _projectileEmitter.position.y); //무기가 바라볼 방향 설정(마우스 클릭한 곳에서 우주하마의 위치 빼기)
            _projectileEmitter.right = t_direction;

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
                // GameObject t_weapon = ObjectPool.GetOrCreate(DocsaPoolType.Weapon).Instantiate(_projectileEmitter.position, _projectileEmitter.rotation);

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

                // GameObject t_weapon = ObjectPool.GetOrCreate(DocsaPoolType.Chim).Instantiate(_projectileEmitter.position, _projectileEmitter.rotation);
            } else if (Character is Hunter hunter)
            {
                initiater = (netGameObject) => {
                    ProjectileNet net = netGameObject.GetComponent<ProjectileNet>();
                    net.ShooterGameObject = Character.gameObject;
                    net.TargetGameObject = hunter.FocusingDocsa.gameObject;
                };
                // ObjectPool.GetOrCreate(DocsaPoolType.Net).InstantiateAfterInit(_projectileEmitter.position, _projectileEmitter.rotation, netInitiater);

            }

            if (initiater == null)
            {
                return;
            }

            ObjectPool.GetOrCreate(WeaponType).InstantiateAfterInit(_projectileEmitter.position, _projectileEmitter.rotation, initiater);
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
            targetDocsa.OriginalParent = targetDocsa.transform.parent;
            targetDocsa.transform.SetParent(Character.GrabDocsaPosition.transform);
            targetDocsa.transform.position = Character.GrabDocsaPosition.position;
        }

        public void Die()
        {
            // 코루틴 정지
            Character.isDie = true;

            // Y축 반전
            SpriteRenderer renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            renderer.flipY = true;

            // 낙하
            BoxCollider2D coll = gameObject.GetComponent<BoxCollider2D>();
            coll.enabled = false;

            // 바운스
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            Vector2 dieVelocity = new Vector2(0, 15f);
            rigid.AddForce(dieVelocity, ForceMode2D.Impulse);

            // 오브젝트 삭제
            Destroy(gameObject, 5f);

        }
    }
}