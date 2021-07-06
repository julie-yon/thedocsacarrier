using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Character
{
    public class UzuHama : Character
    {
        public Vector2 UzuHama_Position;
        public static UzuHama Hama
        {
            get {return GameObject.FindGameObjectWithTag("Player").GetComponent<UzuHama>();}
        }

        public void HamaAttack()
        {
            if(Input.GetMouseButtonDown(0))
            {   
                Behaviour.Attack();
            }
        }
        
        Rigidbody2D rigid;
        public GameObject CrouchGameObject;
        public GameObject StandGameObject;
    

        float directionThreashold = 0.01f;
        float uzuhamaRightScaleX = 1;
        float uzuhamaLeftScaleX = -1;
        bool isStand = true;

        

         
    
        void Crouch()
        {
            CrouchGameObject.SetActive(true);
            StandGameObject.SetActive(false);
            isStand = false;
        }

        void Stand()
        {
            StandGameObject.SetActive(true);
            CrouchGameObject.SetActive(false);
            isStand = true;
        }

        void FixedUpdate()
        {
            if (isStand)
            {
                if(Input.GetKeyDown(KeyCode.S)) Crouch();
            } else
            {
                if(Input.GetKeyUp(KeyCode.S)) Stand();
            }
            HammaMove();
        }

        public void HammaMove()
        {
            if(Input.GetKey(KeyCode.A)|Input.GetKey(KeyCode.D))
            {
                float moveDirection = Input.GetAxisRaw("Horizontal");   
                Behaviour.Move(moveDirection);
            }

            if(Input.GetKeyDown(KeyCode.W))
            {
                Behaviour.Jump();
            }

            
        }
        
        void Update()
        {
            HamaAttack();
            
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("ChunkTrigger"))
            {
                StageManager.instance.ChunkTriggerEnter(collider);
            }
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("ChunkTrigger"))
            {
                StageManager.instance.ChunkTriggerExit(collider);
            }
        }
    }
}



