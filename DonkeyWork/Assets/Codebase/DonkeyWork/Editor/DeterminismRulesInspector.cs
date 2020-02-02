using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace DonkeyWork {
    [CustomEditor(typeof(DeterminismRules)), CanEditMultipleObjects]
    public class DeterminismRulesInspector : Editor {
        [MenuItem("Tools/Take screenshot")]
        static void Screenshot() {
            string[] files = Directory.GetFiles(@"Assets\Textures\Screenshots");
            ScreenCapture.CaptureScreenshot(@"Assets\Textures\Screenshots\screenshot_" + files.Length + ".png", 4);
        }

        public override void OnInspectorGUI() {
            //base.OnInspectorGUI();

            if (this.targets != null && this.targets.Length <= 1) {
                DeterminismRules det = (DeterminismRules)target;

                if (!det.bLoaded || det.rules == null ) { 
                    det.Load();
                    if (det.rules == null) {
                        det.rules = new List<DeterministicRule>();
                    }
                }

                if (GUILayout.Button("Save")) {
                    det.Save();
                    EditorUtility.SetDirty(det);
                }

                if (GUILayout.Button("Load")) {
                    det.Load();
                }

                if (GUILayout.Button("Reset Day")) {
                    det.nCurrentDay = 1;
                }
                det.nCurrentDay = EditorGUILayout.IntField(det.nCurrentDay, "Current Day");

                if (GUILayout.Button("Add Rule")) {
                    det.rules.Add(new DeterministicRule());
                }

                for (int i = 0; i < det.rules.Count; i++) {
                    DeterministicRule rule = det.rules[i];
                    
                    if (string.IsNullOrEmpty(rule.Name)) {
                        rule.Name = $"Rule {(i + 1)}";
                    }

                    rule.IsEditorOpen = EditorGUILayout.Foldout(rule.IsEditorOpen, rule.Name);
                    if (rule.IsEditorOpen) {
                        rule.Name = EditorGUILayout.TextField("Key", rule.Name);
                        rule.Description = EditorGUILayout.TextField("Description", rule.Description);
                        
                        rule.StartValue = GUILayout.Toggle(rule.StartValue, "Start Value");

                        if (GUILayout.Button("Delete")) {
                            det.rules.RemoveAt(i--);
                        }
                    }
                }

            }
        }
    }
}
