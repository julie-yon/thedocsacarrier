using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Docsa.Character
{
    public class UzuHama : Character
    {
        public static UzuHama Hama
        {
            get {return GameObject.FindGameObjectWithTag("Player").GetComponent<UzuHama>();}
        }

        public GameObject StandGameObject;
        public GameObject CrouchGameObject;
        public Baguni Baguni;

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
            if (Core.instance.UserInputEnable)
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
        }

        void Update()
        {
            if (Core.instance.UserInputEnable)
            {
                HamaAttack();
                Behaviour.LookAtMouse();
            }
        }

        public void HammaMove()
        {
            if(Input.GetKey(KeyCode.A)|Input.GetKey(KeyCode.D))
            {
                float moveDirection = Input.GetAxisRaw("Horizontal");   
                Behaviour.Move(moveDirection);
            }

            if(Input.GetButtonDown("Jump"))
            {
                Behaviour.Jump();
            }
        }

        public void HamaAttack()
        {
            if(Input.GetMouseButtonDown(0))
            {   
                Behaviour.Attack();
            }
        }

        public void PlayBounceAnimation()
        {

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



