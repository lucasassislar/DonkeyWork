using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DonkeyWork {
    public class MultipleText : MonoBehaviour {
        public string strDay1;
        public string strDay2;
        public string strDay3;
        public string strDay4;
        public string strDay5;

        private Text text;
        private DeterminismManager manager;
        private void Awake() {
            manager = DeterminismManager.Instance;

            switch (manager.nCurrentDay) {
                case 1:
                    text.text = strDay1;
                    break;
                case 2:
                    text.text = strDay1;
                    break;
                case 3:
                    text.text = strDay1;
                    break;
                case 4:
                    text.text = strDay1;
                    break;
                case 5:
                    text.text = strDay1;
                    break;
            }
        }
    }
}
