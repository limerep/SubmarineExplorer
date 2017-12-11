using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour {

    public MeshFilter[] meshes;
    public List<Material> materials;
    public MeshRenderer[] renderers;
    public Mesh ObjectMesh { get; set; }

    private void Start() {
        DestroyImmediate(this);
    }

    public void CombineMeshes() {
        // Return if there's no meshes
        if (meshes.Length <= 0) {
            return;
        }

        foreach (MeshRenderer meshRenderer in renderers) {
            if (meshRenderer.transform == transform) { continue; }

            Material[] localMaterials = meshRenderer.sharedMaterials;
            foreach (Material localMat in localMaterials) {
                if (!materials.Contains(localMat)) {
                    materials.Add(localMat);
                }
            }
        }

        List<Mesh> submeshes = new List<Mesh>();
        foreach (Material mat in materials) {
            List<CombineInstance> combiners = new List<CombineInstance>();
            foreach (MeshFilter mFilter in meshes) {
                MeshRenderer meshRenderer = mFilter.GetComponent<MeshRenderer>();
                if (meshRenderer == null) { continue; }

                // Create a submesh for each material in the meshrenderer
                Material[] localMaterials = meshRenderer.sharedMaterials;
                for (int matIndex = 0; matIndex < localMaterials.Length; matIndex++) {
                    if (localMaterials[matIndex] != mat) { continue; }
                    CombineInstance ci = new CombineInstance {
                        mesh = mFilter.sharedMesh,
                        subMeshIndex = matIndex,
                        transform = mFilter.transform.localToWorldMatrix
                    };
                    combiners.Add(ci);
                }
            }

            // Create a new mesh and create it from the combine instance
            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combiners.ToArray(), true);
            submeshes.Add(mesh);
        }

        // Final list of combiners 
        List<CombineInstance> finalCombine = new List<CombineInstance>();
        foreach (Mesh mesh in submeshes) {
            CombineInstance ci = new CombineInstance {
                mesh = mesh,
                subMeshIndex = 0,
                transform = Matrix4x4.identity
            };
            finalCombine.Add(ci);
        }

        // Create a new mesh and combine
        Mesh finalMesh = new Mesh();
        finalMesh.CombineMeshes(finalCombine.ToArray(), false);
        GetComponent<MeshFilter>().sharedMesh = finalMesh;
        GetComponent<MeshRenderer>().materials = materials.ToArray();

        ObjectMesh = finalMesh;
    }
}
