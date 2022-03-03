using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Docsa.Character
{
    [RequireComponent(typeof(UzuHamaBehaviour))]
    public class UzuHama : Character
    {
        public static UzuHama Hama

        {
            get {return GameObject.FindGameObjectWithTag("Player").GetComponent<UzuHama>();}
        }
        public Baguni Baguni
        {
            get {return GetComponentInChildren<Baguni>();}
        }
        
        float moveDirection;
        public IInteractable Interactable;
        public Animator EButtonAnimator;

        void Start()
        {
            Core.instance.InputAsset.Player.Move.performed += HamaMove;
            Core.instance.InputAsset.Player.Move.canceled += HamaMove;
            Core.instance.InputAsset.Player.Jump.performed += HamaJump;
            Core.instance.InputAsset.Player.Fire.performed += HamaAttack;
            Core.instance.InputAsset.Player.GrabDocsa.performed += ((UzuHamaBehaviour)Behaviour).GrabDocsa;
            Core.instance.InputAsset.Player.Interact.performed += Interact;
        }

        void FixedUpdate()
        {
            Behaviour.Move(moveDirection);
        }

        void HamaMove(Context context)
        {
            if (context.performed)
            {
                moveDirection = context.ReadValue<float>();
            } else if (context.canceled)
            {
                moveDirection = 0;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            RaycastHit2D _hit;
            _hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, 1<<17);
            //Debug.Log(_hit.distance);
            if (_hit && _hit.distance < 0.4)
            {
                //Debug.Log(_hit.distance);
                Behaviour.JumpCount = 0;
            }
        }

        void HamaJump(Context context)
        {
            Behaviour.JumpCount += 1;
            Behaviour.Jump();
        }
        
        void HamaAttack(Context context)
        {
            Behaviour.Attack(Mouse.current.position.ReadValue());
        }

        void Interact(Context context)
        {
            if (Interactable != null)
            {
                Interactable.Interact();
            }
        }
    }
}



