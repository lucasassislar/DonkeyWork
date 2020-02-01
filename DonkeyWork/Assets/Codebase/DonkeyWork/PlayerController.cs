using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DonkeyWork {
    public class PlayerController : MonoBehaviour {
        private CharacterController playerController;
        private Animator animator;
        public float fMovementSpeed = 5;

        [Header("Physics")]
        public float fGravity = -10;
        public float fMinVelocityY = -100;

        public PlayerWorldState WorldState { get; private set; }

        void Start() {
            playerController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            WorldState = new PlayerWorldState();
        }

        void Update() {
            Vector3 vMovement = new Vector3();
            WorldState.MovementY += fGravity * Time.deltaTime;
            WorldState.MovementY = Math.Max(fMinVelocityY, WorldState.MovementY);

            Keyboard keyboard = Keyboard.current;
            if (keyboard == null) {
                return;
            }

            float fXMovement = 0;
            if (keyboard.enterKey.isPressed)
            {
                animator.Play("Attack");
            }
            if (keyboard.leftArrowKey.isPressed) {
                fXMovement = -1;
            } else if (keyboard.rightArrowKey.isPressed) { 
                fXMovement = 1;
            }

            vMovement.x = fMovementSpeed * fXMovement * Time.deltaTime;
            vMovement.y = WorldState.MovementY;

            playerController.Move(vMovement);
        }
    }
}