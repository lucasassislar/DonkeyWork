using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace DonkeyWork {
    [ExecuteInEditMode]
    public class DeterminismCaller : MonoBehaviour {
        public DeterminismManager Manager { get; private set; }

        public string strDetKey;
        public string strTagToCheck = "Player";

        public bool bExpectedValue;

        public bool bChangeValue;
        public bool bNewValue;

        public UnityEvent eventOnTriggerEnter;
        public UnityEvent eventOnTriggerExit;
        public UnityEvent eventOnAwake;

        private void Start() {
            Manager = DeterminismManager.Instance;
        }

        private void Awake() {
            if (bExpectedValue && Manager.IsRuleEnabled(strDetKey)) {
                eventOnAwake.Invoke();
            }
        }

        void OnTriggerEnter(Collider c) {
            if (bExpectedValue && Manager.IsRuleEnabled(strDetKey)) {
                if (c.transform.tag == strTagToCheck) {
                    eventOnTriggerEnter.Invoke();
                    Debug.Log("colidio");

                    if (bChangeValue) {
                        Manager.ChangeRuleValue(strDetKey, bNewValue);
                    }
                }
            }
        }

        void OnTriggerExit(Collider c) {
            if (bExpectedValue && Manager.IsRuleEnabled(strDetKey)) {
                if (c.transform.tag == strTagToCheck) {
                    eventOnTriggerExit.Invoke();
                }
            }
        }
    }
}
