using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using Context = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Docsa.Character
{
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

        void Start()
        {
            Core.instance.InputAsset.Player.Move.performed += HamaMove;
            Core.instance.InputAsset.Player.Move.canceled += HamaMove;
            Core.instance.InputAsset.Player.Jump.performed += HamaJump;
            Core.instance.InputAsset.Player.Fire.performed += HamaAttack;
        }

        void Update()
        {
            Behaviour.AimToMouse(Behaviour.ProjectileEmitter);
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

        void HamaJump(Context context)
        {
            Behaviour.Jump();
        }

        void HamaAttack(Context context)
        {
            Behaviour.Attack(Mouse.current.position.ReadValue());
        }
    }
}



