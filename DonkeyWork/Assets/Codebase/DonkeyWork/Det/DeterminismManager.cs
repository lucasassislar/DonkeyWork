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
        public bool bIsFirstScene;

        public DeterminismManager() {
            Instance = this;
        }

        public void Start() {
            if (!bIsFirstScene) {
                return;
            }

            rulesAsset.Load();

            for (int i = 0; i < rulesAsset.rules.Count; i++) {
                DeterministicRule rule = rulesAsset.rules[i];
                rule.Value = rule.StartValue;
            }
        }

        public bool IsRuleEnabled(string key) {
            return rulesAsset.rules.First(c => c.Name.Equals(key)).Value;
        }

        public void ChangeRuleValue(string strKey, bool bNewValue) {
            Debug.Log($"Changed rule {strKey} to {bNewValue}");
            rulesAsset.rules.First(c => c.Name.Equals(strKey)).Value = bNewValue;
        }
    }
}
