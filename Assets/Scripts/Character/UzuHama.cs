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
        public int RescuedDocsaNumger = 0;
        public HamaInput InputAsset;
        public HamaInput.PlayingActions PlayingActionMap => InputAsset.Playing;

        public static void AdjustInputAsset()
        {
            Hama.InputAsset.Enable();
            // if (!DocsaSakkiManager.instance.CorrectlyAssigned)
            // {
            //     PlayerActionMap.Disable();
            //     UIActionMap.Enable();
            // } else if (DocsaSakkiManager.instance.CorrectlyAssigned && !ESCUIManager.instance.isOn)
            // {
            //     PlayerActionMap.Enable();
            //     UIActionMap.Disable();
            // } else if (DocsaSakkiManager.instance.CorrectlyAssigned && ESCUIManager.instance.isOn)
            // {
            //     PlayerActionMap.Disable();
            //     UIActionMap.Enable();
            // }
        }

        protected override void Awake()
        {
            base.Awake();
            print("UzuHama Awake");
            InputAsset = new HamaInput();
            PlayingActionMap.Move.performed += HamaMove;
            PlayingActionMap.Move.canceled += HamaMove;
            PlayingActionMap.Jump.performed += HamaJump;
            PlayingActionMap.Fire.performed += HamaAttack;
            PlayingActionMap.GrabDocsa.performed += GrabDocsa;
            PlayingActionMap.Interact.performed += Interact;
            PlayingActionMap.ESC.performed += OnESCPerformed;
        }

        void OnEnable()
        {
            print("UzuHama Enable");
            InputAsset.Enable();
            PlayingActionMap.Enable();
            // AdjustInputAsset();
        }

        void OnDisable()
        {
            InputAsset.Disable();
        }

        void HamaMove(Context context)
        {
            Printer.DebugPrint("HamaMove Input");
            if (context.performed)
            {
                Behaviour.MoveDirection = Vector2.right * context.ReadValue<float>();
            } else if (context.canceled)
            {
                Behaviour.MoveDirection = Vector2.zero;
            }
        }

        void HamaJump(Context context)
        {
            Printer.DebugPrint("HamaJump Input");
            Behaviour.Jump();
        }
        
        void HamaAttack(Context context)
        {
            Printer.DebugPrint("HamaAttack Input");
            Behaviour.Attack(Mouse.current.position.ReadValue());
        }

        void Interact(Context context)
        {
            Printer.DebugPrint("HamaInteract Input");
            if (Interactable != null)
            {
                Interactable.Interact();
            }
        }

        public void GrabDocsa(Context context)
        {
            Printer.DebugPrint("GrabDocsa Input");
            RaycastHit2D hit2D = Physics2D.BoxCast(Hama.transform.position, Behaviour.GrabDocsaBoxCastSize, 0, Vector2.right, 0, Behaviour.GrabDocsaLayerMask);
            if (hit2D.collider != null) Behaviour.GrabDocsa(hit2D.transform.GetComponent<DocsaSakki>());
        }

        void OnESCPerformed(Context context)
        {
            Printer.DebugPrint("ESC Input");
            if (!ESCUIManager.instance.UIOpener.isOpened) ESCUIManager.instance.UIOpener.Open();
        }
    }
}



