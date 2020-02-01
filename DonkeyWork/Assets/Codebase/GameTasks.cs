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
                Rigidbody body = tr.GetComponent<Rigidbody>();
                if (!body) {
                    continue;
                }

                body.isKinematic = false;
            }
        }
    }
}
