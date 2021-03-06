﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UIElements;

namespace DonkeyWork {
    [CustomEditor(typeof(DeterminismCaller)), CanEditMultipleObjects]
    public class DeterminisCallerInspector : Editor {
        [MenuItem("Tools/Take screenshot")]
        static void Screenshot() {
            string[] files = Directory.GetFiles(@"Assets\Textures\Screenshots");
            ScreenCapture.CaptureScreenshot(@"Assets\Textures\Screenshots\screenshot_" + files.Length + ".png", 4);
        }

        public override void OnInspectorGUI() {
            if (this.targets != null && this.targets.Length <= 1) {
                DeterminismCaller caller = (DeterminismCaller)target;

                if (caller.Manager == null) {
                    caller.LoadManager();
                }

                if (caller.Manager.rulesAsset == null) {
                    return;
                }

                caller.Manager.rulesAsset.LoadIfNot();

                string[] keys = (from rule in caller.Manager.rulesAsset.rules
                                 select rule.Name).ToArray();

                int selectedIndex = -1;
                if (!string.IsNullOrEmpty(caller.strDetKey)) {
                    selectedIndex = Array.IndexOf(keys, caller.strDetKey);
                }

                int newIndex = EditorGUILayout.Popup("Event", selectedIndex, keys);

                if (newIndex != selectedIndex) {
                    caller.strDetKey = keys[newIndex];
                }

                caller.nDay = EditorGUILayout.IntField("Day To Call", caller.nDay);
                caller.timeToCallBoss = EditorGUILayout.FloatField("Time To Call Boss", caller.timeToCallBoss);
                caller.strTagToCheck = EditorGUILayout.TextField("Tag To Check", caller.strTagToCheck);
                caller.bExpectedValue = EditorGUILayout.Toggle("Expected Rule Value", caller.bExpectedValue);
                caller.bChangeValue = EditorGUILayout.Toggle("Trigger Enter change rule value", caller.bChangeValue);
                if (caller.bChangeValue) {
                    caller.bNewValue = EditorGUILayout.Toggle("Change To What Value", caller.bNewValue);
                }

                this.serializedObject.Update();
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("eventOnAwake"), true);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("eventOnTriggerEnter"), true);
                EditorGUILayout.PropertyField(this.serializedObject.FindProperty("eventOnTriggerExit"), true);
                this.serializedObject.ApplyModifiedProperties();

                //EditorUtility.SetDirty(caller);
            }
        }

    }
   
}
