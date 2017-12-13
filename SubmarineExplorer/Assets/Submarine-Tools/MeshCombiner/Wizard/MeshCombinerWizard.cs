using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MeshCombinerWizard : ScriptableWizard {
    [SerializeField]
    private string combinedMeshName = "New Combined Mesh";
    [SerializeField]
    private bool optimizeMesh = true;
    
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
        combiner.meshes = new List<MeshFilter>();

        // Add all the meshfilter to the combiner and then put the object 
        // as a child to the object that will hold all old mesh objects.
        for (int i = 0; i < Selection.gameObjects.Length; i++) {
            combiner.meshes.Add(Selection.gameObjects[i].GetComponent<MeshFilter>());
            Selection.gameObjects[i].transform.parent = oldMeshesObject.transform;
        }

        // Add a new list of materials and renderers to the combiner and
        // exectue the Combine function
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
        
        // I haven't made up my mind yet if we should keep this script on the object
        // so that we can use it to update the object itself later or if we should
        // give it a new editor script instead.
        //DestroyImmediate(combinedMeshObject.GetComponent<MeshCombiner>());
    }

}
