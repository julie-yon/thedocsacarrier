using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public delegate void NetInitiater(Docsa.ProjectileNet net);

namespace  Docsa.Character
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterBehaviour : MonoBehaviour
    {
        public Character Character;

        [Header("Behaviour stats")]
        public float MaxSpeed = 3;
        public float MoveAcceleration = 1;
        public float JumpPower = 3;
        public bool isDie = false;
        Rigidbody2D rigid;
        public Vector2 CurrentVelocity
        {
            get {return rigid.velocity;}
        }
        float directionThreashold = 0.01f;
        float _moveRightScaleX = 1;
        float _moveLeftScaleX = -1;
        [Header("GameObjects Refs")]
        [SerializeField] Transform _projectileEmitter = null;

        
        void Awake()
        {
            if (!transform.TryGetComponent<Rigidbody2D>(out rigid))
            {
                rigid = gameObject.AddComponent<Rigidbody2D>();
            }
        }

        public void LookAtMouse()
        {
            // Behaviour에 있어야 할까?
            Vector2 t_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 t_direction = new Vector2(t_mousePos.x - _projectileEmitter.position.x ,
                                        t_mousePos.y - _projectileEmitter.position.y); //무기가 바라볼 방향 설정(마우스 클릭한 곳에서 우주하마의 위치 빼기)
            _projectileEmitter.right = t_direction;

        }

        public void Attack()
        {   
            GameObject t_weapon = ObjectPool.SPoolDict[PoolType.Weapon].Instantiate(_projectileEmitter.position, _projectileEmitter.rotation);

        }


        public void ThrowNet(DocsaSakki targetDocsa)
        {
            NetInitiater netInitiater = (net) => {
                net.Target = targetDocsa;
                net.Shooter = (Hunter)Character;
            };
            ObjectPool.SPoolDict[PoolType.Net].InstantiateAfterInit(_projectileEmitter.position, _projectileEmitter.rotation, netInitiater);
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

            if(rigid.velocity.x > directionThreashold)
            {
                transform.localScale = new Vector2(_moveRightScaleX , transform.localScale.y);
            }
            else if(rigid.velocity.x < -directionThreashold)
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
            isDie = true;

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