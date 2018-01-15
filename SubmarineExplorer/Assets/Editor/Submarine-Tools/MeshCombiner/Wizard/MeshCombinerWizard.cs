using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class MeshCombinerWizard : ScriptableWizard {
    [SerializeField]
    private string combinedMeshName = "New Combined Mesh";
    [SerializeField]
    private bool optimizeMesh = true;
    [SerializeField]
    private bool savePrefab = true;
    [SerializeField]
    private bool exportObj = false;
    
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
        combiner.meshes = oldMeshesObject.GetComponentsInChildren<MeshFilter>();
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

        if (exportObj)
        {
            ObjExporter.DoExport(true);
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

public class ObjExporterScript
{
    private static int StartIndex = 0;

    public static void Start()
    {
        StartIndex = 0;
    }
    public static void End()
    {
        StartIndex = 0;
    }


    public static string MeshToString(MeshFilter meshFilter, Transform trans)
    {
        Vector3 scale = trans.localScale;
        Vector3 position = trans.localPosition;
        Quaternion rotation = trans.localRotation;


        int numVertices = 0;
        Mesh mesh = meshFilter.sharedMesh;
        if (!mesh)
        {
            return "####Error####";
        }
        Material[] mats = meshFilter.GetComponent<Renderer>().sharedMaterials;

        StringBuilder stringbuilder = new StringBuilder();

        foreach (Vector3 verts in mesh.vertices)
        {
            Vector3 vert = trans.TransformPoint(verts);
            numVertices++;
            stringbuilder.Append(string.Format("v {0} {1} {2}\n", vert.x, vert.y, -vert.z));
        }
        stringbuilder.Append("\n");
        foreach (Vector3 meshNormal in mesh.normals)
        {
            Vector3 v = rotation * meshNormal;
            stringbuilder.Append(string.Format("vn {0} {1} {2}\n", -v.x, -v.y, v.z));
        }
        stringbuilder.Append("\n");
        foreach (Vector3 v in mesh.uv)
        {
            stringbuilder.Append(string.Format("vt {0} {1}\n", v.x, v.y));
        }
        for (int material = 0; material < mesh.subMeshCount; material++)
        {
            stringbuilder.Append("\n");
            stringbuilder.Append("usemtl ").Append(mats[material].name).Append("\n");
            stringbuilder.Append("usemap ").Append(mats[material].name).Append("\n");

            int[] triangles = mesh.GetTriangles(material);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                stringbuilder.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                    triangles[i] + 1 + StartIndex, triangles[i + 1] + 1 + StartIndex, triangles[i + 2] + 1 + StartIndex));
            }
        }

        StartIndex += numVertices;
        return stringbuilder.ToString();
    }
}

public class ObjExporter : ScriptableObject
{
    [MenuItem("File/Export/Wavefront OBJ")]
    static void DoExportWSubmeshes()
    {
        DoExport(true);
    }

    [MenuItem("File/Export/Wavefront OBJ (No Submeshes)")]
    static void DoExportWOSubmeshes()
    {
        DoExport(false);
    }


    public static void DoExport(bool makeSubmeshes)
    {
        if (Selection.gameObjects.Length == 0)
        {
            Debug.Log("Didn't Export Any Meshes; Nothing was selected!");
            return;
        }

        string meshName = Selection.gameObjects[0].name;
        string fileName = EditorUtility.SaveFilePanel("Export .obj file", "", meshName, "obj");

        ObjExporterScript.Start();

        StringBuilder meshString = new StringBuilder();

        meshString.Append("#" + meshName + ".obj"
                            + "\n#" + System.DateTime.Now.ToLongDateString()
                            + "\n#" + System.DateTime.Now.ToLongTimeString()
                            + "\n#-------"
                            + "\n\n");

        Transform trans = Selection.gameObjects[0].transform;

        Vector3 originalPosition = trans.position;
        trans.position = Vector3.zero;

        if (!makeSubmeshes)
        {
            meshString.Append("g ").Append(trans.name).Append("\n");
        }
        meshString.Append(processTransform(trans, makeSubmeshes));

        WriteToFile(meshString.ToString(), fileName);

        trans.position = originalPosition;

        ObjExporterScript.End();
        Debug.Log("Exported Mesh: " + fileName);
    }

    static string processTransform(Transform trans, bool makeSubmeshes)
    {
        StringBuilder meshString = new StringBuilder();

        meshString.Append("#" + trans.name
                        + "\n#-------"
                        + "\n");

        if (makeSubmeshes)
        {
            meshString.Append("g ").Append(trans.name).Append("\n");
        }

        MeshFilter meshFilter = trans.GetComponent<MeshFilter>();
        if (meshFilter)
        {
            meshString.Append(ObjExporterScript.MeshToString(meshFilter, trans));
        }

        for (int i = 0; i < trans.childCount; i++)
        {
            meshString.Append(processTransform(trans.GetChild(i), makeSubmeshes));
        }

        return meshString.ToString();
    }

    static void WriteToFile(string s, string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            sw.Write(s);
        }
    }
}
