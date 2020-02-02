using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace DonkeyWork {
    public class EditorUtils {
        [MenuItem("Tools/Remove Prefab Connection")]
        public static void RemovePrefabConnection() {
            GameObject go = Selection.activeGameObject;
            if (go == null) {
                return;
            }
            Undo.RecordObject(go, "Remove Prefab Connection");

            string name = go.name;
            Transform parent = go.transform.parent;

            // unparent the GO so that world transforms are preserved
            go.transform.parent = null;

            // clears prefab link
            GameObject unprefabedGO = (GameObject)UnityEngine.Object.Instantiate(go);

            // assigned reverted values
            unprefabedGO.name = go.name;
            unprefabedGO.SetActive(go.activeSelf);

            // remove old
            GameObject.DestroyImmediate(go); // or this could just hide the old one .active = false;

            // reparent clone
            unprefabedGO.transform.parent = parent;
        }
    }
}
