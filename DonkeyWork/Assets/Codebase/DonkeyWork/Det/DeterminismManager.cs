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

        public DeterminismRules rulesAsset;

        public DeterminismManager() {
            Instance = this;
        }

        public bool IsRuleEnabled(string key) {
            return rulesAsset.rules.First(c => c.Name.Equals(key)).Value;
        }
    }
}
