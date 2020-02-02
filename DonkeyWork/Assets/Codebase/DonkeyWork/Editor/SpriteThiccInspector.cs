using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DonkeyWork {
    [CustomEditor(typeof(SpriteThicc)), CanEditMultipleObjects]
    public class SpriteThiccInspector : Editor {

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (this.targets != null && this.targets.Length <= 1) {
                SpriteThicc thicc = (SpriteThicc)target;

                if (GUILayout.Button("Generate Mesh")) {
                    thicc.GenerateMesh();
                }

                if (GUILayout.Button("Generate Sprite-Sheet")) {
                    thicc.GenerateSpriteSheet();
                }

                if (GUILayout.Button("List Colors")) {
                    thicc.ListColors();
                }

                if (GUILayout.Button("Delete Mesh")) {
                    thicc.DeleteMesh();
                }
            }
        }
    }
}
