using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyWork {
    [Serializable]
    public class DeterministicRule {
        public string Name { get; set; }
        public bool StartValue { get; set; }

        public bool Value { get; set; }
    }
}
