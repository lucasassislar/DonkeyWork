﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DonkeyWork {
    public class DeterminismManager : MonoBehaviour {
        public static DeterminismManager Instance { get; private set; }

        public DeterminismRules rulesAsset;
        public bool bIsFirstScene;

        public UnityEvent eventsDay1;
        public UnityEvent eventsDay2;
        public UnityEvent eventsDay3;
        public UnityEvent eventsDay4;
        public UnityEvent eventsDay5;

        public int CurrentDay { get { return rulesAsset.nCurrentDay; } }

        public DeterminismManager() {
            Instance = this;
        }

        public void Start() {
            if (!bIsFirstScene) {
                return;
            }

            rulesAsset.Load();

            rulesAsset.nCurrentDay = 1;

            for (int i = 0; i < rulesAsset.rules.Count; i++) {
                DeterministicRule rule = rulesAsset.rules[i];
                rule.Value = rule.StartValue;
            }

            switch (rulesAsset.nCurrentDay) {
                case 1:
                    eventsDay1.Invoke();
                    Debug.Log("DAY 1_________________");
                    break;
                case 2:
                    eventsDay2.Invoke();
                    break;
                case 3:
                    eventsDay3.Invoke();
                    break;
                case 4:
                    eventsDay4.Invoke();
                    break;
                case 5:
                    eventsDay5.Invoke();
                    break;
            }
        }

        public void SwapDay() {
            Debug.Log("SwapDAY_______-");
            rulesAsset.nCurrentDay = rulesAsset.nCurrentDay + 1;
            SceneManager.LoadScene("Day_1");
            
        }

        public bool IsRuleEnabled(string key) {
            return rulesAsset.GetRuleByName(key).Value;
        }

        public int RuleChangeDay(string key) {
            return rulesAsset.GetRuleByName(key).DayEnabled;
        }

        public bool IsToday(int nDay) {
            return rulesAsset.nCurrentDay == nDay;
        }

        public void ChangeRuleValue(string strKey, bool bNewValue) {
            Debug.Log($"Changed rule {strKey} to {bNewValue}");
            DeterministicRule rule = rulesAsset.GetRuleByName(strKey);
            rule.Value = bNewValue;
            rule.DayEnabled = rulesAsset.nCurrentDay;
        }
    }
}
