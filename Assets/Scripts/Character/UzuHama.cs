using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using dkstlzu.Utility;

using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Docsa.Character
{
    [RequireComponent(typeof(UzuHamaBehaviour))]
    public class UzuHama : Character
    {
        public new UzuHamaBehaviour Behaviour
        {
            get {return (UzuHamaBehaviour)base.Behaviour;}
        }

        public static UzuHama Hama

        {
            get {return GameObject.FindGameObjectWithTag("Player").GetComponent<UzuHama>();}
        }
        public Baguni Baguni
        {
            get {return GetComponentInChildren<Baguni>();}
        }
        
        public IInteractable Interactable;
        public Animator EButtonAnimator;

        public int RescuedDocsaNumger = 0;

        private Core _core;
        protected override void Awake()
        {
            base.Awake();
            _core = Core.instance;

            _core.InputAsset.Player.Move.performed += HamaMove;
            _core.InputAsset.Player.Move.canceled += HamaMove;
            _core.InputAsset.Player.Jump.performed += HamaJump;
            _core.InputAsset.Player.Fire.performed += HamaAttack;
            _core.InputAsset.Player.GrabDocsa.performed += Behaviour.GrabDocsa;
            _core.InputAsset.Player.Interact.performed += Interact;
        }

        void OnDestroy()
        {
            _core.InputAsset.Player.Move.performed -= HamaMove;
            _core.InputAsset.Player.Move.canceled -= HamaMove;
            _core.InputAsset.Player.Jump.performed -= HamaJump;
            _core.InputAsset.Player.Fire.performed -= HamaAttack;
            _core.InputAsset.Player.GrabDocsa.performed -= Behaviour.GrabDocsa;
            _core.InputAsset.Player.Interact.performed -= Interact;
        }



        void HamaMove(Context context)
        {
            if (context.performed)
            {
                Behaviour.MoveDirection = context.ReadValue<float>();
            } else if (context.canceled)
            {
                Behaviour.MoveDirection = 0;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
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



