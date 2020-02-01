using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DonkeyWork {
    public class PlayerCamera : MonoBehaviour {
        /// <summary>
        /// A reference to the player on the scene
        /// </summary>
        public PlayerController playerController;

        public float fCameraSpeed = 6.0f;

        private Vector3 vOffsetPos;

        private void Start() {
            vOffsetPos = transform.position - playerController.transform.position;
        }

        private void LateUpdate() {
            Vector3 vTargetPos = playerController.transform.position + this.vOffsetPos;
            transform.position = Vector3.Lerp(transform.position, vTargetPos, fCameraSpeed * Time.deltaTime);
        }
    }
}
