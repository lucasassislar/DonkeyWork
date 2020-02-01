using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DonkeyWork {
    [Serializable]
    public class DeterministicRule {
        public string Name;
        public bool StartValue;
        public string Description;
        
        [HideInInspector]
        public bool Value;

        [HideInInspector]
        public bool IsEditorOpen;
    }
}
