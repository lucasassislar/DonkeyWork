using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DonkeyWork {
    public class SpritePlaneAnimation : MonoBehaviour {
        public string CurrentAnimation { get { return currentAnim.strName; } }

        public List<SpriteAnimation> animations;

        public Material matSprite;

        public int nSpriteSheetFrames = 13;

        private int nCurrentFrame = 0;
        private float fTimer = 0;
        private float fTimeBetwenFrames;
        private SpriteAnimation currentAnim;
        private string strQueuedAnimation;
        private float fFrameSize;

        private void Start() {
            if (animations != null && animations.Count > 0) {
                currentAnim = animations[0];
                Play(currentAnim);
            }
        }


        public void Play(string strName, bool bWaitFinish = false) {
            if (currentAnim.strName.Equals(strName)) {
                return;
            }

            if (bWaitFinish) {
                strQueuedAnimation = strName;
                return;
            }

            currentAnim = animations.First(c => c.strName == strName);
            Play(currentAnim);
        }

        public void Play(SpriteAnimation spriteAnim) {
            nCurrentFrame = spriteAnim.nStartFrame;
            fTimer = 0;
        }

        private void Update() {
            fFrameSize = 1 / (float)nSpriteSheetFrames;
            fTimeBetwenFrames = 1 / currentAnim.fAnimationFPS;

            fTimer += Time.deltaTime;

            if (fTimer > fTimeBetwenFrames) {
                fTimer -= fTimeBetwenFrames;
                nCurrentFrame++;
                if (nCurrentFrame > currentAnim.nEndFrame) {

                    if (string.IsNullOrEmpty(strQueuedAnimation)) {
                        nCurrentFrame = currentAnim.nStartFrame;
                    } else {
                        Play(strQueuedAnimation);
                        strQueuedAnimation = String.Empty;
                    }

                }

                UpdateMaterial();
            }
        }

        private void UpdateMaterial() {
            matSprite.SetVector("_MainTex_ST", new Vector4(fFrameSize, 1, nCurrentFrame * fFrameSize, 0));
        }
    }
}
