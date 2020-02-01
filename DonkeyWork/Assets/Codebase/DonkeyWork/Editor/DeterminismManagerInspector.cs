using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace DonkeyWork {
    [CustomEditor(typeof(DeterminismManager)), CanEditMultipleObjects]
    public class DeterminismManagerInspector : Editor {

        [MenuItem("Tools/Take screenshot")]
        static void Screenshot() {
            string[] files = Directory.GetFiles(@"Assets\Textures\Screenshots");
            ScreenCapture.CaptureScreenshot(@"Assets\Textures\Screenshots\screenshot_" + files.Length + ".png", 4);
        }

        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();

            if (this.targets != null && this.targets.Length <= 1) {
                DeterminismManager det = (DeterminismManager)target;

                for (int i = 0; i < det.rules.Count; i++) {
                    DeterministicRule rule = det.rules[i];
                    
                    if (string.IsNullOrEmpty(rule.Name)) {
                        rule.Name = $"Rule {(i + 1)}";
                    }

                    rule.IsEditorOpen = EditorGUILayout.Foldout(rule.IsEditorOpen, rule.Name);
                    if (rule.IsEditorOpen) {
                        
                        rule.StartValue = GUILayout.Toggle(rule.StartValue, "Start Value");
                    }
                }

            }
        }
    }
}
