using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DonkeyWork {
    public class DeterminismCaller : MonoBehaviour {
        private DeterminismManager manager;

        private void Start() {
            manager = DeterminismManager.Instance;
        }
    }
}
