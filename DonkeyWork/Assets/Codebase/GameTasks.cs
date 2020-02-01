using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DonkeyWork {
    public class GameTasks : MonoBehaviour {
        public void EnableKinChildren(Transform parent) {
            foreach (Transform tr in parent) {
                RecursiveEnableKinChildren(tr);
            }
        }

        private void RecursiveEnableKinChildren(Transform parent) {
            Rigidbody body = parent.GetComponent<Rigidbody>();
            if (body) {
                body.isKinematic = false;
            }

            foreach (Transform tr in parent) {
                RecursiveEnableKinChildren(tr);
            }
        }
    }
}
