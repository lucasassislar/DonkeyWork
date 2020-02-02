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
        public Transform trFrameParent;

        public Material matSprite;

        public int nSpriteSheetFrames = 13;
        public int nSpriteLines = 0;

        private int nCurrentFrame = 0;
        private float fTimer = 0;
        private float fTimeBetwenFrames;
        private SpriteAnimation currentAnim;
        private string strQueuedAnimation;
        private List<MeshRenderer> meshFrames;

        private void Start() {
            if (animations != null && animations.Count > 0) {
                currentAnim = animations[0];
                Play(currentAnim);
            }

            if (trFrameParent) {
                meshFrames = new List<MeshRenderer>();
                foreach (Transform tr in trFrameParent) {
                    MeshRenderer meshRen = tr.GetComponent<MeshRenderer>();
                    meshFrames.Add(meshRen);

                    tr.gameObject.SetActive(false);
                }
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
            if (currentAnim == null) {
                return;
            }

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

                    if (meshFrames != null) {
                        for (int i = 0; i < meshFrames.Count; i++) {
                            meshFrames[i].gameObject.SetActive(false);
                        }
                        meshFrames[nCurrentFrame].gameObject.SetActive(true);
                    }
                } else {
                    if (meshFrames != null) {
                        for (int i = 0; i < meshFrames.Count; i++) {
                            meshFrames[i].gameObject.SetActive(false);
                        }
                        meshFrames[nCurrentFrame].gameObject.SetActive(true);
                    }
                }

                UpdateMaterial();
            }
        }

        private void UpdateMaterial() {
            float fHorFrameSize = 1 / (float)nSpriteSheetFrames;
            float fVerFrameSize = 1 / (float)nSpriteLines;
            float fOffsetX = nCurrentFrame * fHorFrameSize;
            float fOffsetY = fVerFrameSize * (float)currentAnim.nSheetLine;

            matSprite.SetVector("_MainTex_ST", new Vector4(
                fHorFrameSize,
                fVerFrameSize,
                fOffsetX,
                fOffsetY));
        }
    }
}
