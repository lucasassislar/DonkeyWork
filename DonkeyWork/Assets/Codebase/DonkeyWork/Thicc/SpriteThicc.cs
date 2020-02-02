using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DonkeyWork {
    public class SpriteThicc : MonoBehaviour {
        public float fThiccness = 0.5f;
        public float fOffsetZ = 0;

        [Serializable]
        public class ColorThiccness {
            public Color32 colToFilter = new Color32(0, 0, 0, 255);
            public float fNewThicness = 0.5f;
        }

        public List<ColorThiccness> customColors;

        public Material matReference;

        private SpriteRenderer spriteRenderer;
        private MeshRenderer meshRen;
        private MeshFilter meshFilter;

        private Vector3 Multiply(Vector3 a, Vector3 b) {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public void DeleteMesh() {
            List<GameObject> allObjs = new List<GameObject>();
            foreach (Transform tr in transform) {
                MeshFilter meshRen = tr.GetComponent<MeshFilter>();
                if (meshRen) {
                    string path = AssetDatabase.GetAssetPath(meshRen.sharedMesh);
                    AssetDatabase.DeleteAsset(path);
                }

                allObjs.Add(tr.gameObject);
            }
            for (int i = 0; i < allObjs.Count; i++) {
                DestroyImmediate(allObjs[i]); ;
            }
        }

        public void ListColors() {
            spriteRenderer = GetComponent<SpriteRenderer>();

            Sprite sprite = spriteRenderer.sprite;
            Texture2D texture = sprite.texture;
            Color32[] pixels = texture.GetPixels32();

            Rect rect = sprite.rect;
            int startX = (int)rect.x;
            int startY = (int)rect.y;
            int endX = (int)(rect.width + rect.x);
            int endY = (int)(rect.height + rect.y);
            List<Color32> uniqueColors = new List<Color32>();

            for (int x = (int)rect.x; x < endX; x++) {
                for (int y = (int)rect.y; y < endY; y++) {
                    int index = x + (y * texture.width);
                    Color32 color = pixels[index];

                    if (color.a < 0.1f) {
                        continue;
                    }

                    if (!uniqueColors.Contains(color)) {
                        uniqueColors.Add(color);
                        string html = ColorUtility.ToHtmlStringRGB(color);
                        Debug.Log($"Unique color: {color} - {html}");
                    }
                }
            }
        }

        public bool ColorClose(Color32 a, Color32 b) {
            int difR = Math.Abs(a.r - b.r);
            int difG = Math.Abs(a.g - b.g);
            int difB = Math.Abs(a.b - b.b);

            const int safeNumber = 3;

            if (difR < safeNumber && difG < safeNumber && difB < safeNumber) {
                return true;
            }
            return false;
        }

        public void GenerateMesh() {
            DeleteMesh();

            string folder = $"Assets//Generated//";
            Directory.CreateDirectory(folder);

            string matName = $"{folder}//Mat_{name}_{Guid.NewGuid()}.mat";

            spriteRenderer = GetComponent<SpriteRenderer>();

            Sprite sprite = spriteRenderer.sprite;
            Texture2D texture = sprite.texture;
            Color32[] pixels = texture.GetPixels32();

            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();
            List<Color32> colors = new List<Color32>();

            Vector3 vMin = spriteRenderer.bounds.size * -0.5F;
            Vector3 vMax = spriteRenderer.bounds.size * 0.5f;
            Debug.DrawLine(vMin, vMax, Color.red, 15);

            Rect rect = sprite.rect;
            int startX = (int)rect.x;
            int startY = (int)rect.y;
            int endX = (int)(rect.width + rect.x);
            int endY = (int)(rect.height + rect.y);

            float xSize = (vMax.x - vMin.x) / (float)rect.width;
            float ySize = (vMax.y - vMin.y) / (float)rect.height;

            Vector3 invScale = transform.lossyScale;
            invScale.x = 1 / invScale.x;
            invScale.y = 1 / invScale.y;
            invScale.z = 1 / invScale.z;

            spriteRenderer.enabled = false;

            List<Color32> uniqueColors = new List<Color32>();

            for (int x = (int)rect.x; x < endX; x++) {
                for (int y = (int)rect.y; y < endY; y++) {
                    int index = x + (y * texture.width);
                    Color32 color = pixels[index];

                    if (color.a < 0.1f) {
                        continue;
                    }

                    int actualX = x - startX;
                    int actualY = y - startY;

                    float startThicness = 0f;
                    ColorThiccness filtered = customColors.FirstOrDefault(c => ColorClose(c.colToFilter, color));
                    if (filtered != null) {
                        startThicness = fThiccness - filtered.fNewThicness;
                    } else {
                        //continue;
                    }

                    if (!uniqueColors.Contains(color)) {
                        uniqueColors.Add(color);
                        Debug.Log($"Unique color: {color}");
                    }

                    // make a cube
                    Vector3 vCubeTopLeft = vMin + new Vector3(actualX * xSize, actualY * ySize, startThicness);
                    Vector3 vCubeTopRight = vMin + new Vector3((actualX + 1) * xSize, actualY * ySize, startThicness);
                    Vector3 vCubeBottomLeft = vMin + new Vector3(actualX * xSize, (actualY + 1) * ySize, startThicness);
                    Vector3 vCubeBottomRight = vMin + new Vector3((actualX + 1) * xSize, (actualY + 1) * ySize, startThicness);

                    Vector3 vCubeTopLeftBack = vMin + new Vector3(actualX * xSize, actualY * ySize, fThiccness);
                    Vector3 vCubeTopRightBack = vMin + new Vector3((actualX + 1) * xSize, actualY * ySize, fThiccness);
                    Vector3 vCubeBottomLeftBack = vMin + new Vector3(actualX * xSize, (actualY + 1) * ySize, fThiccness);
                    Vector3 vCubeBottomRightBack = vMin + new Vector3((actualX + 1) * xSize, (actualY + 1) * ySize, fThiccness);

                    vCubeTopLeft = Multiply(vCubeTopLeft, invScale);
                    vCubeTopRight = Multiply(vCubeTopRight, invScale);
                    vCubeBottomLeft = Multiply(vCubeBottomLeft, invScale);
                    vCubeBottomRight = Multiply(vCubeBottomRight, invScale);

                    vCubeTopLeftBack = Multiply(vCubeTopLeftBack, invScale);
                    vCubeTopRightBack = Multiply(vCubeTopRightBack, invScale);
                    vCubeBottomLeftBack = Multiply(vCubeBottomLeftBack, invScale);
                    vCubeBottomRightBack = Multiply(vCubeBottomRightBack, invScale);

                    // front plane
                    vertices.Add(vCubeTopLeft);
                    vertices.Add(vCubeTopRight);
                    vertices.Add(vCubeBottomLeft);
                    vertices.Add(vCubeBottomRight);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);

                    indices.Add(vertices.Count - 2);
                    indices.Add(vertices.Count - 3);
                    indices.Add(vertices.Count - 4);

                    indices.Add(vertices.Count - 3);
                    indices.Add(vertices.Count - 2);
                    indices.Add(vertices.Count - 1);

                    // back plane
                    vertices.Add(vCubeTopLeftBack);
                    vertices.Add(vCubeTopRightBack);
                    vertices.Add(vCubeBottomLeftBack);
                    vertices.Add(vCubeBottomRightBack);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);

                    indices.Add(vertices.Count - 2);
                    indices.Add(vertices.Count - 3);
                    indices.Add(vertices.Count - 4);

                    indices.Add(vertices.Count - 3);
                    indices.Add(vertices.Count - 2);
                    indices.Add(vertices.Count - 1);

                    // left plane
                    vertices.Add(vCubeTopLeft);
                    vertices.Add(vCubeBottomLeft);
                    vertices.Add(vCubeTopLeftBack);
                    vertices.Add(vCubeBottomLeftBack);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);

                    indices.Add(vertices.Count - 2);
                    indices.Add(vertices.Count - 3);
                    indices.Add(vertices.Count - 4);

                    indices.Add(vertices.Count - 3);
                    indices.Add(vertices.Count - 2);
                    indices.Add(vertices.Count - 1);

                    // right plane
                    vertices.Add(vCubeTopRight);
                    vertices.Add(vCubeBottomRight);
                    vertices.Add(vCubeTopRightBack);
                    vertices.Add(vCubeBottomRightBack);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);

                    indices.Add(vertices.Count - 4);
                    indices.Add(vertices.Count - 3);
                    indices.Add(vertices.Count - 2);

                    indices.Add(vertices.Count - 1);
                    indices.Add(vertices.Count - 2);
                    indices.Add(vertices.Count - 3);

                    // bottom plane
                    vertices.Add(vCubeTopLeft);
                    vertices.Add(vCubeTopLeftBack);
                    vertices.Add(vCubeTopRight);
                    vertices.Add(vCubeTopRightBack);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);

                    indices.Add(vertices.Count - 1);
                    indices.Add(vertices.Count - 4);
                    indices.Add(vertices.Count - 2);

                    indices.Add(vertices.Count - 4);
                    indices.Add(vertices.Count - 1);
                    indices.Add(vertices.Count - 3);

                    // top plane
                    vertices.Add(vCubeBottomLeft);
                    vertices.Add(vCubeBottomLeftBack);
                    vertices.Add(vCubeBottomRight);
                    vertices.Add(vCubeBottomRightBack);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);
                    colors.Add(color);

                    indices.Add(vertices.Count - 2);
                    indices.Add(vertices.Count - 4);
                    indices.Add(vertices.Count - 1);

                    indices.Add(vertices.Count - 3);
                    indices.Add(vertices.Count - 1);
                    indices.Add(vertices.Count - 4);

                    if (vertices.Count > 60000) {
                        ConcateMesh(vertices, colors, indices);
                        vertices.Clear();
                        colors.Clear();
                        indices.Clear();
                    }
                }
            }

            if (vertices.Count > 0) {
                ConcateMesh(vertices, colors, indices);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void ConcateMesh(List<Vector3> vertices, List<Color32> colors, List<int> indices) {
            string folder = $"Assets//Generated//";
            string meshName = $"{folder}//Mat_{name}_{Guid.NewGuid()}.asset";

            GameObject childObj = new GameObject("MeshChild");
            childObj.transform.parent = this.transform;

            childObj.transform.localPosition = new Vector3(0, 0, fOffsetZ);
            childObj.transform.localScale = Vector3.one;

            meshFilter = childObj.AddComponent<MeshFilter>();
            meshRen = childObj.AddComponent<MeshRenderer>();

            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.colors32 = colors.ToArray();
            mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();

            AssetDatabase.CreateAsset(mesh, meshName);
            meshFilter.mesh = mesh;
            meshRen.sharedMaterial = matReference;
        }
    }
}
