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

        public GameObject StandGameObject;
        public GameObject CrouchGameObject;
        public Baguni Baguni;

        bool isStand = true;

        float moveDirection;

        void Start()
        {
            Core.instance.InputAsset.Player.Move.performed += HamaMove;
            Core.instance.InputAsset.Player.Move.canceled += HamaMove;
            Core.instance.InputAsset.Player.Jump.performed += HamaJump;
            Core.instance.InputAsset.Player.Crawl.performed += CrawlOnOff;
            Core.instance.InputAsset.Player.Fire.performed += HamaAttack;
        }

        void Update()
        {
            Behaviour.LookAtMouse();
        }

        void FixedUpdate()
        {
            Behaviour.Move(moveDirection);
        }

        void CrawlOnOff(Context context)
        {
            CrouchGameObject.SetActive(isStand);
            StandGameObject.SetActive(!isStand);
            isStand = !isStand;
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
            Behaviour.Attack(null);
        }

        public void PlayBounceAnimation()
        {

        }
    }
}



