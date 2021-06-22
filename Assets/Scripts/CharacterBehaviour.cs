using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public float MoveSpeed = 1;
    public float MaxSpeed = 3;
    public float JumpPower = 7;
    Rigidbody2D rigid;
    float directionThreashold = 0.01f;
    float uzuhamaRightScaleX = 1;
    float uzuhamaLeftScaleX = -1;
   
    void Awake()
    {
        if (!transform.TryGetComponent<Rigidbody2D>(out rigid))
        {
            rigid = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    public void Attack()
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
        rigid.AddForce(Vector2.right * MoveSpeed * moveDirection, ForceMode2D.Impulse);

        if(rigid.velocity.x > MaxSpeed) //Right Max Speed
            rigid.velocity = new Vector2(MaxSpeed, rigid.velocity.y);
        else if(rigid.velocity.x < MaxSpeed * (-1)) //Left Max Speed
            rigid.velocity = new Vector2(MaxSpeed * (-1), rigid.velocity.y);

        if(rigid.velocity.x > directionThreashold)
            transform.localScale = new Vector2(uzuhamaRightScaleX , transform.localScale.y);
        else if(rigid.velocity.x < -directionThreashold)
            transform.localScale = new Vector2(uzuhamaLeftScaleX , transform.localScale.y);

    }

    public void GrabDocsa(Docsa targetDocsa)
    {

    }

    public void Die()
    {

    }
}
