using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshCombinerWizard : ScriptableWizard {
    [SerializeField]
    private string combinedMeshName = "New Combined Mesh";
    [SerializeField]
    private bool optimizeMesh = true;
    [SerializeField]
    private bool savePrefab = true;
    
    [MenuItem("Submarine Tools/Mesh Combiner...")]
    static void CombineMeshWizard() {
        DisplayWizard<MeshCombinerWizard>("Mesh Combiner", "Combine Selected Meshes");
    }

    private void OnWizardCreate() {
        // Return if no objects are selected
        if (Selection.gameObjects.Length <= 0) { return; }
        if (Selection.activeTransform == null) { return; }

        // Create 2 new objects. One object that will have our new mesh
        // and one that will be a parent to all merged meshes. 
        GameObject combinedMeshObject = new GameObject(combinedMeshName);
        GameObject oldMeshesObject = new GameObject(combinedMeshName + " old meshes");

        // Add a meshcombiner script to the new mesh object.
        MeshCombiner combiner = combinedMeshObject.AddComponent<MeshCombiner>();

        // Make all objects as a child to the oldMeshesObject
        for (int i = 0; i < Selection.gameObjects.Length; i++) {
            Selection.gameObjects[i].transform.parent = oldMeshesObject.transform;
        }

        // Add a new list of materials and renderers to the combiner and
        // exectue the Combine function
        combiner.meshes = oldMeshesObject.GetComponentsInChildren<MeshFilter>(); /// TODO (Richard) Figure out why this causes a crash
        combiner.materials = new List<Material>();
        combiner.renderers = oldMeshesObject.GetComponentsInChildren<MeshRenderer>();
        combiner.CombineMeshes();

        // Optimize the new mesh.
        if (optimizeMesh) {
            MeshUtility.Optimize(combiner.ObjectMesh);
        }

        // Let the user choose a path and name for the new mesh.
        string path = EditorUtility.SaveFilePanel("Save mesh asset", "Assets/", combinedMeshName, "asset");
        path = FileUtil.GetProjectRelativePath(path);

        AssetDatabase.CreateAsset(combiner.ObjectMesh, path);
        AssetDatabase.SaveAssets();

        // Deactivate the object that hold all old meshes.
        oldMeshesObject.SetActive(false);

        // Selection.objects needs to take in an array of
        // objects so we just make an temporary array and 
        // give it the newly created mesh object.
        GameObject[] selectedObjects = { combinedMeshObject };

        Selection.objects = selectedObjects;

        if (savePrefab) {
            SaveObjectPrefab(combinedMeshObject, path);
        }
    }

    private void SaveObjectPrefab(GameObject go, string path) {
        string localPath;

        // Remove .asset from the filename
        if (path.Contains(".asset")) {
            localPath = path.Replace(".asset", "");
        } else {
            localPath = path;
        }

        // Make it a prefab
        localPath += ".prefab";
        if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject))) {
            if (EditorUtility.DisplayDialog("Are you sure?", "The prefab already exists. Do you want to overwrite it?", "Yes", "No")) {
                CreateNewPrefab(go, localPath);
            }
        } else {
            CreateNewPrefab(go, localPath);
        }
    }

    private static void CreateNewPrefab(GameObject go, string localPath) {
        Object prefab = PrefabUtility.CreateEmptyPrefab(localPath);
        PrefabUtility.ReplacePrefab(go, prefab, ReplacePrefabOptions.ConnectToPrefab);
    }

}
