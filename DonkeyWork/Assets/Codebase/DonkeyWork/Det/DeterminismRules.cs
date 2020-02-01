using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace DonkeyWork {
    public class DeterminismRules : ScriptableObject {
        public List<DeterministicRule> rules;

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Determinism Rules File")]
        public static void CreateAsset() {
            ScriptableObjectUtility.CreateAsset<DeterminismRules>();
        }
#endif
    }
}
