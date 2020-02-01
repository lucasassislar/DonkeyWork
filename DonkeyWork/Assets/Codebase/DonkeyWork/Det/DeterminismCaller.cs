using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;


namespace DonkeyWork {
    [ExecuteInEditMode]
    public class DeterminismCaller : MonoBehaviour {
        public DeterminismManager Manager { get; private set; }

        public string strDetKey;
        public string strTagToCheck = "PlayerAttack";

        public bool bExpectedValue;

        public bool bChangeValue = true;
        public bool bNewValue = true;

        public UnityEvent eventOnTriggerEnter;
        public UnityEvent eventOnTriggerExit;
        public UnityEvent eventOnAwake;
        public float timeToCallBoss = 2f;
        private PlayableDirector BossComeTimeLine;
        private void Start() {
            Manager = DeterminismManager.Instance;
            BossComeTimeLine = GetComponent<PlayableDirector>();
        }

        private void Awake() {
            if (bExpectedValue && Manager.IsRuleEnabled(strDetKey)) {
                eventOnAwake.Invoke();
            }
        }

        void OnTriggerEnter(Collider c) {
            if (c.transform.tag != strTagToCheck) {
                return;
            }

            if (Manager.IsRuleEnabled(strDetKey) == bExpectedValue) {
                eventOnTriggerEnter.Invoke();
                Debug.Log("colidio trigger enter");

                if (bChangeValue) {
                    Manager.ChangeRuleValue(strDetKey, bNewValue);
                    Debug.Log("change rule value");
                    Invoke("BossComeToTellPlayerOff",timeToCallBoss);
                }
            } else {
                Debug.Log($"Did not execute {this.name} because {strDetKey} is not the expected value");
            }
        }

        void OnTriggerExit(Collider c) {
            if (c.transform.tag != strTagToCheck) {
                return;
            }

            if (Manager.IsRuleEnabled(strDetKey) == bExpectedValue) {
                eventOnTriggerExit.Invoke();
            } else {
                Debug.Log($"Did not execute {this.name} because {strDetKey} is not the expected value");
            }
        }
        void BossComeToTellPlayerOff()
        {
            Debug.Log("called Timeline");
           if(BossComeTimeLine) BossComeTimeLine.Play();
        }
    }
}
