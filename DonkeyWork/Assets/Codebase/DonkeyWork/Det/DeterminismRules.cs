﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace DonkeyWork {
    public class DeterminismRules : ScriptableObject {
        [SerializeField]
        public byte[] serialized;

        [NonSerialized]
        public List<DeterministicRule> rules;

#if UNITY_EDITOR
        [MenuItem("Assets/Create/Determinism Rules File")]
        public static void CreateAsset() {
            ScriptableObjectUtility.CreateAsset<DeterminismRules>();
        }
#endif

        public void Save() {
            using (MemoryStream stream = new MemoryStream()) {
                using (BinaryWriter writer = new BinaryWriter(stream)) {
                    writer.Write(rules.Count);

                    for (int i = 0; i < rules.Count; i++) {
                        DeterministicRule rule = rules[i];
                        writer.Write(rule.Name);
                        writer.Write(rule.StartValue);
                        if (string.IsNullOrEmpty(rule.Description)) {
                            writer.Write(String.Empty);
                        }else {
                            writer.Write(rule.Description);
                        }
                    }
                }

                serialized = stream.ToArray();
            }
        }

        public void Load() {
            if (serialized == null) {
                return;
            }

            using (MemoryStream stream = new MemoryStream(serialized)) {
                using (BinaryReader reader = new BinaryReader(stream)) {
                    int ruleCount = reader.ReadInt32();
                    rules = new List<DeterministicRule>(ruleCount);

                    for (int i = 0; i < ruleCount; i++) {
                        DeterministicRule rule = new DeterministicRule();
                        rule.Name = reader.ReadString();
                        rule.StartValue = reader.ReadBoolean();
                        rule.Description = reader.ReadString();
                        rules.Add(rule);
                    }
                }
            }

        }
    }
}
