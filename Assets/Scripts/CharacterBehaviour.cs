using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Docsa.Character
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterBehaviour : MonoBehaviour
    {
        public float MoveSpeed = 1;
        public float MaxSpeed = 3;
        public float JumpPower = 3;
        Rigidbody2D rigid;
        float directionThreashold = 0.01f;
        float uzuhamaRightScaleX = 1;
        float uzuhamaLeftScaleX = -1;
        Vector2 MousePosition;
        Camera Camera;
        [SerializeField] GameObject m_goPrefab = null;
        [SerializeField] Transform m_tfWeapon = null;
        
        void Awake()
        {
            if (!transform.TryGetComponent<Rigidbody2D>(out rigid))
            {
                rigid = gameObject.AddComponent<Rigidbody2D>();
            }

            Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        void LookAtMouse()
        {
            Vector2 t_mousePos = Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 t_direction = new Vector2(t_mousePos.x - m_tfWeapon.position.x ,
            t_mousePos.y - m_tfWeapon.position.y); //무기가 바라볼 방향 설정(마우스 클릭한 곳에서 우주하마의 위치 빼기)
            m_tfWeapon.right = t_direction;

        }

        public void Attack()
        {   
            MousePosition = Input.mousePosition;
            
            GameObject t_weapon = Instantiate(m_goPrefab, m_tfWeapon.position, m_tfWeapon.rotation);
            t_weapon.GetComponent<Rigidbody2D>().velocity = t_weapon.transform.right * 10f;

        }

        public void ThrowNet()
        {

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
                transform.localScale = new Vector2(uzuhamaRightScaleX , transform.localScale.y);
            else if(rigid.velocity.x < -directionThreashold)
                transform.localScale = new Vector2(uzuhamaLeftScaleX , transform.localScale.y);

            rigid.AddForce(Vector2.right * MoveSpeed * moveDirection, ForceMode2D.Impulse);
        }

        public void GrabDocsa(Docsa targetDocsa, UzuHama catcherUzuhama, Hunter catcherHunter)
        {
            //if (targetDocsa.targetDocPosition.position == UzuHama_Position.position)
            //{
            //    targetDocsa = UzuHama.Baguni
            //}
            // 
        }

        public void Die()
        {
            
        }

        void Update()
        {
            LookAtMouse();
            
            if(!Input.GetButton("Horizontal"))
            {
                rigid.velocity = new Vector2( 0.5f * rigid.velocity.normalized.x , rigid.velocity.y);
            }
        }
    }
}