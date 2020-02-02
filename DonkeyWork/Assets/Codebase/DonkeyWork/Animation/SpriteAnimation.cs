using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonkeyWork {
    [Serializable]
    public class SpriteAnimation {
        public string strName = "Animation";
        public int nStartFrame = 0;
        public int nEndFrame = 5;
        public int nSheetLine = 0;
        public float fAnimationFPS = 12;
    }
}
