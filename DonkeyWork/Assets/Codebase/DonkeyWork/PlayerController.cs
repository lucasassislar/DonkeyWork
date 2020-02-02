using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DonkeyWork {
    public class PlayerController : MonoBehaviour {
        private SpritePlaneAnimation spriteAnimation;
        private CharacterController playerController;
        private Animator animator;
        private float fSpinTimer;
        private bool bGoingRight;
        private Vector3 vSpinStartAngle;

        [Header("Animation")]
        public AnimationCurve curveRotation = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public float fSpinTime = 0.2f;
        public Transform trBody;

        [Header("Movement")]
        public float fMovementSpeed = 5;
        public float fLockZ = -3;

        [Header("Physics")]
        public float fGravity = -10;
        public float fMinVelocityY = -100;

        public PlayerWorldState WorldState { get; private set; }

        void Start() {
            playerController = GetComponent<CharacterController>();
            spriteAnimation = GetComponent<SpritePlaneAnimation>();
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

            if (keyboard.enterKey.isPressed ||
                keyboard.spaceKey.isPressed) {
                animator.Play("Attack");
                spriteAnimation.Play("Kicking");
            }

            float fXMovement = 0;

            bool bFnGoingRight = false;
            bool bChangedDir = false;

            if (keyboard.leftArrowKey.isPressed ||
                keyboard.aKey.isPressed) {
                fXMovement -= 1;

                if (bGoingRight && fSpinTimer > fSpinTime) {
                    bChangedDir = true;
                }
                bFnGoingRight = false;
            }
            if (keyboard.rightArrowKey.isPressed ||
                keyboard.dKey.isPressed) {
                fXMovement += 1;

                if (!bGoingRight && fSpinTimer > fSpinTime) {
                    bChangedDir = true;
                }
                bFnGoingRight = true;
            }

            fSpinTimer += Time.deltaTime;
            if (fXMovement != 0) {
                if (bChangedDir) {
                    fSpinTimer = 0;
                    vSpinStartAngle = trBody.localRotation.eulerAngles;
                }
                bGoingRight = bFnGoingRight;
            }

            if (bGoingRight) {
                trBody.localRotation = Quaternion.Euler(Vector3.Lerp(vSpinStartAngle, new Vector3(0, 0, 0), Math.Min(1, curveRotation.Evaluate(fSpinTimer / fSpinTime))));
            } else {
                trBody.localRotation = Quaternion.Euler(Vector3.Lerp(vSpinStartAngle, new Vector3(0, 180, 0), Math.Min(1, curveRotation.Evaluate(fSpinTimer / fSpinTime))));
            }

            bool bWaitFinish = spriteAnimation.CurrentAnimation == "Kicking";
            if (fXMovement > 0.1f || fXMovement < -0.1f) {
                spriteAnimation.Play("Walking", bWaitFinish);
            } else {
                spriteAnimation.Play("Idle", bWaitFinish);
            }

            vMovement.x = fMovementSpeed * fXMovement * Time.deltaTime;
            vMovement.y = WorldState.MovementY;

            playerController.Move(vMovement);

            Vector3 vPos = transform.position;
            vPos.z = fLockZ;
            transform.position = vPos;
        }
    }
}