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
                allObjs.Add(tr.gameObject);
            }
            for (int i = 0; i < allObjs.Count; i++) {
                DestroyImmediate(allObjs[i]); ;
            }
        }

        public void GenerateMesh() {
            DeleteMesh();

            string folder = $"Assets//Generated//";
            Directory.CreateDirectory(folder);

            string matName = $"{folder}//Mat_{name}_{Guid.NewGuid()}.mat";
            string meshName = $"{folder}//Mat_{name}_{Guid.NewGuid()}.asset";

            spriteRenderer = GetComponent<SpriteRenderer>();

            Sprite sprite = spriteRenderer.sprite;
            Texture2D texture = sprite.texture;
            Color[] pixels = texture.GetPixels();

            List<Vector3> vertices = new List<Vector3>();
            List<int> indices = new List<int>();
            List<Color32> colors = new List<Color32>();

            Vector3 vCenter = transform.position;
            Vector3 vMin = spriteRenderer.bounds.size * -0.5F;
            Vector3 vMax = spriteRenderer.bounds.size * 0.5f;

            float xSize = (vMax.x - vMin.x) / (float)texture.width;
            float ySize = (vMax.y - vMin.y) / (float)texture.height;

            Vector3 invScale = transform.parent.localScale;
            invScale.x = 1 / invScale.x;
            invScale.y = 1 / invScale.y;
            invScale.z = 1 / invScale.z;

            GameObject childObj = new GameObject("MeshChild");
            childObj.transform.parent = this.transform;

            childObj.transform.localPosition = new Vector3(0, 0, 0);
            childObj.transform.localScale = Vector3.one;
            spriteRenderer.enabled = false;

            for (int x = 0; x < texture.width; x++) {
                for (int y = 0; y < texture.height; y++) {
                    int index = x + (y * texture.width);
                    Color color = pixels[index];
                    Color32 vecColor = new Color32((byte)(color.r * 255), (byte)(color.g * 255), (byte)(color.b * 255), (byte)(color.a * 255));

                    if (color.a < 0.1f) {
                        continue;
                    }

                    // make a cube
                    Vector3 vCubeTopLeft = vMin + new Vector3(x * xSize, y * ySize, 0);
                    Vector3 vCubeTopRight = vMin + new Vector3((x + 1) * xSize, y * ySize, 0);
                    Vector3 vCubeBottomLeft = vMin + new Vector3(x * xSize, (y + 1) * ySize, 0);
                    Vector3 vCubeBottomRight = vMin + new Vector3((x + 1) * xSize, (y + 1) * ySize, 0);

                    Vector3 vCubeTopLeftBack = vMin + new Vector3(x * xSize, y * ySize, fThiccness);
                    Vector3 vCubeTopRightBack = vMin + new Vector3((x + 1) * xSize, y * ySize, fThiccness);
                    Vector3 vCubeBottomLeftBack = vMin + new Vector3(x * xSize, (y + 1) * ySize, fThiccness);
                    Vector3 vCubeBottomRightBack = vMin + new Vector3((x + 1) * xSize, (y + 1) * ySize, fThiccness);

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
                }
            }

            meshFilter = childObj.AddComponent<MeshFilter>();
            meshRen = childObj.AddComponent<MeshRenderer>();

            Mesh mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = indices.ToArray();
            mesh.colors32 = colors.ToArray();
            mesh.RecalculateNormals();

            AssetDatabase.CreateAsset(mesh, meshName);

            meshFilter.mesh = mesh;

            // save material on database
            AssetDatabase.CreateAsset(new Material(matReference), matName);
            meshRen.sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>(matName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
