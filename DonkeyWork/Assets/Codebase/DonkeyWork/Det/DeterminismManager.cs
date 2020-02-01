using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace DonkeyWork {
    public class DeterminismManager : MonoBehaviour {
        public static DeterminismManager Instance { get; private set; }

        public List<DeterministicRule> rules;

        public UnityEvent test;

        public DeterminismManager() {
            Instance = this;
        }

        private void Start() {

        }
    }
}
